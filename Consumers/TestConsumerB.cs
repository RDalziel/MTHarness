using MassTransit;

namespace MTHarness.Consumers;

public class TestConsumerB : IConsumer<MessageB>
{
    public async Task Consume(ConsumeContext<MessageB> context)
    {
        await Task.Delay(TimeSpan.FromSeconds(5));
        await context.Publish(new MessageC());
    }
}