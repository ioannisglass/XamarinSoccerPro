using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using SokkerPro.Models;
using SokkerPro.Views;

namespace SokkerPro.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {

        public ItemsViewModel(string title)
        {
            Title = title;
        }
    }
}