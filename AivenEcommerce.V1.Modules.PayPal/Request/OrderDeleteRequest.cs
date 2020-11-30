using PayPalHttp;

using System;
using System.IO;
using System.Net.Http;

namespace AivenEcommerce.V1.Modules.PayPal.Request
{
    public class OrderDeleteRequest : HttpRequest
    {
        public OrderDeleteRequest(string OrderId) : base("/v1/checkout/orders/{order_id}?", HttpMethod.Delete, typeof(void))
        {
            try
            {
                Path = Path.Replace("{order_id}", Uri.EscapeDataString(Convert.ToString(OrderId)));
            }
            catch (IOException) { }

            ContentType = "application/json";
        }
    }
}
