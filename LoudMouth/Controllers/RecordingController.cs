using System;
using System.Threading.Tasks;

namespace LoudMouth.Controllers {
    public class RecordingController {
        public AudioProcessor audio;

        public void StartRecording(string filename, int seconds = 0) {
            audio = new AudioProcessor(filename);
            audio.Record.Execute(null);
            if (seconds > 0) {
                Task.Run(async () => {
                    await Task.Delay(seconds);
                    audio.Stop.Execute(null);
                });
            }
        }

        public void StopRecording(){
            audio.Stop.Execute(null);
        }
    }
}
