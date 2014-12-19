using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.EF.ManyToMany.DAL.Models
{
    public class Actor
    {
        public int ActorId { get; set; }
        public string Name { get; set; }

        //Many-To-Many
        public virtual ICollection<Film> Films { get; set; }

        public Actor()
        {
            Films = new List<Film>();
        }
    }
}
