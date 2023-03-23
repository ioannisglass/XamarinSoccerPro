using System;
using System.Collections.Generic;
using System.Text;

namespace SokkerPro.Models
{
    class FixtureResponse
    {
        public string status { get; set; }
        public List<Fixture> data { get; set; }
    }
}
