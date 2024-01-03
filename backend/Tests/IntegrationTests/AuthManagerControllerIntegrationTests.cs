using System.Text;
using System.Text.Json;
using FluentAssertions;
using IgorBryt.Store.BLL.Models.Auth;
using Library.Tests.IntegrationTests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace IgorBryt.Store.WebAPI.Tests
{
    [TestFixture]
    public class AuthManagerControllerIntegrationTests : WebApplicationFactory<Startup>
    {
        private HttpClient _client;
        private CustomWebApplicationFactory _factory;

        [SetUp]
        public void Init()
        {
            
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task Register_ValidUser_ReturnsOkResult()
        {
            // Arrange
            var registrationRequest = new UserRegistrationRequestModel
            {
                Name = "user2",
                Email = "user2@gmail.com",
                Password = "Password123!"
            };

            // Act
            var response = await _client.PostAsync("/api/AuthManager/Register",
                new StringContent(JsonSerializer.Serialize(registrationRequest), Encoding.UTF8, "application/json"));

            // Assert
            response.EnsureSuccessStatusCode(); 
            response.Content.Headers.ContentType?.ToString().Should().Be("application/json; charset=utf-8");

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<RegistrationRequestResponseModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            result.Should().NotBeNull();
            result!.Result.Should().BeTrue();
            result.Token.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Login_ValidCredentials_ReturnsOkResult()
        {

        var loginRequest = new UserLoginRequestModel
            {
                Email = "user1@gmail.com",
                Password = "Password123!"
        };
            var response = await _client.PostAsync("/api/AuthManager/Login",
                new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode(); 
            response.Content.Headers.ContentType?.ToString().Should().Be("application/json; charset=utf-8");

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<LoginRequestResponseModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            result.Should().NotBeNull();
            result!.Result.Should().BeTrue();
            result.Token.Should().NotBeNullOrEmpty();
        }
    }
}
