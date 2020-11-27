using MARKET.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MARKET.Services.Comunications
{
    public class EntityResponse<T> : BaseResponse
        where T :class 
    {
        //contain our category data if the request successfully finishes.
        public T Entity { get; private set; }

        private EntityResponse(bool success, string message, T entity) 
            : base(success, message)
        {
            Entity = entity;
        }


        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public EntityResponse(T entity) : this(true, string.Empty, entity)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public EntityResponse(string message) : this(false, message, null)
        { }
    }
}

