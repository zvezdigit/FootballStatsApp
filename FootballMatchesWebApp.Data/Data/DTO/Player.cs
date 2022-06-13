using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FootballMatchesWebApp.Data.Data.DTO
{
    public class Player
    {

		public int? Id { get; set; }
		public string Name { get; set; }	
		public int? Age { get; set; }
		public string Nationality { get; set; }
		public string Height { get; set; }
		public string Weight { get; set; }
		public bool? Injured { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Player other && this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }

}
