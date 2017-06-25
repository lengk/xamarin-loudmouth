using System;
using System.Diagnostics;
using System.IO;
using Android.Content;
using Android.Media;
using LoudMouth.Droid.Services;
using LoudMouth.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AudioController))]
namespace LoudMouth.Droid.Services {
    public class AudioController : IAudioPlayer {
        Context context;

        MediaRecorder _recorder;
        MediaPlayer _player;

        string ROOTPATH = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public AudioController() {
            context = Android.App.Application.Context;
            _player = new MediaPlayer();
            _recorder = new MediaRecorder();
            Debug.WriteLine(context.FilesDir);
        }

        public void PlayAudio(string filename = Constants.DEFAULT_AUDIO_PATH) {
            var path = Path.Combine(ROOTPATH, filename);
            StopAudio();
            _player.SetDataSource(path);
            _player.Prepare();
            _player.Start();
        }

        public void StopAudio() {
            _player.Stop();
            _player.Reset();
        }

        public void OnDestroy() {
            if (_recorder != null) {
                _recorder.Release();
                _recorder.Dispose();
                _recorder = null;
            }
            if (_player != null) {
                _player.Release();
                _player.Dispose();
                _player = null;
            }
        }

        public void saveFile(string filename) {
            System.IO.Stream outputStream;
            try {
                outputStream = context.OpenFileOutput(filename, FileCreationMode.Private);
                outputStream.Close();
            } catch (Exception e) {
                Debug.WriteLine(e);
            }
        }
    }
}
