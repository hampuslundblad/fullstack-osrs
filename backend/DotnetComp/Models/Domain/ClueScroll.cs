using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace DotnetComp.Models.Domain
{
    public enum ClueScrollType
    {
        Beginner,
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
            return type.ToLower() switch
            {
                var t when t.Contains("beginner") => ClueScrollType.Beginner,
                var t when t.Contains("easy") => ClueScrollType.Easy,
                var t when t.Contains("medium") => ClueScrollType.Medium,
                var t when t.Contains("hard") => ClueScrollType.Hard,
                var t when t.Contains("elite") => ClueScrollType.Elite,
                var t when t.Contains("master") => ClueScrollType.Master,
                _ => throw new FormatException($"Invalid clue scroll type {type}"),
            };
        }
    }
}
