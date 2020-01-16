using LibVLCSharp.Shared;
using System.ComponentModel;

namespace VLCSample
{
    public class OnlyVideoViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        private MediaPlayer _mediaPlayer;
        /// <summary>
        /// Gets the <see cref="LibVLCSharp.Shared.MediaPlayer"/> instance.
        /// </summary>
        public MediaPlayer MediaPlayer
        {
            get => _mediaPlayer;
            set => Set(nameof(MediaPlayer), ref _mediaPlayer, value);
        }

        public MediaPlayer PreviousMediaPlayer { get; set; }

        public void OnAppearing()
        {
            //With Prism, we can pass the time, position and Media in the OnNavigatedTo method

            MediaPlayer = new MediaPlayer(PreviousMediaPlayer.Media) { EnableHardwareDecoding = true, AspectRatio = null, Scale = 2, Fullscreen = true }; 

            if (!MediaPlayer.IsPlaying)
                MediaPlayer.Play();

            MediaPlayer.Time = PreviousMediaPlayer.Time;
            MediaPlayer.Position = PreviousMediaPlayer.Position;
        }

        public void OnDisappearing()
        {
            if (MediaPlayer.IsPlaying)
            {
                MediaPlayer.Stop();
            }
        }


        private void Set<T>(string propertyName, ref T field, T value)
        {
            if (field == null && value != null || field != null && !field.Equals(value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
