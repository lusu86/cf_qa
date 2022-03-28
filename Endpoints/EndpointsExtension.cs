using RestSharp;

namespace cf_qa.Endpoints

{
    public class EndpointsExtension
    {
        public static RestRequest TrendingOffers => new("extension/trendingOffers", Method.Get);
    }
}
