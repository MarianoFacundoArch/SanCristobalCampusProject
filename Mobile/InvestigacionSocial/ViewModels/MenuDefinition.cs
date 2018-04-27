using System;
using System.Collections.Generic;
using System.Text;
using InvestigacionSocial.Helpers;
using InvestigacionSocial.Views;
using InvestigacionSocial.Views.Login;
using InvestigacionSocial.Views.Services;
using InvestigacionSocial.Views.Skills;
using Xamarin.Forms;

namespace InvestigacionSocial.ViewModels
{
    public static class MenuDefinition
    {
        private static List<MenuCategory> _menuesCategoryList;
        private static Dictionary<string, MenuCategory> _menuesCategories;
        private static List<Menu> _allMenues;
        private static List<MenuGroup> _menuesGroupedByCategory;

        public static string[] _categoriesColors = {
            "#921243",
            "#B31250",
            "#CD195E",
            "#56329A",
            "#6A40B9",
            "#7C4ECD",
            "#525ABB",
            "#5F7DD4",
            "#7B96E5"
        };

        public static List<MenuCategory> MenuesCategoryList
        {
            get
            {
                if (_menuesCategoryList == null)
                {
                    InitializeMenues();
                }

                return _menuesCategoryList;
            }
        }

        public static Dictionary<string, MenuCategory> MenuCategories
        {
            get
            {
                if (_menuesCategories == null)
                {
                    InitializeMenues();
                }

                return _menuesCategories;
            }
        }

        public static List<Menu> AllMenues
        {
            get
            {
                if (_allMenues == null)
                {
                    InitializeMenues();
                }
                return _allMenues;
            }
        }

        public static List<MenuGroup> MenuGroupedByCategory
        {
            get
            {
                if (_menuesGroupedByCategory == null)
                {
                    InitializeMenues();
                }

                return _menuesGroupedByCategory;
            }
        }


