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

        public void Send(UnifiedMessage message)
        {
            Send(message, null);
        }

        public async void Send(UnifiedMessage message, MessageResponseCallback callback)
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
            }
            
        }
    }

    class Builder
    {
        public Uri endpoint {get; set;}

        public Builder(Uri endpoint)
        {
            this.endpoint = endpoint;
        }

        static Builder Endpoint(Uri endpoint)
        {
            return new Builder(endpoint);
        }

        static Builder Endpoint(string endpoint)
        {
            if (String.IsNullOrEmpty(endpoint))
            {
                throw new Exception("server endpoint cannot be empty");
            }
            return new Builder(new Uri(endpoint.EndsWith("/") ? endpoint : endpoint + "/"));
        }

        SenderClient build()
        {
            return new SenderClient(new UPSHttpClient(endpoint));
        }
    }
}
