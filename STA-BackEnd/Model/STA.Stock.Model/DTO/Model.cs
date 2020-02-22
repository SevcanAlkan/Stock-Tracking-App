using STA.Core.EntityFramework;
using System;

namespace STA.Stock.Model.DTO
{
    public class Model : Table
    {
        public string Name { get; set; }
        public Guid BrandId { get; set; }
        public string PictureId { get; set; }
    }
}
