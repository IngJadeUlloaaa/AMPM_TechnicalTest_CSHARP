using Store.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Store.Controller;
using Store.Model;

namespace Store
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string usuario = UserInput.Text.Trim();
            string contrasena = PasswordInput.Text.Trim();

            if (LoginController.ValidarUsuario(usuario, contrasena, out UsersModel usuarioData))
            {
                MessageBox.Show($"Bienvenido, {usuarioData.Nombre} {usuarioData.Apellido}");
                this.Hide();
                Home homeForm = new Home(usuarioData);
                homeForm.Show();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
            }
        }

        private void UserInput_TextChanged(object sender, EventArgs e)
        {
           // string user = UserInput.Text.Trim();
        }

        private void PasswordInput_TextChanged(object sender, EventArgs e)
        {
            //string password = PasswordInput.Text.Trim();
        }

        private void NoAccountBtn_Click(object sender, EventArgs e)
        {
            NewPeople newPeopleHome = new NewPeople(); // Navigate to New People Form
            newPeopleHome.Show();
            this.Hide(); // Hide Login Form
        }
    }
}
