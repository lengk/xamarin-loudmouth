using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LoudMouth.Controllers;
using LoudMouth.Models;
using LoudMouth.Services;
using LoudMouth.Views;
using Rg.Plugins.Popup;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace LoudMouth.Pages {
    public partial class EnrollPage : ContentPage {
        DataAccessController db;
        ObservableCollection<Attendee> people = new ObservableCollection<Attendee>();
        NetworkController nc = new NetworkController();

        public EnrollPage() {
            InitializeComponent();
            PeopleList.ItemsSource = people;
            db = new DataAccessController();
            SubmitButton.Clicked += OnSubmitClicked;
        }

        void OnSubmitClicked(object sender, EventArgs e) {
            foreach (Attendee a in people) {
                db.Save(a);
            }
            Navigation.PushAsync(new LoudMouthPage());
        }

        async void AddPerson(object sender, EventArgs args) {
            if (string.IsNullOrEmpty(PersonName.Text)) { 
                await DisplayAlert("Error", "Please Enter Name", "OK");
                return;
            }
            Attendee person = new Attendee();
            person.Name = PersonName.Text;
            person.ProfileID = await nc.CreateProfile();
            Action<bool> callback = (result) => {
                if (result) people.Add(person);
            };
            Navigation.PushPopupAsync(new PopupAuth(person, callback));

        }

        void PlayWav(object sender, EventArgs args) {
            DependencyService.Get<IAudioPlayer>().PlayAudio("file.wav");
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e) {
            Attendee a = e.SelectedItem as Attendee;
            DependencyService.Get<IAudioPlayer>().PlayAudio(a.Name);
        }
    }
}
