using Exiled.API.Interfaces;
using System.ComponentModel;

namespace HitMarkers
{
    public class Config : IConfig
    {
        [Description("Whether or not the plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Should debug logs be enabled?")]
        public bool Debug { get; set; } = false;

        [Description("Should a hint be displayed to the attacker?")]
        public bool EnableAttackerHint { get; set; } = true;

        [Description("Hint message for attacker")]
        public string HintAttackerMessage { get; set; } = @"\n\n<size=20>%Damage% <color=red>damage</color></size>";

        [Description("Hint duration for attacker")]
        public float HintAttackerDuration { get; set; } = 1f;

        [Description("Should a hint be displayed to the target?")]
        public bool EnableTargetHint { get; set; } = false;

        [Description("Hint message for target")]
        public string HintTargetMessage { get; set; } = @"\n\n<size=20>you got hit by %AttackerName%</size>";

        [Description("Hint duration for target")]
        public float HintTargetDuration { get; set; } = 1f;

        [Description("Size of custom hitmarker (leave 1 for default)")]
        public float HitMarkerSize { get; set; } = 1f;
    }
}
