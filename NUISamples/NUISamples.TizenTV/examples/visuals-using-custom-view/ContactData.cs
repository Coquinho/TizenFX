﻿/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using System;
using Tizen.NUI;
using Tizen.NUI.Constants;

namespace VisualsUsingCustomView
{
    // The collection of contacts
    static class ContactsList
    {
        private const string _resPath = "/home/owner/apps_rw/NUISamples.TizenTV/res/";

        public static readonly ContactItem[] s_contactData = new ContactItem[]
        {
            new ContactItem ("Emmett Yates", _resPath+"images/gallery-small-43.jpg",
                             new Color((73.0f/255.0f),(182.0f/255.0f), (245.0f/255.0f), 1.0f),
                             (int)PrimitiveVisualShapeType.Cone),
            new ContactItem ("Leslie Wong", _resPath+"images/gallery-2.jpg",
                             new Color((51.0f/255.0f), (51.0f/255.0f), (102.0f/255.0f), 1.0f),
                             (int)PrimitiveVisualShapeType.Sphere),
            new ContactItem ("Walter Jensen", _resPath+"images/gallery-0.jpg",
                             new Color((151.0f/255.0f), (214.0f/255.0f), (240.0f/255.0f), 1.0f),
                             (int)PrimitiveVisualShapeType.Cylinder),
            new ContactItem ("Dan Haynes", _resPath+"images/gallery-1.jpg",
                             new Color((102.0f/255.0f), (251.0f/255.0f), (102.0f/255.0f), 1.0f),
                             (int)PrimitiveVisualShapeType.ConicalFrustrum),
            new ContactItem ("Mable Hodges", _resPath+"images/gallery-3.jpg",
                             new Color((255.0f/255.0f), (102.0f/255.0f), (102.0f/255.0f), 1.0f),
                             (int)PrimitiveVisualShapeType.Cube)
        };
    }

    // The information for an individual contact
    class ContactItem
    {
        private string _name;
        private string _imageURL;
        private Color _color;
        private int _shape;

        public ContactItem (string name, string imageURL, Color color, int shape)
        {
            _name = name;
            _imageURL = imageURL;
            _color = color;
            _shape = shape;
        }

        public string ImageURL
        {
            get
            {
                return _imageURL;
            }
            set
            {
                _imageURL = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        public int Shape
        {
            get
            {
                return _shape;
            }
            set
            {
                _shape = value;
            }
        }
    }
}