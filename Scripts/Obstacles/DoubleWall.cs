using Godot;
using System;

namespace JetPackJoyride.Scripts.Obstacles {
    public partial class DoubleWall : Node2D {
        [Export] public int Min = -100;
        [Export] public int Max = 100;

        public override void _Ready() {
            var yPos = Globals.GlobalRandom.Next(Min, Max);
            Position = new Vector2(Position.X, yPos);
        }
    }
}
