using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep
{
    public class Transakcje
    {
        public int Id { get; set; }
        public Klienci Klienci { get; set; }
        public Produkty Produkty { get; set; }
        public Pracownicy Pracownicy { get; set; }
        public int IlośćKupionegoProduktu { get; set; }
        public double Cena { get; set; }
        public string StatusTransakcji { get; set; }
    }
}
