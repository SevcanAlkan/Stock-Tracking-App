using STA.Core.MongoDB;

namespace STA.Stock.Model.DTO
{
    public class PictureDTO : BaseBson
    {
        public string ModelId { get; set; }

        public string ImageData { get; set; }
    }
}
