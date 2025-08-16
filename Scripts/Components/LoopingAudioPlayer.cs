using Godot;
using System;

namespace JetpackJoyride.Scripts.Components {
    public partial class LoopingAudioPlayer : Node {
        [Export] public AudioStreamPlayer AudioStreamPlayer;

        public override void _Ready() {
            AudioStreamPlayer.Finished += OnAudioStreamPlayerFinished;
        }

        private void OnAudioStreamPlayerFinished() {
            AudioStreamPlayer.Play();
        }
    }
}
