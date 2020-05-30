using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    public class Playlistss
    {
       // public Guid id_uuid { get; set; } = Guid.NewGuid();
        public int playlist_id { get; set; }
       public string  nombre_playlist { get; set; }
       public string cancion_nom { get; set; }
       public string artista_nom { get; set; }
       public string  album_cancion { get; set; }
       public int yyyy_cancion { get; set; }
        public Cassandra.LocalDate fecha_playlist { get; set; } 

    }
  
}
