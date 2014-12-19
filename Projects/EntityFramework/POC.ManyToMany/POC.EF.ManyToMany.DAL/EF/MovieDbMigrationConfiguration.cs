using System.Data.Entity.Migrations;
using System.Linq;
using POC.EF.ManyToMany.DAL.Models;

namespace POC.EF.ManyToMany.DAL.EF
{
    public class MovieDbMigrationConfiguration : DbMigrationsConfiguration<MovieDbContext>
    {
        public MovieDbMigrationConfiguration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MovieDbContext context)
        {
            base.Seed(context);

            // Delete all records
            //DeleteAllRecords(context);

            //Scenerio 01

            //Scenerio01(context);

            //Scenerio02(context);


            
        }

        private static void Scenerio01(MovieDbContext context)
        {
            Actor a1 = new Actor() { Name = "Actor01" };
            Actor a2 = new Actor() { Name = "Actor02" };

            Film f1 = new Film() { Name = "Film01" };
            Film f2 = new Film() { Name = "Film02" };

            context.Actors.Add(a1);
            context.Actors.Add(a2);
            context.Films.Add(f1);
            context.Films.Add(f2);
            context.SaveChanges();
        }

        private static void Scenerio02(MovieDbContext context)
        {
            Actor a1 = new Actor() { Name = "Actor01" };
            Actor a2 = new Actor() { Name = "Actor02" };

            Film f1 = new Film() { Name = "Film01" };
            Film f2 = new Film() { Name = "Film02" };


            a1.Films.Add(f1);
            a1.Films.Add(f2);

            f2.Actors.Add(a2);

            context.Actors.Add(a1);
            context.Actors.Add(a2);
            context.Films.Add(f1);
            context.Films.Add(f2);
            context.SaveChanges();
        }

        private static void Scenerio03(MovieDbContext context)
        {
            Scenerio01(context);


        }

        private void DeleteAllRecords(MovieDbContext context)
        {
            var actors = context.Actors.ToList();
            var films = context.Films.ToList();

            if(actors != null)
            {
                actors.ForEach(x => context.Actors.Remove(x));
            }
            if(films != null)
            {
                films.ForEach(x => context.Films.Remove(x));
            }
            context.SaveChanges();

        }
    }
}
