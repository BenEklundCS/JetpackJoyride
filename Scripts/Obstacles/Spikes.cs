using Godot;
using static Godot.GD;
using System;

namespace JetPackJoyride.Scripts.Obstacles {
    public partial class Spikes : Obstacle {
        [Export] public int MaxSpikeCount = 5;
        [Export] public int MinSpikeCount = 1;
        
        private int _spikeCount = 1;

        private Node2D _pivot;
        
        public override void _Ready() {
            Print(nameof(Spikes) + " spawned!");
            _pivot = GetNode<Node2D>("Pivot");

            var type = GetRandomType();
            if (type == Type.Bottom) {
                SetRotation((float)Math.PI);
            }

            Velocity = new Vector2(-(Globals.LevelMoveSpeed / 2), 0);
            _spikeCount = GetRandomSpikeCount();
            
            
        }

        private int GetRandomSpikeCount() {
            return Globals.GlobalRandom.Next(MinSpikeCount, MaxSpikeCount);
        }

        private new void SetRotation(float rotation) {
            _pivot.Rotation = rotation;
        }
    }
}
