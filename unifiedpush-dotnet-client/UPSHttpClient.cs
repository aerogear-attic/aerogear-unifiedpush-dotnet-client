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
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace AeroGear
{
    public sealed class UPSHttpClient: HttpClient
    {
        private const string AUTHORIZATION_HEADER = "Authorization";
        private const string AUTHORIZATION_METHOD = "Basic";
        private const string SENDER_ENDPOINT = "rest/sender/";

        private HttpWebRequest request;

        public UPSHttpClient(Uri uri)
        {
            request = (HttpWebRequest)WebRequest.Create(new Uri(uri, SENDER_ENDPOINT));
            request.ContentType = "application/json";
            request.Method = "POST";
        }

        public UPSHttpClient(Uri uri, ProxyConfig config) : this(uri)
        {
            if (config != null)
            {
                WebProxy proxy = new WebProxy();
                if (config.user != null)
                {
                    proxy.Credentials = new NetworkCredential(config.user, config.password);
                }

                proxy.Address = config.uri;
                request.Proxy = proxy;
            }
        }

        public UPSHttpClient(Uri uri, string username, string password) : this(uri)
        {
            setUsernamePassword(username, password);
        }

        public void setUsernamePassword(string username, string password)
        {
            request.Headers[AUTHORIZATION_HEADER] = AUTHORIZATION_METHOD + " " + CreateHash(username, password);
        }

        public async Task<HttpStatusCode> Send(UnifiedMessage message)
        {
            using (var postStream = await Task<Stream>.Factory.FromAsync(request.BeginGetRequestStream, request.EndGetRequestStream, request))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(message.Serialize());
                postStream.Write(bytes, 0, bytes.Length);
            }

            HttpWebResponse responseObject = (HttpWebResponse)await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, request);
            var responseStream = responseObject.GetResponseStream();
            var streamReader = new StreamReader(responseStream);

            await streamReader.ReadToEndAsync();
            return responseObject.StatusCode;
        }

        private static string CreateHash(string username, string password)
        {
            return Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(username + ":" + password));
        }
    }
}
