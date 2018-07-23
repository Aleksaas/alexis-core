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
        public async Task Get()
        {
            HttpResponseMessage httpResponse = await _client.GetAsync("api/ships/1");

            var response = await TestHelper.GetResponseContent<Response<ShipDetails>>(httpResponse);

            Assert.AreEqual(1, response.Data.Id);
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

            Assert.AreEqual(1, response.Data.EntriesCount);
        }
    }
}
