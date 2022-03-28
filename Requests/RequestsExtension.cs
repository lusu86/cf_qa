using System.Net;
using System.Threading.Tasks;
using cf_qa.Common;
using cf_qa.Dto;
using cf_qa.Endpoints;
using FluentAssertions;
using RestSharp;

namespace cf_qa.Requests
{

    public static class RequestsExtension
    {
        private static RestClient _client;
        private static RestClient Client => _client ??= TestBase.Client;

        public static async Task<RestResponse<CouponDto[]>> TrendingOffers(HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var response =  await Client.ExecuteGetAsync<CouponDto[]>(EndpointsExtension.TrendingOffers);
            response.StatusCode.Should().Be(statusCode);

            return response;
        }
    }
}
