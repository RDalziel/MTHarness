using MassTransit;
using Microsoft.AspNetCore.Mvc;
using MTHarness.Consumers;

namespace MTHarness.Controllers;

[Route("[controller]")]
public class StubController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;

    public StubController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost("", Name = "GetExercise")]
    public async Task<IActionResult> Start()
    {
        await _publishEndpoint.Publish(new MessageA());
        return Ok();
    }
}