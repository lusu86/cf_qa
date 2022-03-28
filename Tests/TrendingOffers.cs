using System.Linq;
using System.Net;
using cf_qa.Common;
using cf_qa.Endpoints;
using cf_qa.Requests;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;

namespace cf_qa.Tests
{
    class TrendingOffers : TestBase
    {
        [TestCase(TestName = "ID_1 Given catc-version header endpoint returns a list of offers," +
                             "ID_3 Given offers, the count is not higher than 20" +
                             "ID_4 Given offers are unique in terms of DomainName")]
        public void TrendingOffersTests()
        {
            var response = RequestsExtension.TrendingOffers();

            //ID_1
            var couponDtoList = response.Result.Data.ToList();
            couponDtoList.Should().NotBeNullOrEmpty();

            //ID_3
            var maxCount = 20;
            couponDtoList.Should().HaveCountLessOrEqualTo(maxCount);

            //ID_4
            var isUnique = couponDtoList.Select(x => x.DomainName).Distinct().Count() ==
                           couponDtoList.Select(x => x.DomainName).Count();

            isUnique.Should().BeTrue();
        }

        [TestCase(TestName = "ID_2 Given missing header endpoint returns 403 Forbidden HTTP error")]
        public void TrendingOffersMissingHeaderTest()
        {
            var client = GetRestClient(false);
            var response = client.ExecuteGetAsync(EndpointsExtension.TrendingOffers);
            response.Result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }
}
