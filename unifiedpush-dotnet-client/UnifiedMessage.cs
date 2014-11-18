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
        public string pushApplicationId { get; set; }
        public string masterSecret { get; set; }

        public string Serialize()
        {
            return JsonHelper.Serialize(this);
        }
    }

    [DataContract]
    public class Message
    {
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
        public UserData userData { get; set; }
        [DataMember(IsRequired = false, Name = "simple-push")]
        public string simplePush { get; set; }
    }

    public class UserData
    {
        public string key { get; set; }
        public string key2 { get; set; }
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
