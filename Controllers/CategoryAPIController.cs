using Expence_tracker3.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Expence_tracker3.Controllers
{
    public class CategoryAPIController : ApiController
    {
        Expence_trackerEntities dbcontext = new Expence_trackerEntities();
        public IEnumerable<Category> Get()
        {
            using (Expence_trackerEntities dbcontext = new Expence_trackerEntities())
            {
                return dbcontext.Categories.ToList();
            }
        }

        public Category Get(int id)
        {
            using (Expence_trackerEntities dbcontext = new Expence_trackerEntities())
            {
                return dbcontext.Categories.FirstOrDefault(e => e.CategoryId == id);
            }
        }
    }
}
