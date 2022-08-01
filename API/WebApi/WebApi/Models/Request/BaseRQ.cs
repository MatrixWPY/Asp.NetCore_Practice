using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Request
{
    public class IdRQ
    {
        [Required(ErrorMessage = "{0} 為必填欄位。")]
        public long? ID { get; set; }
    }

    public class PageRQ
    {
        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
