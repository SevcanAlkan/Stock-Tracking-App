using STA.Core.EntityFramework;
using System;

namespace STA.Stock.Model.DTO
{
    public class ProductStock : Table
    {
        public Guid ProductId { get; set; }
        public int Number { get; set; }
        public int Amount { get; set; }
    }
}
