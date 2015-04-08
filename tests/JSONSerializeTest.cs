/**
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