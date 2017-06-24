using System;
using System.Diagnostics;
using Android.Media;
using Android.App;
using Android.Content;
using LoudMouth.Droid.Services;
using LoudMouth.Services;
using LoudMouth;
using Xamarin.Forms;

[assembly: Dependency(typeof(Recorder))]
namespace LoudMouth.Droid.Services {
    public class Recorder : IRecordingService {
        Context context;

        MediaRecorder _recorder;
        MediaPlayer _player;

        public Recorder() {
            context = Android.App.Application.Context;
            _player = new MediaPlayer();
            _recorder = new MediaRecorder();
            Debug.WriteLine(context.FilesDir
                           );
        }

        string filePath;

        public string StartRecording(string filename = Constants.DEFAULT_AUDIO_PATH, int seconds = 60*5) {
            Debug.WriteLine("Started Recording for " + filename);
            StopAudio();

            saveFile(filename);
            filePath = context.FilesDir.AbsolutePath + filename;
            try {
                _recorder.SetAudioSource(AudioSource.Mic);
                _recorder.SetOutputFormat(OutputFormat.ThreeGpp);
                _recorder.SetAudioEncoder(AudioEncoder.Default);
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
            Debug.WriteLine("Stopped Recording");
            _recorder.Stop();
            _recorder.Reset();
        }

        public void PlayAudio(string filename = Constants.DEFAULT_AUDIO_PATH) {
            var path = context.FilesDir.AbsolutePath + filename;
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
            _player.Release();
            _recorder.Release();
            _player.Dispose();
            _recorder.Dispose();
            _player = null;
            _recorder = null;
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
