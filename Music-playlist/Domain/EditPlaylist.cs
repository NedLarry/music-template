using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_playlist.Domain
{
    public class EditPlaylist
    {
        StringBuilder menubuilder = new StringBuilder();

        public void Run()
        {
            EditMainMenu();

            var option = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(option))
            {
                var isValidOption = int.TryParse(option, out _);

                if (!isValidOption)
                {
                    Console.WriteLine("No character or String allowed!");
                    Run();
                }

                switch (Convert.ToInt32(option))
                {
                    case 1:
                        EditPlaylistService.ChangePlaylistName();
                        Run();
                        break;
                    case 2:
                        EditPlaylistService.AddNewSong();
                        Run();
                        break;
                    case 3:
                        EditPlaylistService.RemoveSong();
                        Run();
                        break;
                    case 4:
                        //_playlistMaker.Run();
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        Run();
                        break;

                }
            }


            Run();
        }

        void EditMainMenu()
        {
            menubuilder.AppendLine("1. Edit Playlist Name");
            menubuilder.AppendLine("2. Add song to Playlist");
            menubuilder.AppendLine("3. Remove song from Playlist");
            menubuilder.AppendLine("4. Playlist Menu");
            menubuilder.AppendLine("5. Exit");


            Console.WriteLine(menubuilder.ToString());
            menubuilder.Clear();
        }
    }

    internal static class EditPlaylistService
    {

        public static void ChangePlaylistName()
        {

            if (MusicPlayer.PlaylistDictionary.Count <= 0)
            {
                Console.WriteLine("No playlist exits");
                Console.WriteLine();
                return;
            }

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

            var playlist2Change = MusicPlayer.PlaylistDictionary.ElementAt(--intChoice);

            MusicPlayer.PlaylistDictionary[playlistName] = playlist2Change.Value;

            Console.WriteLine($"{playlist2Change.Key} changed to {playlistName}");

            MusicPlayer.PlaylistDictionary.Remove(playlist2Change.Key);

            Console.WriteLine();

        }


        public static void AddNewSong()
        {

            if (MusicPlayer.PlaylistDictionary.Count <= 0)
            {
                Console.WriteLine("No playlist exits");
                Console.WriteLine();
                return;
            }

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
            var playlist2Add = MusicPlayer.PlaylistDictionary.ElementAt(--intChoice);

            ChooseNewSong:
            Console.WriteLine("Choose song(s) to add, q to quit");
            counter = 1;
            for (int i = 0; i < MusicPlayer.MusicList.Count; i++)
            {
                Console.WriteLine($"{counter}. {MusicPlayer.MusicList[i].Title}");
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

                if (playlist2Add.Value.PlaylistSongs.Contains(MusicPlayer.MusicList[--numberChoice]))
                {
                    Console.WriteLine("Songs exits already in playlist");
                    Console.WriteLine();
                    goto ChooseNewSong;
                }

                playlist2Add.Value.PlaylistSongs.Add(MusicPlayer.MusicList[--numberChoice]);

                Console.WriteLine("Music added");

                Console.WriteLine();

                goto ChooseNewSong;
            }




        }

        public static void RemoveSong()
        {

            if (MusicPlayer.PlaylistDictionary.Count <= 0)
            {
                Console.WriteLine("No playlist exits");
                Console.WriteLine();
                return;
            }

            var counter = 1;
            foreach (var kvp in MusicPlayer.PlaylistDictionary)
            {
                Console.WriteLine($"{counter}. {kvp.Key}");
                counter++;
            }

            ChoosePlaylist:
            Console.WriteLine("Choose the playlist to remove song(s)");
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
            var playlist2Remove = MusicPlayer.PlaylistDictionary.ElementAt(--intChoice);

            ChooseNewSong:
            Console.WriteLine("Choose song(s) to remove, q to quit");
            counter = 1;
            for (int i = 0; i < playlist2Remove.Value.PlaylistSongs.Count; i++)
            {
                Console.WriteLine($"{counter}. {playlist2Remove.Value.PlaylistSongs[i].Title}");
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


                playlist2Remove.Value.PlaylistSongs.Remove(playlist2Remove.Value.PlaylistSongs.ElementAt(--numberChoice));

                Console.WriteLine("Music Removed");

                Console.WriteLine();

                goto ChooseNewSong;
            }
        }

    }
}
