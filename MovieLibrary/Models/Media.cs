using System;

namespace MovieLibrary.Models
{
    public abstract class Media
    {
        public uint MediaId { get; set; }
        public string Title { get; set; }

        public virtual string Display()
        {
            return $"Id: {MediaId}\nTitle: {Title}\n";
        }
    }
}
