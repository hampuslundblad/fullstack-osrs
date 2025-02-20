using DotnetComp.Models.Domain;

namespace DotnetComp.Models.Dto
{
    public sealed class PlayerDTO
    {
        public required string Name { get; set; }
        public required int TotalExperience { get; set; }
        public required int TotalLevel { get; set; }

        public required List<PlayerExperienceDTO> ExperienceOverTime { get; set; }

        public static PlayerDTO FromDomain(Player domain)
        {
            return new PlayerDTO
            {
                Name = domain.PlayerName,
                TotalExperience = domain.TotalExperience,
                TotalLevel = domain.TotalLevel,
                ExperienceOverTime =
                [
                    .. domain.ExperienceOverTime.Select(PlayerExperienceDTO.FromDomain),
                ],
            };
        }
    }
}
