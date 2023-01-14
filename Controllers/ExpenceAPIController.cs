using Expence_tracker3.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Expence_tracker3.Controllers
{
    public class ExpenceAPIController : ApiController
    {
        Expence_trackerEntities dbcontext = new Expence_trackerEntities();
        public IEnumerable<ExpenceData> Get()
        {
            using (Expence_trackerEntities dbcontext = new Expence_trackerEntities())
            {
                return dbcontext.ExpenceDatas.Include(e => e.Category).ToList();
            }
        }

        public ExpenceData Get(int Id)
        {
            using(Expence_trackerEntities dbcontext = new Expence_trackerEntities())
            {
                return dbcontext.ExpenceDatas.Include(e => e.Category).FirstOrDefault(e => e.ExpenceId == Id);
            }
        }

        [HttpGet]
        public IEnumerable<ExpenceData> CategoryVise(int id)
        {
            return dbcontext.ExpenceDatas.Include(e => e.Category).Where(e => e.CategoryId == id).ToList();
        }
    }
}
