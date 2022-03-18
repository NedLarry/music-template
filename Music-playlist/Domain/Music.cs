using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_playlist.Domain
{
    public class Music
    {
        public string Title { get; set; }

        public string ArtistName { get; set; }

        public TimeOnly Length { get; set; }
    }
}
