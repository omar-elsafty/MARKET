using MARKET.Data.ModelsRepo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MARKET.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        IPaymentTypeRepository PaymentTypes { get; }
        ITagRepository Tags { get; }
    }
}
