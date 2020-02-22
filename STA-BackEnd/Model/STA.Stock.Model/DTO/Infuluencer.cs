using STA.Core.EntityFramework;
using STA.Core.Enum;

namespace STA.Stock.Model.DTO
{
    public class Infuluencer : Table
    {
        public string  Name { get; set; }
        public string Address { get; set; }
        public string Instagram { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string Description { get; set; }
        public int Followers { get; set; }
    }
}
