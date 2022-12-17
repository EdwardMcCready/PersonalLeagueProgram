using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using PersonalLeagueProgram.ChampionModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;
using System.ComponentModel;

namespace PersonalLeagueProgram.ViewModels
{
    public class ChampionViewModel : INotifyPropertyChanged
    {
        private Dictionary<string, Champion> AllChampionsById { get; set; } = new Dictionary<string, Champion>();

        public List<RiotModels.ChampionMasteryDto> AllChampionMastery = new List<RiotModels.ChampionMasteryDto>();

        private ObservableCollection<Champion> champions = new ObservableCollection<Champion>();
        public ObservableCollection<Champion> Champions 
        { 
            get
            {
                return champions;
            }
            set
            {
                champions = value;
                OnPropertyChanged(nameof(Champions));
            }
        } 

        private const string ImagePath = "/PersonalLeagueProgram;component/Resources/";

        public ChampionViewModel()
        {
        }

        public void FilterChampions(APIController apiController, FilterType filterType)
        {
            Champions.Clear();
            FilterByMastery(apiController, (int)filterType);
        }

        public void PopulateChampions(APIController apiController)
        {
            SetAllChampions();
            AllChampionMastery = apiController.GetChampionMasteries();

            // champions are populated using the JSON file so we need to check the user's
            // mastery and add it ourselves
            foreach (var mastery in AllChampionMastery)
            {
                if (AllChampionsById.TryGetValue(mastery.ChampionId.ToString(), out var champion))
                {
                    champion.MasteryNumber = mastery.ChampionPoints;
                    champion.Mastery = mastery.ChampionLevel.ToString();
                    Champions.Add(champion);
                }
            }
        }


        public void FilterByMastery(APIController apiController, int masteryFilter)
        {
            // AllChampionsById will have mastery level by now
            foreach (var champion in AllChampionsById)
            {
                if (champion.Value.Mastery == masteryFilter.ToString())
                {
                    Champions.Add(champion.Value);
                }
            }

            Champions = new ObservableCollection<Champion>(Champions.OrderByDescending(x => x.MasteryNumber));
        }

        private void SetAllChampions()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "PersonalLeagueProgram.Resources.champion.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string jsonFile = reader.ReadToEnd();
                    var championData = Newtonsoft.Json.JsonConvert.DeserializeObject<ChampionModels.ChampionInfo>(jsonFile);

                    foreach (var champion in championData.Data)
                    {
                        var championInfo = champion.Value;

                        AllChampionsById.Add(champion.Value.Key,
                            new Champion()
                            {
                                Name = championInfo.Name,
                                Title = championInfo.Title,
                                Image = ImagePath + championInfo.Image.Full
                            });
                    }
                }
            }
        }
        public enum FilterType
        {
            Mastery7 = 7
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
