using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{

    public partial class Form1 : Form
    {
        //public EnlaceCassandra conexion;

        public Form1()
        {

            InitializeComponent();
            dataGridView1.DataSource = null;
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            comboBox2.Items.Add("cancion_nom");
            comboBox2.Items.Add("artista_nom");
            comboBox2.Items.Add("album_cancion");
            comboBox3.Items.Add("cancion_nom");
            comboBox3.Items.Add("artista_nom");
            comboBox3.Items.Add("album_cancion");


        }

        public void Form1_Load(object sender, EventArgs e)
        {

        }

        public void btnAdd_Click(object sender, EventArgs e)
        {

            if ( textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "")
            {
                var dateAndTime = DateTime.Now;
                var date = dateAndTime.Date;             
                int result;
                int result2;
                var playlist_id = 0;
                var yyyy_cancion = 0;
                if (int.TryParse(textBox1.Text, out result))
                {
                    playlist_id = Int32.Parse(textBox1.Text);
                    
                }
                else
                {   
                    playlist_id =20;
                    MessageBox.Show("playlist_id se guardo con el valor default de 20", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                if (int.TryParse(textBox6.Text, out result2))
                {
                    yyyy_cancion = Int32.Parse(textBox6.Text);

                }
                else
                {
                    yyyy_cancion = 2020;
                    MessageBox.Show("yyyy_cancion se guardo con el valor default de 2020", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                var nombre_playlist = textBox2.Text;
                var cancion_nom = (textBox3.Text);
                var artista_nom = textBox4.Text;
                var album_cancion = textBox5.Text;             
                var fecha_playlist = date;


                var conn = new EnlaceCassandra();
                conn.InsertaDatos(playlist_id, nombre_playlist, cancion_nom, artista_nom, album_cancion, yyyy_cancion, fecha_playlist);



                MessageBox.Show("Registro Agregado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
            }
            else
            {
                MessageBox.Show("Debe Escribir todos los datos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void btnGetAll_Click(object sender, EventArgs e)
        {

            var conn = new EnlaceCassandra();
            List<Playlistss> lst1 = new List<Playlistss>();
            lst1 = conn.Get_All();

            comboBox1.DisplayMember = "nombre_playlist";
            comboBox1.ValueMember = "playlist_id";
            comboBox1.DataSource = lst1;
            dataGridView1.DataSource = lst1;
            if (lst1.Count == 0)
            {
                MessageBox.Show("No existen Playlists", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void btnMostrar_Click(object sender, EventArgs e)
        {
           

            if (dataGridView1.DataSource != null)
            {
                string str = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                var conn = new EnlaceCassandra();
                List<Playlistss> lst1 = new List<Playlistss>();
                lst1 = conn.Get_One(str);



                dataGridView2.DataSource = lst1;
            }
            else
            {
                MessageBox.Show("Debe seleccionar la Playlist primero", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }

        private void BtnEliminarRegistro_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null && dataGridView2.DataSource != null)
            {

                string str = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string str2 = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                var conn = new EnlaceCassandra();
                if (str != str2)
                {
                    MessageBox.Show("La playlist seleccionada no coincide con la mostrada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    conn.Delete_Playlist(str);
                    MessageBox.Show("Registro Eliminado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dataGridView2.DataSource = null;
                    dataGridView1.DataSource = null;
                    comboBox1.DataSource = null;
                }
            }
            else
            {
                MessageBox.Show("Debe mostrar la Playlist primero", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEliminarDato_Click(object sender, EventArgs e)
        {

            if (dataGridView1.DataSource != null && dataGridView2.DataSource != null && comboBox2.Text != "")
            {

                string str = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string str2 = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                string str3 = comboBox2.SelectedItem.ToString();
                var conn = new EnlaceCassandra();
                if (str != str2)
                {
                    MessageBox.Show("La playlist seleccionada no coincide con la mostrada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                else
                {
                    conn.Delete_One(str, str3);
                    MessageBox.Show("Dato Eliminado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dataGridView2.DataSource = null;
                    dataGridView1.DataSource = null;
                    comboBox1.DataSource = null;
                }
            }
            else
            {
                if (comboBox2.Text == "")
                {
                    MessageBox.Show("No ha seleccionado el dato", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                if (dataGridView1.DataSource == null && dataGridView2.DataSource == null) {
                    MessageBox.Show("Debe mostrar la Playlist primero", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnModificarDato_Click_1(object sender, EventArgs e)
        {

            if (dataGridView1.DataSource != null && dataGridView2.DataSource != null && comboBox3.Text != "")
            {

                string str = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string str2 = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                string str3 = comboBox3.SelectedItem.ToString();
                var str4 = textBox10.Text;
                var conn = new EnlaceCassandra();
                if (str != str2)
                {
                    MessageBox.Show("La playlist seleccionada no coincide con la mostrada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                else
                {
                    conn.Modifica_One(str, str3, str4);
                    MessageBox.Show("Dato Modificado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dataGridView2.DataSource = null;
                    dataGridView1.DataSource = null;
                    comboBox1.DataSource = null;
                    textBox10.Text = "";
                }
            }
            else
            {
                if (comboBox3.Text == "")
                {
                    MessageBox.Show("No ha seleccionado el dato", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                if (dataGridView1.DataSource == null && dataGridView2.DataSource == null)
                {
                    MessageBox.Show("Debe mostrar la Playlist primero", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

    }
}
