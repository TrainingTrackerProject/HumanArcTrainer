using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace HumanArc.Compliance.Web.Helpers
{
    /// <summary>
    ///     Creates JsonNetResult objects to return results to the client with consistent status codes and object layout.
    /// </summary>
    public static class JsonResponse
    {
        public static JsonNetResult Success(Object data)
        {
            return new JsonNetResult { Data = data };
        }

        public static JsonNetResult ValidationFailure(IList<ValidationFailure> validationErrors)
        {
            return new JsonNetResult
            {
                Data = new
                {
                    ErrorList = validationErrors.Select(t => t.ErrorMessage)
                },
                StatusCode = HttpStatusCode.BadRequest,
                StatusDescription = "Validation Errors"
            };
        }

        public static JsonNetResult ServerErrors()
        {
            return new JsonNetResult
            {
                Data = new
                {
                    Error = "Server error. Please contact a developer."
                },
                StatusCode = HttpStatusCode.BadRequest,
                StatusDescription = "Server Errors"
            };
        }

        public static JsonNetResult Exception(Exception ex)
        {
            return new JsonNetResult
            {
                Data = new
                {
                    Message = "Error: " + ex.Message,
                    Exception = ex.ToString()
                },
                StatusCode = HttpStatusCode.InternalServerError,
                StatusDescription = "Exception"
            };
        }

        public static JsonNetResult Exception(string message)
        {
            return new JsonNetResult
            {
                Data = new
                {
                    Message = message,
                    Exception = message
                },
                StatusCode = HttpStatusCode.InternalServerError,
                StatusDescription = "Exception"
            };
        }
    }
}