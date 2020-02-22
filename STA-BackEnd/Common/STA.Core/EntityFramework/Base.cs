using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace STA.Core.EntityFramework
{
    public interface IBase
    {
        Guid Id { get; set; }
        bool IsDeleted { get; set; }
    }
    public class Base : IBase
    {
        [Key]
        public Guid Id { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
