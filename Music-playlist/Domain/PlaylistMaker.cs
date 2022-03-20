using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_playlist.Domain
{

    //defintely going to be a fat class
    public class PlaylistMaker
    {

        StringBuilder menubuilder = new StringBuilder();
        public void EditPlaylist()
        {

        }

        void EditMenu()
        {
            menubuilder.AppendLine("1. Edit Playlist Name");
            menubuilder.AppendLine("2. Add song to Playlist");
            menubuilder.AppendLine("3. Remove song from Playlist");

            menubuilder.ToString();
        }
    }


    internal static class PlaylistMakerService
    {
        static void ChangePlaylistName()
        {

            var counter = 1;
            foreach (var kvp in MusicPlayer.PlaylistDictionary)
            {
                Console.WriteLine($"{counter}. {kvp.Key}");
                counter++;
            }

            ChoosePlaylist:
                Console.WriteLine("Choose the playlist name to change");
                var choice = Console.ReadLine();
            Console.WriteLine();

            var isValidChoice = int.TryParse(choice, out _);
            while (!isValidChoice)
            {

                Console.WriteLine("Wrong input");
                Console.WriteLine();
                goto ChoosePlaylist;

            }

            ChoosePlaylistname:
                Console.WriteLine("Enter playlist new Name:");
                var playlistName = Console.ReadLine();
            Console.WriteLine();

            while (string.IsNullOrWhiteSpace(playlistName))
            {
                Console.WriteLine("Playlist name cannot be empty");
                Console.WriteLine();

                goto ChoosePlaylistname;
            }

            if (MusicPlayer.PlaylistDictionary.ContainsKey(playlistName))
            {
                Console.WriteLine($"{playlistName} already exits");
                Console.WriteLine();

                goto ChoosePlaylist;
            }

            var intChoice = Convert.ToInt32(choice);

            var playlist2Change = MusicPlayer.PlaylistDictionary.ElementAt(intChoice--);

            MusicPlayer.PlaylistDictionary[playlistName] = playlist2Change.Value;

            Console.WriteLine($"{playlist2Change.Key} changed to {playlistName}");

            MusicPlayer.PlaylistDictionary.Remove(playlist2Change.Key);

            Console.WriteLine();

            

            


        }


        static void AddNew()
        {
            var counter = 1;
            foreach (var kvp in MusicPlayer.PlaylistDictionary)
            {
                Console.WriteLine($"{counter}. {kvp.Key}");
                counter++;
            }

            ChoosePlaylist:
                Console.WriteLine("Choose the playlist to add new song(s)");
                var choice = Console.ReadLine();
            Console.WriteLine();

            var isValidChoice = int.TryParse(choice, out _);
            while (!isValidChoice)
            {

                Console.WriteLine("Wrong input");
                Console.WriteLine();
                goto ChoosePlaylist;

            }

            var intChoice = Convert.ToInt32(choice);
            var playlist2Add = MusicPlayer.PlaylistDictionary.ElementAt(intChoice--);

            ChooseNewSong:
                Console.WriteLine("Choose song(s) to add, q to quit");
                counter = 1;
            for (int i= 0; i < MusicPlayer.MusicList.Count; i++)
            {
                Console.WriteLine($"{counter}. {MusicPlayer.MusicList[i]}");
                Console.WriteLine();
                counter++;
            }

            var musicChoice = Console.ReadLine();
            Console.WriteLine();


            if (string.IsNullOrWhiteSpace(musicChoice)) goto ChooseNewSong;

            while (!string.IsNullOrWhiteSpace(musicChoice))
            {

                if (musicChoice == "q") break;

                if (!int.TryParse(musicChoice, out _))
                {
                    Console.WriteLine("Choose a number corresponding to music and press Enter key.");
                    Console.WriteLine();
                    goto ChooseNewSong;
                }

                var numberChoice = int.Parse(musicChoice);

                if (playlist2Add.Value.PlaylistSongs.Contains(MusicPlayer.MusicList[numberChoice--]))
                {
                    Console.WriteLine("Songs exits already in playlist");
                    Console.WriteLine();
                    goto ChooseNewSong;
                }

                playlist2Add.Value.PlaylistSongs.Add(MusicPlayer.MusicList[numberChoice--]);

                Console.WriteLine("Music added");

                Console.WriteLine();

                goto ChooseNewSong;
            }

            


        }
    }
}

