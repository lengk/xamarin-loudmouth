using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using LoudMouth.Services;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.IO;
using XLabs.Platform.Mvvm;
using XLabs.Platform.Services.Media;
using XLabs;

namespace LoudMouth.Controllers {
    public class RecordingController {
        public RecordingController() {

        }

        IAudioStream audioStream;
        static WaveRecorder recorder;
        static string FileName = "/chicken.wav";

        public void Start() {
            AudioProcessor processor = new AudioProcessor();
            var SampleRate = 16000;

            var device = Resolver.Resolve<IDevice>();

            var app = Resolver.Resolve<IXFormsApp>();
            audioStream = device.Microphone;
            recorder = new WaveRecorder();

            audioStream.OnBroadcast += audioStream_OnBroadcast;

            Stream stream = DependencyService.Get<IFileLoader>().LoadWriteStream(FileName);
            Task.Run(async () => {
                await Task.Delay(5000);
                Stop();


                Play();
            });

            recorder.StartRecorder(audioStream, stream, SampleRate).ContinueWith(t => {
                if (t.IsCompleted) {
                    System.Diagnostics.Debug.WriteLine("Microphone recorder {0}.", t.IsCompleted ? "was started" : "failed to start.");
                    return true;
                } else if (t.IsFaulted) {
                    audioStream.OnBroadcast -= audioStream_OnBroadcast;
                }
                return true;
            });
        }

        async void Stop() {
            audioStream.OnBroadcast -= audioStream_OnBroadcast;
            await recorder.StopRecorder();
            //this.audioStream.Stop.Execute(this);
            Debug.WriteLine("Microphone recorder was stopped.");
        }
        private void audioStream_OnBroadcast(object sender, EventArgs<byte[]> e) {
            Debug.WriteLine("Microphone recorded {0} bytes.", e.Value.Length);
        }

        void Play() {
            DependencyService.Get<IRecordingService>().PlayAudio(FileName);
        }
    }
}
