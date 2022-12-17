using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using PersonalLeagueProgram.ViewModels;

namespace PersonalLeagueProgram.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private RiotModels.SummonerDTO Summoner { get; set; }

        private bool loggedIn;
        public bool LoggedIn
        {
            get { return loggedIn; }
            set { loggedIn = value; OnPropertyChanged(nameof(LoggedIn)); }
        }

        public LoginViewModel()
        {
        }

        public void Login(APIController apiController, string summonerName)
        {
            Summoner = apiController.GetSummonerByName(summonerName);
            LoggedIn = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
