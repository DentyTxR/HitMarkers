using System;
using Exiled.API.Features;
using Player = Exiled.Events.Handlers.Player;

namespace HitMarkers
{

    public class HitMarkers : Plugin<Config>
    {

        private EventHandler EventHandler;
        public static HitMarkers Singleton;

        public override string Name { get; } = "HitMarkers";
        public override string Author { get; } = "Denty";
        public override string Prefix { get; } = "HitMarkers";
        public override Version Version { get; } = new Version(1, 1, 1);
        public override Version RequiredExiledVersion { get; } = new Version(6, 0, 0);


        public override void OnEnabled()
        {
            Singleton = this;
            EventHandler = new EventHandler();

            Player.Hurting += EventHandler.HurtingEvent;
            Player.Dying += EventHandler.DyingEvent;

            base.OnEnabled();
        }


        public override void OnDisabled()
        {
            Player.Hurting -= EventHandler.HurtingEvent;
            Player.Dying -= EventHandler.DyingEvent;

            EventHandler = null;
            Singleton = null;

            base.OnDisabled();
        }
    }
}
