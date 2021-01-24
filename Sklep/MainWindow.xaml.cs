using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Sklep
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            using (var context = new MyContext())
            {
                
                /*var transakcje = (from st in context.Transakcjes select st);
                foreach (var transakcja in transakcje)
                {
                    context.Transakcjes.Attach(transakcja);
                    context.Transakcjes.Remove(transakcja);
                }

                var klienci = (from st in context.Kliencis select st);
                foreach (var klient in klienci)
                {
                    context.Kliencis.Attach(klient);
                    context.Kliencis.Remove(klient);
                }
                var dostawy = (from st in context.Dostawys select st);
                foreach (var dostawa in dostawy)
                {
                    context.Dostawys.Attach(dostawa);
                    context.Dostawys.Remove(dostawa);
                }
                var pracownicy = (from st in context.Pracownicys select st);
                foreach (var pracownik in pracownicy)
                {
                    context.Pracownicys.Attach(pracownik);
                    context.Pracownicys.Remove(pracownik);
                }
                context.SaveChanges();
                */
                /*
                var właściciel = new Pracownicy
                {
                    Login = "JanDrabekBoss",
                    Hasło = "Admin1",
                    Stanowisko = "Właściciel",
                    Wynagrodzenie = 1000000,
                    Email="a"
                };
                context.Pracownicys.Add(właściciel);
                var kasjerka = new Pracownicy
                {
                    Login = "PaniKasia",
                    Hasło = "PaniKasia",
                    Stanowisko = "Sprzedawca",
                    Wynagrodzenie = 2000,
                    Email="aa"
                };
                var dostawca = new Pracownicy
                {
                    Login = "PanMietek",
                    Hasło = "PanMietek",
                    Stanowisko = "Dostawca",
                    Wynagrodzenie = 3500,
                    Email="aaa"
                };
                context.Pracownicys.Add(kasjerka);
                context.Pracownicys.Add(dostawca);
                context.SaveChanges();
                
                List<Produkty> produkty = new List<Produkty>();
                var Adżwan = new Produkty{
                    Nazwa = "Adżwan",
                    Ilość = 10,
                    Cena = 3.40
                };
                produkty.Add(Adżwan);
                var Aframon = new Produkty
                {
                    Nazwa = "Aframon",
                    Ilość = 10,
                    Cena = 12.40
                };
                produkty.Add(Aframon);
                var Amchurmangowproszku = new Produkty
                {
                    Nazwa = "Amchur - mango w proszku",
                    Ilość = 10,
                    Cena = 30.40
                };
                produkty.Add(Amchurmangowproszku);
                var Annanto = new Produkty
                {
                    Nazwa = "Annanto",
                    Ilość = 10,
                    Cena = 3.45
                };
                produkty.Add(Annanto);
                var Anyż = new Produkty
                {
                    Nazwa = "Anyż",
                    Ilość = 10,
                    Cena = 2.20
                };
                produkty.Add(Anyż);
                var Asafetida = new Produkty
                {
                    Nazwa = "Asafetida",
                    Ilość = 10,
                    Cena = 5
                };
                produkty.Add(Asafetida);
                var Bazylia = new Produkty
                {
                    Nazwa = "Bazylia",
                    Ilość = 10,
                    Cena = 3.60
                };
                produkty.Add(Bazylia);
                var Boldoboldyna = new Produkty
                {
                    Nazwa = "Boldo (boldyna)",
                    Ilość = 10,
                    Cena = 33.40
                };
                produkty.Add(Boldoboldyna);
                var Cebula = new Produkty
                {
                    Nazwa = "Cebula",
                    Ilość = 10,
                    Cena = 0.40
                };
                produkty.Add(Cebula);
                var Chili = new Produkty
                {
                    Nazwa = "Chili",
                    Ilość = 10,
                    Cena = 4.40
                };
                produkty.Add(Chili);
                var Chrzan = new Produkty
                {
                    Nazwa = "Chrzan",
                    Ilość = 10,
                    Cena = 1.40
                };
                produkty.Add(Chrzan);
                var Curryliście = new Produkty
                {
                    Nazwa = "Curry liście",
                    Ilość = 10,
                    Cena = 6.40
                };
                produkty.Add(Curryliście);
                var Cynamon = new Produkty
                {
                    Nazwa = "Cynamon",
                    Ilość = 10,
                    Cena = 3.70
                };
                produkty.Add(Cynamon);
                var Cynamonowiecwonnykasja = new Produkty
                {
                    Nazwa = "Cynamonowiec wonny (kasja)",
                    Ilość = 10,
                    Cena = 5.50
                };
                produkty.Add(Cynamonowiecwonnykasja);
                var Cząber = new Produkty
                {
                    Nazwa = "Cząber",
                    Ilość = 10,
                    Cena = 5.80
                };
                produkty.Add(Cząber);
                var Czarnuszka = new Produkty
                {
                    Nazwa = "Czarnuszka",
                    Ilość = 10,
                    Cena = 4.50
                };
                produkty.Add(Czarnuszka);
                var Czosnek = new Produkty
                {
                    Nazwa = "Czosnek",
                    Ilość = 10,
                    Cena = 2.70
                };
                produkty.Add(Czosnek);
                var CzosnekNiedzwiedzi = new Produkty
                {
                    Nazwa = "Czosnek Niedzwiedzi",
                    Ilość = 10,
                    Cena = 4.80
                };
                produkty.Add(CzosnekNiedzwiedzi);
                var Czubrica = new Produkty
                {
                    Nazwa = "Czubrica",
                    Ilość = 10,
                    Cena = 6.20
                };
                produkty.Add(Czubrica);
                var Dzięgielarcydzięgiel = new Produkty
                {
                    Nazwa = "Dzięgiel, arcydzięgiel",
                    Ilość = 10,
                    Cena = 13.40
                };
                produkty.Add(Dzięgielarcydzięgiel);
                var Estragon = new Produkty
                {
                    Nazwa = "Estragon",
                    Ilość = 10,
                    Cena = 10.20
                };
                produkty.Add(Estragon);
                var Galangal = new Produkty
                {
                    Nazwa = "Galangal",
                    Ilość = 10,
                    Cena = 1.30
                };
                produkty.Add(Galangal);
                var Gałkamuszkatałowa = new Produkty
                {
                    Nazwa = "Gałka muszkatałowa",
                    Ilość = 10,
                    Cena = 10.10
                };
                produkty.Add(Gałkamuszkatałowa);
                var Gorczyca = new Produkty
                {
                    Nazwa = "Gorczyca",
                    Ilość = 10,
                    Cena = 2.10
                };
                produkty.Add(Gorczyca);
                var Goździki = new Produkty
                {
                    Nazwa = "Goździki",
                    Ilość = 10,
                    Cena = 5.70
                };
                produkty.Add(Goździki);
                var Hibiskus = new Produkty
                {
                    Nazwa = "Hibiskus",
                    Ilość = 10,
                    Cena = 8.40
                };
                produkty.Add(Hibiskus);
                var Hyzop = new Produkty
                {
                    Nazwa = "Hyzop",
                    Ilość = 10,
                    Cena = 9.30
                };
                produkty.Add(Hyzop);
                var Imbir = new Produkty
                {
                    Nazwa = "Imbir",
                    Ilość = 10,
                    Cena = 4.40
                };
                produkty.Add(Imbir);
                var Jałowiec = new Produkty
                {
                    Nazwa = "Jałowiec",
                    Ilość = 10,
                    Cena = 11.40
                };
                produkty.Add(Jałowiec);
                var Kapary = new Produkty
                {
                    Nazwa = "Kapary",
                    Ilość = 10,
                    Cena = 3.10
                };
                produkty.Add(Kapary);
                var Kardamon = new Produkty
                {
                    Nazwa = "Kardamon",
                    Ilość = 10,
                    Cena = 4.90
                };
                produkty.Add(Kardamon);
                var Kminrzymskikumin = new Produkty
                {
                    Nazwa = "Kmin rzymski (kumin)",
                    Ilość = 10,
                    Cena = 3.40
                };
                produkty.Add(Kminrzymskikumin);
                var Kminek = new Produkty
                {
                    Nazwa = "Kminek",
                    Ilość = 10,
                    Cena = 3.20
                };
                produkty.Add(Kminek);
                var Kminekczarny = new Produkty
                {
                    Nazwa = "Kminek czarny",
                    Ilość = 10,
                    Cena = 3.80
                };
                produkty.Add(Kminekczarny);
                var Kokum = new Produkty
                {
                    Nazwa = "Kokum",
                    Ilość = 10,
                    Cena = 5.40
                };
                produkty.Add(Kokum);
                var Kolendra = new Produkty
                {
                    Nazwa = "Kolendra",
                    Ilość = 10,
                    Cena = 2.70
                };
                produkty.Add(Kolendra);
                var Komosapiżmowa = new Produkty
                {
                    Nazwa = "Komosa piżmowa",
                    Ilość = 10,
                    Cena = 5.20
                };
                produkty.Add(Komosapiżmowa);
                var KoperwłoskiFenkuł = new Produkty
                {
                    Nazwa = "Koper włoski - Fenkuł",
                    Ilość = 10,
                    Cena = 3.50
                };
                produkty.Add(KoperwłoskiFenkuł);
                var Koperkoperek = new Produkty
                {
                    Nazwa = "Koper, koperek",
                    Ilość = 10,
                    Cena = 3.90
                };
                produkty.Add(Koperkoperek);
                var Kozieradka = new Produkty
                {
                    Nazwa = "Kozieradka",
                    Ilość = 10,
                    Cena = 31.40
                };
                produkty.Add(Kozieradka);
                var Kurkuma = new Produkty
                {
                    Nazwa = "Kurkuma",
                    Ilość = 10,
                    Cena = 6.60
                };
                produkty.Add(Kurkuma);
                var KurkumaBiałaPlamista = new Produkty
                {
                    Nazwa = "Kurkuma Biała (Plamista)",
                    Ilość = 10,
                    Cena = 7.40
                };
                produkty.Add(KurkumaBiałaPlamista);
                var Lawenda = new Produkty
                {
                    Nazwa = "Lawenda",
                    Ilość = 10,
                    Cena = 8.70
                };
                produkty.Add(Lawenda);
                var Lippiatrójlistnawerbenacytrynowa = new Produkty
                {
                    Nazwa = "Lippia trójlistna (werbena cytrynowa)",
                    Ilość = 10,
                    Cena = 3.00
                };
                produkty.Add(Lippiatrójlistnawerbenacytrynowa);
                var Liśćlaurowy = new Produkty
                {
                    Nazwa = "Liść laurowy",
                    Ilość = 10,
                    Cena = 20.00
                };
                produkty.Add(Liśćlaurowy);
                var Lubczyk = new Produkty
                {
                    Nazwa = "Lubczyk",
                    Ilość = 10,
                    Cena = 3.40
                };
                produkty.Add(Lubczyk);
                var Lukrecja = new Produkty
                {
                    Nazwa = "Lukrecja",
                    Ilość = 10,
                    Cena = 4.40
                };
                produkty.Add(Lukrecja);
                var MacisKwiatmuszkatołowy = new Produkty
                {
                    Nazwa = "Macis - Kwiat muszkatołowy",
                    Ilość = 10,
                    Cena = 6.40
                };
                produkty.Add(MacisKwiatmuszkatołowy);
                var Mahlab = new Produkty
                {
                    Nazwa = "Mahlab",
                    Ilość = 10,
                    Cena = 9.40
                };
                produkty.Add(Mahlab);
                var Majeranek = new Produkty
                {
                    Nazwa = "Majeranek",
                    Ilość = 10,
                    Cena = 3.10
                };
                produkty.Add(Majeranek);
                var Mak = new Produkty
                {
                    Nazwa = "Mak",
                    Ilość = 10,
                    Cena = 3.50
                };
                produkty.Add(Mak);
                var Mastyks = new Produkty
                {
                    Nazwa = "Mastyks",
                    Ilość = 10,
                    Cena = 3.90
                };
                produkty.Add(Mastyks);
                var Melisa = new Produkty
                {
                    Nazwa = "Melisa",
                    Ilość = 10,
                    Cena = 4.20
                };
                produkty.Add(Melisa);
                var Mięta = new Produkty
                {
                    Nazwa = "Mięta",
                    Ilość = 10,
                    Cena = 3.10
                };
                produkty.Add(Mięta);
                var Mirt = new Produkty
                {
                    Nazwa = "Mirt",
                    Ilość = 10,
                    Cena = 2.40
                };
                produkty.Add(Mirt);
                var Mydlinica = new Produkty
                {
                    Nazwa = "Mydlinica",
                    Ilość = 10,
                    Cena = 6.70
                };
                produkty.Add(Mydlinica);
                var NasionaakacjiaustralijskiejWattleseed = new Produkty
                {
                    Nazwa = "Nasiona akacji australijskiej (Wattleseed)",
                    Ilość = 10,
                    Cena = 9.20
                };
                produkty.Add(NasionaakacjiaustralijskiejWattleseed);
                var Nasionaselera = new Produkty
                {
                    Nazwa = "Nasiona selera",
                    Ilość = 10,
                    Cena = 2.10
                };
                produkty.Add(Nasionaselera);
                var Oregano = new Produkty
                {
                    Nazwa = "Oregano",
                    Ilość = 10,
                    Cena = 1.40
                };
                produkty.Add(Oregano);
                var PapedaKaffirLimonkaLiście = new Produkty
                {
                    Nazwa = "Papeda (Kaffir Limonka - Liście)",
                    Ilość = 10,
                    Cena = 5.40
                };
                produkty.Add(PapedaKaffirLimonkaLiście);
                var Papryczkapeperoni = new Produkty
                {
                    Nazwa = "Papryczka peperoni",
                    Ilość = 10,
                    Cena = 5.20
                };
                produkty.Add(Papryczkapeperoni);
                var Papryka = new Produkty
                {
                    Nazwa = "Papryka",
                    Ilość = 10,
                    Cena = 3.10
                };
                produkty.Add(Papryka);
                var PaprykaAleppo = new Produkty
                {
                    Nazwa = "Papryka Aleppo",
                    Ilość = 10,
                    Cena = 6.40
                };
                produkty.Add(PaprykaAleppo);
                var Pieprz = new Produkty
                {
                    Nazwa = "Pieprz",
                    Ilość = 10,
                    Cena = 1.00
                };
                produkty.Add(Pieprz);
                var PieprzKajeńskiCayenne = new Produkty
                {
                    Nazwa = "Pieprz Kajeński (Cayenne)",
                    Ilość = 10,
                    Cena = 2.40
                };
                produkty.Add(PieprzKajeńskiCayenne);
                var Pieprzkubeba = new Produkty
                {
                    Nazwa = "Pieprz kubeba",
                    Ilość = 10,
                    Cena = 3.60
                };
                produkty.Add(Pieprzkubeba);
                var PieprzSyczuański = new Produkty
                {
                    Nazwa = "Pieprz Syczuański",
                    Ilość = 10,
                    Cena = 3.70
                };
                produkty.Add(PieprzSyczuański);
                var Pietruszka = new Produkty
                {
                    Nazwa = "Pietruszka",
                    Ilość = 10,
                    Cena = 2.70
                };
                produkty.Add(Pietruszka);
                var Rozmaryn = new Produkty
                {
                    Nazwa = "Rozmaryn",
                    Ilość = 10,
                    Cena = 4.60
                };
                produkty.Add(Rozmaryn);
                var Rukiewwodna = new Produkty
                {
                    Nazwa = "Rukiewwodna",
                    Ilość = 10,
                    Cena = 1.40
                };
                produkty.Add(Rukiewwodna);
                var Sasafras = new Produkty
                {
                    Nazwa = "Sasafras",
                    Ilość = 10,
                    Cena = 7.40
                };
                produkty.Add(Sasafras);
                var Sezam = new Produkty
                {
                    Nazwa = "Sezam",
                    Ilość = 10,
                    Cena = 3.00
                };
                produkty.Add(Sezam);
                var Shorama = new Produkty
                {
                    Nazwa = "Shorama",
                    Ilość = 10,
                    Cena = 8.40
                };
                produkty.Add(Shorama);
                var SiemieLniane = new Produkty
                {
                    Nazwa = "Siemie Lniane",
                    Ilość = 10,
                    Cena = 1.20
                };
                produkty.Add(SiemieLniane);
                var Sól = new Produkty
                {
                    Nazwa = "Sól",
                    Ilość = 10,
                    Cena = 0.30
                };
                produkty.Add(Sól);
                var Sumak = new Produkty
                {
                    Nazwa = "Sumak",
                    Ilość = 10,
                    Cena = 9.40
                };
                produkty.Add(Sumak);
                var ŚwiętypieprzHojaSanta = new Produkty
                {
                    Nazwa = "Święty pieprz (Hoja Santa)",
                    Ilość = 10,
                    Cena = 11.40
                };
                produkty.Add(ŚwiętypieprzHojaSanta);
                var Szafran = new Produkty
                {
                    Nazwa = "Szafran",
                    Ilość = 10,
                    Cena = 14.40
                };
                produkty.Add(Szafran);
                var Szałwia = new Produkty
                {
                    Nazwa = "Szałwia",
                    Ilość = 10,
                    Cena = 5.40
                };
                produkty.Add(Szałwia);
                var Szczaw = new Produkty
                {
                    Nazwa = "Szczaw",
                    Ilość = 10,
                    Cena = 2.50
                };
                produkty.Add(Szczaw);
                var Szczypiorek = new Produkty
                {
                    Nazwa = "Szczypiorek",
                    Ilość = 10,
                    Cena = 1.10
                };
                produkty.Add(Szczypiorek);
                var Tamarynd = new Produkty
                {
                    Nazwa = "Tamarynd",
                    Ilość = 10,
                    Cena = 13.70
                };
                produkty.Add(Tamarynd);
                var TatarakKalamus = new Produkty
                {
                    Nazwa = "Tatarak, Kalamus",
                    Ilość = 10,
                    Cena = 34.90
                };
                produkty.Add(TatarakKalamus);
                var Trawacytrynowa = new Produkty
                {
                    Nazwa = "Trawa cytrynowa",
                    Ilość = 10,
                    Cena = 4.60
                };
                produkty.Add(Trawacytrynowa);
                var Trybula = new Produkty
                {
                    Nazwa = "Trybula",
                    Ilość = 10,
                    Cena = 5.10
                };
                produkty.Add(Trybula);
                var Tymianek = new Produkty
                {
                    Nazwa = "Tymianek",
                    Ilość = 10,
                    Cena = 3.40
                };
                produkty.Add(Tymianek);
                var Wanilia = new Produkty
                {
                    Nazwa = "Wanilia",
                    Ilość = 10,
                    Cena = 2.30
                };
                produkty.Add(Wanilia);
                var Wasabi = new Produkty
                {
                    Nazwa = "Wasabi",
                    Ilość = 10,
                    Cena = 23.40
                };
                produkty.Add(Wasabi);
                var Zieleangielskie = new Produkty
                {
                    Nazwa = "Ziele angielskie",
                    Ilość = 10,
                    Cena = 1.20
                };
                produkty.Add(Zieleangielskie);
                context.Produktys.AddRange(produkty);
                context.SaveChanges();
                */
                InitializeComponent();
            }
        }
        private void Button_Rejestracja(object sender, RoutedEventArgs e)
        {
            if (DobreHasło(TBHasłoR.Text))
            {
                using (MyContext context = new MyContext())
                {
                    var istniejacyklienci = (from st in context.Kliencis
                             where st.Login == TBLoginR.Text
                             select st);
                    var istniejacypracownicy = (from st in context.Pracownicys
                             where st.Login == TBLoginR.Text
                             select st);
                    if (istniejacyklienci.Count() == 0 && istniejacypracownicy.Count() == 0)
                    {
                        var klient = new Klienci
                        {
                            Login = TBLoginR.Text,
                            Hasło = TBHasłoR.Text,
                            IlośćPieniędzy = 0
                        };
                        context.Kliencis.Add(klient);
                        context.SaveChanges();
                        MessageBox.Show("Pomyślnie zarejestrowano nowego użytkownika");
                    }
                    else
                    {
                        MessageBox.Show("Istnieje już taki użytkownik");
                    }
                }
            }
            else
            {
                MessageBox.Show("Hasło musi zawierać Wielką litere, małą literą oraz cyfrę");
            }
        }
        public bool DobreHasło(string haslo)
        {
            Regex malelitery = new Regex("[a-z]");
            Regex wielkielitery = new Regex("[A-Z]");
            Regex cyfry = new Regex("[0-9]");
            var warunek1 = (malelitery.Matches(haslo)).Count;
            var warunek3 = (wielkielitery.Matches(haslo)).Count;
            var warunek2 = (cyfry.Matches(haslo)).Count;
            if (warunek1 >= 1 && warunek2 >= 1 && warunek3 >= 1)
            {
                return true;
            }
            return false;
        }
        private void Button_Logowanie(object sender, RoutedEventArgs e)
        {
            using (MyContext context = new MyContext())
            {
                Użytkownik użytkownik = null;
                if (RBKlient.IsChecked.GetValueOrDefault())
                {
                    użytkownik = context.Kliencis.FirstOrDefault(x => x.Login == TBLogin.Text && x.Hasło == TBHasło.Password);
                }
                else
                {
                    użytkownik = context.Pracownicys.FirstOrDefault(x => x.Login == TBLogin.Text && x.Hasło == TBHasło.Password);
                }
                if (użytkownik != null)
                {
                    if (użytkownik is Klienci)
                    {
                        var window = new OknoKlienta(użytkownik as Klienci, this);
                        Hide();
                        window.Show();
                    }
                    if (użytkownik is Pracownicy)
                    {
                        var window = new OknoPracownika(użytkownik as Pracownicy, this);
                        Hide();
                        window.Show();
                    }
                }
                else
                {
                    var istniejacyklienci = (from st in context.Kliencis
                             where st.Login == TBLogin.Text
                             select st);
                    var istniejacypracownicy = (from st in context.Pracownicys
                             where st.Login == TBLogin.Text
                             select st);
                    if (istniejacyklienci.Count() == 0 && istniejacypracownicy.Count() == 0)
                    {
                        MessageBox.Show("Podano błędny login");
                    }
                    else
                    {
                        {
                            MessageBox.Show("Podano błędne hasło");
                        }
                    }
                }
            }
        }
    }
}