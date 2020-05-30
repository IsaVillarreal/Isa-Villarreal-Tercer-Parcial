using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;
using Cassandra.Mapping;
using System.Configuration;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    class EnlaceCassandra
    {
        static private string _dbServer { set; get; }
        static private string _dbKeySpace { set; get; }
        static private Cluster _cluster;
        static private ISession _session;

        private static void conectar()
        {
            _dbServer = ConfigurationManager.AppSettings["Cluster"].ToString();
            _dbKeySpace = ConfigurationManager.AppSettings["KeySpace"].ToString();

            _cluster = Cluster.Builder()
                .AddContactPoint(_dbServer)
                .Build();

            _session = _cluster.Connect(_dbKeySpace);
        }

        private static void desconectar()
        {
            _cluster.Dispose();
        }

        public void InsertaDatos( int playlist_id, string nombre_playlist, string cancion_nom, string artista_nom, string album_cancion, int yyyy_cancion, DateTime fecha_playlist)
        {
           
            try
            {
                conectar();
                var auxiliar = fecha_playlist.Year;
                var auxiliar2 = fecha_playlist.Month;
                var auxiliar3 = fecha_playlist.Day;
               var auxiliar4 = new LocalDate(auxiliar, auxiliar2, auxiliar3);
                string qry = "insert into Playlistss( playlist_id, nombre_playlist, cancion_nom, artista_nom, album_cancion, yyyy_cancion,fecha_playlist) values( {0}, '{1}','{2}', '{3}', '{4}',{5}, '{6}');";
                qry = string.Format(qry, playlist_id, nombre_playlist, cancion_nom, artista_nom, album_cancion, yyyy_cancion, auxiliar4);

                _session.Execute(qry);
                
            }
            catch(Exception e)
            {
                throw e;   
            }
            finally
            {
                desconectar();
            }
        }

        public List<Playlistss> Get_One(string dato)
        {
            string query = "SELECT  playlist_id, nombre_playlist, cancion_nom, artista_nom, album_cancion,yyyy_cancion, fecha_playlist FROM Playlistss WHERE nombre_playlist = ?;";
            conectar();
            IMapper mapper = new Mapper(_session);
            IEnumerable<Playlistss> users = mapper.Fetch<Playlistss>(query, dato);

            desconectar();
            return users.ToList();
        }

        public void Delete_One(string dato, string dato2)
        {
            try
            {
                conectar();

                string query = "DELETE {0} FROM Playlistss WHERE nombre_playlist = '{1}';";
                query = string.Format(query, dato2, dato);

                _session.Execute(query);

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                desconectar();
            }

        }

        public void Modifica_One(string dato, string dato2, string dato3)
        {
            try
            {
                conectar();

                string query = "UPDATE Playlistss SET {0} = '{1}' WHERE nombre_playlist = '{2}';";
                query = string.Format(query, dato2,dato3, dato);

                _session.Execute(query);

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                desconectar();
            }

        }


        public void Delete_Playlist(string dato)
        {
            try
            {
                conectar();

                string query = "DELETE FROM Playlistss WHERE nombre_playlist = '{0}';";
                query = string.Format(query, dato);

                _session.Execute(query);

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                desconectar();
            }
      
        }

        public List<Playlistss> Get_All()
        {
            string query = "SELECT * FROM Playlistss;";
            conectar();
            
            IMapper mapper = new Mapper(_session);
            IEnumerable<Playlistss> users = mapper.Fetch<Playlistss>(query);

            desconectar();
            return users.ToList();
            
        }

        // Ejemplo de leer row x row
        public void GetOne()
        {
            conectar();

            string query = "SELECT  playlist_id, nombre_playlist, cancion_nom, artista_nom, album_cancion,yyyy_cancion, fecha_playlist FROM Playlistss;";

            // Execute a query on a connection synchronously 
            var rs = _session.Execute(query);
            
            // Iterate through the RowSet 
            foreach (var row in rs)
            {
               // var valueid = row.GetValue<Guid>("id_uuid");
                var value = row.GetValue<int>("playlist_id");
                // Do something with the value 
                var texto = row.GetValue<string>("nombre_playlist");
                // Do something with the value 
                var texto2 = row.GetValue<Array>("cancion_nom");
                // Do something with the value 
                var texto3 = row.GetValue<string>("artista_nom");
                // Do something with the value 
                var texto4 = row.GetValue<string>("album_cancion");
                // Do something with the value 
                var fecha1 = row.GetValue<int>("yyyy_cancion");

                var fecha2 = row.GetValue<DateTime>("fecha_playlist");
                // Do something with the value 

                MessageBox.Show(texto, value.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

           

            }
        }
 
    }
}
