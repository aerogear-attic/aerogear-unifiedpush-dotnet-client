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

        public SenderClient build()
        {
            return new SenderClient(new UPSHttpClient(endpoint, pushApplicationId, masterSecret));
        }
    }
}
