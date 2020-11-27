using MARKET.Data.ModelsRepo.Interfaces;
using MARKET.Data.Repository;
using MARKET.Models;
using MARKET.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MARKET.Data.ModelsRepo.Classes
{
    public class TagRepository: Repository<Tag>,ITagRepository
    {
        public TagRepository(DBMarket context)
       : base(context)
        {

        }
    }
}
