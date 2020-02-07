using NGA.Core.Model;
using NGA.Core.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace NGA.Data.ViewModel
{
    public class MessageVM : BaseVM
    {
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public DateTime Date { get; set; }
        public Guid? ToUserId { get; set; }
    }

    public class MessageAddVM : AddVM
    {
        [Required, MaxLength(500)]
        public string Text { get; set; }
        [GuidValidation]
        public Guid UserId { get; set; }
        [GuidValidation]
        public Guid GroupId { get; set; }
        public Guid? ToUserId { get; set; }
    }

    public class MessageUpdateVM : UpdateVM
    {
        [Required, MaxLength(500)]
        public string Text { get; set; }
        [GuidValidation]
        public Guid UserId { get; set; }
        [GuidValidation]
        public Guid GroupId { get; set; }
        public Guid? ToUserId { get; set; }
    }
}
