using System;
using System.Diagnostics;
using Android.Media;
using LoudMouth.Droid.Services;
using LoudMouth.Services;

[assembly: Xamarin.Forms.Dependency(typeof(Recorder))]
namespace LoudMouth.Droid.Services {
    public class Recorder : IRecordingService {
        MediaRecorder _recorder;
        MediaPlayer _player;
        string path = "/test.3gpp";

        public Recorder() {
            _player = new MediaPlayer();
            _recorder = new MediaRecorder();
        }

        public void StartRecording() {
            try {
                _recorder.SetAudioSource(AudioSource.Mic);
                _recorder.SetOutputFormat(OutputFormat.ThreeGpp);
                _recorder.SetAudioEncoder(AudioEncoder.Default);
                _recorder.SetOutputFile(path);
                _recorder.Prepare();
                _recorder.Start();
            } catch (Exception e) {
                Debug.WriteLine(e);
            }
        }

        public void StopRecording() {
            _recorder.Stop();
            _recorder.Reset();
            _player.SetDataSource(path);
            _player.Prepare();
            _player.Start();
        }

        public void PlayAudio(string path) {
            
        }

        public void OnDestroy() {
            _player.Release();
            _recorder.Release();
            _player.Dispose();
            _recorder.Dispose();
            _player = null;
            _recorder = null;
        }
    }
}
