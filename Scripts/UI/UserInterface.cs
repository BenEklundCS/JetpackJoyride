using Godot;
using System;
using System.Linq;

namespace JetPackJoyride.Scripts.UI {
    public partial class UserInterface : Godot.Control {
        [Export] public string ScoreFormat = "N0";
        [Export] public int ScorePerSecond = 100;
        
        private Label _health;
        private Label _gameOver;
        private Label _score;
        
        private const string HealthIcon = "❤️";
        public override void _Ready() {
            _health = GetNode<Label>("Health");
            _gameOver = GetNode<Label>("GameOver");
            _score = GetNode<Label>("Score");
        }

        public override void _Process(double delta) {   
            
        }

        public void UpdateUserInterface(Player player, double timeAlive) {
            _health.Text = HpToHearts(player.Hp);
            _score.Text = FormatScore(timeAlive * ScorePerSecond);
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

        private static string HpToHearts(int hp) {
            return string.Concat(Enumerable.Repeat(HealthIcon, hp));
        }

        private string FormatScore(double score) {
            return score.ToString(ScoreFormat);
        }
    }
}

