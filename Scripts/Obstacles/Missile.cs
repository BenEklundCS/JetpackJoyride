using Godot;
using System;
using JetPackJoyride.Scripts.Components;
using JetPackJoyride.Scripts.Obstacles;

namespace JetpackJoyride.Scripts.Obstacles {
    public partial class Missile : Obstacle {
        private KillBox _killBox;
        private AnimatedSprite2D _sprite;
        private AudioStreamPlayer _explosionSound;
        public override void _Ready() {
            _killBox =  GetNode<KillBox>("KillBox");
            _killBox.BodyEntered += OnBodyEntered;
            
            _sprite =  GetNode<AnimatedSprite2D>("AnimatedSprite2D");
            _sprite.Play();

            _explosionSound = GetNode<AudioStreamPlayer>("ExplosionSound");
            _explosionSound.Finished += OnFinished;
        }

        private void OnBodyEntered(Node2D body) {
            if (body.Name == _killBox.Target) {
                Explode();
            }
        }

        private void OnFinished() {
            QueueFree();
        }

        private void Explode() {
            _sprite.Animation = "explode";
            _explosionSound.Play();
        }
    }
}
