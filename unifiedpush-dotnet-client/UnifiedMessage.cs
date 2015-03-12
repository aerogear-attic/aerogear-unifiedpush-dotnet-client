﻿/**
 * JBoss, Home of Professional Open Source
 * Copyright Red Hat, Inc., and individual contributors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * 	http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AeroGear
{
    [DataContract]
    public class UnifiedMessage
    {
        public UnifiedMessage()
        {
            this.message = new Message();
            this.config = new Config();
            this.criteria = new Criteria();
        }
        [DataMember]
        public Message message { get; set; }
        [DataMember]
        public Criteria criteria { get; set; }
        [DataMember]
        public Config config { get; set; }

        public string Serialize()
        {
            return JsonHelper.Serialize(this);
        }
    }

    [DataContract]
    public class Message
    {
        public Message()
        {
            this.userData = new Dictionary<string, string>();
        }

        [DataMember]
        public Windows windows { get; set; }
        [DataMember]
        public string alert { get; set; }
        [DataMember(IsRequired = false, Name = "action-category")]
        public string actionCategory { get; set; }
        [DataMember]
        public string sound { get; set; }
        [DataMember]
        public int badge { get; set; }
        [DataMember(IsRequired = false, Name = "content-available")]
        public bool contentAvailable { get; set; }
        [DataMember(IsRequired = false, Name = "user-data")]
        public IDictionary<string, string> userData { get; set; }
        [DataMember(IsRequired = false, Name = "simple-push")]
        public string simplePush { get; set; }
    }

    [DataContract]
    public class Windows
    {
        public Windows()
        {
            this.images = new List<string>();
            this.textFields = new List<string>();
        }
        public MessageType type { get; set; }
        [DataMember(Name = "type")]
        string MessageType
        {
            get { return this.type.ToString(); }
            set { }
        }
        public BadgeType? badge { get; set; }
        [DataMember(Name = "badgeType")]
        string BadgeType
        {
            get { return this.badge.ToString(); }
            set { }
        }
        [DataMember]
        public string duration { get; set; }
        public TileType? tileType { get; set; }
        [DataMember(Name = "tileType")]
        string TileType
        {
            get { return this.tileType.ToString(); }
            set { }
        }
        public ToastType? toastType { get; set; }
        [DataMember(Name = "toastType")]
        string ToastType
        {
            get { return this.toastType.ToString(); }
            set { }
        }

        [DataMember]
        public IList<string> images { get; set; }
        [DataMember]
        public IList<string> textFields { get; set; }
    }

    public class Criteria
    {
        public string[] alias { get; set; }
        public string[] deviceType { get; set; }
        public string[] categories { get; set; }
        public string[] variants { get; set; }
    }

    public class Config
    {
        public int ttl { get; set; }
    }
}
