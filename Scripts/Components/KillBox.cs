using Godot;
using static Godot.GD;
using System;

namespace JetPackJoyride.Scripts.Components {
    public partial class KillBox : Area2D {
        [Export] public int Damage = 1;
        [Export] public string Target = "Player";

        public override void _Ready() {
            BodyEntered += OnBodyEntered;
        }
        
        private void OnBodyEntered(Node2D node) {
            Print($"Body entered: {node.Name}");
            // detect player collisions to deal damage
            if (node.Name == Target) {
                ((Player)node).Hit(Damage);
            }
        }
    }
}

