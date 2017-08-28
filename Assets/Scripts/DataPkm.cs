using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPkm
{
    public enum GameVersion
    {
        UNKNOW = -1,
        RUBY_SAPPHIRE_EMERALD = 0,
        FIRERED_LEAFGREEN,
        DIAMOND_PEARL_PLATINUM,
        HEARTGOLD_SOULSILVER,
        BLACK_WHITE,
        BLACK_WHITE_2,
        X_Y,
        OMEGARUBY_ALPHASAPPHIRE,
        SUN_MOON,
        ULTRASUN_ULTRAMOON,
    }

    public static readonly Dictionary<GameVersion, string> GameVersionStringKey
    = new Dictionary<GameVersion, string>
    {
        { GameVersion.UNKNOW, "UNKNOW" },
        { GameVersion.RUBY_SAPPHIRE_EMERALD, "Ruby/Sapphire/Emerald" },
        { GameVersion.FIRERED_LEAFGREEN, "FireRed/LeafGreen" },
        { GameVersion.DIAMOND_PEARL_PLATINUM, "Diamond/Pearl/Platinum" },
        { GameVersion.HEARTGOLD_SOULSILVER, "HeartGold/SoulSilver" },
        { GameVersion.BLACK_WHITE, "Black/White" },
        { GameVersion.BLACK_WHITE_2, "Black2/White2" },
        { GameVersion.X_Y, "X/Y" },
        { GameVersion.OMEGARUBY_ALPHASAPPHIRE, "RubyOmega/SapphireAlpha" },
        { GameVersion.SUN_MOON, "Sun/Moon" },
        { GameVersion.ULTRASUN_ULTRAMOON, "UltraSun/UltraMoon" }
    };

    public static string GetGameVersionString(GameVersion e)
    {
        if (GameVersionStringKey.ContainsKey(e))
            return GameVersionStringKey[e];
        return "UNKNOW-GAME";
    }

    public enum HuntingMode
    {
        UNKNOW = -1,
        WILD = 0, 
        BREEDING,
        BREEDING_MASUDA, // SPEC WITH CHROMA
        POKERADAR, // SPEC
        HORDE, // SPEC
        FRIEND_SAFARI, // SPEC // NO CHROMA
        CHAIN_FISHING, // SPEC
        DEXNAV, // VERY SPEC
        SOS_CALLS // VERY SPEC
    }

    public static readonly Dictionary<HuntingMode, string> HuntingModeStringKey
    = new Dictionary<HuntingMode, string>
    {
        { HuntingMode.UNKNOW, "UNKNOW" },
        { HuntingMode.WILD, "Wild" },
        { HuntingMode.BREEDING, "Breeding" },
        { HuntingMode.BREEDING_MASUDA, "BreedingMasuda" },
        { HuntingMode.POKERADAR, "Pokeradar" },
        { HuntingMode.HORDE, "Horde" },
        { HuntingMode.FRIEND_SAFARI, "FriendSafari" },
        { HuntingMode.CHAIN_FISHING, "ChainFishing" },
        { HuntingMode.DEXNAV, "DexNav" },
        { HuntingMode.SOS_CALLS, "SOSCalls" },
    };

    public static string GetHuntingModeString(HuntingMode e)
    {
        if (HuntingModeStringKey.ContainsKey(e))
            return HuntingModeStringKey[e];
        return "UNKNOW-METHOD";
    }

    public static readonly Dictionary<string, float> ShinyRatesGeneral
    = new Dictionary<string, float>
    {
        { "BEFORE_6G", 1f/8192f },
        { "AFTER_6G", 1f/4096f }
    };

    public static readonly int ShinyCharmMultiplier = 3;
    public static readonly int HordeMutiplier = 5;
    public static readonly float FriendSafariShinyRate = 1f/512f;


    // Define which method is available for each game
    public static readonly Dictionary<GameVersion, List<HuntingMode>> HuntingMethodByGeneration
    = new Dictionary<GameVersion, List<HuntingMode>>
        {
            { GameVersion.RUBY_SAPPHIRE_EMERALD, new List<HuntingMode> { HuntingMode.WILD } },
            { GameVersion.FIRERED_LEAFGREEN, new List<HuntingMode> { HuntingMode.WILD } },
            {
                GameVersion.DIAMOND_PEARL_PLATINUM,
                new List<HuntingMode>
                {
                    HuntingMode.WILD,
                    HuntingMode.BREEDING,
                    HuntingMode.BREEDING_MASUDA,
                    HuntingMode.POKERADAR
                }
            },
            {
                GameVersion.HEARTGOLD_SOULSILVER,
                new List<HuntingMode>
                {
                    HuntingMode.WILD,
                    HuntingMode.BREEDING,
                    HuntingMode.BREEDING_MASUDA
                }
            },
            {
                GameVersion.BLACK_WHITE,
                new List<HuntingMode>
                {
                    HuntingMode.WILD,
                    HuntingMode.BREEDING,
                    HuntingMode.BREEDING_MASUDA,
                }
            },
            {
                GameVersion.BLACK_WHITE_2,
                new List<HuntingMode>
                {
                    HuntingMode.WILD,
                    HuntingMode.BREEDING,
                    HuntingMode.BREEDING_MASUDA,
                }
            },
            {
                GameVersion.X_Y,
                new List<HuntingMode>
                {
                    HuntingMode.WILD,
                    HuntingMode.BREEDING,
                    HuntingMode.BREEDING_MASUDA,
                    HuntingMode.HORDE,
                    HuntingMode.FRIEND_SAFARI,
                    HuntingMode.POKERADAR,
                    HuntingMode.CHAIN_FISHING
                }
            },
            {
                GameVersion.OMEGARUBY_ALPHASAPPHIRE,
                new List<HuntingMode>
                {
                    HuntingMode.WILD,
                    HuntingMode.BREEDING,
                    HuntingMode.BREEDING_MASUDA,
                    HuntingMode.HORDE,
                    HuntingMode.DEXNAV,
                    HuntingMode.CHAIN_FISHING
                }
            },
            {
                GameVersion.SUN_MOON,
                new List<HuntingMode>
                {
                    HuntingMode.WILD,
                    HuntingMode.BREEDING,
                    HuntingMode.BREEDING_MASUDA,
                    HuntingMode.SOS_CALLS,
                    HuntingMode.CHAIN_FISHING
                }
            },
            {
                GameVersion.ULTRASUN_ULTRAMOON,
                new List<HuntingMode>
                {
                    HuntingMode.WILD,
                    HuntingMode.BREEDING,
                    HuntingMode.BREEDING_MASUDA,
                    HuntingMode.SOS_CALLS,
                    HuntingMode.CHAIN_FISHING
                }
            }
        };

    // Define specific rate for every game regarding Masuda Method
    public static readonly Dictionary<GameVersion, float> ShinyRateByGenerationMasuda
    = new Dictionary<GameVersion, float>
    {
        { GameVersion.DIAMOND_PEARL_PLATINUM, 5f/8192f },
        { GameVersion.HEARTGOLD_SOULSILVER, 5f/8192f },
        { GameVersion.BLACK_WHITE, 6f/8192f },
        { GameVersion.BLACK_WHITE_2, 6f/8192f },
        { GameVersion.X_Y, 6f/4096f },
        { GameVersion.OMEGARUBY_ALPHASAPPHIRE, 6f/4096f },
        { GameVersion.SUN_MOON, 6f/4096f },
        { GameVersion.ULTRASUN_ULTRAMOON, 6f/4096f }
    };

    // Define specific rate for every game regarding Masuda Method and ShinyCharm
    public static readonly Dictionary<GameVersion, float> ShinyRateByGenerationMasudaChroma
    = new Dictionary<GameVersion, float>
{
        { GameVersion.BLACK_WHITE, 1f/1024f },
        { GameVersion.BLACK_WHITE_2, 1f/1024f },
        { GameVersion.X_Y, 1f/512f },
        { GameVersion.OMEGARUBY_ALPHASAPPHIRE, 1f/512f },
        { GameVersion.SUN_MOON, 1f/512f },
        { GameVersion.ULTRASUN_ULTRAMOON, 1f/512f }
};
}
