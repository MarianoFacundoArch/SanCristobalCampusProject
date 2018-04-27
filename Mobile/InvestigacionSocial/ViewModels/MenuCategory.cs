using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace InvestigacionSocial.ViewModels
{
    public class MenuCategory
    {
        public string Name { get; set; }

        public Color BackgroundColor { get; set; }

        public String BackgroundImage { get; set; }

        public List<Menu> MenuItemList { get; set; }

        public string Icon { get; set; }

        public int Badge { get; set; }
    }
}
