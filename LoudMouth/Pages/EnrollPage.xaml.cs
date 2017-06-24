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

        public EnrollPage() {
            InitializeComponent();
            PeopleList.ItemsSource = people;
            db = new DataAccessController();
            SubmitButton.Clicked += OnSubmitClicked;
        }

        void OnSubmitClicked(object sender, EventArgs e) {
            //string name = NameEntry.Text;
            //Attendee person = new Attendee();
            //person.Name = name;
        }

        void AddPerson(object sender, EventArgs args) {
            Attendee person = new Attendee();
            person.Name = PersonName.Text;
            Navigation.PushPopupAsync(new PopupAuth(person));
            people.Add(person);
        }
    }
}
