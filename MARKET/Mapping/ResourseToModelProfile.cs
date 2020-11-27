using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MARKET.Models.Entities;
using MARKET.Models.Resources;

namespace MARKET.Mapping
{
    public class ResourseToModelProfile:Profile
    {
        public ResourseToModelProfile()
        {
            CreateMap<SaveElementResource, Category>();
            CreateMap(typeof(Source<>), typeof(Destination<>));
        }

        public class Source<T>
        {
            public T Value { get; set; }
        }

        public class Destination<T>
        {
            public T Value { get; set; }
        }

        // Create the mapping
        MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap(typeof(Source<>), typeof(Destination<>)));
    }
}
