using System;
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
        public enum TestNumber { ID_1, ID_3, ID_4 }
        [TestCase(TestNumber.ID_1, TestName = "ID_1 Given catc-version header endpoint returns a list of offers,")]
        [TestCase(TestNumber.ID_3, TestName = "ID_3 Given offers, the count is not higher than 2")]
        [TestCase(TestNumber.ID_4, TestName = "ID_4 Given offers are unique in terms of DomainName")]
        public void TrendingOffersTests(TestNumber testNumber)
        {
            var response = RequestsExtension.TrendingOffers();
            var couponDtoList = response.Result.Data.ToList();

            switch (testNumber)
            {
                case TestNumber.ID_1:
                    couponDtoList.Should().NotBeNullOrEmpty();
                    break;

                case TestNumber.ID_3:
                    var maxCount = 20;
                    couponDtoList.Should().HaveCountLessOrEqualTo(maxCount);
                    break;

                case TestNumber.ID_4:
                    var isUnique = couponDtoList.Select(x => x.DomainName).Distinct().Count() ==
                                   couponDtoList.Select(x => x.DomainName).Count();
                    isUnique.Should().BeTrue();
                    break;
            }
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
