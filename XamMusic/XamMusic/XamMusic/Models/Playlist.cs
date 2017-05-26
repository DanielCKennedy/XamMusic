using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamMusic.Models
{
    public class Playlist
    {
        public ulong Id { get; set; }

        public string Title { get; set; }

        public IList<Song> Songs { get; set; }

        public bool IsDynamic { get; set; }

        public DateTime DateModified { get; set; }

    }
}
