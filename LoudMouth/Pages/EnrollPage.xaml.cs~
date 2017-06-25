using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LoudMouth.Controllers;
using LoudMouth.Models;
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
            string name = PersonName.Text;
            Attendee person = new Attendee();
            person.Name = name;
        }

        async void AddPerson(object sender, EventArgs args) {
            Attendee person = new Attendee();
            person.Name = PersonName.Text;
            person.ProfileID = await nc.CreateProfile();
            await Navigation.PushPopupAsync(new PopupAuth(person));
            people.Add(person);
        }
    }
}
