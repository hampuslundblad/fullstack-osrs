using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DotnetComp.Models.Entities
{
    public class PlayerBossKillEntity
    {
        [ForeignKey("PlayerId")]
        public int PlayerId { get; set; }

        [ForeignKey("BossId")]
        public int BossId { get; set; }
        public int Id { get; set; }

        public int Kills { get; set; }

        public DateTime DateTime { get; set; }
    }
}
