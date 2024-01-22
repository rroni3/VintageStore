﻿using VintageStore.Views;

namespace VintageStore
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Register", typeof(RegisterPage));
            Routing.RegisterRoute("Login", typeof(MainPage));

        }
    }
}