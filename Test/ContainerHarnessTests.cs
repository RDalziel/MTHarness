using MTHarness.Consumers;
using NUnit.Framework;

namespace MTHarness.Test;

public class ContainerHarnessTests: ApiTestsFixture
{

    [Test]
    public async Task ChainedMessageTests()
    {
        var client = TestServer.CreateClient();

        var result = await client.PostAsync("stub", null);
        
        Assert.That(result.IsSuccessStatusCode, Is.True);

        Assert.That(await MassTransitTestHarness.Published.Any<MessageCConsumed>(), Is.True);
    }
    
}