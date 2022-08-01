namespace WebApi.Models.Response
{
    public class Result<T>
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public T Data { get; set; }
    }
}
