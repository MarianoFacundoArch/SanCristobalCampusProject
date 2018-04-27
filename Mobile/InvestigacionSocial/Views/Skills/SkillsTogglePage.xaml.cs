using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonCross.DTOS;
using InvestigacionSocial.Services;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvestigacionSocial.Views.Skills
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SkillsTogglePage : ContentPage
	{
        //public List<SkillDTO> skillsList { get; set; }

        public SkillsTogglePage()
        {
            InitializeComponent();

        }
        /*
        public void refrescar()
        {

                skillsList.Sort((x, y) => x.title.CompareTo(y.title));

                skillsDrawerList.ItemsSource = skillsList;
                skillsDrawerList.IsRefreshing = false;


        }


	    async void actualizar(bool noServer = false)
        {
            try
            {
                skillsDrawerList.IsRefreshing = true;

                skillsList = JsonConvert.DeserializeObject<List<SkillDTO>>(await BackendService.sendGet("Skills/All"));
                refrescar();



            }
            catch (Exception e)
            {
                //datosGlobales.enviarExcepciones(e.ToString());
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            actualizar();
        }

        private bool estaTrabajando = false;
        void proceso(bool trabajando)
        {
            if (trabajando)
            {
                estaTrabajando = true;
                skillsDrawerList.IsRefreshing = true;
                skillsDrawerList.IsEnabled = false;

            }
            else
            {
                skillsDrawerList.IsRefreshing = false;
                skillsDrawerList.IsEnabled = true;
                estaTrabajando = false;
            }
        }
        */
        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            /*
            if (estaTrabajando || companeroList == null)
                return;

            try
            {
                if (!datosGlobales.miUsuario.rango.StartsWith("delegado"))
                {
                    DisplayAlert(datosGlobales.nombreAplicacion + " - Error",
                        "Solo los delegados pueden agregar companeros.", "Continuar");
                    return;
                }
                proceso(true);
                string serializado = JsonConvert.SerializeObject(companeroList);
                var formContent = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("lista", serializado)

                });


                var myHttpClient = new HttpClient();
                var response =
                    await myHttpClient.PostAsync(
                        datosGlobales.backendUrl + "accion=actualizarListaPedido&mail=" + datosGlobales.miUsuario.mail +
                        "&contrasena=" + datosGlobales.miUsuario.contrasena + "&id_pedido=" + idPedido, formContent);
                string respuesta = await response.Content.ReadAsStringAsync();

                if (respuesta == "ok")
                {
                    proceso(false);
                    Navigation.PopAsync();
                }
                else
                {
                    DisplayAlert(datosGlobales.nombreAplicacion + " - Error", "Error. Re intente mas tarde." + respuesta, "Continuar");
                }

            }
            catch (Exception exception)
            {
                DisplayAlert(datosGlobales.nombreAplicacion + " - Error", "Error. Re intente mas tarde." + exception.ToString(), "Continuar");
                datosGlobales.enviarExcepciones(exception.ToString());
            }
            proceso(false);

            */


        }


        
        private void CompaneroDrawerList_OnRefreshing(object sender, EventArgs e)
        {
            /*actualizar();*/
        }
        
        private async void MenuItemEliminar_OnClicked(object sender, EventArgs e)
        {
            /*
            try
            {


                var mi = (MenuItem)sender;


                usuarioClase usuarioSeleccionado = (usuarioClase)mi.CommandParameter;

                if (!datosGlobales.miUsuario.rango.StartsWith("delegado"))
                {
                    DisplayAlert(datosGlobales.nombreAplicacion + " - Error",
                        "Solo los delegados pueden eliminar usuarios", "Continuar");
                    return;
                }

                if (usuarioSeleccionado.dado_alta == "si")
                {
                    DisplayAlert(datosGlobales.nombreAplicacion + " - Error",
                        "El usuario ya se registro en el sistema, no puede eliminarlo", "Continuar");
                    return;
                }

                var answer = await DisplayAlert("Eliminar usuario", "Desea eliminar el companero de forma permanente?",
                    "Si", "No");
                if (answer)
                {
                    var clienteWeb = new HttpClient();
                    string peticion =
                        await clienteWeb.GetStringAsync(datosGlobales.backendUrl + "accion=descargarCompanero&mail=" +
                                                        datosGlobales.miUsuario.mail +
                                                        "&contrasena=" + datosGlobales.miUsuario.contrasena + "&id=" +
                                                        usuarioSeleccionado.id_usuario);
                    if (peticion == "ok")
                    {
                        actualizar();
                    }
                    else
                    {
                        DisplayAlert(datosGlobales.nombreAplicacion + " - Error",
                            "Ha ocurrido un error. Reintente mas tarde." + peticion, "Continuar");
                    }
                }
            }
            catch (Exception exc)
            {
                DisplayAlert(datosGlobales.nombreAplicacion + " - Error",
                            "Ha ocurrido un error. Reintente mas tarde." + exc.ToString(), "Continuar");
                datosGlobales.enviarExcepciones(exc.ToString());
            }
            */

        }

        private void CompaneroDrawerList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            /*((ListView)sender).SelectedItem = null;*/
        }

        private void SwitchUsuario_OnToggled(object sender, ToggledEventArgs e)
        {
            /*
            int cuentaHabilitados = 0;
            foreach (usuarioPedidoClase usu in companeroList)
            {
                if (usu.activado)
                {
                    cuentaHabilitados++;
                }
            }


            if (e.Value)
            {
                
 // Cuento si debo deshabilitar el resto de los switces

                if (cuentaHabilitados > usuariosMaximos)
                {

                    ((Switch)sender).IsToggled = false;
                    DisplayAlert(datosGlobales.nombreAplicacion + " - Error",
                        "No puede agregar en este pedido mas de " + usuariosMaximos +
                        " companeros. Contrate mas con la empresa,", "Continuar");


                }
            }

            actualizarContadorFooter();

    */

        }
    }
}