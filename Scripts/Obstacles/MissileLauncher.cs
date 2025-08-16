using Godot;
using static Godot.GD;
using System;
using Timer = JetPackJoyride.Scripts.Components.Timer;

namespace JetPackJoyride.Scripts.Obstacles {
    public partial class MissileLauncher : Obstacle {
        [Export] public new bool Debug = false;
        [Export] public Vector2 MissileVelocity = new Vector2(-1500, 0);
        [Export] public float LauncherSpeed = 100;
        [Export] public float MissileSpawnRate = 1f;
        [Export] public int MissileScale = 4;
        [Export] public bool Enabled = false;

        private Timer _spawnTimer = null;
        private AudioStreamPlayer _launchSound;

        private float _top;
        private float _bottom;
        
        public override void _Ready() {
            _top = 0.0f;
            _bottom = GetViewport().GetVisibleRect().Size.Y;
            Velocity = new Vector2(0, LauncherSpeed);
            _spawnTimer = new Timer(0.0f, MissileSpawnRate, 0.0f, false);
            _launchSound = GetNode<AudioStreamPlayer>("LaunchSound");
        }

        public override void _Process(double delta) {
            if (!Enabled) {
                return;
            }
            
            if (OutOfBoundsY()) {
                Velocity = new Vector2(0, Velocity.Y * -1);
            }
            
            var shouldSpawn = _spawnTimer.UpdateTimer(delta);
            if (shouldSpawn) {
                SpawnMissile();
            }
            
            base._Process(delta);
        }

        private void SpawnMissile() {
            var missile = (Obstacle)ObstacleFactory.GetObstacle(ObstacleFactory.ObstacleType.Missile);
            missile.Velocity = MissileVelocity;
            missile.GlobalPosition = GlobalPosition;
            missile.Scale *= MissileScale;
            missile.SetRotation((float)Math.PI);
            GetTree().Root.AddChild(missile);
            _launchSound.Play();
        }
    }
}
