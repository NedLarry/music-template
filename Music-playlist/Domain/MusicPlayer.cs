using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_playlist.Domain
{

    public class MusicPlayer
    {
        public static List<Music> MusicList { get; set; } = new List<Music>();

        public static Dictionary<string, Playlist> PlaylistDictionary = new Dictionary<string, Playlist>();

        public string AppName { get; set; } = "Simple Music Player";

        public List<Music> ShuffleMusic ()
        {
            var shuffleList = MusicList.ToList();
            var musicCount = MusicList.Count;

            var randomNumGen = new Random();

            //int n = list.Count;
            while (musicCount > 1)
            {
                musicCount--;

                int randNumber = randomNumGen.Next(musicCount + 1);

                var music = shuffleList[randNumber];
                shuffleList[randNumber] = shuffleList[musicCount];
                shuffleList[musicCount] = music;

            }

            return shuffleList;
        }

        public void AddDummyData()
        {
            MusicList.Clear ();
            IEnumerable<Music> music2Add = new List<Music>()
            {
                new Music
                {
                    Title = "King's Dead",
                    ArtistName = "Kendrick Larmar"
                },

                new Music
                {
                    Title = "Bloody Waters",
                    ArtistName = "Kendrick Larmar"
                },

                new Music
                {
                    Title = "Maadd City",
                    ArtistName = "Kendrick Larmar"
                },

                new Music
                {
                    Title = "Let Go My Hand",
                    ArtistName = "Jermaine Cole"
                },

                new Music
                {
                    Title = "Miss America",
                    ArtistName = "Jermaine Cole"
                },

                new Music
                {
                    Title = ".95 South",
                    ArtistName = "Jermaine Cole"
                },

                new Music
                {
                    Title = "Appying Pressure",
                    ArtistName = "Jermaine Cole"
                }
            };

            MusicList.AddRange(music2Add);
        }

        public void PlaylistLength()
        {
            throw new NotImplementedException ();
        }

        public List<Music> AllMusics() => MusicList.ToList();


    }
}
