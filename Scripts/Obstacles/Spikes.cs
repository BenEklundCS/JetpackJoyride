using Godot;
using static Godot.GD;
using System;

namespace JetPackJoyride.Scripts.Obstacles {
    public partial class Spikes : Obstacle {
        [Export] public new bool Debug = false;
        private Node2D _pivot;
        
        public override void _Ready() {
            if (Debug) {
                Print(nameof(Spikes) + " spawned!");
            }
            _pivot = GetNode<Node2D>("Pivot");

            var type = GetRandomType();
            if (type == Type.Bottom) {
                SetRotation((float)Math.PI);
            }

            Velocity = new Vector2(-(Globals.LevelMoveSpeed / 2), 0);
        }

        private new void SetRotation(float rotation) {
            _pivot.Rotation = rotation;
        }
    }
}
