﻿using MigracaoWorkerService.Context.Interface;
using MigracaoWorkerService.Service.Models.Query;
using MigracaoWorkerService.Service.Repository.Query.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Repository.Query
{
    public class CargoQueryRepository : RepositoryQueryBase<CargoQuery>, ICargoQueryRepository
    {
        public CargoQueryRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
