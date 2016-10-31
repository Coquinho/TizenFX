/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Tizen.System;

namespace Tizen.Sensor
{
    internal static class Globals
    {
        internal const string LogTag = "Tizen.Sensor";
    }

    /// <summary>
    /// Sensor class for storing hardware information about a particular sensor
    /// </summary>
    public abstract class Sensor : IDisposable
    {
        private string _name;
        private string _vendor;
        private float _minValue;
        private float _maxValue;
        private float _resolution;
        private int _minInterval;
        private int _fifoCount;
        private int _maxBatchCount;
        private bool _isSensing = false;
        private bool _disposed = false;
        private TimeSpan _timeSpan;
        private uint _interval = 0;
        private uint _maxBatchLatency = 0;
        private SensorPausePolicy _pausePolicy = SensorPausePolicy.None;
        private IntPtr _sensorHandle = IntPtr.Zero;
        private IntPtr _listenerHandle = IntPtr.Zero;

        internal abstract SensorType GetSensorType();
        protected internal abstract void EventListenStart();
        protected internal abstract void EventListenStop();

        internal Sensor(uint index)
        {
            SensorType type = GetSensorType();
            GetHandleList(type, index);
            if (CheckSensorHandle())
            {
                CreateListener();
                GetProperty();
            }
        }

        ~Sensor()
        {
            Dispose(false);
        }

        /// <summary>
        /// Property: For getting the name of the sensor
        /// </summary>
        public string Name
        {
            get
            {
                Log.Info(Globals.LogTag, "Getting the sensor name");
                return _name;
            }
        }

        /// <summary>
        /// Property: Gets the vendor.
        /// </summary>
        public string Vendor
        {
            get
            {
                Log.Info(Globals.LogTag, "Getting the sensor vendor name");
                return _vendor;
            }
        }

        /// <summary>
        /// Property: Gets the minimum value of range of sensor data.
        /// </summary>
        public float MinValue
        {
            get
            {
                Log.Info(Globals.LogTag, "Getting the min value of the sensor");
                return _minValue;
            }
        }

        /// <summary>
        /// Property: Gets the maximum value of range of sensor data.
        /// </summary>
        public float MaxValue
        {
            get
            {
                Log.Info(Globals.LogTag, "Getting the max value of the sensor");
                return _maxValue;
            }
        }

        /// <summary>
        /// Property: Gets the resolution.
        /// </summary>
        public float Resolution
        {
            get
            {
                Log.Info(Globals.LogTag, "Getting the resolution of the sensor");
                return _resolution;
            }
        }

        /// <summary>
        /// Property: Gets the minimum interval.
        /// </summary>
        public int MinInterval
        {
            get
            {
                Log.Info(Globals.LogTag, "Getting the min interval for the sensor");
                return _minInterval;
            }
        }

        /// <summary>
        /// Property: Gets the fifo count.
        /// </summary>
        public int FifoCount
        {
            get
            {
                Log.Info(Globals.LogTag, "Getting the fifo count of the sensor");
                return _fifoCount;
            }
        }

        /// <summary>
        /// Property: Gets the maximum batch count.
        /// </summary>
        public int MaxBatchCount
        {
            get
            {
                Log.Info(Globals.LogTag, "Getting the max batch count of the sensor");
                return _maxBatchCount;
            }
        }

        /// <summary>
        /// Sets the interval of the sensor for sensor data event
        /// Callbacks will be called at frequency of this interval
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the operation is invalid for the current state</exception>
        public uint Interval
        {
            set
            {
                Log.Info(Globals.LogTag, "Setting the interval of the sensor");
                _interval = value;
                SetInterval();
            }
            get
            {
                Log.Info(Globals.LogTag, "Getting the interval of the sensor");
                return _interval;
            }
        }

        /// <summary>
        /// Sets the max batch latency for the sensor corresponding to the sensor data event.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the operation is invalid for the current state</exception>
        public uint MaxBatchLatency
        {
            set
            {
                Log.Info(Globals.LogTag, "Setting the max batch latency of the sensor");
                _maxBatchLatency = value;
                SetMaxBatchLatency();
            }
            get
            {
                Log.Info(Globals.LogTag, "Getting the max batch latency of the sensor");
                return _maxBatchLatency;
            }
        }

