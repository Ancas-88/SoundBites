using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Voto
    {
        public int IdVoto { get; set; }
        public int IdUsuario { get; set; }
        public int IdPlato { get; set; }
        public int Valor { get; set; }
    }
}
