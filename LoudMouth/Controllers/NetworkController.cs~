﻿using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LoudMouth.Controllers;
using LoudMouth.Models;
using Newtonsoft.Json.Linq;

namespace LoudMouth {
    public class NetworkController {
        string BaseUrl = "https://westus.api.cognitive.microsoft.com/spid/v1.0/";
        string EnrollProfileURL = "identificationProfiles/{0}/enroll?shortAudio={1}"; // POST
        string DeleteProfileURL = "identificationProfiles/{0}"; // DELETE
        string CreateProfileURL = "identifictionProfiles"; // POST
        string IndentifyProfileURL = "identify?{0}&shortaudio={1}";

        HttpClient client;
        DataAccessController db;

        const string SUBSCRIPTION_KEY = "";

        MediaTypeHeaderValue octetMedia = new MediaTypeHeaderValue("application/octet");
        MediaTypeHeaderValue jsonMedia = new MediaTypeHeaderValue("application/json");

        public NetworkController() {
            client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SUBSCRIPTION_KEY);
            db = new DataAccessController();
        }


        // Post
        public async void CreateProfileFor(Attendee person) {
            Debug.WriteLine("Creating a Profile");
            var data = new JObject();
            data.Add("locale", "en-us");
            var content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(CreateProfileURL, content);

            var body = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(body);
            person.ProfileID = (string) json.GetValue("asd"); // Need to work out the profile ID
            Debug.WriteLine(new JObject(body).ToString(Newtonsoft.Json.Formatting.Indented));
        }

        // POST 
        public async void EnrolProfile(Attendee person, bool shortAudio = false) {
            Debug.WriteLine("Enrol a Profile");
            var url = string.Format(EnrollProfileURL, person.ProfileID, shortAudio);

            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("");

            using (var content = new ByteArrayContent(byteData)) {
                content.Headers.ContentType = octetMedia;
                response = await client.PostAsync(url, content);
            }
        }

        // POST
        public async void IdentifyProfile(Attendee[] people, bool shortAudio = false) {
            // Request parameters
            var url = string.Format(IndentifyProfileURL, shortAudio);

            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("");

            using (var content = new ByteArrayContent(byteData)) {
                content.Headers.ContentType = octetMedia;
                response = await client.PostAsync(url, content);
            }

            var body = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(body); // Need get 
        }

        // DELETE
        public async Task<bool> DeleteProfile(Attendee person) {
            Debug.WriteLine("Delete a Profile");
            var url = string.Format(DeleteProfileURL, person.ProfileID);
            var response = await client.DeleteAsync(url);
            if (response.IsSuccessStatusCode) {
                Debug.WriteLine("Deleted user");
                return true;
            } else {
                var body = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("MS API Error: \n" + body);
                return false;
            }
        }
    }
}
