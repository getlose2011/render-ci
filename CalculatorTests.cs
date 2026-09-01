using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using render_ci.Controllers;
using Xunit;

namespace render_ci.Tests;

public class CalculatorTests
{
    [Fact]
    public void Add_25_And_35_Returns_60()
    {
        var controller = new WeatherForecastController(NullLogger<WeatherForecastController>.Instance);

        var result = controller.Add(25, 35);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var resultValue = okResult.Value!
            .GetType()
            .GetProperty("result")!
            .GetValue(okResult.Value);

        Assert.Equal(60, resultValue);
    }
}