using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using LoudMouth.Controllers;
using LoudMouth.Models;
using LoudMouth.Services;
using Newtonsoft.Json.Linq;
using System.IO;

namespace LoudMouth {
    public class NetworkController {

        string BaseUrl = "https://westus.api.cognitive.microsoft.com/spid/v1.0/";
        string EnrollProfileURL = "identificationProfiles/{0}/enroll?shortAudio={1}"; // POST
        string DeleteProfileURL = "identificationProfiles/{0}"; // DELETE
        string CreateProfileURL = "identifictionProfiles"; // POST
        string IndentifyProfileURL = "identify?identificationProfileIds={0}&shortaudio={1}";

        HttpClient client;
        DataAccessController db;

        const string SUBSCRIPTION_KEY = "4722a947064640e38bf302deac06e0fc";

        MediaTypeHeaderValue octetMedia = new MediaTypeHeaderValue("application/octet-stream");

        public NetworkController() {
            client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SUBSCRIPTION_KEY);
            db = new DataAccessController();
        }


        // Post
        public async Task<string> CreateProfile() {
            var data = new JObject();
            data.Add("locale", "en-us");
            var content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://westus.api.cognitive.microsoft.com/spid/v1.0/identificationProfiles", content);

            var body = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(body);
            var id = (string)json.GetValue("identificationProfileId");
            Debug.WriteLine("Creating a Profile " + id);
            return id;
        }

        // POST 
        public async Task<bool> EnrolProfile(Attendee person, AudioFile file, bool shortAudio = false) {
            Debug.WriteLine("Enrol a Profile");
            try {
                var url = string.Format(EnrollProfileURL, person.ProfileID, shortAudio);

                HttpResponseMessage response;

                // Request body
                IFileLoader loader = DependencyService.Get<IFileLoader>();
                Stream byteData = loader.LoadFile(file.FileName);

                using (var content = new StreamContent(byteData)) {
                    content.Headers.ContentType = octetMedia;
                    response = await client.PostAsync(url, content);
                }

                if (!response.IsSuccessStatusCode) {
                    var body = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(body);
                }
                return response.IsSuccessStatusCode;
            } catch(Exception e){
                Debug.WriteLine(e);
            }
            return false;         
        }

        // POST
        public async Task<Attendee> IdentifyProfile(IEnumerable<Attendee> people, AudioFile file, bool shortAudio = false) {
            // Request parameters
            try {
                string profileIds = "";

                foreach (Attendee a in people) {
                    profileIds += "," + a.ProfileID;
                }
                var url = string.Format(IndentifyProfileURL, profileIds, shortAudio);

                HttpResponseMessage response;

                // Request body
                IFileLoader loader = DependencyService.Get<IFileLoader>();
                Stream byteData = loader.LoadFile(file.FileName);

                using (var content = new StreamContent(byteData)) {
                    content.Headers.ContentType = octetMedia;
                    response = await client.PostAsync(url, content);
                }

                var body = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(body); // Need get 
            } catch (Exception e) {
                Debug.WriteLine(e);
            }
            return null;
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