        /// <summary>
        /// Sets the pause policy of the sensor.
        /// </summary>
        /// <value>The pause policy</value>
        /// <exception cref="InvalidOperationException">Thrown when the operation is invalid for the current state</exception>
        public SensorPausePolicy PausePolicy
        {
            set
            {
                Log.Info(Globals.LogTag, "Setting the pause policy of the sensor");
                _pausePolicy = value;
                SetAttribute(SensorAttribute.PausePolicy, (int)_pausePolicy);
            }
            get
            {
                Log.Info(Globals.LogTag, "Getting the pause policy of the sensor");
                return _pausePolicy;
            }
        }

        /// <summary>
        /// Gets or sets the time span.
        /// </summary>
        /// <value>The time span.</value>
        public TimeSpan TimeSpan
        {
            set
            {
                Log.Info(Globals.LogTag, "Setting the timespan of the sensor values");
                _timeSpan = value;
            }
            get
            {
                Log.Info(Globals.LogTag, "Getting the timespan of the sensor values");
                return _timeSpan;
            }
        }

        /// <summary>
        /// Indicates whether this sensor is sensing.
        /// </summary>
        /// <value><c>true</c> if this sensor is sensing; otherwise, <c>false</c>.</value>
        public bool IsSensing
        {
            get
            {
                Log.Info(Globals.LogTag, "Checking if the sensor is started");
                return _isSensing;
            }
        }

        internal IntPtr ListenerHandle
        {
            get
            {
                return _listenerHandle;
            }
        }

        internal static bool CheckIfSupported(SensorType type, String key)
        {
            bool isSupported = false;
            bool error = SystemInfo.TryGetValue(key, out isSupported);

            if (!error || !isSupported)
            {
                Log.Error(Globals.LogTag, "Error checking if sensor is supported(systeminfo)");
                return false;
            }

            int ret = Interop.SensorManager.SensorIsSupported(type, out isSupported);
            if (ret != (int)SensorError.None)
            {
                Log.Error(Globals.LogTag, "Error checking if sensor is supported");
                isSupported = false;
            }

            return isSupported;
        }

        /// <summary>
        /// Starts the sensor.
        /// After this the event handlers will start receiving events.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the operation is invalid for the current state</exception>
        public void Start()
        {
            Log.Info(Globals.LogTag, "Starting the sensor");
            if (CheckListenerHandle())
            {
                int error = Interop.SensorListener.StartListener(_listenerHandle);
                if (error != (int)SensorError.None)
                {
                    Log.Error(Globals.LogTag, "Error starting sensor");
                    throw SensorErrorFactory.CheckAndThrowException(error, "Unable to Start Sensor Listener");
                }
                EventListenStart();
                _isSensing = true;
                Log.Info(Globals.LogTag, "Sensor started");
            }
        }

