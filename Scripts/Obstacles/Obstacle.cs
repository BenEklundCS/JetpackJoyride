using Godot;
using static Godot.GD;
using System;

namespace JetPackJoyride.Scripts.Obstacles {
    public abstract partial class Obstacle : CharacterBody2D {
        
        [Export] public int DeleteOffset = 1000;
        [Export] public bool Debug = false;
        protected enum Type {
            Top,
            Bottom
        }

        public override void _Process(double delta) {
            MoveAndSlide();
            CheckDeath();
        }
        
        protected void CheckDeath() {
            if (GlobalPosition.X < -(DeleteOffset)) {
                if (Debug) {
                    Print("Deleting object.");
                }
                QueueFree();
            }
        }
        
        protected static Type GetRandomType() {
            var top = Globals.GlobalRandom.Next(0, 2);
            return (top == 0) ? Type.Bottom : Type.Top;
        }
    }
}
