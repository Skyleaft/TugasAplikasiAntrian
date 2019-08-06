using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikasi_Antrian
{
    public class playr
    {
        private Queue<string> playlist;
        private IWavePlayer player;
        private WaveStream fileWaveStream;

        public playr(List<string> startingPlaylist)
        {
            playlist = new Queue<string>(startingPlaylist);
        }

        public void PlaySong()
        {
            if (playlist.Count < 1)
            {
                return;
            }

            if (player != null && player.PlaybackState != PlaybackState.Stopped)
            {
                player.Stop();
            }
            if (fileWaveStream != null)
            {
                fileWaveStream.Dispose();
            }
            if (player != null)
            {
                player.Dispose();
                player = null;
            }

            player = new WaveOutEvent();
            fileWaveStream = new AudioFileReader(playlist.Dequeue());
            player.Init(fileWaveStream);
            player.PlaybackStopped += (sender, evn) => { PlaySong(); };
            player.Play();
        }

    }

}
