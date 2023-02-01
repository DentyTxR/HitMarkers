using Exiled.Events.EventArgs.Player;
using System;

namespace HitMarkers
{
    public class EventHandler
    {
        Config _config = HitMarkers.Singleton.Config;

        public void HurtingEvent(HurtingEventArgs ev)
        {
            string attackerHintBuilder = _config.HintAttackerMessage.Replace("%TargetName%", ev.Player.Nickname).Replace("%TargetRole%", ev.Player.Role.ToString()).Replace("%Damage%", ev.Amount.ToString()).Replace(@"\n", Environment.NewLine);
            string targetHintBuilder = _config.HintTargetMessage.Replace("%AttackerName%", ev.Attacker.Nickname).Replace("%AttackerRole%", ev.Attacker.Role.ToString()).Replace("%Damage%", ev.Amount.ToString()).Replace(@"\n", Environment.NewLine);

            if (ev.Player == null || ev.Player.IsDead || ev.Player == ev.Attacker || ev.Amount < 0)
                return;

            if (_config.EnableAttackerHint)
                ev.Attacker.ShowHint(attackerHintBuilder, _config.HintAttackerDuration);

            if(_config.EnableTargetHint)
                ev.Player.ShowHint(targetHintBuilder, _config.HintTargetDuration);

            if (_config.HitMarkerSize > 1)
                ev.Attacker.ShowHitMarker(_config.HitMarkerSize);
        }
    }
}
