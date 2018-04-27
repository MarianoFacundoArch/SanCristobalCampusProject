using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace InvestigacionSocial.ViewModels
{
    public class MasterDetailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Menu _selectedMenu;

        public MasterDetailViewModel(INavigation navigation)
        {
            MenuesCategories = new List<MenuCategory>(MenuDefinition.MenuCategories.Values);
            AllMenues = MenuDefinition.AllMenues;
            MenuesGroupedByCategory = MenuDefinition.MenuGroupedByCategory;
        }

        public List<MenuCategory> MenuesCategories { get; set; }

        public List<Menu> AllMenues { get; set; }

        public List<MenuGroup> MenuesGroupedByCategory { get; set; }

        public Menu SelectedMenu
        {
            get
            {
                return _selectedMenu;
            }

            set
            {
                if (value != _selectedMenu)
                {
                    _selectedMenu = value;

                    RaisePropertyChanged("SelectedMenu");
                }
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
