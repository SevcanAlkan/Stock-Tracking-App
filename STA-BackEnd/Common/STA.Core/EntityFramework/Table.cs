using System;

namespace STA.Core.EntityFramework
{
    public interface ITable : IBase
    {
        DateTime CreateDT { get; set; }
        Guid CreateBy { get; set; }
    }
    public class Table : Base, ITable
    {
        public DateTime CreateDT { get; set; }
        public Guid CreateBy { get; set; }
    }
}
