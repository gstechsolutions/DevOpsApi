using DevOpsApi.core.api.Models;
using DevOpsApi.core.api.Models.Auth;
using DevOpsApi.core.api.Services.Auth;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = Xunit.Assert;

namespace DevOpsApi.core.api.tests.AuthTests
{
    public class AuthUserTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient Client;

        private const String API_URL = "api/auth";

        private readonly WebApplicationFactory<Startup> _factory;
        private readonly IJwtService JwtService;

        public AuthUserTests(WebApplicationFactory<Startup> factory)
        {
            Client = factory.CreateClient();
            _factory = factory;

            var scope = _factory.Services.CreateScope();
            JwtService = scope.ServiceProvider.GetRequiredService<IJwtService>();
        }

        //[Route("api/auth/user/insert")]
        //to register new user John
        //"bw6eo/vK8z8LP8Xxbut8jKPXEJDnPErpwT+yO2KQht2EmC0L6dhxpUpMQpJncGcxquznRZv24XvkIR2AHyarJQ=="
        //to login user John John1234
        //"bw6eo/vK8z8LP8Xxbut8jKPXEJDnPErpwT+yO2KQht3QyZmL6Y0LzOXrCs6t/L0I"
        [Fact]
        [TestCategory("Integration")]
        public async Task RegisterUserIntegrationTest()
        {
            //arrange
            var user = "john";


            var responseDelete = await Client.GetAsync($"{API_URL}/user/delete/{user}");
            var resultsDelete = JsonConvert.DeserializeObject<bool>(await responseDelete.Content.ReadAsStringAsync());

            var encryptedUser = new RequestFilters{
                Data = "bw6eo/vK8z8LP8Xxbut8jKPXEJDnPErpwT+yO2KQht2EmC0L6dhxpUpMQpJncGcxquznRZv24XvkIR2AHyarJQ=="
            };

            var content = new StringContent(JsonConvert.SerializeObject(encryptedUser), Encoding.UTF8, "application/json");

            //act
            var response = await Client.PostAsync($"{API_URL}/user/insert", content);
            var results = JsonConvert.DeserializeObject<UserModelToInsert>(await response.Content.ReadAsStringAsync());

            //assert
            Assert.True(results.RequestId != 2);
        }

        [Fact]
        [TestCategory("Integration")]
        public async Task RegisterUserCallingMethodIntegrationTest()
        {
            //arrange
            //if it exists delete it
            var delete = await JwtService.DeleteUser("john");

            var encryptedUser = new RequestFilters
            {
                Data = "bw6eo/vK8z8LP8Xxbut8jKPXEJDnPErpwT+yO2KQht2EmC0L6dhxpUpMQpJncGcxquznRZv24XvkIR2AHyarJQ=="
            };

            //act
            var insertResult = await JwtService.InsertNewUserModel(encryptedUser);

            //assert
            Assert.True(insertResult.RequestId != 2);

        }

        [Fact]
        [TestCategory("Integration")]
        public async Task DeleteUserCallingMethodIntegrationTest()
        {
            //arrange
            //if it exists delete it
            var delete = await JwtService.DeleteUser("john");

           

            //assert
            Assert.True(delete);

        }
    }
}
