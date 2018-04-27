using System;
using System.Collections.Generic;
using System.Text;

namespace InvestigacionSocial.ViewModels
{
    public class MasterCoordinator
    {
        public static event EventHandler<SampleEventArgs> SelectedSampleChanged;
        public static event EventHandler<EventArgs> PresentMainMenuOnAppearance;
        public static event EventHandler<SampleEventArgs> MenuSelected;

        private static Menu _selectedMenu = null;

        public static void RaisePresentMainMenuOnAppearance()
        {
            if (PresentMainMenuOnAppearance != null)
            {
                PresentMainMenuOnAppearance(typeof(MasterCoordinator), null);
            }
        }

        public static void RaiseSampleSelected(Menu menu)
        {
            if (MenuSelected != null)
            {
                MenuSelected(typeof(MasterCoordinator), new SampleEventArgs(menu));
            }
        }

        public static Menu SelectedMenu
        {
            get
            {
                return _selectedMenu;
            }

            set
            {
                if (_selectedMenu != value)
                {
                    _selectedMenu = value;

                    if (SelectedSampleChanged != null)
                    {
                        SelectedSampleChanged(typeof(MasterCoordinator), new SampleEventArgs(value));
                    }
                }
            }
        }
    }

    public class SampleEventArgs : EventArgs
    {
        private readonly Menu _menu;

        public SampleEventArgs(Menu newMenu)
        {
            _menu = newMenu;
        }

        public Menu Menu
        {
            get
            {
                return _menu;
            }
        }
    }
}
