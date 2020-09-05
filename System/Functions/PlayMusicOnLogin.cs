using Server;
using Server.Network;

namespace Felladrin.Automations
{
    public static class PlayMusicOnLogin
    {
        public static class Config
        {
            public static bool Enabled = true;                          // Is this system enabled?
            public static bool PlayRandomMusic = true;                  // Should we play a random music from the list?
            public static MusicName SingleMusic = MusicName.Odyssey;    // Music to be played if PlayRandomMusic = false.
        }

        public static void Initialize()
        {
            if (Config.Enabled)
                EventSink.Login += OnLogin;
        }

        static void OnLogin(LoginEventArgs args)
        {
            MusicName toPlay = Config.SingleMusic;

            if (Config.PlayRandomMusic)
                toPlay = MusicList[Utility.Random(MusicList.Length)];

            args.Mobile.Send(PlayMusic.GetInstance(toPlay));
        }
        
        public static MusicName[] MusicList = {
            MusicName.Traveling,
            MusicName.Explore,
            MusicName.Adventure,
            MusicName.Searching,
            MusicName.Scouting,
            MusicName.Wrong,
            MusicName.Hunting,
            MusicName.Seeking,
            MusicName.Despise,
            MusicName.Wandering,
            MusicName.Odyssey,
            MusicName.Expedition,
            MusicName.Roaming
        };
    }
}