﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballMatchesWebApp.Data.Data.DTO
{
    public class Fixture
    {

        public int? Id { get; set; }

        public DateTime Date { get; set; }

        public string Referee { get; set; }
    }
}
