# Zaabee.AspNetCoreProtobuf

Protobuf extensions for asp.net core

## QuickStart

### NuGet

    Install-Package Zaabee.AspNetCoreProtobuf

### Build Project

Create an asp.net core project and import reference in startup.cs

    using Zaabee.AspNetCoreProtobuf;

Modify the ConfigureServices like this

```CSharp
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc(options => { options.AddProtobufSupport(); });
    }
```

Now you can send a request with content-type header "application/x-protobuf" to get a protobuf format response.

### Demo

Create an asp.net core project and import the Zaabee.AspNetCoreProtobuf from nuget as above,and add class and enum for test like this

```CSharp
    [ProtoContract]
    public class TestDto
    {
        [ProtoMember(1)] public Guid Id { get; set; }
        [ProtoMember(2)] public string Name { get; set; }
        [ProtoMember(3)] public DateTime CreateTime { get; set; }
        [ProtoMember(4)] public List<TestDto> Kids { get; set; }
        [ProtoMember(5)] public long Tag { get; set; }
        [ProtoMember(6)] public TestEnum Enum { get; set; }
    }

    public enum TestEnum
    {
        Apple,
        Banana,
        Pear
    }
```

Modify the default controller which named ValuesController to return the dto

```CSharp
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<TestDto> Get()
        {
            return new List<TestDto>
            {
                new TestDto
                {
                    Id = Guid.NewGuid(),
                    Tag = long.MaxValue,
                    CreateTime = DateTime.Now,
                    Name = "0",
                    Enum = TestEnum.Apple,
                    Kids = new List<TestDto>
                    {
                        new TestDto
                        {
                            Id = Guid.NewGuid(),
                            Tag = long.MaxValue - 1,
                            CreateTime = DateTime.Now,
                            Name = "00",
                            Enum = TestEnum.Banana
                        },
                        new TestDto
                        {
                            Id = Guid.NewGuid(),
                            Tag = long.MaxValue - 2,
                            CreateTime = DateTime.Now,
                            Name = "01",
                            Enum = TestEnum.Pear
                        }
                    }
                }
            };
        }
    }
```

### Test Project

Create a XUnit project and get Microsoft.AspNetCore.TestHost from nuget

    Install-Package Microsoft.AspNetCore.TestHost

Reference the Demo project and add a test class like this

```CSharp
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
        public async Task Test()
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

            Assert.Equal("0", result[0].Name);
            Assert.Equal("00", result[0].Kids[0].Name);
            Assert.Equal("01", result[0].Kids[1].Name);
        }
    }
```

You can run or debug the test in Rider(Cross-platform .NET IDE by JetBrains) or Visual Studio with Test Explorer.