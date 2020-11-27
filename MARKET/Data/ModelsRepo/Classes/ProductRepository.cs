using MARKET.Data.ModelsRepo.Interfaces;
using MARKET.Data.Repository;
using MARKET.Models;
using MARKET.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MARKET.Data.ModelsRepo.Interfaces
{
    public class ProductRepository: Repository<Product>, IProductRepository
    {
        public ProductRepository(DBMarket context)
            :base(context)
        {

        }
    }
}
