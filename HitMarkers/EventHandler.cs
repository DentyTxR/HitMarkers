using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using PlayerRoles;
using System;

namespace HitMarkers
{
    public class EventHandler
    {
        Config _config = HitMarkers.Singleton.Config;

        public void VerifiedEvent(VerifiedEventArgs ev)
        {
            if (ev.Player.DoNotTrack)
            {
                Log.Debug($"{ev.Player.UserId} has DNT enabled, Not adding to kill tracker");
            } else if (!ev.Player.DoNotTrack && !HitMarkers.Singleton.KillCount.ContainsKey(ev.Player))
            {
                HitMarkers.Singleton.KillCount.Add(ev.Player, 0);
            }
        }

        public void HurtingEvent(HurtingEventArgs ev)
        {
            if (ev.Attacker == null || ev.Player == null)
                return;
            if (!ev.IsAllowed || ev.Amount < 0 || ev.Attacker == ev.Player)
                return;
            if (ev.Attacker.Role.Team == Team.SCPs && _config.EnableScpHints == false)
                return;

            string attackerHintBuilder = _config.HintAttackerMessage.Replace("%TargetName%", ev.Player.Nickname)
                .Replace("%TargetRole%", ev.Player.Role.ToString())
                .Replace("%Damage%", Math.Round(ev.Amount).ToString()).Replace(@"\n", Environment.NewLine);

            string targetHintBuilder = _config.HintTargetMessage.Replace("%AttackerName%", ev.Attacker.Nickname)
                .Replace("%AttackerRole%", ev.Attacker.Role.ToString())
                .Replace("%Damage%", Math.Round(ev.Amount).ToString()).Replace(@"\n", Environment.NewLine);
            

            if (_config.EnableAttackerHint)
                ev.Attacker.ShowHint(attackerHintBuilder, _config.HintAttackerDuration);

            if (_config.EnableTargetHint)
                ev.Player.ShowHint(targetHintBuilder, _config.HintTargetDuration);

            if (_config.HitMarkerSize > 1)
                ev.Attacker.ShowHitMarker(_config.HitMarkerSize);
        }

        public void DyingEvent(DyingEventArgs ev)
        {
            if (ev.Attacker == null || ev.Player == null || ev.Attacker == ev.Player)
                return;

            if (HitMarkers.Singleton.KillCount.ContainsKey(ev.Attacker))
                HitMarkers.Singleton.KillCount[ev.Attacker]++;

            string killerHintStringBuilder = _config.KillHintMessage.Replace("%TargetName%", ev.Player.Nickname)
                .Replace("%TargetRole%", ev.Player.Role.ToString())
                .Replace(@"\n", Environment.NewLine);

            string scpkillHintStringBuilder = _config.KillHintMessage.Replace("%TargetName%", ev.Player.Nickname)
                .Replace("%TargetRole%", ev.Player.Role.ToString())
                .Replace(@"\n", Environment.NewLine);


            if (_config.EnableKillHint)
                ev.Attacker.ShowHint(killerHintStringBuilder, _config.KillHintDuration);

            if (_config.EnableKillHintForScp)
                ev.Attacker.ShowHint(scpkillHintStringBuilder, _config.ScpKillHintDuration);
        }

        public void RoundEnded(RoundEndedEventArgs ev)
        {
            HitMarkers.Singleton.KillCount.Clear();
            Log.Debug("Cleared kills");
        }
    }
}
