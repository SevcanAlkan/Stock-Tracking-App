using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NGA.Data.Service;
using NGA.Data.ViewModel;
using NGA.Domain;
using System;
using System.Threading.Tasks;

namespace NGA.MonolithAPI.Controllers.V2
{
    public class ParameterController : DefaultApiCRUDController<ParameterAddVM, ParameterUpdateVM, ParameterVM, Parameter, IParameterService>
    {
        public ParameterController(IParameterService service, ILogger<ParameterController> logger)
             : base(service, logger)
        {

        }
    }
}
