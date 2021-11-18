using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SoftwareDesignExam {
    
    class SoundManager {
        private string _path;
        MediaPlayer mediaPlayer = new();
        MediaPlayer soundEffects = new();

        public SoundManager() {
            _path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\sounds\\";
        }

        private void PlayMusic(string file) {
            mediaPlayer.Open(new Uri(_path + file + ".wav"));
            mediaPlayer.Volume = 0.3;
            mediaPlayer.Play();
        }
        private void PlayEffect(string file) {
            soundEffects.Open(new Uri(_path + file + ".wav"));
            soundEffects.Volume = 0.3;
            soundEffects.Play();
        }

        public void MainMenuMusic() {
            PlayMusic(@"main_music");
        }
        public void RegisterPlayerMusic() {
            PlayMusic(@"register_players_music");
        }
        public void HighscoreMenuMusic() {
            PlayMusic(@"highscore_music");
        }
        public void GamepageMusic() {
            PlayMusic(@"gamepage_music");
        }
        public void WhistleEffect() {
            PlayEffect(@"whistle");
        }
    }
}
