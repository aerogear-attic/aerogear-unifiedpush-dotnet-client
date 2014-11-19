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
            await Send(message, null);
        }

        public async Task Send(UnifiedMessage message, MessageResponseCallback callback)
        {
            httpClient.setUsernamePassword(message.pushApplicationId, message.masterSecret);
            try
            {
                HttpStatusCode status = await httpClient.Send(message);
                if (callback != null)
                {
                    callback.OnComplete((int) status);
                }
            }
            catch (Exception e)
            {
                if (callback != null)
                {
                    callback.OnError(e);
                }
                else
                {
                    throw e;
                }
            }
            
        }
    }

    public class Builder
    {
        public Uri endpoint {get; set;}

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

        public SenderClient build()
        {
            return new SenderClient(new UPSHttpClient(endpoint));
        }
    }
}
