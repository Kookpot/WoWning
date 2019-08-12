using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WoWning.DataLayer.Entities
{
    public class WoWUser : IdentityUser
    {
        [PersonalData]
        public int PositiveRecommendations { get; set; }

        [PersonalData]
        public int NegativeRecommendations { get; set; }

        public ICollection<Character> Characters { get; set; }

        [NotMapped]
        public List<string> AlternateNames { get { return Characters.Select(x => x.Name).ToList(); } }

        [NotMapped]
        public int Recommendations { get { return PositiveRecommendations - NegativeRecommendations; } }

        [NotMapped]
        public bool CanRecommend { get; set; }
    }
}