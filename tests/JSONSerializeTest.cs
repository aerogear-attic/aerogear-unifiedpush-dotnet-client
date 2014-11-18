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

            //when
            string json = unifiedMessage.Serialize();

            //then
            Assert.AreEqual("{\"config\":{\"ttl\":0},\"criteria\":{\"alias\":null,\"categories\":null,\"deviceType\":null,\"variants\":null},\"message\":{\"action-category\":null,\"alert\":\"hello\",\"badge\":0,\"content-available\":false,\"simple-push\":null,\"sound\":null,\"user-data\":null}}", json);
        }
    }
}