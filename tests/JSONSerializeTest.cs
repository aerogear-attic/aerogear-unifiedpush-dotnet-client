using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AeroGear;

namespace tests
{
    [TestClass]
    public class JSONSerializeTest
    {
        [TestMethod]
        public void ShouldSerialseMessage()
        {
            //given
            UnifiedMessage unifiedMessage = new UnifiedMessage();
            unifiedMessage.message.alert = "hello";
            unifiedMessage.message.windows = new Windows();
            unifiedMessage.message.windows.images.Add("Assets/test.jpg");
            unifiedMessage.message.windows.type = MessageType.toast;
            unifiedMessage.message.userData.Add("key", "value");
            unifiedMessage.message.userData.Add("key2", "value2");

            //when
            string json = unifiedMessage.Serialize();

            //then
            Assert.AreEqual(@"{""config"":{""ttl"":0},""criteria"":{""alias"":null,""categories"":null,""deviceType"":null,""variants"":null},""message"":{""action-category"":null,""alert"":""hello"",""badge"":0,""content-available"":false,""simple-push"":null,""sound"":null,""user-data"":{""key"":""value"",""key2"":""value2""},""windows"":{""badgeType"":"""",""duration"":null,""images"":[""Assets\/test.jpg""],""textFields"":[],""tileType"":"""",""toastType"":"""",""type"":""toast""}}}", json);
        }
    }
}