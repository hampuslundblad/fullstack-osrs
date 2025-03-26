using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetComp.Models.Entities
{
    public class PlayerBossStat
    {
        public int Id { get; set; }

        [ForeignKey("BossId")]
        public int BossId { get; set; }
        public int Kills { get; set; }
        public int Rank { get; set; }
        public DateTime DateTime { get; set; }

        [ForeignKey("PlayerId")]
        public int PlayerId { get; set; }
    }
}
