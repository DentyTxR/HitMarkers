using System;
using System.Collections.Generic;
using Exiled.API.Features;
using PlayerHandler = Exiled.Events.Handlers.Player;
using ServerHandler = Exiled.Events.Handlers.Server;
using Player = Exiled.API.Features.Player;
namespace HitMarkers
{

    public class HitMarkers : Plugin<Config>
    {

        private EventHandler EventHandler;
        public static HitMarkers Singleton;

        public override string Name { get; } = "HitMarkers";
        public override string Author { get; } = "Denty";
        public override string Prefix { get; } = "HitMarkers";
        public override Version Version { get; } = new Version(1, 2, 0);
        public override Version RequiredExiledVersion { get; } = new Version(8, 0, 0);

        public Dictionary<Player, int> KillCount = new Dictionary<Player, int>();

        public override void OnEnabled()
        {
            Singleton = this;
            EventHandler = new EventHandler();

            PlayerHandler.Verified += EventHandler.VerifiedEvent;
            PlayerHandler.Hurting += EventHandler.HurtingEvent;
            PlayerHandler.Dying += EventHandler.DyingEvent;
            ServerHandler.RoundEnded += EventHandler.RoundEnded;
            
            base.OnEnabled();
        }


        public override void OnDisabled()
        {
            PlayerHandler.Verified -= EventHandler.VerifiedEvent;
            PlayerHandler.Hurting -= EventHandler.HurtingEvent;
            PlayerHandler.Dying -= EventHandler.DyingEvent;
            ServerHandler.RoundEnded -= EventHandler.RoundEnded;

            EventHandler = null;
            Singleton = null;

            base.OnDisabled();
        }
    }
}
