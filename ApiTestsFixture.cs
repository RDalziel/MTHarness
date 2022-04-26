using MassTransit.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace MTHarness;

public class ApiTestsFixture
{
    protected TestServer TestServer;
    private IServiceScope TestSetupScope;
    protected ITestHarness MassTransitTestHarness;

    private static IWebHostBuilder CreateHostBuilder()
    {
        var hostBuilder = new WebHostBuilder()
            .ConfigureAppConfiguration(cb =>
            {
                cb.AddJsonFile("appsettings.json", false)
                    .AddEnvironmentVariables()
                    .AddInMemoryCollection();
            })
            .UseStartup<Startup>();

        return hostBuilder;
    }

    [TearDown]
    public async Task TearDown()
    {
        TestSetupScope.Dispose();
        await TestServer.Host.StopAsync();
        TestServer.Dispose();
    }

    [SetUp]
    public async Task Setup()
    {
        TestServer = new TestServer(CreateHostBuilder());
        TestSetupScope = TestServer.Services.CreateScope();
        MassTransitTestHarness = TestServer.Services.GetTestHarness();

        MassTransitTestHarness.TestTimeout = TimeSpan.FromHours(1);
        MassTransitTestHarness.TestInactivityTimeout = TimeSpan.FromHours(1);
        
        await TestServer.Host.StartAsync();
        await MassTransitTestHarness.Start();
    }
}