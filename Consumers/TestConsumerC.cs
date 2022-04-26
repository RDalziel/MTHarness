using MassTransit;

namespace MTHarness.Consumers;

public class TestConsumerC : IConsumer<MessageC>
{
    public async Task Consume(ConsumeContext<MessageC> context)
    {
        await Task.Delay(TimeSpan.FromSeconds(5));
        await context.Publish(new MessageCConsumed());
    }
}