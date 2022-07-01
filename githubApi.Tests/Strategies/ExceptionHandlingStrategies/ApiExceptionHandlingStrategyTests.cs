using System.Threading.Tasks;

using githubApi.Facades.Strategies.ExceptionHandlingStrategies;
using githubApi.Tests.Setup.Controller;

using NSubstitute;

using Serilog;

using Shouldly;

using Xunit;

namespace githubApi.Tests.Strategies.ExceptionHandlingStrategies
{
    public class ApiExceptionHandlingStrategyTests
    {
        private readonly ILogger _logger;

        public ApiExceptionHandlingStrategyTests()
        {
            _logger = Substitute.For<ILogger>();
        }

        private ApiExceptionHandlingStrategy CreateApiExceptionHandlingStrategy()
        {
            return new ApiExceptionHandlingStrategy(_logger);
        }

        [Fact]
        public async Task HandleAsyncExpectedBehaviorAsync()
        {
            var apiExceptionHandlingStrategy = CreateApiExceptionHandlingStrategy();

            var context = ControllerSetup.HttpContext;
            var exception = ControllerSetup.ApiException;

            var result = await apiExceptionHandlingStrategy.HandleAsync(
                context,
                exception);

            result.Response.StatusCode.ShouldBe((int)exception.StatusCode);
        }
    }
}
