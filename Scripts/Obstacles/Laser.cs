using Godot;
using static Godot.GD;

namespace JetPackJoyride.Scripts.Obstacles {
    public partial class Laser : Node2D {
        public override void _Ready() {
            Print(nameof(Laser) + " spawned!");
        }
    }
}