using NGA.Core.Model;
using System;

namespace NGA.Data.ViewModel
{
    public class GroupUserVM : BaseVM
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
    }

    public class GroupUserAddVM : AddVM
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
    }

    public class GroupUserUpdateVM : UpdateVM
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
    }
}
