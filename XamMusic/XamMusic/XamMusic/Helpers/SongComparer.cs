using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamMusic.Models;

namespace XamMusic.Helpers
{
    public class SongComparer : IEqualityComparer<Song>
    {
        public bool Equals(Song x, Song y)
        {
            return x.Id == y.Id && (bool)x.Uri?.Equals(y.Uri);
        }

        public int GetHashCode(Song obj)
        {
            return (int)obj.Id;
        }
    }
}
