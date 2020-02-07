using NGA.Core.Model;

namespace NGA.Data.ViewModel
{
    public class ParameterVM : BaseVM
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string GroupCode { get; set; }
        public string Value { get; set; }
        public int OrderIndex { get; set; }
    }

    public class ParameterAddVM : AddVM
    {
    }

    public class ParameterUpdateVM : UpdateVM
    {
    }
}
