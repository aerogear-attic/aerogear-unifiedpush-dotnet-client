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
using System.Text;
using System.Threading.Tasks;

namespace AeroGear
{
    public sealed class UPSHttpClient : HttpClient
    {
        private const string AuthorizationHeader = "Authorization";
        private const string AuthorizationMethod = "Basic";
        private const string SenderEndpoint = "rest/sender/";

        internal readonly HttpWebRequest _request;

        public UPSHttpClient(Uri uri)
        {
            _request = (HttpWebRequest) WebRequest.Create(new Uri(uri, SenderEndpoint));
            _request.ContentType = "application/json";
            _request.Method = "POST";
        }

        public UPSHttpClient(Uri uri, ProxyConfig config) : this(uri)
        {
            if (config == null) return;
            var proxy = new WebProxy();
            if (config.user != null)
            {
                proxy.Credentials = new NetworkCredential(config.user, config.password);
            }

            proxy.Address = config.uri;
            _request.Proxy = proxy;
        }

        public UPSHttpClient(Uri uri, string username, string password) : this(uri)
        {
            setUsernamePassword(username, password);
        }

        public void setUsernamePassword(string username, string password)
        {
            _request.Headers[AuthorizationHeader] = AuthorizationMethod + " " + CreateHash(username, password);
        }

        public async Task<HttpStatusCode> Send(UnifiedMessage message)
        {
            using (
                var postStream =
                    await
                        Task<Stream>.Factory.FromAsync(_request.BeginGetRequestStream, _request.EndGetRequestStream,
                            _request))
            {
                var bytes = Encoding.UTF8.GetBytes(message.Serialize());
                postStream.Write(bytes, 0, bytes.Length);
            }

            var responseObject =
                (HttpWebResponse)
                    await Task<WebResponse>.Factory.FromAsync(_request.BeginGetResponse, _request.EndGetResponse, _request);
            var responseStream = responseObject.GetResponseStream();
            if (responseStream != null)
            {
                var streamReader = new StreamReader(responseStream);
                await streamReader.ReadToEndAsync();
            }
            return responseObject.StatusCode;
        }

        private static string CreateHash(string username, string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password));
        }
    }
}