using WebApi.Models.Response;

namespace WebApi.Commands
{
    public class BaseCommand
    {
        public ApiResult<T> SuccessRP<T>(T result)
        {
            return new ApiResult<T>()
            {
                Code = 0,
                Msg = "Success",
                Result = result
            };
        }

        public ApiResult<T> FailRP<T>(int errorCode, string errorMsg)
        {
            return new ApiResult<T>()
            {
                Code = errorCode,
                Msg = errorMsg
            };
        }
    }
}