        /// <summary>
        /// Stop the sensor.
        /// After this the event handlers will stop receiving the events
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the operation is invalid for the current state</exception>
        public void Stop()
        {
            Log.Info(Globals.LogTag, "Stopping the sensor");
            if (_isSensing)
            {
                int error = Interop.SensorListener.StopListener(_listenerHandle);
                if (error != (int)SensorError.None)
                {
                    Log.Error(Globals.LogTag, "Error stopping the sensor");
                    throw SensorErrorFactory.CheckAndThrowException(error, "Unable to Stop Sensor Listener");
                }
                EventListenStop();
                _isSensing = false;
                Log.Info(Globals.LogTag, "Sensor stopped");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            DestroyHandles();
            _disposed = true;
        }

        protected void SetAttribute(SensorAttribute attribute, int option)
        {
            if (CheckListenerHandle())
            {
                int error = Interop.SensorListener.SetAttribute(_listenerHandle, attribute, option);
                if (error != (int)SensorError.None)
                {
                    Log.Error(Globals.LogTag, "Error setting sensor pause policy");
                    throw SensorErrorFactory.CheckAndThrowException(error, "Setting Sensor.PausePolicy Failed");
                }
            }
        }

        private void GetHandleList(SensorType type, uint index)
        {
            IntPtr list;
            IntPtr[] sensorList;
            int count;
            int error = Interop.SensorManager.GetSensorList(type, out list, out count);
            if (error != (int)SensorError.None)
            {
                Log.Error(Globals.LogTag, "Error getting sensor list");
                throw SensorErrorFactory.CheckAndThrowException(error, "Sensor.GetSensorList Failed");
            }
            sensorList = Interop.IntPtrToIntPtrArray(list, count);
            _sensorHandle = sensorList[index];
            Interop.Libc.Free(list);
        }

        private void GetProperty()
        {
            int error = (int)SensorError.None;

            error = Interop.Sensor.GetName(_sensorHandle, out _name);
            if (error != (int)SensorError.None)
            {
                Log.Error(Globals.LogTag, "Error getting sensor name");
                throw SensorErrorFactory.CheckAndThrowException(error, "Sensor.Name Failed");
            }

            error = Interop.Sensor.GetVendor(_sensorHandle, out _vendor);
            if (error != (int)SensorError.None)
            {
                Log.Error(Globals.LogTag, "Error getting sensor vendor name");
                throw SensorErrorFactory.CheckAndThrowException(error, "Sensor.Vendor Failed");
            }

            error = Interop.Sensor.GetMinRange(_sensorHandle, out _minValue);
            if (error != (int)SensorError.None)
            {
                Log.Error(Globals.LogTag, "Error getting sensor min value");
                throw SensorErrorFactory.CheckAndThrowException(error, "Sensor.MinValue Failed");
            }

            error = Interop.Sensor.GetMaxRange(_sensorHandle, out _maxValue);
            if (error != (int)SensorError.None)
            {
                Log.Error(Globals.LogTag, "Error getting sensor max value");
                throw SensorErrorFactory.CheckAndThrowException(error, "Sensor.MaxValue Failed");
            }

            error = Interop.Sensor.GetResolution(_sensorHandle, out _resolution);
            if (error != (int)SensorError.None)
            {
                Log.Error(Globals.LogTag, "Error getting sensor resolution");
                throw SensorErrorFactory.CheckAndThrowException(error, "Sensor.Resolution Failed");
            }

            error = Interop.Sensor.GetMinInterval(_sensorHandle, out _minInterval);
            if (error != (int)SensorError.None)
            {
                Log.Error(Globals.LogTag, "Error getting sensor min interval");
                throw SensorErrorFactory.CheckAndThrowException(error, "Sensor.MinInterval Failed");
            }

            error = Interop.Sensor.GetFifoCount(_sensorHandle, out _fifoCount);
            if (error != (int)SensorError.None)
            {
                Log.Error(Globals.LogTag, "Error getting sensor fifo count");
                throw SensorErrorFactory.CheckAndThrowException(error, "Sensor.FifoCount Failed");
            }

            error = Interop.Sensor.GetMaxBatchCount(_sensorHandle, out _maxBatchCount);
            if (error != (int)SensorError.None)
            {
                Log.Error(Globals.LogTag, "Error getting sensor max batch count");
                throw SensorErrorFactory.CheckAndThrowException(error, "Sensor.MaxBatchCount Failed");
            }
        }

        private void CreateListener()
        {
            int error = Interop.SensorListener.CreateListener(_sensorHandle, out _listenerHandle);
            if (error != (int)SensorError.None)
            {
                Log.Error(Globals.LogTag, "Error cerating sensor listener handle");
                throw SensorErrorFactory.CheckAndThrowException(error, "Sensor.CreateListener Failed");
            }
        }

        private void SetInterval()
        {
            if (CheckListenerHandle())
            {
                if (_isSensing)
                {
                    int error = Interop.SensorListener.SetInterval(_listenerHandle, _interval);
                    if (error != (int)SensorError.None)
                    {
                        Log.Error(Globals.LogTag, "Error setting sensor interval");
                        throw SensorErrorFactory.CheckAndThrowException(error, "Setting Sensor.SetInterval Failed");
                    }
                }
            }
        }

        private void SetMaxBatchLatency()
        {
            if (CheckListenerHandle())
            {
                int error = Interop.SensorListener.SetMaxBatchLatency(_listenerHandle, _maxBatchLatency);
                if (error != (int)SensorError.None)
                {
                    Log.Error(Globals.LogTag, "Error setting max batch latency");
                    throw SensorErrorFactory.CheckAndThrowException(error, "Setting Sensor.MaxBatchLatency Failed");
                }
            }
        }

        private bool CheckListenerHandle()
        {
            bool result = false;
            if (_listenerHandle != IntPtr.Zero)
            {
                result = true;
            }
            else
            {
                Log.Error(Globals.LogTag, "Sensor listener handle is null");
                throw new ArgumentException("Invalid Parameter: Sensor is null");
            }
            return result;
        }

        private bool CheckSensorHandle()
        {
            bool result = false;
            if (_sensorHandle != IntPtr.Zero)
            {
                result = true;
            }
            else
            {
                Log.Error(Globals.LogTag, "Sensor handle is null");
                throw new ArgumentException("Invalid Parameter: Sensor is null");
            }
            return result;
        }

        private void DestroyHandles()
        {
            Interop.SensorListener.DestroyListener(_listenerHandle);
        }
    }
}
