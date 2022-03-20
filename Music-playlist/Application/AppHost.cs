using Microsoft.Extensions.DependencyInjection;
using Music_playlist.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_playlist.Application
{
    public static class AppHost
    {

        public static ServiceProvider BuildContainer()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<App>();
            serviceCollection.AddTransient<MusicPlayer>();
            serviceCollection.AddTransient<PlaylistMaker>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
