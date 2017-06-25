using System;
using System.Diagnostics;
using Android.Media;
using Android.App;
using Android.Content;
using LoudMouth.Droid.Services;
using LoudMouth.Services;
using LoudMouth;
using Xamarin.Forms;
using System.IO;

[assembly: Dependency(typeof(AudioController))]
namespace LoudMouth.Droid.Services {
    public class AudioController : IRecordingService {
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

        public string StartRecording(string filename = Constants.DEFAULT_AUDIO_PATH, int seconds = 60 * 5) {
            Debug.WriteLine("Started Recording for " + filename);
            StopAudio();
            var filePath = Path.Combine(ROOTPATH, filename);
            try {
                _recorder.SetAudioSource(AudioSource.Mic);
                _recorder.SetOutputFormat(OutputFormat.Default);
                _recorder.SetAudioEncoder(AudioEncoder.AmrWb);
                _recorder.SetOutputFile(filePath);
                _recorder.Prepare();
                _recorder.Start();

                return filePath;
            } catch (Exception e) {
                Debug.WriteLine(e);
            }
            return null;
        }

        public void StopRecording() {
            _recorder.Stop();
            _recorder.Reset();
            Debug.WriteLine("Stopped Recording");
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
