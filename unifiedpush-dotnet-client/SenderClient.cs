﻿/// <summary>
/// JBoss, Home of Professional Open Source
/// Copyright Red Hat, Inc., and individual contributors.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
/// 
/// 	http://www.apache.org/licenses/LICENSE-2.0
/// 
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AeroGear
{
    public class SenderClient: Sender
    {
        private HttpClient httpClient;
        public SenderClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task Send(UnifiedMessage message)
        {
            await httpClient.Send(message);
        }
    }

    public class Builder
    {
        public Uri endpoint {get; set;}
        public string pushApplicationId { get; set; }
        public string masterSecret { get; set; }
        public ProxyConfig proxyConfig
        {
            get
            {
                if (proxyConfig == null)
                {
                    proxyConfig = new ProxyConfig();
                }
                return proxyConfig;
            }
            set
            {
                proxyConfig = value;
            }

        }

        public Builder(Uri endpoint)
        {
            this.endpoint = endpoint;
        }

        public static Builder Endpoint(Uri endpoint)
        {
            return new Builder(endpoint);
        }

        public static Builder Endpoint(string endpoint)
        {
            if (String.IsNullOrEmpty(endpoint))
            {
                throw new Exception("server endpoint cannot be empty");
            }
            return new Builder(new Uri(endpoint.EndsWith("/") ? endpoint : endpoint + "/"));
        }

        public Builder setPushApplicationId(string pushApplicationId)
        {
            this.pushApplicationId = pushApplicationId;
            return this;
        }

        public Builder setMasterSecret(string masterSecret)
        {
            this.masterSecret = masterSecret;
            return this;
        }

        public Builder proxy(Uri proxyUri)
        {
            proxyConfig.uri = proxyUri;
            return this;
        }

        public Builder proxyUser(String proxyUser)
        {
            proxyConfig.user = proxyUser;
            return this;
        }

        public Builder proxyPassword(String proxyPassword)
        {
            proxyConfig.password = proxyPassword;
            return this;
        }

        public SenderClient build()
        {
            return new SenderClient(new UPSHttpClient(endpoint, pushApplicationId, masterSecret));
        }
    }
}
