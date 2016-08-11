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
using AeroGear;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace tests
{
    [TestClass]
    public class UpsHttpClientTest
    {
        [TestMethod]
        public void ShouldSetAuthorizationHeader()
        {
            //when
            var upsClient = new UPSHttpClient(new Uri("http://localhost"), "user", "pass");

            //then
            var request = upsClient._request;
            Assert.AreEqual("localhost", request.Host);
            Assert.AreEqual("Basic dXNlcjpwYXNz", request.Headers["Authorization"]);
        }

        [TestMethod]
        public void ShouldSetProxy()
        {
            //given
            var proxy = new Uri("http://proxy");

            //when
            var upsClient = new UPSHttpClient(new Uri("http://localhost"), new ProxyConfig()
            {
                password = "pass",
                user = "user",
                uri = proxy
            });

            //then
            var request = upsClient._request;
            Assert.AreEqual(proxy, request.Proxy.GetProxy(proxy));
        }

    }
}
