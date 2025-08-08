using System;
using Godot;
using JetPackJoyride.Scripts.UI;
using JetPackJoyride.Scripts.Components;
using static Godot.GD;
using Timer = JetPackJoyride.Scripts.Components.Timer;

namespace JetPackJoyride.Scripts {
    public partial class Root : Node2D {
        // nodes
        private Node2D _scrollPoint;
        private ObstacleFactory _obstacleFactory;
        private UserInterface _ui;
        private Controller _controller;
        private Player _player;
        // timers
        private Timer _wallSpawnTimer;
        private Timer _spikeSpawnTimer;
        // data
        private Vector2 _scrollPointOrigin;
        // scorekeeping
        private double _timeAlive = 0.0f;

        public override void _Ready() {
            // nodes
            _obstacleFactory = GetNode<ObstacleFactory>("ObstacleFactory");
            _scrollPoint = GetNode<Node2D>("ScrollPoint");
            _ui = GetNode<UserInterface>("UI");
            _controller = GetNode<Controller>("Controller");
            _player = GetNode<Player>("Controller/Player");
            
            // timers
            _wallSpawnTimer = new Timer(0.0f, 1.0f, 3.0f, true);
            _spikeSpawnTimer = new Timer(0.0f, 1.0f, 2.0f, true);
            
            // data
            _scrollPointOrigin = _scrollPoint.GlobalPosition;
        }

        public override void _Process(double delta) {
            _controller.GetInput(_player);
            // tick down
            var spawnWall = _wallSpawnTimer.UpdateTimer(delta);
            var spawnSpike = _spikeSpawnTimer.UpdateTimer(delta);
            // tick up
            _timeAlive += delta;
            // spawners
            if (spawnWall) {
                var wall = (Node2D)ObstacleFactory.GetRandomWall();
                AddNodeFromFactory(wall);
            }
            if (spawnSpike) {
                var spikes = (Node2D)ObstacleFactory.GetObstacle(ObstacleFactory.ObstacleType.Spikes);
                AddNodeFromFactory(spikes);
            }
            // continuously scroll the level sideways
            ScrollLevel(delta);
            // check for player death
            if (IsInstanceValid(_player)) {
                UpdateInterface();
            }
            else {
                GameOver();
            }
        }

        public override void _EnterTree() {
            Print("Game Started");
        }

        public override void _ExitTree() { 
            Print("Game Over");
        }

        private void UpdateInterface() {
            _ui.UpdateUserInterface(_player, _timeAlive);
        }

        private void GameOver() {
            _ui.SetHealthVisible(false);
            _ui.SetGameOverVisible(true);
        }

        private void AddNodeFromFactory(Node2D node) {
            node.GlobalPosition = _obstacleFactory.GlobalPosition;
            AddChild(node);
        }

        private void ScrollLevel(double delta) {
            var x = _scrollPoint.GlobalPosition.X - (float)((Globals.LevelMoveSpeed / 2) * delta);
            var y = _scrollPoint.GlobalPosition.Y;
            _scrollPoint.GlobalPosition = new Vector2(x, y);
            if (_scrollPoint.GlobalPosition.X <= GetViewport().GetVisibleRect().Size.Y) {
                _scrollPoint.GlobalPosition = _scrollPointOrigin;
            }
        }
    }
}