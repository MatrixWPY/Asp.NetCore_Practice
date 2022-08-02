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
        private int _PageIndex = 1;
        private int _PageSize = 10;

        public int PageIndex
        {
            get => _PageIndex;
            set => _PageIndex = (value < 1 ? 1 : value);
        }

        public int PageSize
        {
            get => _PageSize;
            set => _PageSize = (value < 1 ? 10 : value);
        }
    }
}
