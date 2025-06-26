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

namespace Store.View
{
    public partial class Home : Form
    {
        private UsersModel usuarioActual; // user
        private System.Windows.Forms.Timer searchTimer; // time to search
        private string ultimoTextoBuscado = "";


        public Home(UsersModel usuarioData)
        {
            InitializeComponent();

            // charge data
            guna2DataGridView1.CellValueChanged += guna2DataGridView1_CellValueChanged;
            guna2DataGridView1.CurrentCellDirtyStateChanged += guna2DataGridView1_CurrentCellDirtyStateChanged;
            guna2DataGridView1.CellContentClick += guna2DataGridView1_CellContentClick;

            usuarioActual = usuarioData;
            lblBienvenida.Text = $"Bienvenido, {usuarioActual.Nombre}";
        }

        private void CargarProductosActivos()
        {
            var productos = ShowActiveProductsController.ObtenerProductosActivos();
            guna2DataGridView1.DataSource = productos;
        }

        private void CargarProductosInactivos()
        {
            var productos = ShowInactiveProductsController.ObtenerProductosInactivos();
            guna2DataGridView1.DataSource = productos;
        }

        private void Home_Load(object sender, EventArgs e)
        {

            UpdtCode.ReadOnly = true;
            GrpProductsEdit.Visible = false; // hide update container
            guna2ComboBox1.Items.Add("Pdt. Activos");
            guna2ComboBox1.Items.Add("Pdt. Inactivos");
            guna2ComboBox1.SelectedIndex = 0;

            // Estilo de encabezado de columnas
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(23, 36, 64); // #172440
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            guna2DataGridView1.EnableHeadersVisualStyles = false;

            // Estilo de las celdas
            guna2DataGridView1.DefaultCellStyle.BackColor = Color.White;
            guna2DataGridView1.DefaultCellStyle.ForeColor = Color.Gray;
            guna2DataGridView1.DefaultCellStyle.SelectionBackColor = Color.Orange;
            guna2DataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            guna2DataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Otros ajustes
            guna2DataGridView1.BackColor = Color.White;
            guna2DataGridView1.GridColor = Color.FromArgb(231, 229, 255);
            guna2DataGridView1.ReadOnly = false;
            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            guna2DataGridView1.AutoResizeColumns();
            guna2DataGridView1.AllowUserToResizeColumns = false;

            // Alineación
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            guna2DataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // scroll
            guna2DataGridView1.ScrollBars = ScrollBars.Both; // Horizontal + Vertical
            guna2DataGridView1.AllowUserToResizeColumns = true;

            guna2DataGridView1.Columns["Codigo"].Width = 150;
            guna2DataGridView1.Columns["Nombre"].Width = 150;
            guna2DataGridView1.Columns["Existencias"].Width = 150;
            guna2DataGridView1.Columns["Opcion"].Width = 150;

            guna2DataGridView1.AllowUserToResizeColumns = false;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            guna2DataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // add delete column with checkbox
            if (!guna2DataGridView1.Columns.Contains("Eliminar"))
            {
                DataGridViewCheckBoxColumn chkEliminar = new DataGridViewCheckBoxColumn();
                chkEliminar.Name = "Eliminar";
                chkEliminar.HeaderText = "Eliminar";
                chkEliminar.Width = 60;
                chkEliminar.ReadOnly = false;
                chkEliminar.TrueValue = true;
                chkEliminar.FalseValue = false;
                guna2DataGridView1.Columns.Insert(0, chkEliminar);
            }

            // Add update column with checkbox
            if (!guna2DataGridView1.Columns.Contains("Actualizar"))
            {
                DataGridViewCheckBoxColumn chkActualizar = new DataGridViewCheckBoxColumn();
                chkActualizar.Name = "Actualizar";
                chkActualizar.HeaderText = "Actualizar";
                chkActualizar.Width = 70;
                chkActualizar.ReadOnly = false;
                chkActualizar.TrueValue = true;
                chkActualizar.FalseValue = false;
                guna2DataGridView1.Columns.Insert(1, chkActualizar);
            }

            CargarProductosActivos();

            searchTimer = new System.Windows.Forms.Timer();
            searchTimer.Interval = 3000; // 3 seconds to search
            searchTimer.Tick += SearchTimer_Tick;
        }

