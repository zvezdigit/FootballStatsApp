using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchesWebApp.Data.Data.DTO
{
    public class ApiResponse<T>
    {
        [JsonProperty("response")]
        public List<T> Data { get; set; }
    }
}
