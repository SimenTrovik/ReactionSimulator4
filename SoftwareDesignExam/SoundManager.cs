using System;
using System.IO;
using System.Reflection;
using System.Windows.Media;

namespace SoftwareDesignExam {
    internal class SoundManager {
        #region Fields
        private readonly string _path;
        private readonly MediaPlayer _mediaPlayer = new();
        private readonly MediaPlayer _soundEffects = new();
        #endregion

        #region Constructor
        public SoundManager() {
            _path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Sounds\\";
        }
        #endregion

        #region Methods
        private void PlayMusic(string file) {
            _mediaPlayer.Open(new Uri(_path + file + ".wav"));
            _mediaPlayer.Volume = 0.3;
            _mediaPlayer.Play();
        }
        private void PlayEffect(string file) {
            _soundEffects.Open(new Uri(_path + file + ".wav"));
            _soundEffects.Volume = 0.3;
            _soundEffects.SpeedRatio = 2;
            _soundEffects.Play();
        }

        public void MainMenuMusic() {
            PlayMusic(@"main_music");
        }
        public void RegisterPlayerMusic() {
            PlayMusic(@"register_players_music");
        }
        public void HighScoreMenuMusic() {
            PlayMusic(@"highscore_music");
        }
        public void GamePageMusic() {
            PlayMusic(@"gamepage_music");
        }
        public void WhistleEffect() {
            PlayEffect(@"whistle");
        }
        #endregion
    }
}