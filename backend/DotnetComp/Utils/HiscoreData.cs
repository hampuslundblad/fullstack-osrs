using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DotnetComp.Utils.HiscoreEntry;

namespace DotnetComp.Utils
{
    public class HiscoreEntry(string name, HiscoreEntryType type)
    {
        public enum HiscoreEntryType
        {
            Total,
            Skill,
            Boss,
            ClueScroll,
            Minigame,
            Event,
            CollectionLog,
            Other,
        }

        public string Name { get; set; } = name;
        public HiscoreEntryType Type { get; set; } = type;
    }

    public class HiscoreData
    {
        // The ordering of these are important as they reflect the ordering of the response from the osrs hiscores api
        // As of 2025-02-28 it should follow
        /**
        - Skills
        - Events
        - Clue Scrolls
        - Minigames
        - Collection Logs
        - Bosses
        */


        public static readonly List<HiscoreEntry> HiscoreEntries =
        [
            new("Overall", HiscoreEntryType.Total),
            new("Attack", HiscoreEntryType.Skill),
            new("Defence", HiscoreEntryType.Skill),
            new("Strength", HiscoreEntryType.Skill),
            new("Hitpoints", HiscoreEntryType.Skill),
            new("Ranged", HiscoreEntryType.Skill),
            new("Prayer", HiscoreEntryType.Skill),
            new("Magic", HiscoreEntryType.Skill),
            new("Cooking", HiscoreEntryType.Skill),
            new("Woodcutting", HiscoreEntryType.Skill),
            new("Fletching", HiscoreEntryType.Skill),
            new("Fishing", HiscoreEntryType.Skill),
            new("Firemaking", HiscoreEntryType.Skill),
            new("Crafting", HiscoreEntryType.Skill),
            new("Smithing", HiscoreEntryType.Skill),
            new("Mining", HiscoreEntryType.Skill),
            new("Herblore", HiscoreEntryType.Skill),
            new("Agility", HiscoreEntryType.Skill),
            new("Thieving", HiscoreEntryType.Skill),
            new("Slayer", HiscoreEntryType.Skill),
            new("Farming", HiscoreEntryType.Skill),
            new("Runecrafting", HiscoreEntryType.Skill),
            new("Hunter", HiscoreEntryType.Skill),
            new("Construction", HiscoreEntryType.Skill),
            new("League Points", HiscoreEntryType.Event),
            new("Deadman Points", HiscoreEntryType.Event),
            new("Bounty Hunter - Hunter", HiscoreEntryType.Minigame),
            new("Bounty Hunter - Rogue", HiscoreEntryType.Minigame),
            new("Bounty Hunter (Legacy) - Hunter", HiscoreEntryType.Minigame),
            new("Bounty Hunter (Legacy) - Rogue", HiscoreEntryType.Minigame),
            new("Clue Scrolls (all)", HiscoreEntryType.ClueScroll),
            new("Clue Scrolls (beginner)", HiscoreEntryType.ClueScroll),
            new("Clue Scrolls (easy)", HiscoreEntryType.ClueScroll),
            new("Clue Scrolls (medium)", HiscoreEntryType.ClueScroll),
            new("Clue Scrolls (hard)", HiscoreEntryType.ClueScroll),
            new("Clue Scrolls (elite)", HiscoreEntryType.ClueScroll),
            new("Clue Scrolls (master)", HiscoreEntryType.ClueScroll),
            new("LMS - Rank", HiscoreEntryType.Minigame),
            new("PvP Arena - Rank", HiscoreEntryType.Minigame),
            new("Soul Wars Zeal", HiscoreEntryType.Minigame),
            new("Rifts closed", HiscoreEntryType.Minigame),
            new("Colosseum Glory", HiscoreEntryType.Minigame),
            new("Collections Logged", HiscoreEntryType.CollectionLog),
            new("Abyssal Sire", HiscoreEntryType.Boss),
            new("Alchemical Hydra", HiscoreEntryType.Boss),
            new("Amoxliatl", HiscoreEntryType.Boss),
            new("Araxxor", HiscoreEntryType.Boss),
            new("Artio", HiscoreEntryType.Boss),
            new("Barrows Chests", HiscoreEntryType.Boss),
            new("Bryophyta", HiscoreEntryType.Boss),
            new("Callisto", HiscoreEntryType.Boss),
            new("Cal'varion", HiscoreEntryType.Boss),
            new("Cerberus", HiscoreEntryType.Boss),
            new("Chambers of Xeric", HiscoreEntryType.Boss),
            new("Chambers of Xeric: Challenge Mode", HiscoreEntryType.Boss),
            new("Chaos Elemental", HiscoreEntryType.Boss),
            new("Chaos Fanatic", HiscoreEntryType.Boss),
            new("Commander Zilyana", HiscoreEntryType.Boss),
            new("Corporeal Beast", HiscoreEntryType.Boss),
            new("Crazy Archaeologist", HiscoreEntryType.Boss),
            new("Dagannoth Prime", HiscoreEntryType.Boss),
            new("Dagannoth Rex", HiscoreEntryType.Boss),
            new("Dagannoth Supreme", HiscoreEntryType.Boss),
            new("Deranged Archaeologist", HiscoreEntryType.Boss),
            new("Duke Sucellus", HiscoreEntryType.Boss),
            new("General Graardor", HiscoreEntryType.Boss),
            new("Giant Mole", HiscoreEntryType.Boss),
            new("Grotesque Guardians", HiscoreEntryType.Boss),
            new("Hespori", HiscoreEntryType.Boss),
            new("Kalphite Queen", HiscoreEntryType.Boss),
            new("King Black Dragon", HiscoreEntryType.Boss),
            new("Kraken", HiscoreEntryType.Boss),
            new("Kree'Arra", HiscoreEntryType.Boss),
            new("K'ril Tsutsaroth", HiscoreEntryType.Boss),
            new("Lunar Chests", HiscoreEntryType.Boss),
            new("Mimic", HiscoreEntryType.Boss),
            new("Nex", HiscoreEntryType.Boss),
            new("Nightmare", HiscoreEntryType.Boss),
            new("Phosani's Nightmare", HiscoreEntryType.Boss),
            new("Obor", HiscoreEntryType.Boss),
            new("Phantom Muspah", HiscoreEntryType.Boss),
            new("Royal Titans", HiscoreEntryType.Boss),
            new("Sarachnis", HiscoreEntryType.Boss),
            new("Scorpia", HiscoreEntryType.Boss),
            new("Scurrius", HiscoreEntryType.Boss),
            new("Skotizo", HiscoreEntryType.Boss),
            new("Sol Heredit", HiscoreEntryType.Boss),
            new("Spindel", HiscoreEntryType.Boss),
            new("Tempoross", HiscoreEntryType.Boss),
            new("The Gauntlet", HiscoreEntryType.Boss),
            new("The Corrupted Gauntlet", HiscoreEntryType.Boss),
            new("The Hueycoatl", HiscoreEntryType.Boss),
            new("The Leviathan", HiscoreEntryType.Boss),
            new("The Whisperer", HiscoreEntryType.Boss),
            new("Theatre of Blood", HiscoreEntryType.Boss),
            new("Theatre of Blood: Hard Mode", HiscoreEntryType.Boss),
            new("Thermonuclear Smoke Devil", HiscoreEntryType.Boss),
            new("Tombs of Amascut", HiscoreEntryType.Boss),
            new("Tombs of Amascut: Expert Mode", HiscoreEntryType.Boss),
            new("TzKal-Zuk", HiscoreEntryType.Boss),
            new("TzTok-Jad", HiscoreEntryType.Boss),
            new("Vardorvis", HiscoreEntryType.Boss),
            new("Venenatis", HiscoreEntryType.Boss),
            new("Vet'ion", HiscoreEntryType.Boss),
            new("Vorkath", HiscoreEntryType.Boss),
            new("Wintertodt", HiscoreEntryType.Boss),
            new("Zalcano", HiscoreEntryType.Boss),
            new("Zulrah", HiscoreEntryType.Boss),
            new("Unkown", HiscoreEntryType.Other),
        ];
        private static readonly List<HiscoreEntry> Skills =
        [
            new("Overall", HiscoreEntryType.Total),
            new("Attack", HiscoreEntryType.Skill),
            new("Defence", HiscoreEntryType.Skill),
            new("Strength", HiscoreEntryType.Skill),
            new("Hitpoints", HiscoreEntryType.Skill),
            new("Ranged", HiscoreEntryType.Skill),
            new("Prayer", HiscoreEntryType.Skill),
            new("Magic", HiscoreEntryType.Skill),
            new("Cooking", HiscoreEntryType.Skill),
            new("Woodcutting", HiscoreEntryType.Skill),
            new("Fletching", HiscoreEntryType.Skill),
            new("Fishing", HiscoreEntryType.Skill),
            new("Firemaking", HiscoreEntryType.Skill),
            new("Crafting", HiscoreEntryType.Skill),
            new("Smithing", HiscoreEntryType.Skill),
            new("Mining", HiscoreEntryType.Skill),
            new("Herblore", HiscoreEntryType.Skill),
            new("Agility", HiscoreEntryType.Skill),
            new("Thieving", HiscoreEntryType.Skill),
            new("Slayer", HiscoreEntryType.Skill),
            new("Farming", HiscoreEntryType.Skill),
            new("Runecrafting", HiscoreEntryType.Skill),
            new("Hunter", HiscoreEntryType.Skill),
            new("Construction", HiscoreEntryType.Skill),
        ];
    }
}
