using SokkerPro.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SokkerPro.ViewModels
{
    public class FixtureViewModel : BaseViewModel
    {
        public Fixture fixture { get; set; }

        public FixtureViewModel(Fixture fixture)
        {
            this.fixture = fixture;
        }
    }
}
