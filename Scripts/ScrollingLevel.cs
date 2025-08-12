using Godot;
using System;
using Timer = JetPackJoyride.Scripts.Components.Timer;

namespace JetPackJoyride.Scripts {
    public partial class ScrollingLevel : Node2D {
    
        // nodes
        private Node2D _scrollPoint;
        private ObstacleFactory _obstacleFactory;
        // data
        private Vector2 _scrollPointOrigin;
        // timers
        private Timer _wallSpawnTimer;
        private Timer _spikeSpawnTimer;
        
        public override void _Ready() {
            // nodes
            _obstacleFactory = GetNode<ObstacleFactory>("ObstacleFactory");
            _scrollPoint = GetNode<Node2D>("ScrollPoint");
            // data
            _scrollPointOrigin = _scrollPoint.GlobalPosition;
            // timers
            _wallSpawnTimer = new Timer(0.0f, 1.0f, 3.0f, true);
            _spikeSpawnTimer = new Timer(0.0f, 1.0f, 2.0f, true);
        }

        public override void _Process(double delta) {
            // tick down
            var spawnWall = _wallSpawnTimer.UpdateTimer(delta);
            var spawnSpike = _spikeSpawnTimer.UpdateTimer(delta);
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
        }

        private void AddNodeFromFactory(Node2D node) {
            node.GlobalPosition = _obstacleFactory.GlobalPosition;
            node.ZIndex = 100;
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

