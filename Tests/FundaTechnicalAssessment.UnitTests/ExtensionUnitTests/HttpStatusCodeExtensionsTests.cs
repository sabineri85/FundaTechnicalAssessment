using System.Net;
using Xunit;
using FundaTechnicalAssessment.ExternalIntegrations.Extensions;

namespace FundaTechnicalAssessment.UnitTests.ExtensionUnitTests
{
    public class HttpStatusCodeExtensionsTests
    {
        [Theory]
        [InlineData(HttpStatusCode.BadRequest, typeof(InvalidOperationException), "Client error: 400 BadRequest")]
        [InlineData(HttpStatusCode.NotFound, typeof(InvalidOperationException), "Client error: 404 NotFound")]
        [InlineData(HttpStatusCode.InternalServerError, typeof(Exception), "Server error: 500 InternalServerError")]
        [InlineData(HttpStatusCode.ServiceUnavailable, typeof(Exception), "Server error: 503 ServiceUnavailable")]
        public void ThrowExceptionOnFailedCall_ThrowsExpectedException(HttpStatusCode statusCode, Type expectedExceptionType, string expectedMessage)
        {       
            var ex = Assert.Throws(expectedExceptionType, () => statusCode.ThrowExceptionOnFailedCall());
            Assert.Contains(expectedMessage, ex.Message);
        }

        [Theory]
        [InlineData(HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.Created)]
        [InlineData(HttpStatusCode.Accepted)]
        [InlineData(HttpStatusCode.Found)]
        public void ThrowExceptionOnFailedCall_DoesNotThrow_ForValidCodes(HttpStatusCode statusCode)
        {
            var exception = Record.Exception(() => statusCode.ThrowExceptionOnFailedCall());
            Assert.Null(exception);
        }
    }
}
