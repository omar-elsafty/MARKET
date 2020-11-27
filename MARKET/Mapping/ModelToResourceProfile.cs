using AutoMapper;
using MARKET.Models.Entities;
using MARKET.Models.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MARKET.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            //Since the classes’ properties have the same names and types,
            //we don’t have to use any special configuration for them.
            CreateMap<Category, ElementResource>();
        }
    }
}
