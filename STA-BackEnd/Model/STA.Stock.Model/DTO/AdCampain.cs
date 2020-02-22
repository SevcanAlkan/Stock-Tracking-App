using STA.Core.EntityFramework;
using STA.Core.Enum;
using System;

namespace STA.Stock.Model.DTO
{
    public class AdCampain : Table
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public AdStatus Status { get; set; }
        public double Price { get; set; }
        public string CompleteComment { get; set; }
        public Guid InfuluencerId { get; set; }
    }
}
