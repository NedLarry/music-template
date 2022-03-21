using Music_playlist.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_playlist.Domain
{

    //definitely going to be a fat class
    public class PlaylistMaker
    {

        //private readonly App _app;
        private readonly EditMenu _editMenu;

        public PlaylistMaker(EditMenu _editMenu)
        {
            //this._app = _app;
            this._editMenu = _editMenu;
        }

        StringBuilder menubuilder = new StringBuilder();

        public void Run()
        {
            PrintPlaylistMenu();
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
                        PlaylistMakerService.CreatePlaylist();
                        Run();
                        break;
                    case 2:
                        //_musicPlayer.CreatePlaylist();
                        Run();
                        break;
                    case 3:
                        //PrintMusic(_musicPlayer.ShuffleMusic());
                        Run();
                        break;
                    case 4:
                        _editMenu.Run();
                        Run();
                        break;
                    case 5:
                        //_app.Run();
                        break;
                    case 6:
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

        

        void PrintPlaylistMenu()
        {
            menubuilder.AppendLine($"Welcome to our Playlist Maker");
            menubuilder.AppendLine("1. Create Playlist");
            menubuilder.AppendLine("2. View All Playlist");
            menubuilder.AppendLine("3. Delete Playlist");
            menubuilder.AppendLine("4. Edit Playlist");
            menubuilder.AppendLine("5. Main Menu");
            menubuilder.AppendLine("6. Quit");

            Console.WriteLine(menubuilder.ToString());
            menubuilder.Clear();
        }
    }


    internal static class PlaylistMakerService
    {

        public static void CreatePlaylist()
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


            while (MusicPlayer.PlaylistDictionary.ContainsKey(playlistName))
            {
                Console.WriteLine($"Playlist with name: {playlistName} exits. Try another!!!");
                playlistName = Console.ReadLine();
            }

            var newPlaylist = new Playlist
            {
                PlaylistName = playlistName,
                PlaylistSongs = SelectedPlaylistSongs()
            };

            MusicPlayer.PlaylistDictionary.Add(playlistName, newPlaylist);

            Console.WriteLine($"{playlistName} playlist created");
            Console.WriteLine();

            return;


        }

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
            for (int i= 0; i < MusicPlayer.MusicList.Count; i++)
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

        static List<Music> SelectedPlaylistSongs()
        {


            var availableSongs = MusicPlayer.MusicList.ToList();
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

    }

    public class EditMenu
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
                        PlaylistMakerService.ChangePlaylistName();
                        Run();
                        break;
                    case 2:
                        PlaylistMakerService.AddNewSong();
                        Run();
                        break;
                    case 3:
                        PlaylistMakerService.RemoveSong();
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


}

