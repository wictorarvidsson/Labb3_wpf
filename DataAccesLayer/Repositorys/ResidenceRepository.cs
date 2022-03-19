using System;
using System.Collections.Generic;
using System.Text;
using DataAccesLayer.Models;
using DataAccesLayer.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccesLayer.Repositorys
{
    public class ResidenceRepository : GenericRepository<Residence>, IResidenceRepository
    {
        public ResidenceRepository(MyContext context) : base(context)
        {
        }

        public IEnumerable<Residence> FindByCity(string city)
        {
            return new List<Residence>();
        }

        public IEnumerable<Residence> SortByCity()
        {
            return dbSet.OrderBy(r => r.City);
        }

        public IEnumerable<Residence> SortByPrice()
        {
            return dbSet.OrderBy(r => r.PricePerDay);
        }

        public IEnumerable<Residence> SortByRating()
        {
            return dbSet.OrderBy(r => r.Rating);
        }
    }
}
