using AppEpgEtec.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using AppEpgEtec.Services.Usuarios;
using AppEpgEtec.Views;

namespace AppEpgEtec.ViewModels.Usuarios
{
    public class UsuarioViewModel : BaseViewModel
    {

        private UsuarioService uService;
        private Usuario Usuario;

        public ICommand EntrarCommand { get; set; }

        public void RegistrarCommands()
        {
            EntrarCommand = new Command(async () => { await ConsultarUsuario(); });
        }

        public UsuarioViewModel()
        {
            this.Usuario = new Usuario();
            uService = new UsuarioService();
            RegistrarCommands();

        }

        public async Task ConsultarUsuario()  // Método para buscar um usuário
        {
            try
            {
                Usuario u = null;
                u = await uService.PostLoginUsuarioAsync(Usuario);

                if (!String.IsNullOrEmpty(u.Token))
                {
                    Application.Current.Properties["UsuarioId"] = u.Id;
                    Application.Current.Properties["UsuarioUsername"] = u.Username;
                    Application.Current.Properties["UsuarioPerfil"] = u.Perfil;
                    Application.Current.Properties["UsuarioToken"] = u.Token;

                    String mensagem = string.Format("Bem- vindo {0}", u.Username);
                    await Application.Current.MainPage
                        .DisplayAlert("Informação", mensagem, "Ok");

                    Application.Current.MainPage = new FlyoutMenu();
                }
                else
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Informação", "Dados incorretos: (", "Ok");
                }

            }

            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + "Detalhes: " + ex.InnerException, "Ok");

            }
        }
        #region View Login
        public String Login
        {
            get { return this.Usuario.Username; }
            set
            {
                this.Usuario.Username = value;
                onPropertyChanged();
            }
        }

        public String Senha
        {
            get { return this.Usuario.PasswordString; }
            set
            {
                this.Usuario.PasswordString = value;
                onPropertyChanged();
            }
        }
        #endregion







    }
}

