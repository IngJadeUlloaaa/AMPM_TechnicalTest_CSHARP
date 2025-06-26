using Store.Controller;
using Store.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Store.View
{
    public partial class NewPeople : Form
    {
        public NewPeople()
        {
            InitializeComponent();
        }

        private void BackToLogin_Click(object sender, EventArgs e)
        {
            Login backToLogin = new Login(); // Back to Login
            backToLogin.Show();
            this.Hide(); // Hide New People Form
        }

        private void SendNewUserData_Click(object sender, EventArgs e)
        {
            string nombre = NameRegisterInput.Text.Trim();
            string correo = MailRegisterInput.Text.Trim();
            string telefono = PhoneRegisterInput.Text.Trim();

            // Validar correo
            string patronCorreo = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(correo, patronCorreo))
            {
                MessageBox.Show("El correo ingresado no es válido. Ejemplo: juan@gmail.com");
                return;
            }

            // Validar teléfono: debe tener formato 0000-0000
            string patronTelefono = @"^\d{4}-\d{4}$";
            if (!Regex.IsMatch(telefono, patronTelefono))
            {
                MessageBox.Show("El número de teléfono debe tener el formato 0000-0000");
                return;
            }

            // Crear modelo y registrar
            RegisterModel nuevo = new RegisterModel
            {
                Nombre = nombre,
                Correo = correo,
                Telefono = telefono
            };

            if (RegisterController.RegistrarUsuarioBasico(nuevo))
            {
                MessageBox.Show("Usuario registrado correctamente.");
                this.Close();
                Login backToLogin = new Login(); // Back to Login
                backToLogin.Show();

            }
            else
            {
                MessageBox.Show("Error al registrar usuario.");
            }
        }
    }
}
