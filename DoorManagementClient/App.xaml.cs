using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DoorManagementClient.ViewModels;
using DoorManagementClient.IRepository;
using DoorManagementClient.Repository;
using DoorManagementClient.Data;
using Microsoft.AspNetCore.SignalR.Client;
using DoorManagementClient.ClientTransporter;

namespace DoorManagementClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IDoorRepository doorRepository;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            HubConnection connection = new HubConnectionBuilder()
               .WithUrl("http://localhost:5000/DoorManagement")
               .Build();

            doorRepository = new DoorRepository(new DoorManagementContext());
            HomeViewModel homeViewModel = new HomeViewModel(doorRepository, new SignalRMessageService(connection));
            Home home = new Home(homeViewModel);
            home.Show();
        }
    }
}
