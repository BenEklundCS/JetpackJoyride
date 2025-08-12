using Godot;
using static Godot.GD;

namespace JetPackJoyride.Scripts.Obstacles {
    public partial class Laser : Node2D {
        [Export] public bool Debug = false;
        public override void _Ready() {
            if (Debug) {
                Print(nameof(Laser) + " spawned!");
            }
        }
    }
}