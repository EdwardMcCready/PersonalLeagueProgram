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

namespace PersonalLeagueProgram.ViewModels
{
    public class MainFormViewModel :  INotifyPropertyChanged 
    {
        private APIController APIController { get; set; }
        private RiotModels.SummonerDTO Summoner { get; set; }

        private ICommand loginCommand;
        public ICommand LoginCommand => loginCommand ?? (loginCommand = new RelayCommand<LoginParameters>(Login));

        public Dictionary<string, Champion> ChampionMasteries = new Dictionary<string, Champion>();
        public ObservableCollection<Champion> Champions { get; set; } = new ObservableCollection<Champion>();

        public LoginParameters LoginParameters { get; set; } = new LoginParameters();

        private bool loggedIn;
        public bool LoggedIn
        {
            get { return loggedIn; }
            set { loggedIn = value; OnPropertyChanged(nameof(LoggedIn)); }
        }

        private Dictionary<string, Champion> ChampionsById { get; set; } = new Dictionary<string, Champion>();
        private const string ImagePath = "/PersonalLeagueProgram;component/Resources/";
        private void ReadJsonFile()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "PersonalLeagueProgram.Resources.champion.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string jsonFile = reader.ReadToEnd(); //Make string equal to full file
                    var championData = Newtonsoft.Json.JsonConvert.DeserializeObject<ChampionModels.ChampionInfo>(jsonFile);

                    foreach (var champion in championData.Data)
                    {
                        var championInfo = champion.Value;

                        ChampionsById.Add(champion.Value.Key,
                            new Champion(championInfo.Name,
                                         championInfo.Title,
                                         ImagePath + championInfo.Image.Full));
                    }
                }
            }
        }

        private void Login(LoginParameters loginParameters)
        {
            ReadJsonFile();
            APIController =  new APIController(loginParameters.Key, loginParameters.Region);
            Summoner = APIController.GetSummonerByName(loginParameters.Name);
            LoggedIn = true;
            GetMasteries();
        }

        private void GetMasteries()
        {
            var masteries = APIController.GetChampionMasteries();

            foreach (var mastery in masteries)
            {
                if (ChampionsById.TryGetValue(mastery.ChampionId.ToString(), out var champion))
                     Champions.Add(champion);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
