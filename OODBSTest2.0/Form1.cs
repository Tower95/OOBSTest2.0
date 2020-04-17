using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Windows.Forms;

namespace OODBSTest2._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Persona> personas = new List<Persona>();
        TimeSpan TiempoInicio, TiempoFinal;
        Random Num = new Random();
        Stopwatch crono = new Stopwatch();
        PersonaDBServices PersonaDB = new PersonaDBServices();

        string[] nombres = { "CARLOS", "ARMANDO", "KARELY", "LUCIA", "CARMEN", "JUAN", "MARTIN", "ALFONSO", "RAMON", "EDUARDO", "HUGO", "ADAN", "ROSARIO", "FERNANDO", "ADRIAN", "ISABEL", "MARCELA", "ROXANA", "CESAR", "JUDITH", "JOSE", "GUADALUPE" };
        string[] apellidos = { "MARTINEZ", "MURRIETA", "MENDEZ", "GOMEZ", "ROBLEDO", "LOPEZ", "PEREZ", "VALENZUELA", "RODRIGUEZ", "IBARRA", "VEGA", "VAZQUEZ", "GOMEZ", "ESPINOZA", "CONTRERAS", "AGUILAR", "CRUZ", "ARMENTA", "GONZALEZ", "MORALES" };

        private void button1_Click(object sender, EventArgs e)
        {
            GenerarPersonas(int.Parse(textBox1.Text));
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = checkBox1.Checked;
        }
        #region Generar Personas

        private void GenerarPersonas(int cantidad)
        {
            Persona persona;
            Mensaje("Iniciando generación de personas");
            crono.Start();
            for (int x = 1; x <= cantidad; x++)
            {
                persona = new Persona();
                persona.Nombre = GenerarNombre();
                persona.Apellidos = GenerarApellidos();
                persona.Edad = GenerarEdad();
                //agregar a la lista
                personas.Add(persona);
            }
            crono.Stop();
            Mensaje("Finalizó generación de personas");
            Mensaje("Tiempo transcurrido: ", true);
            dgvPesonas.DataSource = personas;
            lblRows.Text = dgvPesonas.RowCount.ToString();

        }

        #region Funciones al azar

        private string GenerarNombre()
        {

            return nombres[Num.Next(nombres.Length)];

        }
        private string GenerarApellidos()
        {
            string ape;
            ape = apellidos[Num.Next(apellidos.Length)];
            ape = ape + " " + apellidos[Num.Next(apellidos.Length)];
            return ape;

        }

        private int GenerarEdad()
        {
            return Num.Next(18, 27);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Mensaje("Iniciando almacenamiento de objetos");
            crono.Start();
            PersonaDB.GuardarListaPersonas(personas);
            crono.Stop();
            Mensaje("Finalizó almacenamiento");
            Mensaje("Tiempo transcurrido:", true);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Enabled = checkBox2.Enabled;
        }

        private void btnAmigos_Click(object sender, EventArgs e)
        {

            Persona ObjPersona;
            int cantidad = personas.Count;
            Mensaje("Iniciando asignación de amigos");
            for (int x = 2; x < cantidad; x++)
            {
                ObjPersona = personas[x];
                ObjPersona.AgregarAmigo(personas[x - 1]);
                ObjPersona.AgregarAmigo(personas[x - 2]);

            }
            ObjPersona = personas[0];
            ObjPersona.AgregarAmigo(personas[1]);
            ObjPersona.AgregarAmigo(personas[2]);
            ObjPersona = personas[1];
            ObjPersona.AgregarAmigo(personas[3]);
            ObjPersona.AgregarAmigo(personas[4]);
            Mensaje("Finalizó asignación de amigos");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Mensaje("Iniciando actualización de objetos");
            crono.Start();
            PersonaDB.UpdateListaPersonas(personas);
            crono.Stop();
            Mensaje("Finalizó actualización");
            Mensaje("Tiempo transcurrido: ", true);
        }

        private void btnPersonas_Click(object sender, EventArgs e)
        {
            dgvPesonas.DataSource = null;
            personas.Clear();
            Mensaje("Iniciando consulta de personas");
            crono.Start();
            personas = PersonaDB.ListaPersonas();
            crono.Stop();
            dgvPesonas.DataSource = personas;
            Mensaje("Finalizó consulta");
            Mensaje("Tiempo transcurrido: ", true);
        }

        private void btnPersonasConApellido_Click(object sender, EventArgs e)
        {
            personas.Clear();
            Mensaje("Iniciando consulta de personas \npor apellido(s)");
            crono.Start();
            personas = PersonaDB.BuscarPersonaPorApellido(txtApellido.Text);
            crono.Stop();
            dgvPesonas.DataSource = personas;
            Mensaje("Finalizó consulta por apellido");
            Mensaje("Tiempo transcurrido: ", true);
        }

        private void btnPersonasConEdad_Click(object sender, EventArgs e)
        {
            personas.Clear();
            Mensaje("Iniciando consulta de personas \npor edades");
            crono.Start();
            personas = PersonaDB.BuscarPersonaPorEdad(int.Parse(txtEdad1.Text), int.Parse(txtEdad2.Text));
            crono.Stop();
            dgvPesonas.DataSource = personas;
            Mensaje("Finalizó consulta por edad");
            Mensaje("Tiempo transcurrido: ", true);
        }

        private void txtEdad1_TextChanged(object sender, EventArgs e)
        {
            txtEdad2.Text = txtEdad1.Text;
        }

        private void dgvPesonas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPesonas.DataSource != null)
            {
                //dgvAmigos.DataSource = ((Persona)dgvPesonas.SelectedRows[0].DataBoundItem).Amigos();
                dgvAmigos.DataSource = ((Persona)dgvPesonas.CurrentRow.DataBoundItem).Amigos();
            }
           
        }

        private void btnBorarDB_Click(object sender, EventArgs e)
        {
            DBServices db = new  DBServices();
            db.BorrarDB();

        }

        #endregion

        #endregion

        #region Mensaje
        private void Mensaje(string str, bool conTiempo = false)
        {
            if (conTiempo)
            {
                str = str + string.Format(" {0:00}:{1:00}:{2:00}.{3:000}",
                crono.Elapsed.Hours, crono.Elapsed.Minutes, crono.Elapsed.Seconds, crono.Elapsed.Milliseconds);
                listBox1.Items.Add(str);
            }
            else
                listBox1.Items.Add(str);

            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }
        #endregion

    }
}
