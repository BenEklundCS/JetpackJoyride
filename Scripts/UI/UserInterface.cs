using Godot;
using static Godot.GD;
using System;
using System.Linq;

namespace JetPackJoyride.Scripts.UI {
    public partial class UserInterface : Godot.Control {
        [Export] public string ScoreFormat = "N0";
        [Export] public int ScorePerSecond = 100;
        
        private Label _health;
        private Label _gameOver;
        private Label _score;
        private double _s;
        private double _hs;
        private Label _highScore;
        
        private const string HealthIcon = "❤️";
        public override void _Ready() {
            _health = GetNode<Label>("Health");
            _gameOver = GetNode<Label>("GameOver");
            _score = GetNode<Label>("Score");
            _highScore = GetNode<Label>("HighScore");
            _s = 0;
            _hs = _s;
        }

        public override void _Process(double delta) {   
            
        }

        public void SetGameData(GameData gameData) {
            _hs = gameData.HighScore;
        }

        public GameData GetGameData() {
            GameData gameData = new GameData();
            gameData.HighScore = (int)_hs;
            return gameData;
        }

        public void UpdateUserInterface(Player player, double timeAlive) {
            _health.Text = HpToHearts(player.Hp);
            _s = timeAlive * ScorePerSecond;
            _hs = Math.Max(_s, _hs);
            _score.Text = FormatScore(timeAlive * ScorePerSecond);
            _highScore.Text = FormatScore(_hs);
        }

        public void SetGameOverVisible(bool visibility) {
            _gameOver.Visible = visibility;
        }
        
        public void SetHealthVisible(bool visible) {
            _health.Visible = visible;
        }
        
        public void SetScoreVisible(bool visible) {
            _score.Visible = visible;
        }

        public double GetScore() {
            return _s;
        }
        
        

        private static string HpToHearts(int hp) {
            return string.Concat(Enumerable.Repeat(HealthIcon, hp));
        }

        private string FormatScore(double score) {
            return score.ToString(ScoreFormat);
        }
    }
}

