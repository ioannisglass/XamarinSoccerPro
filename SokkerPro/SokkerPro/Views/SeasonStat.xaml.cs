using SokkerPro.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SokkerPro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeasonStat : ContentPage
    {
        SeasonStatViewModel seasonVM;
        public SeasonStat(int? season_id, String countryflag, String leaguename)
        {
            InitializeComponent();
            BindingContext = seasonVM = new SeasonStatViewModel(season_id, countryflag, leaguename);
        }

        /*public static async Task<SeasonStat> Create(int? season_id, String countryflag, String leaguename)
        {
            var myClass = new SeasonStat();
            await myClass.Initialize(season_id, countryflag, leaguename);
            return myClass;
        }*/

        /*private async Task Initialize(int? season_id, string countryflag, string leaguename)
        {
            this.BindingContext = this.seasonVM = await SeasonStatViewModel.Create(season_id, countryflag, leaguename);
        }*/
    }
}