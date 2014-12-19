using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POC.EF.ManyToMany.DAL.EF;

namespace POC.EF.ManyToMany
{
    class Program
    {
        static void Main(string[] args)
        {
            using(MovieDbContext dbContext = new MovieDbContext())
            {
                var actors = dbContext.Actors.ToList();
                var films = dbContext.Films.ToList();
            }
        }
    }
}
