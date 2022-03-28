using NUnit.Framework;
using RestSharp;

namespace cf_qa.Common
{
    public class TestBase
    {
        public static RestClient Client { get; set; }

        public static RestClient GetRestClient(bool auth = true)
        {
            Client = new RestClient("https://couponfollow.com/api/");

            if (auth)
            {
                Client.AddDefaultHeader("catc-version", "5.0.0");
            }
            return Client;
        }

        [SetUp]
        public void Setup()
        {
            Client ??= GetRestClient();
        }
    }
}