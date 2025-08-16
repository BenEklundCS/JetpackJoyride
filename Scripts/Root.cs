using System;
using Godot;
using JetPackJoyride.Scripts.UI;
using JetPackJoyride.Scripts.Components;
using JetPackJoyride.Scripts.Obstacles;
using static Godot.GD;

namespace JetPackJoyride.Scripts {
    public partial class Root : Node2D {
        // level saver
        private GameSaver _saver;
        private UserInterface _ui;
        private Controller _controller;
        private Player _player;
        private MissileLauncher _missileLauncher;
        // scorekeeping
        private double _timeAlive = 0.0f;
        [Export] public bool Debug = false;
        [Export] public int MinimumMissileLauncherScore = 1000;

        public override void _Ready() {
            // game saver
            _saver = GetNode<GameSaver>("GameSaver");
            // attempt to load save
            var loadedData = _saver.Load();
            _ui = GetNode<UserInterface>("UI");
            _ui.SetGameData(loadedData);
            _controller = GetNode<Controller>("Controller");
            _player = GetNode<Player>("Controller/Player");
            _missileLauncher = GetNode<Node2D>("ScrollingLevel").GetNode<MissileLauncher>("MissileLauncher");
        }

        public override void _Process(double delta) {
            _controller.GetInput(_player);
            // tick up
            _timeAlive += delta;
            // check for player death
            if (IsInstanceValid(_player)) {
                UpdateInterface();
                if (_ui.GetScore() > MinimumMissileLauncherScore) {
                    EnableMissileLauncher();
                }
            }
            else {
                GameOver();
            }
        }

        public override void _EnterTree() {
            if (Debug) {
                Print("Game Started");
            }
        }

        public override void _ExitTree() {
            if (Debug) {
                Print("Game Over");
            }
        }

        private void UpdateInterface() {
            _ui.UpdateUserInterface(_player, _timeAlive);
        }

        private void GameOver() {
            SaveGameData();
            GetTree().ChangeSceneToFile("res://Scenes/title_screen.tscn");
        }

        private void SaveGameData() {
            _saver.Save(_ui.GetGameData());
        }

        private void EnableMissileLauncher() {
            _missileLauncher.Enabled = true;
        }
    }
}