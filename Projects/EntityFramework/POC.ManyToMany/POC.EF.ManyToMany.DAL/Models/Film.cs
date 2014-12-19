using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.EF.ManyToMany.DAL.Models
{
    public class Film
    {
        public int FilmId { get; set; }
        public string Name { get; set; }

        //Many-To-Many
        public virtual ICollection<Actor> Actors { get; set; }

        public Film()
        {
            Actors = new List<Actor>();
        }
    }
}
