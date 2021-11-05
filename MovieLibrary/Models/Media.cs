using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Models
{
    public abstract class Media
    {
        public uint MediaId { get; set; }
        public string Title { get; set; }
        public abstract string Display();

        //Movie
        public virtual List<string> Genres { get; set; }
        
        //Show
        public virtual int Season { get; set; }
        public virtual int Episode { get; set; }
        public virtual List<string> Writers { get; set; }

        //Video
        public virtual string Format { get; set; }
        public virtual int Length { get; set; }
        public virtual List<int> Regions { get; set; }


    }
}
