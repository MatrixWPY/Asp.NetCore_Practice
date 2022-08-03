namespace WebApi.Models.Response
{
    public class ApiResult<T>
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public T Result { get; set; }
    }

    public class PageData<T>
    {
        public PageRP PageInfo { get; set; }

        public T Data { get; set; }
    }

    public class PageRP
    {
        /// <summary>
        /// 分頁頁碼
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 資料數量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 分頁總數
        /// </summary>
        public int PageCnt { get; set; }

        /// <summary>
        /// 資料總數
        /// </summary>
        public int TotalCnt { get; set; }
    }
}
