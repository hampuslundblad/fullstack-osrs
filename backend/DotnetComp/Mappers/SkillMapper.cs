using DotnetComp.Models.Domain;

namespace DotnetComp.Mappers
{
    public static class SkillMapper
    {
        private static readonly int RANK_OFFSET = 0;
        private static readonly int LEVEL_OFFSET = 1;
        private static readonly int EXPERIENCE_OFFSET = 2;

        private static readonly string[] skillNamesInOrder = [
            "Attack",
            "Defence",
            "Strength",
            "Hitpoints",
            "Ranged",
            "Prayer",
            "Magic",
            "Cooking",
            "Woodcutting",
            "Fletching",
            "Fishing",
            "Firemaking",
            "Crafting",
            "Smithing",
            "Mining",
            "Herblore",
            "Agility",
            "Thieving",
            "Slayer",
            "Farming",
            "Runecrafting",
            "Hunter",
            "Construction",
        ];


        public static List<Skill> MapStringToSkill(string[] skillRankLevelExperience)
        {
            var skills = new List<Skill>();
            try
            {
                for (int i = 0; i < skillRankLevelExperience.Length; i++)
                {
                    var parts = skillRankLevelExperience[i].Split(',');
                    var skill = new Skill
                    {
                        Name = skillNamesInOrder[i],
                        Rank = int.Parse(parts[RANK_OFFSET]),
                        Level = int.Parse(parts[LEVEL_OFFSET]),
                        Experience = int.Parse(parts[EXPERIENCE_OFFSET])
                    };

                    skills.Add(skill);
                }

                return skills;
            }
            catch (Exception e)
            {
                throw new Exception("Error parsing skill data", e);
            }
        }
    }
}