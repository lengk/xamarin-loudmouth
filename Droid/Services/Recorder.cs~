using System;
using Android.Media;
using LoudMouth.Droid.Services;
using LoudMouth.Services;

[assembly: Xamarin.Forms.Dependency(typeof(Recorder))]
namespace LoudMouth.Droid.Services {
    public class Recorder : IRecordingService{
        MediaRecorder _recorder;
        MediaPlayer _player;
        string path = "/test.3gpp";

        public Recorder() {
        }

        public void StartRecording() {
            _recorder.SetAudioSource(AudioSource.Mic);
            _recorder.SetOutputFormat(OutputFormat.ThreeGpp);
            _recorder.SetAudioEncoder(AudioEncoder.Default);
            _recorder.SetOutputFile(path);
            _recorder.Prepare();
            _recorder.Start();
        }

        public void StopRecording() {
            _recorder.Stop();
            _recorder.Reset();

            _player.SetDataSource(path);
            _player.Prepare();
            _player.Start();
        }

        public void PlayAudio(string path){
           
        }
    }
}