        // timer function
        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();

            string texto = SearchBoxInput.Text.Trim();

            if (texto == ultimoTextoBuscado)
                return;

            ultimoTextoBuscado = texto;

            if (texto == "")
            {
                if (guna2ComboBox1.SelectedItem.ToString() == "Pdt. Activos")
                    CargarProductosActivos();
                else
                    CargarProductosInactivos();
            }
            else
            {
                if (guna2ComboBox1.SelectedItem.ToString() == "Pdt. Activos")
                    BuscarProductosActivos(texto);
                else
                    BuscarProductosInactivos(texto);
            }
        }

        private void BuscarProductosActivos(string termino)
        {
            var productos = ShowActiveProductsController.SearchActProducts(termino);
            guna2DataGridView1.DataSource = productos;
        }

        private void BuscarProductosInactivos(string termino)
        {
            var productos = ShowInactiveProductsController.SearchIncProducts(termino);
            guna2DataGridView1.DataSource = productos;
        }

        private void guna2GroupBox3_Click(object sender, EventArgs e)
        {
   
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedItem.ToString() == "Pdt. Activos")
            {
                CargarProductosActivos();
            } else
            {
                CargarProductosInactivos();
            }

            SearchBoxInput.Text = ""; // clean searchbox

            if (guna2ComboBox1.SelectedItem.ToString() == "Pdt. Activos")
                CargarProductosActivos();
            else
                CargarProductosInactivos();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= guna2DataGridView1.Rows.Count)
                return;

            if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                string codigo = guna2DataGridView1.Rows[e.RowIndex].Cells["Codigo"].Value.ToString();

                DialogResult confirmacion = MessageBox.Show(
                    $"¿Estás seguro que deseas eliminar el producto con código: {codigo}?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacion == DialogResult.Yes)
                {
                    bool eliminado = DeleteActiveProductsController.EliminarProducto(codigo);
                    if (eliminado)
                    {
                        MessageBox.Show("✅ Producto eliminado correctamente.");
                        CargarProductosActivos(); // refresh datagrew
                    }
                    else
                    {
                        MessageBox.Show("❌ Error al eliminar el producto.");
                    }
                }

                return;
            }

            if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "Actualizar")
            {
                DataGridViewRow fila = guna2DataGridView1.Rows[e.RowIndex];

                UpdtCode.Text = fila.Cells["Codigo"].Value.ToString();
                UpdtName.Text = fila.Cells["Nombre"].Value.ToString();
                UpdtQuantities.Text = fila.Cells["Existencias"].Value.ToString();
                UpdtOption.Text = fila.Cells["Opcion"].Value.ToString();

                GrpProductsEdit.Visible = true;
                GrpProductsEdit.BringToFront();

                // desable checkbox 
                fila.Cells["Actualizar"].Value = false;
            }

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void UpdtSaveBtn_Click(object sender, EventArgs e)
        {
            string estadoSeleccionado = guna2ComboBox1.SelectedItem.ToString();

            if (estadoSeleccionado == "Pdt. Activos")
            {
                var productoEditado = new UpdateActiveProductsModel
                {
                    Codigo = UpdtCode.Text.Trim(),
                    Nombre = UpdtName.Text.Trim(),
                    Existencias = UpdtQuantities.Text.Trim(),
                    Opcion = UpdtOption.Text.Trim()
                };

                bool actualizado = UpdateActiveProductsController.ActualizarProducto(productoEditado);

                if (actualizado)
                {
                    MessageBox.Show("✅ Producto activo actualizado correctamente.");
                    GrpProductsEdit.Visible = false;
                    CargarProductosActivos();
                }
                else
                {
                    MessageBox.Show("❌ No se pudo actualizar el producto activo.");
                }
            }
            else if (estadoSeleccionado == "Pdt. Inactivos")
            {
                var productoInactivo = new UpdateInactiveProductsModel
                {
                    Codigo = UpdtCode.Text.Trim(),
                    Nombre = UpdtName.Text.Trim(),
                    Existencias = UpdtQuantities.Text.Trim(),
                    Opcion = UpdtOption.Text.Trim()
                };

                bool actualizado = UpdateInactiveProductsController.ActualizarProductoInactivo(productoInactivo);

                if (actualizado)
                {
                    MessageBox.Show("✅ Producto inactivo actualizado correctamente.");
                    GrpProductsEdit.Visible = false;
                    CargarProductosInactivos();
                }
                else
                {
                    MessageBox.Show("❌ No se pudo actualizar el producto inactivo.");
                }
            }
        }

        private void UpdtCancelBtn_Click(object sender, EventArgs e)
        {
            GrpProductsEdit.Visible = false; // hide it again
        }

        private void guna2DataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (guna2DataGridView1.IsCurrentCellDirty && guna2DataGridView1.CurrentCell is DataGridViewCheckBoxCell)
            {
                string columnName = guna2DataGridView1.Columns[guna2DataGridView1.CurrentCell.ColumnIndex].Name;

                if (columnName == "Eliminar" || columnName == "Actualizar")
                {
                    guna2DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }

        private void guna2DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var column = guna2DataGridView1.Columns[e.ColumnIndex].Name;
            DataGridViewRow fila = guna2DataGridView1.Rows[e.RowIndex];

            if (column == "Eliminar")
            {
                bool eliminar = Convert.ToBoolean(fila.Cells["Eliminar"].Value);
                if (eliminar)
                {
                    string codigo = fila.Cells["Codigo"].Value.ToString();
                    DialogResult confirmacion = MessageBox.Show(
                        $"¿Estás seguro que deseas eliminar el producto con código: {codigo}?",
                        "Confirmar eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (confirmacion == DialogResult.Yes)
                    {
                        bool eliminado = DeleteActiveProductsController.EliminarProducto(codigo);
                        if (eliminado)
                        {
                            MessageBox.Show("✅ Producto eliminado correctamente.");
                            CargarProductosActivos();
                        }
                        else
                        {
                            MessageBox.Show("❌ Error al eliminar el producto.");
                            fila.Cells["Eliminar"].Value = false;
                        }
                    }
                    else
                    {
                        fila.Cells["Eliminar"].Value = false;
                    }
                }
            }
            else if (column == "Actualizar")
            {
                bool actualizar = Convert.ToBoolean(fila.Cells["Actualizar"].Value);
                if (actualizar)
                {
                    MessageBox.Show("Se ejecutó actualizar");

                    // Cargar datos al formulario
                    UpdtCode.Text = fila.Cells["Codigo"].Value.ToString();
                    UpdtName.Text = fila.Cells["Nombre"].Value.ToString();
                    UpdtQuantities.Text = fila.Cells["Existencias"].Value.ToString();
                    UpdtOption.Text = fila.Cells["Opcion"].Value.ToString();

                    GrpProductsEdit.Visible = true;

                    // Desable checkbx for only 1 action
                    fila.Cells["Actualizar"].Value = false;
                }
            }
        }

        private void GrpProductsEdit_Click(object sender, EventArgs e)
        {

        }

        private void SearchBoxInput_TextChanged(object sender, EventArgs e)
        {
            // reset timer
            searchTimer.Stop();
            searchTimer.Start();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login(); // Navigate to login form
            loginForm.Show();
            this.Hide(); // Hide home Form
        }
    }
}