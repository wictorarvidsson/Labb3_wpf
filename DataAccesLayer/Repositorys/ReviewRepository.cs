using System;
using System.Collections.Generic;
using System.Text;
using DataAccesLayer.Models;
using DataAccesLayer.Interface;

namespace DataAccesLayer.Repositorys
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(MyContext context) : base(context)
        {
        }
    }
}
