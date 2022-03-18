using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_playlist.Domain
{
    public class MusicPlayer
    {
        public List<Music> MusicList { get; set; } = new List<Music>();

        protected Dictionary<string, Playlist> PlaylistDictionary = new Dictionary<string, Playlist>();

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

        public void CreatePlaylist()
        {
            Console.WriteLine("Enter prefered playlist name");
            var playlistName = Console.ReadLine();
            Console.WriteLine();

            while (string.IsNullOrWhiteSpace(playlistName))
            {
                Console.WriteLine("Playlist name cannot be null or empty space");
                Console.WriteLine("Enter prefered playlist name");
                playlistName = Console.ReadLine();
            }


            while (PlaylistDictionary.ContainsKey(playlistName))
            {
                Console.WriteLine($"Playlist with name: {playlistName} exits. Try another!!!");
                playlistName = Console.ReadLine();
            }

            var newPlaylist = new Playlist
            {
                PlaylistName = playlistName,
                PlaylistSongs = SelectedPlaylistSongs()
            };

            PlaylistDictionary.Add(playlistName, newPlaylist);

            Console.WriteLine($"{playlistName} playlist created");
            Console.WriteLine();

            return;


        }

        private List<Music> SelectedPlaylistSongs()
        {


            var availableSongs = AllMusics();
            var playlist = new List<Music>();


            DataEnry:
                Console.WriteLine("Choose number corresponding to music you'd like to add to playlist and press Enter key to add \n" +
                "Press q to quit\n");

            ShowMusic();

            var choice = Console.ReadLine();
            Console.WriteLine();

            if (string.IsNullOrWhiteSpace(choice)) goto DataEnry;

            while (!string.IsNullOrWhiteSpace(choice))
            {

                if (choice == "q") return playlist;

                if (!int.TryParse(choice, out _))
                {
                    Console.WriteLine("Choose a number corresponding to music and press Enter key.");
                    Console.WriteLine();
                    goto DataEnry;
                }

                var numberChoice = int.Parse(choice);


                playlist.Add(availableSongs[numberChoice]);
                Console.WriteLine("Music added");

                Console.WriteLine();
                availableSongs.RemoveAt(numberChoice);

                goto DataEnry;
            }

            return null;



            void ShowMusic()
            {
                int counter = 0;
                foreach (var songs in availableSongs)
                {
                    Console.WriteLine($"{counter}. {songs.Title}");
                    counter++;
                }

                Console.WriteLine();
            }
        }

        public void PlaylistLength()
        {
            throw new NotImplementedException ();
        }

        public List<Music> AllMusics() => MusicList.ToList();


    }
}
