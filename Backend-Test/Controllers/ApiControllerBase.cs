using Backend_Test.Common;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Test.Controllers
{
    /// <summary>
    /// Maps a <see cref="Result"/> to the matching HTTP status code so the
    /// translation lives in one place instead of being duplicated per action.
    /// </summary>
    public abstract class ApiControllerBase : ControllerBase
    {
        protected ActionResult<T> FromResult<T>(Result<T> result) => result.Status switch
        {
            ResultStatus.Success => Ok(result.Value),
            ResultStatus.NotFound => NotFound(result.Error),
            ResultStatus.Invalid => BadRequest(result.Error),
            ResultStatus.Conflict => Conflict(result.Error),
            _ => StatusCode(500)
        };

        protected ActionResult FromResult(Result result) => result.Status switch
        {
            ResultStatus.Success => NoContent(),
            ResultStatus.NotFound => NotFound(result.Error),
            ResultStatus.Invalid => BadRequest(result.Error),
            ResultStatus.Conflict => Conflict(result.Error),
            _ => StatusCode(500)
        };
    }
}
