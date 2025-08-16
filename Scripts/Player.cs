using Godot;
using static Godot.GD;
using Timer = JetPackJoyride.Scripts.Components.Timer;

namespace JetPackJoyride.Scripts {
    public partial class Player : CharacterBody2D {
        // nodes
        private AnimatedSprite2D _animatedSprite2D;
        private GpuParticles2D _particles;
        private AudioStreamPlayer _hitSound;
        private bool _boosting = false;
        // hit handling
        private Timer _flashRedTimer;
        private bool _hit;
        private int _flashedTimes = 0;

        // public data
        [Export] public int BoostStrength = 30;
        [Export] public int Hp = 3;
        [Export] public int FlashTimes = 6;
        [Export] public float FlashRate = 0.25f; // test
        [Export] public bool Debug = false;

        // called on scene init
        public override void _Ready() {
            if (Debug) {
                Print(nameof(Player) + " spawned!");
            }
            _animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
            _particles = GetNode<GpuParticles2D>("GPUParticles2D");
            _hitSound = GetNode<AudioStreamPlayer>("HitSound");
            _flashRedTimer = new Timer(0.0f, FlashRate, 0.0f, false);
        }

        // called once per frame
        public override void _Process(double delta) {
            // boost if boosting
            if (_boosting) {
                Boost();
            }
            // move player
            Move(delta);
            // flash
            if (_hit) {
                FlashRed(delta);
            }
            // animate
            Animate();
            // particles
            Particles();
            
        }

        public void SetBoosting(bool boosting) {
            _boosting = boosting;
        }
        
        public void Hit(int damage) {
            if (Debug) {
                Print($"I've been hit! {damage}");
            }
            Hp -= damage;
            _hit = true;
            _hitSound.Play();
            if (Hp <= 0) {
                Kill();
            }
        }

        private void Move(double delta) {
            Velocity += Vector2.Down * Globals.Gravity * (float)delta;
            MoveAndSlide();
        }

        private void FlashRed(double delta) {
            var shouldFlash = _flashRedTimer.UpdateTimer(delta);
            if (!shouldFlash) {
                return;
            }

            _flashedTimes++;
            if (_flashedTimes > FlashTimes) {
                ResetHit();
            }
            
            var color = (_flashedTimes % 2 != 0) 
                ? Color.Color8(255, 0, 0) 
                : Color.Color8(255, 255, 255);
            _animatedSprite2D.SetModulate(color);
        }
        
        private void ResetHit() {
            _hit =  false;
            _flashedTimes = 0;
            _animatedSprite2D.Modulate = Color.Color8(255, 255, 255);
        }

        private void Kill() {
            QueueFree();
        }

        private void Animate() {
            if (!IsOnFloor()) {
                if (Velocity.Y > 0 && _animatedSprite2D.Animation != "moving_down") {
                    _animatedSprite2D.Play("moving_down");
                }
                else if (Velocity.Y < 0 && _animatedSprite2D.Animation != "moving_up") {
                    _animatedSprite2D.Play("moving_up");
                }
            }
            else {
                _animatedSprite2D.Play("idle");
            }
        }

        private void Particles() {
            _particles.Emitting = _boosting;
        }

        // player methods
        private void Boost() {
            Velocity = new Vector2(Velocity.X, Velocity.Y - BoostStrength);
        }

        private void ResetVelocity() {
            Velocity = Vector2.Zero;
        }
    }
}