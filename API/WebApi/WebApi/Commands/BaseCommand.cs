using WebApi.Models.Response;

namespace WebApi.Commands
{
    public class BaseCommand
    {
        public Result<T> SuccessRP<T>(T data)
        {
            return new Result<T>()
            {
                Code = 0,
                Msg = "Success",
                Data = data
            };
        }

        public Result<T> FailRP<T>(int errorCode, string errorMsg)
        {
            return new Result<T>()
            {
                Code = errorCode,
                Msg = errorMsg
            };
        }
    }
}
