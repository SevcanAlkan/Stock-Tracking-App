using System;
using System.Collections.Generic;
using System.Text;

namespace STA.Stock.Data.Service
{
    public class ProductService : BaseService<ProductAddVM, ProductUpdateVM, ProductVM, ProductDTO, ProductDbContext>, IProductService
    {
        #region Ctor

        public ProductService(UnitOfWork<ProductDbContext> _uow, IMapper _mapper, ILogger<BaseService<ProductAddVM, ProductUpdateVM, ProductVM, ProductDTO, ProductDbContext>> _logger)
            : base(_uow, _mapper, _logger)
        {

        }

        #endregion

        #region Methods

        #endregion
    }

    public interface IProductService : IBaseService<ProductAddVM, ProductUpdateVM, ProductVM, ProductDTO, ProductDbContext>
    {
        List<ProductVM> GetProductsByGroupId(Guid groupId);
    }
}
