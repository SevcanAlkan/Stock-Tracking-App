using AutoMapper;
using Microsoft.Extensions.Logging;
using NGA.Data.SubStructure;
using NGA.Data.ViewModel;
using NGA.Domain;

namespace NGA.Data.Service
{
    public class ParameterService : BaseService<ParameterAddVM, ParameterUpdateVM, ParameterVM, Parameter>, IParameterService
    {
        #region Ctor

        public ParameterService(UnitOfWork _uow, IMapper _mapper, ILogger<BaseService<ParameterAddVM, ParameterUpdateVM, ParameterVM, Parameter>> _logger)
            : base(_uow, _mapper, _logger)
        {

        }

        #endregion

        #region Methods                

        #endregion
    }

    public interface IParameterService : IBaseService<ParameterAddVM, ParameterUpdateVM, ParameterVM, Parameter>
    {

    }
}
