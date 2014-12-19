using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POC.EF.ManyToMany.DAL.EF.TableDefs;
using POC.EF.ManyToMany.DAL.Models;

namespace POC.EF.ManyToMany.DAL.EF
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext()
            : base("MovieDBConStr")
        {
            InitDbContext();
        }

      

        private void InitDbContext()
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<MovieDbContext, MovieDbMigrationConfiguration>()
                );
        }

        public IDbSet<Actor> Actors { get; set; }
        public IDbSet<Film> Films { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new FilmTableDef());
            modelBuilder.Configurations.Add(new ActorTableDef());

             modelBuilder.Entity<Actor>()
                 .HasMany(actor => actor.Films)
                 .WithMany(film => film.Actors)
                 .Map(map =>  map.MapLeftKey("ActorId")
                                    .MapRightKey("FilmId")
                                    .ToTable("Actor_Film_Map")
                 );


        }
    }
}
