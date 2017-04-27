using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamMusic.Models
{
    public class Song
    {
        public ulong Id { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }

        public string Album { get; set; }

        public string Genre { get; set; }

        public object Artwork { get; set; }

        public ulong Duration { get; set; }

        //public DateTime Date { get; set; }

        public string Uri { get; set; }

    }
}
