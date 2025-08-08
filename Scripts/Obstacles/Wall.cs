using Godot;
using static Godot.GD;
using System;

namespace JetPackJoyride.Scripts.Obstacles {
    public partial class Wall : Obstacle {
        private Node2D _pivot;
        private Sprite2D _sprite;
        [Export] public bool Flippable = true;

        public override void _Ready() {
            Print(nameof(Wall) + " spawned!");
            // find the pivot
            _pivot = GetNode<Node2D>("Pivot");

            var type = GetRandomType();
            if (type == Type.Bottom && Flippable) {
                SetRotation((float)Math.PI);
            }

            Velocity = new Vector2(-Globals.LevelMoveSpeed, 0);
        }
        
        private new void SetRotation(float rotation) {
            _pivot.Rotation = rotation;
        }
    }
}