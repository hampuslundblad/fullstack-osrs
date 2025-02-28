using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace DotnetComp.Models.Domain
{
    public enum ClueScrollType
    {
        Easy,
        Medium,
        Hard,
        Elite,
        Master,
    }

    public class ClueScroll
    {
        public required ClueScrollType Type { get; set; }
        public required int Completed { get; set; }

        public required int Rank { get; set; }

        /// <summary>
        /// Converts a clue scroll string to a ClueScroll object.
        /// </summary>
        /// <param name="clueScrollName">The type of the clue scroll.</param>
        /// <param name="clueScrollString">The clue scroll string in the format "rank, completed".</param>
        /// <returns>A ClueScroll object with the specified type, rank, and completed values.</returns>
        /// <exception cref="FormatException">Thrown when the clue scroll string does not contain valid integers for rank and completed.</exception>
        public static ClueScroll FromString(string clueScrollName, string clueScrollString)
        {
            var parts = clueScrollString.Split(",");
            bool isValidRank = int.TryParse(parts[0], out int rank) && rank > 0;
            bool isValidCompleted = int.TryParse(parts[1], out int completed) && completed >= 0;

            if (!isValidCompleted || !isValidRank)
            {
                throw new FormatException("Clue scroll string must contain a valid integer");
            }

            var type = ParseClueScrollType(clueScrollName);

            return new ClueScroll
            {
                Type = type,
                Rank = rank,
                Completed = completed,
            };
        }

        private static ClueScrollType ParseClueScrollType(string type)
        {
            if (type.Contains("Easy", StringComparison.OrdinalIgnoreCase))
            {
                return ClueScrollType.Easy;
            }
            else if (type.Contains("Medium", StringComparison.OrdinalIgnoreCase))
            {
                return ClueScrollType.Medium;
            }
            else if (type.Contains("Hard", StringComparison.OrdinalIgnoreCase))
            {
                return ClueScrollType.Hard;
            }
            else if (type.Contains("Elite", StringComparison.OrdinalIgnoreCase))
            {
                return ClueScrollType.Elite;
            }
            else if (type.Contains("Master", StringComparison.OrdinalIgnoreCase))
            {
                return ClueScrollType.Master;
            }
            throw new FormatException($"Invalid clue scroll type {type}");
        }
    }
}
