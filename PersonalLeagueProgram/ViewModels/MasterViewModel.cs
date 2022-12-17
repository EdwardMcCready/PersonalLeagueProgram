using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using PersonalLeagueProgram.ChampionModels;

namespace PersonalLeagueProgram.ViewModels
{
    public class MasterViewModel 
    {
        private APIController APIController { get; set; }
        public LoginViewModel LoginViewModel { get; set; }
        public ChampionViewModel ChampionViewModel { get; set; }

        public LoginParameters LoginParameters { get; set; } = new LoginParameters();
        private ICommand loginCommand;
        public ICommand LoginCommand => loginCommand ?? (loginCommand = new RelayCommand(Login));

        private ICommand filterCommand;
        public ICommand FilterCommand => filterCommand ?? (filterCommand = new RelayCommand(Filter));

        public MasterViewModel()
        {
            LoginViewModel = new LoginViewModel();
            ChampionViewModel = new ChampionViewModel();
        }

        public void Login()
        {
            APIController = new APIController(LoginParameters.Key, LoginParameters.Region);
            LoginViewModel.Login(APIController, LoginParameters.Name);
            ChampionViewModel.PopulateChampions(APIController);
        }

        private void Filter()
        {
            ChampionViewModel.FilterChampions(APIController, ChampionViewModel.FilterType.Mastery7);
        }

    }
}
