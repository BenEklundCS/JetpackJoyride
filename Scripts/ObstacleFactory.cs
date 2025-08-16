using System;
using System.Linq;
using Godot;
using static Godot.GD;

namespace JetPackJoyride.Scripts {
    public partial class ObstacleFactory : Node2D {
        [Export] public bool Debug = false;
        
        public enum ObstacleType {
            Wall,
            DoubleWall,
            Spikes,
            Missile,
        }

        public override void _Ready() {
            if (Debug) {
                Print(nameof(ObstacleFactory) + " spawned!");
            }
        }

        public static Node GetRandomWall() {
            ObstacleType[] enums = { ObstacleType.Wall, ObstacleType.DoubleWall }; //Enum.GetValues(typeof(ObstacleType)).Cast<ObstacleType>().ToList();
            var myEnum = enums[Globals.GlobalRandom.Next(0, enums.Length)];
            return GetObstacle(myEnum);
        }

        public static Node GetObstacle(ObstacleType type) {
            return type switch {
                ObstacleType.Wall => Load<PackedScene>("res://Scenes/Obstacles/wall.tscn").Instantiate(),
                ObstacleType.DoubleWall => Load<PackedScene>("res://Scenes/Obstacles/double_wall.tscn").Instantiate(),
                ObstacleType.Spikes => Load<PackedScene>("res://Scenes/Obstacles/spikes.tscn").Instantiate(),
                ObstacleType.Missile => Load<PackedScene>("res://Scenes/Obstacles/missile.tscn").Instantiate(),
                _ => null
            };
        }
    }
}