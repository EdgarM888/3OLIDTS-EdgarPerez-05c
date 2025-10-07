using System;
using System.IO; //Libreria para escritura y lectrura de archivos
using System.Text.RegularExpressions; // Para la validacion de formatos de textos 
using System.Windows.Forms;
using MySql.Data.MySqlClient; // Libreria de conexion a MySQL-Base de datos

namespace _3OLIDTS_EdgarPerez_04c
{
    public partial class Form1 : Form
    {
        // Datos de conexion a Mysql (Xammp)
        string conexionSQL = "Server=locashost; Port= 3306; Database=programacionavanzada; Uid=roor; Pwd=root";
        
        // Metodo para insertar registros
        public Form1()
        {
            InitializeComponent();
            // Creación de manejadores de evento
            tbNombre.TextChanged += validarNombre;
            tbApellido.TextChanged += validarApellido;
            tbEstatura.TextChanged += validarEstatura;
            tbEdad.TextChanged += validarEdad;
            tbTelefono.Leave += validarTelefono;
        }

        private void InsertarRegistro (string nombre, string apellido, int edad, decimal estatura, string telefono, string genero)
        {
            using (MySqlConnection conection = new MySqlConnection(conexionSQL))
            {
                conection.Open();
                string insertQuery = "INSERT INTO registros (Nombre, Apellido, Edad, Estatura, Telefono, Genero" +
                    "VALUES (@Nombre, @Apellido, @Edad, @Estatura, @Telefono, @Genero)";

                using (MySqlCommand command = new MySqlCommand(insertQuery, conection))
                {
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Apellido", apellido);
                    command.Parameters.AddWithValue("@Edad", edad);
                    command.Parameters.AddWithValue("@Estatura", estatura);
                    command.Parameters.AddWithValue("@Telefono", telefono);
                    command.Parameters.AddWithValue("@Genero", genero);

                    command.ExecuteNonQuery();
                }
                conection.Close();
            }
        }

        private bool EsEnteroValido(string valor)
        {
            int resultado;
            return int.TryParse(valor, out resultado);
            //return false;
        }

        private bool EsDecimalValido(string valor)
        {
            decimal resultado;
            return decimal.TryParse(valor, out resultado);
        }

        private bool EsEnteroValido10Digitos(string valor)
        {
            long resultado;
            return long.TryParse(valor, out resultado) && valor.Length == 10;
        }

        private bool EsTextoValido(string valor)
        {
            return Regex.IsMatch(valor, @"^[A-Z-a-z\s]+$");
        }

        private void validarNombre(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (!EsTextoValido(textbox.Text))
            {
                MessageBox.Show("Ingrese valroes correctos para el nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void validarApellido(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (!EsTextoValido(textbox.Text))
            {
                MessageBox.Show("Ingrese valores correctos para el apellido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void validarEstatura(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (!EsDecimalValido(textbox.Text))
            {
                MessageBox.Show("Ingrese datos validos para el estatura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void validarEdad(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (!EsDecimalValido(textbox.Text))
            {
                MessageBox.Show("Ingrese datos validos para la edad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void validarTelefono(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (!EsEnteroValido10Digitos(textbox.Text))
            {
                MessageBox.Show("Ingrese datos validos para el numero de telefono", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            tbApellido.Clear();
            tbNombre.Clear();
            tbTelefono.Clear();
            tbEstatura.Clear();
            tbEdad.Clear();
            rbMasculino.Checked = false;
            rbMasculino.Checked = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string apellido = tbApellido.Text;
            string nombre = tbNombre.Text;
            string telefono = tbTelefono.Text;
            string estatura = tbEstatura.Text;
            string edad = tbEdad.Text;
            string genero = "";
            if (rbFemenino.Checked)
            {
                genero = "Femenino";
            }
            else if (rbMasculino.Checked)
            {
                genero = "Masculino";
            }
            string datos = $"Nombre: {nombre}\r\nApellido: {apellido}\r\n" +
                $"Telefono: {telefono}\r\nEstatura: {estatura}\r\n" +
                $"Edad: {edad}\r\nGenero: {genero}\r\n";
            
            //Guardado del archivo 

            //  string ruta = "C:/Users/HUAWEI/Documents/Semestre 3/Programación avanzada.txt"
            //  string ruta = @"C:\Users\HUAWEI\Documents\Semestre 3\Programación avanzada";
            string ruta = "C:\\Users\\HUAWEI\\Documents\\Semestre 3\\3OLIDTS2025.txt";
            bool archivoExiste = File.Exists(ruta);
            using (StreamWriter writer = new StreamWriter(ruta, true))
            {
                if (archivoExiste)
                {
                    writer.WriteLine();
                    // Programacion de funcionalidad de insert SQL
                    InsertarRegistro(nombre, apellido, int.Parse(edad), decimal.Parse(estatura), estatura, genero);
                    MessageBox.Show("Dato ingresados correctamente");
                }
                writer.WriteLine(datos);
            }
            MessageBox.Show(datos, "Valores ingresados", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
