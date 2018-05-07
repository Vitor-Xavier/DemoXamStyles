﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DemoXamStyles.Models;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace DemoXamStyles.ViewModels
{
    public class ListViewModel : BindableObject
    {
        public ObservableCollection<Character> Characters { get; set; }

        public ICommand SearchCommand => new Command(SearchCommandExecute);

        public ICommand RefreshCommand => new Command(LoadCharacters);

        private string _searchTerm;

        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value;
                if (_searchTerm.Length > 2)
                    Search();
                if (_searchTerm.Length == 0)
                    LoadCharacters();
                OnPropertyChanged();
            }
        }

        private bool swithControl;

        public bool SwithControl
        {
            get { return swithControl; }
            set
            {
                swithControl = value;
                OnPropertyChanged();
            }
        }

        private bool refresh;

        public bool Refresh
        {
            get { return refresh; }
            set
            {
                refresh = value;
                OnPropertyChanged();
            }
        }

        private INavigation _navigation;

        public ListViewModel(INavigation navigation)
        {
            _navigation = navigation;
            Characters = new ObservableCollection<Character>();
            LoadCharacters();

            MessagingCenter.Subscribe<string>(this, "SearchTerm", (searchTerm) =>
            {
                SearchTerm = searchTerm;
            });
        }

        private async void SearchCommandExecute()
        {
            await _navigation.PushPopupAsync(new Pages.SearchPage(SearchTerm));
        }

        private void Search()
        {
            var searchResult = Characters.Where(c => c.Name.ToLower().Contains(SearchTerm.ToLower())).ToList();
            Characters.Clear();
            foreach (var item in searchResult)
                Characters.Add(item);
        }

        private void LoadCharacters()
        {
            Refresh = true;
            Characters.Clear();
            Characters.Add(new Character()
            {
                CharacterId = 1,
                Name = "Rogerinho",
                AvatarSource = "https://pbs.twimg.com/profile_images/968279522905874432/mI4jZTo0_400x400.jpg",
                Vehicle = new Vehicle { VehicleId = 1, Brand = "Mercedes-Benz", Model = "Sprinter", Color = "Azul e Vermelha", ImageSource = "https://img.class.posot.com.br/pt_br/2016/10/01/Mercedes-Benz-Sprinter-310-Van-1999-1999-Vermelho-Diesel-20161001183023.jpg" }
            });
            Characters.Add(new Character()
            {
                CharacterId = 2,
                Name = "Renan",
                AvatarSource = "https://media.tenor.com/images/6eb4e24150ca009808e3336f5a85888b/tenor.png",
                Vehicle = new Vehicle { VehicleId = 2, Brand = "Asia Motors", Model = "Towner", Color = "Azul Bebê", ImageSource = "data:image/jpeg;base64,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" }
            });
            Characters.Add(new Character()
            {
                CharacterId = 3,
                Name = "Maurilio",
                AvatarSource = "https://pbs.twimg.com/profile_images/800925111373205504/kh9JKoWJ_400x400.jpg",
                Vehicle = new Vehicle { VehicleId = 4, Brand = "Volkswagen", Model = "Kombi", Color = "Branca", Year = 1984, ImageSource = "data:image/jpeg;base64,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" }
            });
            Characters.Add(new Character()
            {
                CharacterId = 4,
                Name = "Julinho",
                AvatarSource = "https://pbs.twimg.com/profile_images/800892278470537216/eejoSRRh_400x400.jpg",
                Vehicle = new Vehicle { VehicleId = 3, Brand = "Mercedes-Benz", Model = "Sprinter", Color = "Branca", ImageSource = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQWLmFqyaa7SPaUbk81JLqXLNpXsi2u_GlKubmgVWA0UCW3sMmdpQ" }
            });
            Refresh = false;
        }
    }
}