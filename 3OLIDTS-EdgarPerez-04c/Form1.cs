using System;
using System.Windows.Forms;
using System.IO; //Libreria para escritura y lectrura de archivos

namespace _3OLIDTS_EdgarPerez_04c
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                }
                writer.WriteLine(datos);
            }
            MessageBox.Show(datos, "Valores ingresados", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
