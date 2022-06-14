using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace FootballMatchesWebApp.Application.Models.DataImport
{
    
    public class ImportDataFormViewModel
    {

        [Required]
        [Range(2010, 2021, ErrorMessage = "Year should be between {1} and {2}")]
        public int Year { get; set; }
        public int Season { get; set; }

        public int LeagueId { get; set; }
    }

    
}
