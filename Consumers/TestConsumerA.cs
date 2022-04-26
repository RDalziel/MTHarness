using MassTransit;

namespace MTHarness.Consumers;

public class TestConsumerA : IConsumer<MessageA>
{
    public async Task Consume(ConsumeContext<MessageA> context)
    {
        await Task.Delay(TimeSpan.FromSeconds(5));
        await context.Publish(new MessageB());
    }
}