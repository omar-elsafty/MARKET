using MARKET.Data.ModelsRepo.Classes;
using MARKET.Data.ModelsRepo.Interfaces;
using MARKET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MARKET.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DBMarket context)
        {
            Products = new ProductRepository(context);
            Categories = new CategoryRepository(context);
            PaymentTypes = new PaymentTypeRepository(context);
            Tags = new TagRepository(context);
        }

        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }
        public IPaymentTypeRepository PaymentTypes { get; }
        public ITagRepository Tags { get; }
    }
}
