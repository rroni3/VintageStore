﻿using Microsoft.Extensions.Logging;
using VintageStore.Services;
using VintageStore.ViewModels;
using VintageStore.Views;

namespace VintageStore
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<StoreService>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddTransient<LoadingPage>();
            builder.Services.AddSingleton<StorePage>();
            builder.Services.AddSingleton<StorePageViewModel>();
            builder.Services.AddSingleton<RegisterPageViewModel>();
            builder.Services.AddSingleton<RegisterPage>();
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<HomePageViewModel>();
            builder.Services.AddSingleton<ProfilePageViewModel>();  
            builder.Services.AddSingleton<ProfilePage>();
            builder.Services.AddSingleton<AdminPageViewModel>();
            builder.Services.AddSingleton<AdminPage>();
            builder.Services.AddSingleton<ItemsPage>();
            builder.Services.AddSingleton<ItemsPageViewModel>();




#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}