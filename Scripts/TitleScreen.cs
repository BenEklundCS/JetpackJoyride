using System.Runtime.InteropServices.JavaScript;
using Godot;
using JetPackJoyride.Scripts;
using static Godot.GD;

namespace JetpackJoyride.Scripts {
    public partial class TitleScreen : Control {
        public enum Display {
            Title,
            GameOver,
        }
        
        [Export] public string ScoreFormat = "N0";
        
        private GameSaver _saver; 
        
        private Label _title;
        private Label _highScore;
        private Button _start;
        private Button _reset;
        private Button _quit;

        private Display _display;
        private static int _displayOpenedTimes = 0;

        public override void _Ready() {
            _saver = GetNode<GameSaver>("GameSaver");
            _highScore = GetNode<Label>("HighScore");
            
            _title = GetNode<Label>("Title");
            _start = GetNode<Button>("ButtonBar/Start");
            _reset = GetNode<Button>("ButtonBar/Reset");
            _quit = GetNode<Button>("ButtonBar/Quit");

            SetDisplay();
            SetGameText();

            _start.Pressed += OnStartPressed;
            _reset.Pressed += OnResetPressed;
            _quit.Pressed += OnQuitPressed;
        }

        private void SetDisplay() {
            _displayOpenedTimes++;
            _display = _displayOpenedTimes <= 1 ? Display.Title : Display.GameOver;
        }
        
        private void SetGameText() {
            if (_display == Display.Title) {
                _title.Text = "Jetpack Joyride";
                _highScore.SetVisible(false);
                _start.Text = "START";
                _reset.Text = "RESET";
                _quit.Text = "QUIT";
            }
            else {
                _title.Text = "Game Over\n";
                _start.Text = "RESTART";
                _highScore.Text = FormatScore(_saver.Load().HighScore);
                _highScore.SetVisible(true);
            }
        }
        
        private void OnStartPressed() {
            GetTree().ChangeSceneToFile("res://Scenes/root.tscn");
        }

        private void OnResetPressed() {
            _saver.Clear();
        }

        private void OnQuitPressed() {
            GetTree().Quit();
        }
        
        private string FormatScore(double score) {
            return score.ToString(ScoreFormat);
        }
    }
}
