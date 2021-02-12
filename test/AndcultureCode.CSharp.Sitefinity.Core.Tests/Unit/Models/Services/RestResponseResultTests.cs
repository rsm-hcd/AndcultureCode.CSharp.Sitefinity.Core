using AndcultureCode.CSharp.Sitefinity.Core.Models.Services;
using RestSharp;
using Shouldly;
using System.Net;
using Xunit;

namespace AndcultureCode.CSharp.Sitefinity.Core.Tests.Unit.Model.Services
{
    public class RestResponseResultTests : BaseUnitTest
    {
        #region WasExpectedStatusCode

        [Fact]
        public void WasExpectedStatusCode_Given_RestResponse_StatusCode_Matches_ExpectedStatusCode_Returns_True()
        {
            // Arrange
            var statusCode = HttpStatusCode.OK;
            var restResponse = new RestResponse() { StatusCode = statusCode };

            var restResponseResult = new RestResponseResult<bool>(
                statusCode,
                restResponse
            );

            // Act & Assert
            restResponseResult.WasExpectedStatusCode.ShouldBeTrue();
        }

        [Fact]
        public void WasExpectedStatusCode_Given_RestResponse_StatusCode_Does_Not_Match_ExpectedStatusCode_Returns_False()
        {
            // Arrange
            var restResponse = new RestResponse() { StatusCode = HttpStatusCode.OK };

            var restResponseResult = new RestResponseResult<bool>(
                HttpStatusCode.InternalServerError,
                restResponse
            );

            // Act & Assert
            restResponseResult.WasExpectedStatusCode.ShouldBeFalse();
        }

        #endregion WasExpectedStatusCode

        #region WasUnexpectedStatusCode

        [Fact]
        public void WasUnexpectedStatusCode_Given_RestResponse_StatusCode_Matches_ExpectedStatusCode_Returns_False()
        {
            // Arrange
            var statusCode = HttpStatusCode.OK;
            var restResponse = new RestResponse() { StatusCode = statusCode };

            var restResponseResult = new RestResponseResult<bool>(
                statusCode,
                restResponse
            );

            // Act & Assert
            restResponseResult.WasUnexpectedStatusCode.ShouldBeFalse();
        }

        [Fact]
        public void WasUnexpectedStatusCode_Given_RestResponse_StatusCode_Does_Not_Match_ExpectedStatusCode_Returns_True()
        {
            // Arrange
            var restResponse = new RestResponse() { StatusCode = HttpStatusCode.OK };

            var restResponseResult = new RestResponseResult<bool>(
                HttpStatusCode.InternalServerError,
                restResponse
            );

            // Act & Assert
            restResponseResult.WasUnexpectedStatusCode.ShouldBeTrue();
        }

        #endregion WasUnexpectedStatusCode
    }
}
