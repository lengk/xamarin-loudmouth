using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using LoudMouth.Models;

namespace LoudMouth {
    public class NetworkController {
        string BaseUrl = "https://westus.api.cognitive.microsoft.com/spid/v1.0/"; 
        string EnrollProfileURL = "identificationProfiles/{0}/enroll?shortAudio={1}"; // POST
        string DeleteProfileURL = "identificationProfiles/{0}"; // DELETE
        string CreateProfileURL = "identifictionProfiles"; // POST
        string IndentifyProfileURL = "identify?{0}&shortaudio={1}";

        HttpClient client;


        public NetworkController() {
            client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
        }


        public void CreateProfile(Attendee person) {
            //client.
        }

        public async void IdentifyProfile(){
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "{subscription key}");

            // Request parameters
            let uri = string.Format()
            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("{body}");

            using (var content = new ByteArrayContent(byteData)) {
                content.Headers.ContentType = new MediaTypeHeaderValue("< your content type, i.e. application/json >");
                response = await client.PostAsync(uri, content);
            }

        }
    }
}
