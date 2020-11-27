using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MARKET.Data.ModelsRepo.Interfaces;
using MARKET.Data.Repository;
using MARKET.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MARKET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypesController : ApiController<PaymentType>
    {
        public PaymentTypesController(IPaymentTypeRepository context , IMapper mapper)
            :base(context,mapper)
        {

        }
    }
}
