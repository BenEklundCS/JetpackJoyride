using Godot;
using static Godot.GD;
using System;

namespace JetPackJoyride.Scripts.Components {
    public partial class Controller : Node2D {
        /**
         * GetInput - Processes Game Input, Controlling a given Player object.
         * @param player - The player to handle input for
         * @returns void
         * This function internally validates if the passed player object is valid, to support the concept of
         * "Global Input" vs "Player Input", great for when the player dies and is no longer a valid node.
         */
        public void GetInput(Player player) {
            PlayerInput(player);
            GlobalInput();
        }

        private static void PlayerInput(Player player) {
            // player actions check for validity
            if (!IsInstanceValid(player)) {
                return;
            }
            var boosting = Input.IsActionPressed("boost");
            player.SetBoosting(boosting);   
        }

        private void Reload() {
            GetTree().ReloadCurrentScene();
        }

        private void Quit() {
            GetTree().Quit();
        }

        private void GlobalInput() {
            if (Input.IsActionJustPressed("reload")) CallDeferred(nameof(Reload));
            if (Input.IsActionJustPressed("quit")) CallDeferred(nameof(Quit));
        }
    }
}