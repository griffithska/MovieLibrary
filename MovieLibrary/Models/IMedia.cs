using System;

namespace MovieLibrary.Models
{
    interface IMedia
    {
        public uint MediaId { get; set; }
        public string Title { get; set; }

        public string Display();
    }
}
