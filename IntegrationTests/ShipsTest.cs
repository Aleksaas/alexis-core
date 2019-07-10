using AlexisCorePro.Business.Common.Model;
using AlexisCorePro.Business.Common.Model.Search;
using AlexisCorePro.Business.Ships;
using AlexisCorePro.Business.Ships.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Transactions;

namespace IntegrationTests
{
    [TestClass]
    public class ShipsTest
    {
        private readonly HttpClient _client;

        private TransactionScope _transactionScope;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            _transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew);
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            Transaction.Current.Rollback();
            _transactionScope.Dispose();
        }

        public ShipsTest()
        {
            _client = TestHelper.GetClient();
            
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TestHelper.GetToken());
        }

        [TestMethod]
        public async Task CreateShip()
        {
            HttpResponseMessage httpResponse = await _client.PostAsJsonAsync("api/ships", new ShipCommand
            {
                Name = "Ship2",
                Date = DateTime.Now,
                CustomerId = 1,
                Imd = 10,
                Mmsi = 99
            });

            var response = TestHelper.GetResponseContent<Response<ShipDetails>>(httpResponse);

            Assert.AreEqual("Ship2", response.Data.Name);
        }

        [TestMethod]
        public async Task GetById_Success()
        {
            HttpResponseMessage httpResponse = await _client.GetAsync("api/ships/1");

            var response = TestHelper.GetResponseContent<Response<ShipDetails>>(httpResponse);

            Assert.AreEqual(1, response.Data.Id);
        }

        [TestMethod]
        public async Task GetById_Failure()
        {
            HttpResponseMessage httpResponse = await _client.GetAsync("api/ships/999999");

            var response = TestHelper.GetResponseContent<Response<ShipDetails>>(httpResponse);

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

            var response = TestHelper.GetResponseContent<Response<SearchResponse<ShipListItem>>>(httpResponse);

            Assert.AreNotSame(0, response.Data.EntriesCount);
        }
    }
}
