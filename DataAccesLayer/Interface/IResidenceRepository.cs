﻿using System;
using System.Collections.Generic;
using System.Text;
using DataAccesLayer.Models;

namespace DataAccesLayer.Interface
{
    public interface IResidenceRepository : IGenericRepository<Residence>
    {
        public IEnumerable<Residence> FindByCity(string city);
    }
}
