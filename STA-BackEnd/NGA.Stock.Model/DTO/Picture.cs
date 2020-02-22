using NGA.Core.MongoDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGA.Stock.Model.DTO
{
    public class Picture : BaseBson
    {
        public string ModelId { get; set; }

        public string ImageData { get; set; }
    }
}
