﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SoftwareDesignExam.WPF;

namespace SoftwareDesignExam
{
    class GameLogic
    {
        #region Fields
        private MainWindow _mainWindow;
        private MenuPage _menuPage = new();
        private RegisterPlayerPage _registerPlayerPage = new();
        private ShowHighScorePage _showHighScorePage = new();
        private GamePage _gamePage = new();
        private Timer _timer = Timer.GetInstance();
        private ScoreDao _scoreDao = new();
        private PlayerManager _playerManager = new();
        private List<Key> _activePlayerKeys = new();
        private SoundManager _soundManager = new();
        #endregion

        #region Constructor
        public GameLogic()
        {
            _mainWindow = new MainWindow();

            _mainWindow.Show();

            SetupDelegateEvents();

            NavigateToMenuPage();
        }
        #endregion

        #region Methods
        private void ResetPlayers()
        {
            _playerManager.ResetPlayers();
            _registerPlayerPage.ClearActivePlayers();
            _mainWindow.HidePlayerBoxes();
        }

        private void GameLoop()
        {
            _playerManager.ResetScores();
            _mainWindow.DisplayPlayers(_playerManager.GetPlayerDictionary());
            _activePlayerKeys = _playerManager.GetPlayerKeyList();
            _gamePage.Start();
            Task.Run(() =>
            {
                // While there still are active players, the loop continues. Breaksgit show
                //
                //when no active players left, and game stops
                while (_activePlayerKeys.Count != 0)
                {
                    Thread.Sleep(10);
                }
                SoundManager effectSoundManager = new();
                effectSoundManager.WhistleEffect();
                _gamePage.Stop();
                SaveHighScores();
                _gamePage.DisplayWinner(_playerManager.GetWinner());
            });
        }

        // We are using 1 main window, and this window navigates to different pages
        private void NavigateToMenuPage()
        {
            _mainWindow.MainFrame.Navigate(_menuPage);
            _mainWindow.HidePlayerBoxes();
            _soundManager.MainMenuMusic();
        }

        private void NavigateToRegisterPlayerPage()
        {
            ResetPlayers();
            _registerPlayerPage.ClearActivePlayers();
            _mainWindow.MainFrame.Navigate(_registerPlayerPage);
            _soundManager.RegisterPlayerMusic();
        }

        private void NavigateToShowHighScorePage()
        {
	        _mainWindow.MainFrame.Navigate(_showHighScorePage);
            _soundManager.HighscoreMenuMusic();
        }

        private void NavigateToGamePage()
        {
            _mainWindow.MainFrame.Navigate(_gamePage);
            _soundManager.GamepageMusic();
        }

        private void NavigateToRegisterPlayersPageEventHandler(object sender, EventArgs e)
        {
            NavigateToRegisterPlayerPage();
        }
        
        //Button for MenuPage -> HighScorePage
        private void NavigateToShowHighScorePageEventHandler(object sender, EventArgs e)
        {
            NavigateToShowHighScorePage();
        }

        private void NavigateToMenuPageEventHandler(object sender, EventArgs e)
        {
            NavigateToMenuPage();
        }

        private void DisplayPlayersEventHandler(Object sender, PlayerEventArgs e)
        {
            _mainWindow.DisplayPlayers(_playerManager.GetPlayerDictionary());
        }

        //gets and sends high scores to page
        private void DisplayHighScoresEventHandler(Object sender, EventArgs e)
        {
            _showHighScorePage.DisplayHighScore(_scoreDao.GetHighScores());
        }

        private void AddPlayerEventHandler(Object sender, PlayerEventArgs e)
        {
            if (!_playerManager.IsKeyTaken(e.Key)) _playerManager.AddPlayer(e.Name, e.PlayerType, e.Key);
        }

        private void StartGameEventHandler(object sender, EventArgs e)
        {
            NavigateToGamePage();
            GameLoop();
        }

        private void PlayAgainEventHandler(object sender, EventArgs e)
        {
            GameLoop();
        }

        private void RegisterPlayerClickEventHandler(object sender, KeyEventArgs e)
        {
            if (!_activePlayerKeys.Contains(e.Key)) return;
            _playerManager.RegisterPlayerReactionTime(e.Key, _timer.GetTimeMs());
            _mainWindow.DisplayPlayers(_playerManager.GetPlayerDictionary());
            _activePlayerKeys.Remove(e.Key);
        }

        private void SetupDelegateEvents()
        {
            _registerPlayerPage.RegisterPlayerEvents += AddPlayerEventHandler;
            _registerPlayerPage.RegisterPlayerEvents += DisplayPlayersEventHandler;
            _registerPlayerPage.StartGameEvent += StartGameEventHandler;

            _gamePage.registeredPlayerClickEvent += RegisterPlayerClickEventHandler;
            _gamePage.playAgainClickEvent += PlayAgainEventHandler;
            _gamePage.showMenuClickEvent += NavigateToMenuPageEventHandler;

            _menuPage.StartNewGameEvent += NavigateToRegisterPlayersPageEventHandler;
            _menuPage.ShowHighScoreEvent += NavigateToShowHighScorePageEventHandler;

            _showHighScorePage.loadHighScoreEvent += DisplayHighScoresEventHandler;
            _showHighScorePage.showMenuClickEvent += NavigateToMenuPageEventHandler;
            //_showHighScorePage.delegatenavn += navn på event
        }
        private void SaveHighScores()
        {
            _scoreDao.SaveListOfPlayers(_playerManager.GetPlayerList());
        }
        #endregion
    }
}