        internal static Dictionary<string, MenuCategory> CreateMenues()
        {
            var categories = new Dictionary<string, MenuCategory>();
            if (GlobalData.myUser != null && GlobalData.myUser.is_provider == 0)
            {
                categories.Add(
                    "centroasistencia",
                    new MenuCategory
                    {
                        Name = "Asistencia",
                        BackgroundColor = Color.FromHex(_categoriesColors[0]),
                        //BackgroundImage = SampleData.DashboardImagesList[6],
                        Icon = TopTechIconFont.OnlineSupport,
                        Badge = 2,
                        MenuItemList = new List<Menu> {
                            new Menu("Solicitar grúa", typeof(mapPage),"",TopTechIconFont.Crane,false,false),
                        }
                    }
                );

            }
            else
            {
                categories.Add(
                    "prestadores",
                    new MenuCategory
                    {
                        Name = "Prestadores",
                        BackgroundColor = Color.FromHex(_categoriesColors[0]),
                        //BackgroundImage = SampleData.DashboardImagesList[6],
                        Icon = TopTechIconFont.PermanentJob,
                        Badge = 3,
                        MenuItemList = new List<Menu> {
                            new Menu("Mapa de averías", typeof(AvailableServicesMapPage),"",TopTechIconFont.RoadWorker,false,false),
                        }
                    }
                );
            }

            

            categories.Add(
                "cuenta",
                new MenuCategory
                {
                    Name = "Mi cuenta",
                    BackgroundColor = Color.FromHex(_categoriesColors[0]),
                    //BackgroundImage = SampleData.DashboardImagesList[6],
                    Icon = TopTechIconFont.UserCredentials,
                    Badge = 5,
                    MenuItemList = new List<Menu> {
                        new Menu("Ajustes", typeof(RegisterPage),"",TopTechIconFont.Settings,false,false), // To be coded
                        new Menu("Desconectarse", typeof(LogoutPage),"",TopTechIconFont.LogoutRounded,false,false),

                    }
                }
            );
            

            /*
            categories.Add(
                "SOCIAL",
                new MenuCategory
                {
                    Name = "Social",
                    BackgroundColor = Color.FromHex(_categoriesColors[0]),
                    BackgroundImage = SampleData.DashboardImagesList[6],
                    Icon = GrialShapesFont.Person,
                    Badge = 2,
                    MenuItemList = new List<Menu> {
                        new Menu("Document Timeline", typeof(DocumentTimelinePage), SampleData.DashboardImagesList[6], GrialShapesFont.QueryBuilder, false, true),
                        new Menu("Timeline", typeof(TimelinePage), SampleData.DashboardImagesList[6], GrialShapesFont.QueryBuilder, false, true),

                        new Menu("User Profile", typeof(ProfilePage), SampleData.DashboardImagesList[6], GrialShapesFont.AccountCircle),
                        new Menu("Social", typeof(SocialPage), SampleData.DashboardImagesList[6], GrialShapesFont.Group),
                        new Menu("Social Variant", typeof(SocialVariantPage), SampleData.DashboardImagesList[6], GrialShapesFont.Group),

                    }
                }
            );

            categories.Add(
                "ARTICLES",
                new MenuCategory
                {
                    Name = "Articles",
                    BackgroundColor = Color.FromHex(_categoriesColors[1]),
                    BackgroundImage = SampleData.DashboardImagesList[4],
                    Icon = GrialShapesFont.InsertFile,
                    Badge = 2,
                    MenuItemList = new List<Menu> {
                        new Menu("Articles Classic View", typeof(ArticlesClassicViewPage), SampleData.DashboardImagesList[4], GrialShapesFont.InsertFile, false, true),
                        new Menu("Front Page News", typeof(FrontPageNewsPage), SampleData.DashboardImagesList[4], GrialShapesFont.InsertFile, false, true),

                        new Menu("Article View", typeof(ArticleViewPage), SampleData.DashboardImagesList[4], GrialShapesFont.InsertFile),
                        new Menu("Articles List", typeof(ArticlesListPage), SampleData.DashboardImagesList[4], GrialShapesFont.InsertFile),
                        new Menu("Articles List Variant", typeof(ArticlesListVariantPage), SampleData.DashboardImagesList[4], GrialShapesFont.InsertFile),
                        new Menu("Articles Feed", typeof(ArticlesFeedPage), SampleData.DashboardImagesList[4], GrialShapesFont.InsertFile),

                    }
                }
            );

            categories.Add(
                "DASHBOARD",
                new MenuCategory
                {
                    Name = "Dashboards",
                    BackgroundColor = Color.FromHex(_categoriesColors[2]),
                    BackgroundImage = SampleData.DashboardImagesList[3],
                    Badge = 5,
                    Icon = GrialShapesFont.Dashboard,
                    MenuItemList = new List<Menu> {
                        new Menu("Grial Movies", typeof(DashboardMultipleScrollPage), SampleData.DashboardImagesList[3], GrialShapesFont.LocalMovies, false, true),
                        new Menu("Movie Selection", typeof(MovieSelectionPage), SampleData.DashboardImagesList[3], GrialShapesFont.LocalMovies, false, true),
                        new Menu("Dashboard Cards", typeof(DashboardCardsPage), SampleData.DashboardImagesList[3], GrialShapesFont.Dashboard, false, true),
                        new Menu("Multiple Tiles", typeof(DashboardMultipleTilesPage), SampleData.DashboardImagesList[3], GrialShapesFont.Dashboard, false, true),
                        new Menu("Scrollable Dashboards", typeof(DashboardScrollablePage), SampleData.DashboardImagesList[3], GrialShapesFont.Dashboard, false, true),

                        new Menu("Icons Dashboard", typeof(DashboardPage), SampleData.DashboardImagesList[3], GrialShapesFont.Dashboard),
                        new Menu("Flat Dashboard", typeof(DashboardFlatPage), SampleData.DashboardImagesList[3], GrialShapesFont.Dashboard),
                        new Menu("Images Dashboard", typeof(DashboardWithImagesPage), SampleData.DashboardImagesList[3], GrialShapesFont.Dashboard, false),

                    }
                }
            );


            categories.Add(
                "NAVIGATION",
                new MenuCategory
                {
                    Name = "Navigation",
                    BackgroundColor = Color.FromHex(_categoriesColors[3]),
                    BackgroundImage = SampleData.DashboardImagesList[2],
                    Badge = 5,
                    Icon = GrialShapesFont.Menu,
                    MenuItemList = new List<Menu> {
                        new Menu("Card List", typeof(TableCallsPage), SampleData.DashboardImagesList[2], GrialShapesFont.List, false, true),
                        new Menu("Empty State", typeof(EmptyStatePage), SampleData.DashboardImagesList[2], GrialShapesFont.Hourglass, false, true),
                        new Menu("Notifications", typeof(NotificationsPage), SampleData.DashboardImagesList[2], GrialShapesFont.Notifications, false, true),
                        new Menu("Welcome Page", typeof (WelcomePage), SampleData.DashboardImagesList[2], GrialShapesFont.Place, true, true),

                        new Menu("Categories List Flat", typeof(CategoriesListFlatPage), SampleData.DashboardImagesList[2], GrialShapesFont.List),
                        new Menu("Image Categories", typeof(CategoriesListWithImagesPage), SampleData.DashboardImagesList[2], GrialShapesFont.List),
                        new Menu("Icon Categories", typeof(CategoriesListWithIconsPage), SampleData.DashboardImagesList[2], GrialShapesFont.List),
                        new Menu("Custom NavBar", typeof(CustomNavBarPage), SampleData.DashboardImagesList[2], GrialShapesFont.WebAsset),
                        new Menu("Root Page", typeof(RootPage), SampleData.DashboardImagesList[2], GrialShapesFont.Menu, false),
                    }
                }
            );

            categories.Add(
                "LOGINS",
                new MenuCategory
                {
                    Name = "Logins",
                    BackgroundColor = Color.FromHex(_categoriesColors[4]),
                    BackgroundImage = SampleData.DashboardImagesList[5],
                    Badge = 2,
                    Icon = GrialShapesFont.Lock,
                    MenuItemList = new List<Menu> {
                        new Menu("Simple Sign Up", typeof(SimpleSignUpPage), SampleData.DashboardImagesList[5], GrialShapesFont.CheckCircle, true, true),
                        new Menu("Simple Login", typeof(SimpleLoginPage), SampleData.DashboardImagesList[5], GrialShapesFont.CheckCircle, true, true),

                        new Menu("Login", typeof(LoginPage), SampleData.DashboardImagesList[5], GrialShapesFont.Lock, true),
                        new Menu("Sign Up", typeof(SignUpPage), SampleData.DashboardImagesList[5], GrialShapesFont.CheckCircle, true),
                        new Menu("Password Recovery", typeof(PasswordRecoveryPage), SampleData.DashboardImagesList[5], GrialShapesFont.SettingsRestore, true)
                    }
                }
            );

            categories.Add(
                "ECOMMERCE",
                new MenuCategory
                {
                    Name = "Ecommerce",
                    BackgroundColor = Color.FromHex(_categoriesColors[5]),
                    BackgroundImage = SampleData.DashboardImagesList[1],
                    Badge = 3,
                    Icon = GrialShapesFont.ShoppingCart,
                    MenuItemList = new List<Menu> {
                        new Menu("Product FullScreen", typeof(ProductItemFullScreenPage), SampleData.DashboardImagesList[1], GrialShapesFont.CardGiftcard, false, true),
                        new Menu("Products Catalog", typeof(ProductsCatalogPage), SampleData.DashboardImagesList[1], GrialShapesFont.CardGiftcard, false, true),
                        new Menu("Product Order", typeof(ProductOrder), SampleData.DashboardImagesList[5], GrialShapesFont.CardGiftcard, false, true),

                        new Menu("Products Grid", typeof(ProductsGridPage), SampleData.DashboardImagesList[1] , GrialShapesFont.Module),
                        new Menu("Products Grid Variant", typeof(ProductsGridVariantPage), SampleData.DashboardImagesList[1] , GrialShapesFont.Module),
                        new Menu("Product Item View", typeof(ProductItemViewPage), SampleData.DashboardImagesList[1], GrialShapesFont.CardGiftcard),
                        new Menu("Products Carousel", typeof(ProductsCarouselPage), SampleData.DashboardImagesList[1], GrialShapesFont.CardGiftcard),

                    }
                }
            );

            categories.Add(
                "WALKTROUGH",
                new MenuCategory
                {
                    Name = "Walkthroughs",
                    BackgroundColor = Color.FromHex(_categoriesColors[6]),
                    BackgroundImage = SampleData.DashboardImagesList[7],
                    Badge = 2,
                    Icon = GrialShapesFont.Carousel,
                    MenuItemList = new List<Menu> {
                        new Menu("Walkthrough", typeof(WalkthroughPage), SampleData.DashboardImagesList[7], GrialShapesFont.Carousel, true, true),
						//new Menu("Walkthrough Gradient", typeof(WalkthroughGradientPage), SampleData.DashboardImagesList[7], GrialShapesFont.Carousel, true, true),
						new Menu("Walkthrough Flat", typeof(WalkthroughFlatPage), SampleData.DashboardImagesList[7], GrialShapesFont.Carousel, true, true),

                        new Menu("Walkthrough Variant", typeof(WalkthroughVariantPage), SampleData.DashboardImagesList[7], GrialShapesFont.Carousel, true)
                    }
                }
            );

            categories.Add(
                "MESSAGES",
                new MenuCategory
                {
                    Name = "Messages",
                    BackgroundColor = Color.FromHex(_categoriesColors[7]),
                    BackgroundImage = SampleData.DashboardImagesList[8],
                    Badge = 2,
                    Icon = GrialShapesFont.Email,
                    MenuItemList = new List<Menu> {
                        new Menu("Chat Timeline", typeof( ChatTimelinePage ), SampleData.DashboardImagesList[2], GrialShapesFont.QueryBuilder, false, true),
                        new Menu("Recent Chat List", typeof(RecentChatListPage), SampleData.DashboardImagesList[2], GrialShapesFont.QueryBuilder, false, true),

                        new Menu("Messages", typeof(MessagesListPage), SampleData.DashboardImagesList[8], GrialShapesFont.Email),
                        new Menu("Chat Messages List", typeof(ChatMessagesListPage), SampleData.DashboardImagesList[8], GrialShapesFont.Forum),

                    }
                }
            );

            categories.Add(
                "THEME",
                new MenuCategory
                {
                    Name = "Grial Theme",
                    BackgroundColor = Color.FromHex(_categoriesColors[8]),
                    BackgroundImage = SampleData.DashboardImagesList[0],
                    Badge = 9,
                    Icon = GrialShapesFont.ColorPalette,
                    MenuItemList = new List<Menu> {
                        new Menu("Animations", typeof(AnimationsPage), SampleData.DashboardImagesList[0], GrialShapesFont.LogoLottie, false, true),
                        new Menu("Tab Control", typeof(TabControlSamplePage), SampleData.DashboardImagesList[0], GrialShapesFont.LogoGrial, false, true),
                        new Menu("Tabbed Page", typeof(TabsPage), SampleData.DashboardImagesList[0], GrialShapesFont.Tab, false, true),
                        new Menu("Demo Settings", typeof(GrialDemoSettings), SampleData.DashboardImagesList[0], GrialShapesFont.Settings, false, true),

                        new Menu("About", typeof(GenericAboutPage), SampleData.DashboardImagesList[0], GrialShapesFont.Help, false, true),
                        new Menu("Custom Settings Page", typeof(CustomSettingsPage), SampleData.DashboardImagesList[0], GrialShapesFont.Settings, false, true),
                        new Menu("Custom Activity Indicator", typeof(CustomActivityIndicatorPage), SampleData.DashboardImagesList[0], GrialShapesFont.Loop, false, true),
                        new Menu("Responsive Helpers", typeof(ResponsiveHelpersPage), SampleData.DashboardImagesList[0], GrialShapesFont.TabletAndroid, false, true),
                        new Menu("Grial Font Icons", typeof(IconsPage), SampleData.DashboardImagesList[0], GrialShapesFont.LogoGrial, false, true),

                        new Menu("Native Controls", typeof(ThemePage), SampleData.DashboardImagesList[0], GrialShapesFont.ColorPalette),
                        new Menu("Custom Renderers", typeof(CustomRenderersPage), SampleData.DashboardImagesList[0], GrialShapesFont.ColorPalette),
                        new Menu("Grial Common Views", typeof(CommonViewsPage), SampleData.DashboardImagesList[0], GrialShapesFont.LogoGrial),
                        new Menu("Settings Page", typeof(SettingsPage), SampleData.DashboardImagesList[0], GrialShapesFont.Settings),
                        new Menu("WebView", typeof(WebViewPage), SampleData.DashboardImagesList[0], GrialShapesFont.Public),

                    }
                }
            );
            */
            return categories;
        }

        internal static void InitializeMenues()
        {
            _menuesCategories = CreateMenues();

            _menuesCategoryList = new List<MenuCategory>();

            foreach (var sample in _menuesCategories.Values)
            {
                _menuesCategoryList.Add(sample);
            }

            _allMenues = new List<Menu>();

            _menuesGroupedByCategory = new List<MenuGroup>();

            foreach (var sampleCategory in MenuCategories.Values)
            {

                var sampleItem = new MenuGroup(sampleCategory.Name.ToUpper());

                foreach (var sample in sampleCategory.MenuItemList)
                {
                    _allMenues.Add(sample);
                    sampleItem.Add(sample);
                }

                _menuesGroupedByCategory.Add(sampleItem);
            }
        }

        private static void RootPageCustomNavigation(INavigation navigation)
        {
            MasterCoordinator.RaisePresentMainMenuOnAppearance();
            navigation.PopToRootAsync();
        }
    }

    public class MenuGroup : List<Menu>
    {
        private readonly string _name;

        public MenuGroup(string name)
        {
            _name = name;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }
    }
}
