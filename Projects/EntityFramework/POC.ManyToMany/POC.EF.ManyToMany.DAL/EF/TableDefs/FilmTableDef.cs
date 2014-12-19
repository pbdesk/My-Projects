using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POC.EF.ManyToMany.DAL.Models;

namespace POC.EF.ManyToMany.DAL.EF.TableDefs
{
    public class FilmTableDef : EntityTypeConfiguration<Film>
    {
        public FilmTableDef()
        {
            Init();
        }

        private void Init()
        {
            ToTable("Films");
            HasKey(x => x.FilmId);
            Property(x => x.Name).HasColumnName("FilmName").IsRequired().HasMaxLength(1000);

        }
    }
}
