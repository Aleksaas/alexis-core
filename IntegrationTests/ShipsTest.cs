using AlexisCorePro.Business.Common.Model;
using AlexisCorePro.Business.Common.Model.Search;
using AlexisCorePro.Business.Ships;
using AlexisCorePro.Business.Ships.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntegrationTests
{
    [TestClass]
    public class ShipsTest
    {

        private readonly HttpClient _client;

        public ShipsTest()
        {
            _client = TestHelper.GetClient();
        }

        [TestMethod]
        public async Task GetById_Success()
        {
            HttpResponseMessage httpResponse = await _client.GetAsync("api/ships/1");

            var response = await TestHelper.GetResponseContent<Response<ShipDetails>>(httpResponse);

            Assert.AreEqual(1, response.Data.Id);
        }

        [TestMethod]
        public async Task GetById_Failure()
        {
            HttpResponseMessage httpResponse = await _client.GetAsync("api/ships/999999");

            var response = await TestHelper.GetResponseContent<Response<ShipDetails>>(httpResponse);

            Assert.AreNotSame(0, response.Errors.Count);
        }

        [TestMethod]
        public async Task Search()
        {
            HttpResponseMessage httpResponse = await _client.PostAsJsonAsync("api/ships/search", new SearchRequest<ShipQuery>
            {
                Query = new ShipQuery
                {
                     
                }
            });

            var response = await TestHelper.GetResponseContent<Response<SearchResponse<ShipListItem>>>(httpResponse);

            Assert.AreNotSame(0, response.Data.EntriesCount);
        }
    }
}
