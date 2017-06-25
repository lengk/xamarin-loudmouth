

using System.Diagnostics;
using System.Linq;
using LoudMouth.Services;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Mvvm;
using XLabs.Platform.Services.Media;

namespace LoudMouth.Controllers {
    public class AudioProcessor {
        /// <summary>
        /// The file name
        /// </summary>
        private string _fileName;
        /// <summary>
        /// The sample rate
        /// </summary>
        private int _sampleRate;
        /// <summary>
        // Indicates if it is recording
        /// </summary>
        private bool _isRecording;
        /// <summary>
        /// The audio stream
        /// </summary>
        private readonly IAudioStream _audioStream;
        /// <summary>
        /// The recorder
        /// </summary>
        private readonly WaveRecorder _recorder;

        /// <summary>
        /// Initializes a new instance of the <see cref="WaveRecorderViewModel"/> class.
        /// </summary>
        public AudioProcessor(string file) {
            SampleRate = 16000;

            var app = Resolver.Resolve<IXFormsApp>();

            var device = Resolver.Resolve<IDevice>();

            if (device != null) {
                _audioStream = device.Microphone;
                _recorder = new WaveRecorder();
            }
            var fileStream = DependencyService.Get<IFileLoader>().LoadWriteStream(file);

            Record = new Command(
                () => {
                    
                    _audioStream.OnBroadcast += audioStream_OnBroadcast;
                    //this.audioStream.Start.Execute(this.SampleRate);
                    _recorder.StartRecorder(
                        _audioStream,
                        fileStream, 
                        SampleRate).ContinueWith(t => {
                            if (t.IsCompleted) {
                                IsRecording = t.Result;
                                System.Diagnostics.Debug.WriteLine("Microphone recorder {0}.", IsRecording ? "was started" : "failed to start.");
                                Record.ChangeCanExecute();
                                Stop.ChangeCanExecute();
                            } else if (t.IsFaulted) {
                                _audioStream.OnBroadcast -= audioStream_OnBroadcast;
                            }
                        });
                },
                () => RecordingEnabled &&
                _audioStream.SupportedSampleRates.Contains(SampleRate) &&
                    !IsRecording &&
                    device.FileManager != null
                );

            Stop = new Command(
                async () => {
                    _audioStream.OnBroadcast -= audioStream_OnBroadcast;
                    await _recorder.StopRecorder();
                    //this.audioStream.Stop.Execute(this);
                    Debug.WriteLine("Microphone recorder was stopped.");
                    Record.ChangeCanExecute();

                    Stop.ChangeCanExecute();
                },
                () => {
                    return IsRecording;
                }
                );
        }

        /// <summary>
        /// Audioes the stream_ on broadcast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void audioStream_OnBroadcast(object sender, XLabs.EventArgs<byte[]> e) {
            Debug.WriteLine("Microphone recorded {0} bytes.", e.Value.Length);
        }

        /// <summary>
        /// Gets a value indicating whether [recording enabled].
        /// </summary>
        /// <value><c>true</c> if [recording enabled]; otherwise, <c>false</c>.</value>
        public bool RecordingEnabled {
            get {
                return (_audioStream != null);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is recording.
        /// </summary>
        /// <value><c>true</c> if this instance is recording; otherwise, <c>false</c>.</value>
        public bool IsRecording {
            get { return _isRecording; }
            set { _isRecording = value; }
        }

        /// <summary>
        /// Gets or sets the sample rate.
        /// </summary>
        /// <value>The sample rate.</value>
        public int SampleRate {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string  FileName {
            get;
            set;
        }

        /// <summary>
        /// Gets the record.
        /// </summary>
        /// <value>The record.</value>
        public Command Record {
            get;
            private set;
        }

        /// <summary>
        /// Gets the stop.
        /// </summary>
        /// <value>The stop.</value>
        public Command Stop {
            get;
            private set;
        }
    }
}
