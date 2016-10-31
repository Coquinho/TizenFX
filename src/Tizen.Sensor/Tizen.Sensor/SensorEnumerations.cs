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

namespace Tizen.Sensor
{
    /// <summary>
    /// The SensorType Enum defintion for all sensor types.
    /// </summary>
    internal enum SensorType
    {
        /// <summary>
        /// All sensors. This can be used to retrieve Sensor class object for all available sensors.
        /// </summary>
        All = -1,
        /// <summary>
        /// Accelerometer sensor.
        /// </summary>
        Accelerometer = 0,
        /// <summary>
        /// Gravity sensor.
        /// </summary>
        GravitySensor = 1,
        /// <summary>
        /// Linear Acceleration sensor.
        /// </summary>
        LinearAccelerationSensor = 2,
        /// <summary>
        /// Magnetometer sensor.
        /// </summary>
        Magnetometer = 3,
        /// <summary>
        /// Rotation Vector sensor.
        /// </summary>
        RotationVectorSensor = 4,
        /// <summary>
        /// Orientation sensor.
        /// </summary>
        OrientationSensor = 5,
        /// <summary>
        /// Gyroscope sensor.
        /// </summary>
        Gyroscope = 6,
        /// <summary>
        /// Light sensor.
        /// </summary>
        LightSensor = 7,
        /// <summary>
        /// Proximity sensor.
        /// </summary>
        ProximitySensor = 8,
        /// <summary>
        /// Pressure sensor.
        /// </summary>
        PressureSensor = 9,
        /// <summary>
        /// Ultraviolet sensor.
        /// </summary>
        UltravioletSensor = 10,
        /// <summary>
        /// Temperature sensor.
        /// </summary>
        TemperatureSensor = 11,
        /// <summary>
        /// Humidity sensor.
        /// </summary>
        HumiditySensor = 12,
        /// <summary>
        /// Hear rate monitor sensor.
        /// </summary>
        HeartRateMonitor = 13,
        /// <summary>
        /// Uncalibrated Gyroscope sensor.
        /// </summary>
        UncalibratedGyroscope = 17,
        /// <summary>
        /// Uncalibrated Geomagnetic sensor.
        /// </summary>
        UncalibratedMagnetometer = 18,
        /// <summary>
        /// Gyroscope-based rotation vector sensor.
        /// </summary>
        GyroscopeRotationVectorSensor = 19,
        /// <summary>
        /// Geomagnetic-based rotation vector sensor.
        /// </summary>
        MagnetometerRotationVectorSensor = 20,
        /// <summary>
        /// Pedometer sensor.
        /// </summary>
        Pedometer = 0x300,
        /// <summary>
        /// Sleep monitor sensor.
        /// </summary>
        SleepMonitor = 0x301,
        /// <summary>
        /// Walking activity detector.
        /// </summary>
        WalkingActivityDetector = 0x1A00,
        /// <summary>
        /// Running activity detector.
        /// </summary>
        RunningActivityDetector = 0x1A00,
        /// <summary>
        /// Stationary activity detector.
        /// </summary>
        StationaryActivityDetector = 0x1A00,
        /// <summary>
        /// InVehicle activity detector.
        /// </summary>
        InVehicleActivityDetector = 0x1A00,
        /// <summary>
        /// Wrist up gesture detector.
        /// </summary>
        WristUpGestureDetector = 0x1201,
        /// <summary>
        /// Pick up gesture detector.
        /// </summary>
        PickUpGestureDetector = 0x1204,
        /// <summary>
        /// Face down gesture detector.
        /// </summary>
        FaceDownGestureDetector = 0x1205
    }

    /// <summary>
    /// SensorDataAccuracy Enum definition for all possible sensor data accuracy Values.
    /// </summary>
    public enum SensorDataAccuracy
    {
        /// <summary>
        /// Undefined sensor data accuracy.
        /// </summary>
        Undefined = -1,
        /// <summary>
        /// Sensor data not accurate.
        /// </summary>
        Bad = 0,
        /// <summary>
        /// Moderately accurate sensor data.
        /// </summary>
        Normal = 1,
        /// <summary>
        /// Highly accurate sensor data.
        /// </summary>
        Good = 2,
        /// <summary>
        /// Very highly accurate sensor data.
        /// </summary>
        VeryGood = 3
    }

    /// <summary>
    /// Sensor Option Enum definition for sensor option Values
    /// </summary>
    public enum SensorPausePolicy
    {
        /// <summary>
        /// Does not receive data when the LCD is off and in the power save mode.
        /// </summary>
        None,
        /// <summary>
        /// Receives data when the LCD is off.
        /// </summary>
        DisplayOff,
        /// <summary>
        /// Receives data in the power save mode.
        /// </summary>
        PowerSaveMode,
        /// <summary>
        /// Receives data when the LCD is off and in the power save mode.
        /// </summary>
        All
    }

    /// <summary>
    /// Sensor attribute.
    /// </summary>
    public enum SensorAttribute
    {
        /// <summary>
        /// The axis orientation.
        /// </summary>
        AxisOrientation,

        /// <summary>
        /// The pause policy.
        /// </summary>
        PausePolicy
    }

    /// <summary>
    /// Pedometer state.
    /// </summary>
    public enum PedometerState
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// Stop state.
        /// </summary>
        Stop,

        /// <summary>
        /// Walking state.
        /// </summary>
        Walk,

        /// <summary>
        /// Running state.
        /// </summary>
        Run
    }

    /// <summary>
    /// Sleep monitor state.
    /// </summary>
    public enum SleepMonitorState
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// The wake state.
        /// </summary>
        Wake,

        /// <summary>
        /// The sleeping state.
        /// </summary>
        Sleep
    }

    /// <summary>
    /// Proximity sensor state.
    /// </summary>
    public enum ProximitySensorState
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// Near sate.
        /// </summary>
        Near = 0,

        /// <summary>
        /// Far state.
        /// </summary>
        Far = 5
    }

    /// <summary>
    /// Detector sensor state.
    /// </summary>
    public enum DetectorState
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// None sate.
        /// </summary>
        NotDetected = 0,

        /// <summary>
        /// Detected state.
        /// </summary>
        Detected = 1
    }
}
