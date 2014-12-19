using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POC.EF.ManyToMany.DAL.Models;

namespace POC.EF.ManyToMany.DAL.EF.TableDefs
{
    public class ActorTableDef : EntityTypeConfiguration<Actor>
    {
        public ActorTableDef()
        {
            Init();
        }

        private void Init()
        {
            ToTable("Actors");
            HasKey(x => x.ActorId);
            Property(x => x.Name).HasColumnName("ActorName").IsRequired().HasMaxLength(512);
        }
    }
}
