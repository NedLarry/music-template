// See https://aka.ms/new-console-template for more information


using Microsoft.Extensions.DependencyInjection;
using Music_playlist.Application;


var appContainer = Music_playlist.Application.AppHost.BuildContainer();

var appInstance = appContainer.GetRequiredService<App>();
appInstance.Run();

