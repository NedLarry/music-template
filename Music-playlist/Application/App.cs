﻿using Music_playlist.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_playlist.Application
{
    public class App
    {


        StringBuilder menuBuilder  = new StringBuilder();
        private readonly MusicPlayer _musicPlayer;

        public App(MusicPlayer _musicPlayer)
        {
            this._musicPlayer = _musicPlayer;
            this._musicPlayer.AddDummyData();
        }



        public void Run ()
        {
            PrintMainMenu();
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
                        PrintMusic(_musicPlayer.AllMusics());
                        Run();
                        break;
                    case 2:
                        _musicPlayer.CreatePlaylist();
                        Run();
                        break;
                    case 3:
                        PrintMusic(_musicPlayer.ShuffleMusic());
                        Run();
                        break;
                    case 4:
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

        void PrintMainMenu()
        {
            menuBuilder.AppendLine($"Welcome to {_musicPlayer.AppName}. Choose an option");
            menuBuilder.AppendLine("1. Show all Music");
            menuBuilder.AppendLine("2. Make Playlist");
            menuBuilder.AppendLine("3. Shuffle");
            menuBuilder.AppendLine("4. Quit");

            Console.WriteLine(menuBuilder.ToString());
            menuBuilder.Clear();
        }

        void PrintPlaylistMenu()
        {
            menuBuilder.AppendLine($"Welcome to our Playlist Maker");
            menuBuilder.AppendLine("1. Create Playlist");
            menuBuilder.AppendLine("2. View All Playlist");
            menuBuilder.AppendLine("3. Delete Playlist");
            menuBuilder.AppendLine("4. Main Menu");
            menuBuilder.AppendLine("5. Quit");

            Console.WriteLine(menuBuilder.ToString());
            menuBuilder.Clear();
        }

        void PrintMusic(List<Music> musicList)
        {
            if (musicList == null) return;

            foreach(var music in musicList)
            {
                Console.WriteLine($"Title: {music.Title} \n" +
                    $"Artist: {music.ArtistName}");
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
