using AeroGear;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net;

namespace tests
{
    [TestClass]
    public class SenderClientTest
    {
        [TestMethod]
        public void SentMessage()
        {
            //given
            var mock = new Mock<HttpClient>();
            UnifiedMessage unifiedMessage = new UnifiedMessage();
            unifiedMessage.message.alert = "Test push message";
            unifiedMessage.pushApplicationId = "appId";
            unifiedMessage.masterSecret = "secret";
            SenderClient client = new SenderClient(mock.Object);

            //when
            mock.Setup(httpClient => httpClient.Send(unifiedMessage)).ReturnsAsync(HttpStatusCode.OK);
            client.Send(unifiedMessage);

            //then
            mock.Verify();
        }
    }
}
