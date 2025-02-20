using DotnetComp.Models.Domain;

namespace DotnetComp.Mappers
{
    public static class SkillMapper
    {
        private static readonly string[] skillNamesInOrder =
        [
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
                    var skill = Skill.FromString(skillNamesInOrder[i], skillRankLevelExperience[i]);
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
