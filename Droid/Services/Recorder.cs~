using System;
using System.Diagnostics;
using Android.Media;
using LoudMouth.Droid.Services;
using LoudMouth.Services;
using LoudMouth;

[assembly: Xamarin.Forms.Dependency(typeof(Recorder))]
namespace LoudMouth.Droid.Services {
    public class Recorder : IRecordingService {
        MediaRecorder _recorder;
        MediaPlayer _player;

        public Recorder() {
            _player = new MediaPlayer();
            _recorder = new MediaRecorder();
        }

        public void StartRecording(string path = Constants.DEFAULT_AUDIO_PATH) {
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
            PlayAudio();
        }

        public void PlayAudio(string path = Constants.DEFAULT_AUDIO_PATH) {
            _player.SetDataSource(path);
            _player.Prepare();
            _player.Start();
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
