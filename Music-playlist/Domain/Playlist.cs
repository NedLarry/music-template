using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_playlist.Domain
{
    public  class Playlist
    {
        public string PlaylistName { get; set; }

        public List<Music> PlaylistSongs { get; set; } = new List<Music>();

        private string PlaylistLength()
        {
            throw new NotImplementedException();
        }
    }
}
