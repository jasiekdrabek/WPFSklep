using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep
{
    public class Dostawy
    {
        public int Id { get; set; }
        public Pracownicy Pracownicy { get; set; }
        public Produkty Produkty { get; set; }
        public int Ilość { get; set; }
    }
}
