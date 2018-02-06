using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Demo;
using Xunit;

namespace UnitTest
{
    public class AspNetCoreProtobufTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public AspNetCoreProtobufTest()
        {
            _server = new TestServer(
                new WebHostBuilder()
                    .UseKestrel()
                    .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task Test1()
        {
            // Act
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-protobuf"));
            var response = await _client.GetAsync("/api/values");
            response.EnsureSuccessStatusCode();

            var result = ProtoBuf.Serializer.Deserialize<List<TestDto>>(await response.Content.ReadAsStreamAsync());

            // Assert
            Assert.Equal("application/x-protobuf", response.Content.Headers.ContentType.MediaType);
            
            Assert.Equal(long.MaxValue, result[0].Tag);
            Assert.Equal(long.MaxValue-1, result[0].Kids[0].Tag);
            Assert.Equal(long.MaxValue-2, result[0].Kids[1].Tag);
            Assert.Equal(long.MaxValue-3, result[1].Tag);
            Assert.Equal(long.MaxValue-4, result[1].Kids[0].Tag);
            Assert.Equal(long.MaxValue-5, result[1].Kids[1].Tag);
            
            Assert.Equal("0", result[0].Name);
            Assert.Equal("00", result[0].Kids[0].Name);
            Assert.Equal("01", result[0].Kids[1].Name);
            Assert.Equal("1", result[1].Name);
            Assert.Equal("10", result[1].Kids[0].Name);
            Assert.Equal("11", result[1].Kids[1].Name);
        }
    }
}