﻿using mDome.Model;
using mDome.Model.Requests;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace mDome.MobileApp.ViewModels
{
    class AddListOnTheFlyViewModel : BaseViewModel
    {
        private readonly APIService _tracklistService = new APIService("Tracklist");
        private readonly APIService _albumListService = new APIService("AlbumList");
        public AddListOnTheFlyViewModel()
        {
            SubmitCommand = new Command(async () => await Submit());
            GenerateKeyCommand = new Command(GenerateKey);
            SelectedType = "Private";
        }
        public ICommand SubmitCommand { get; set; }
        public ICommand GenerateKeyCommand { get; set; }
        public event EventHandler<object> DisappearHandler;
        bool _isAlbumList;
        public bool IsAlbumList
        {
            get
            {
                return _isAlbumList;
            }

            set
            {
                _isAlbumList = value;
                OnPropertyChanged("IsAlbumList");
            }
        }
        string _name;
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        string _uniqueKey;
        public string UniqueKey
        {
            get
            {
                return _uniqueKey;
            }

            set
            {
                _uniqueKey = value;
                OnPropertyChanged("UniqueKey");
            }
        }
        string _albumListDesc;
        public string AlbumListDesc
        {
            get
            {
                return _albumListDesc;
            }

            set
            {
                _albumListDesc = value;
                OnPropertyChanged("AlbumListDesc");
            }
        }
        string _selectedType;
        public string SelectedType
        {
            get
            {
                return _selectedType;
            }

            set
            {
                _selectedType = value;
                OnPropertyChanged("SelectedType");
            }
        }
        private void GenerateKey()
        {
            var newGuid = Guid.NewGuid();
            var messageID = Convert.ToBase64String(newGuid.ToByteArray());
            UniqueKey = messageID.Substring(0, 18);
        }
        private bool ValidateEmpty()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(UniqueKey) || string.IsNullOrWhiteSpace(SelectedType))
                return false;
            return true;
        }
        private bool ValidateInvalidName()
        {
            if (IsAlbumList == true)
                if (Name == "Liked Albums")
                    return false;
            if (IsAlbumList == false)
                if (Name == "My Discovery Queue" || Name == "History" || Name == "Liked Tracks")
                    return false;
            return true;
        }
        private bool ValidateKeyLength()
        {
            if (UniqueKey.Length >= 20)
                return false;
            return true;
        }
        private async Task Submit()
        {
            if (!ValidateEmpty())
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please fill out the required fields (Name and Unique Key)", "OK");
                return;
            }
            else if (!ValidateInvalidName())
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid list name!", "OK");
                return;
            }
            else if (!ValidateKeyLength())
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Maximum key length is 20!", "OK");
                return;
            }
            else
            {
                if (IsAlbumList)
                {
                    AlbumListUpsertRequest request = new AlbumListUpsertRequest()
                    {
                        AlbumListDescription = AlbumListDesc,
                        AlbumListName = Name,
                        AlbumListType = SelectedType,
                        ListDateCreated = DateTime.Now,
                        UniqueKey = UniqueKey,
                        UserId = APIService.loggedProfile.UserId
                    };
                    var check = await _albumListService.Get<List<AlbumList>>(new AlbumListSearchRequest()
                    {
                        AlbumListName = Name,
                        UniqueKey = UniqueKey
                    });
                    if (check.Count > 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Combination already exists. Please change the list name or unique key.", "OK");
                        return;
                    }
                    var result = await _albumListService.Insert<AlbumList>(request);
                    await Application.Current.MainPage.DisplayAlert("Success", "New album list successfully added!", "OK");
                    DisappearHandler?.Invoke(this, true);
                }
                else
                {
                    TracklistUpsertRequest request = new TracklistUpsertRequest()
                    {
                        ListDateCreated = DateTime.Now,
                        TracklistName = Name,
                        TracklistType = SelectedType,
                        UniqueKey = UniqueKey,
                        UserId = APIService.loggedProfile.UserId
                    };
                    var check = await _tracklistService.Get<List<Tracklist>>(new TracklistSearchRequest()
                    {
                        TracklistName = Name,
                        UniqueKey = UniqueKey
                    });
                    if (check.Count > 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Combination already exists. Please change the list name or unique key.", "OK");
                        return;
                    }
                    var result = await _tracklistService.Insert<Tracklist>(request);
                    await Application.Current.MainPage.DisplayAlert("Success", "New tracklist successfully added!", "OK");
                    DisappearHandler?.Invoke(this, true);
                }
            }
        }
    }
}
