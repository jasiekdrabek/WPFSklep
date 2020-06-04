using CsvHelper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sklep
{
    /// <summary>
    /// Interaction logic for OknoPracownika.xaml
    /// </summary>
    public partial class OknoPracownika : Window
    {
        public ObservableCollection<Transakcje> Zamówienia { get; set; }
        public ObservableCollection<Produkty> ProduktyWSklepie { get; set; }
        public MainWindow Okno { get; set; }
        public Pracownicy Użytkownik { get; set; }
        public List<int> ListaSztuk { get; set; }
        public List<string> ListaNazw { get; set; }
        public List<CheckBox> ListaZamówionych { get; set; }
        public OknoPracownika(Pracownicy pracownicy, MainWindow window)
        {
            InitializeComponent();
            DataContext = this;
            Zamówienia = new ObservableCollection<Transakcje>();
            ProduktyWSklepie = new ObservableCollection<Produkty>();
            ListaZamówionych = new List<CheckBox>();
            ListaSztuk = new List<int>();
            ListaNazw = new List<string>();
            CBDostawa.SelectedIndex = 0;
            LBZamówienia.ItemsSource = Zamówienia;
            LWSklep.ItemsSource = ProduktyWSklepie;
            LWSklep1.ItemsSource = ProduktyWSklepie;
            Użytkownik = pracownicy;
            Okno = window;
            Okno.TBHasło.Password = "";
            Okno.TBHasłoR.Text = "";
            Okno.TBLogin.Text = "";
            Okno.TBLoginR.Text = "";
            LInfo.Content += Użytkownik.Login;
            using (var context = new MyContext())
            {
                var transakcjewtrakcie = context.Transakcjes.Where(x => x.StatusTransakcji == "W trakcie realizacji");
                foreach (var transakcja in transakcjewtrakcie)
                {
                    transakcja.Klienci = context.Kliencis.FirstOrDefault(x => x.Id == context.Transakcjes.FirstOrDefault(y => y.Id == transakcja.Id).Klienci.Id);
                    transakcja.Produkty = context.Produktys.FirstOrDefault(x => x.Id == context.Transakcjes.FirstOrDefault(y => y.Id == transakcja.Id).Produkty.Id);
                    Zamówienia.Add(transakcja);
                }
                var produkty = (from st in context.Produktys
                         select st);
                foreach (var produkt in produkty)
                {
                    ProduktyWSklepie.Add(produkt);

                    var item = new ComboBoxItem
                    {
                        Content = ProduktyWSklepie.Last().Nazwa + " (" + ProduktyWSklepie.Last().Ilość + ")"
                    };
                    CBDostawa.Items.Add(item);
                }
                var pracownicyaleniewlasciciel = (from st in context.Pracownicys where st.Stanowisko != "Właściciel" select st);
                foreach (var pracownik in pracownicyaleniewlasciciel)
                {
                    var item = new ComboBoxItem
                    {
                        Content = pracownik.Login
                    };
                    CBZwolnij.Items.Add(item);
                }
            }
            CBZwolnij.SelectedIndex = 0;
            if (Użytkownik.Stanowisko == "Sprzedawca")
            {
                TIObsługaDostaw.Visibility = Visibility.Collapsed;
            }
            if (Użytkownik.Stanowisko == "Dostawca")
            {
                TIObsługaZamówień.Visibility = Visibility.Collapsed;
            }
            if (Użytkownik.Stanowisko != "Właściciel")
            {
                GNieDlaZwykłychPracowników.Visibility = Visibility.Collapsed;
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Close();
            Okno.Show();
        }
        private void Button_Zmień_Hasło(object sender, RoutedEventArgs e)
        {
            if (TBStareHasło.Text == Użytkownik.Hasło)
            {
                if (Okno.DobreHasło(TBNoweHasło.Text))
                {
                    Użytkownik.Hasło = TBNoweHasło.Text;
                    using (var context = new MyContext())
                    {
                        var klient = context.Pracownicys.First(x => x.Login == Użytkownik.Login);
                        klient.Hasło = Użytkownik.Hasło;
                        context.SaveChanges();
                    }
                }
                else
                {
                    MessageBox.Show("Nowe hasło musi zawierać małą literę,wielką literę oraz cyfrę");
                }
            }
            else
            {
                MessageBox.Show("Podane hasło nie jest prawidłowe");
            }
        }
        private void Button_Zrealizuj(object sender, RoutedEventArgs e)
        {
            try
            {
                var wybranezamowienie = Zamówienia[LBZamówienia.SelectedIndex];
                ObservableCollection<Produkty> temp = new ObservableCollection<Produkty>();
                foreach (var p in ProduktyWSklepie)
                {
                    temp.Add(p);
                }
                ProduktyWSklepie.Clear();
                using (var context = new MyContext())
                {
                    if (context.Produktys.FirstOrDefault(x => x.Id == wybranezamowienie.Produkty.Id).Ilość >=
                        context.Transakcjes.FirstOrDefault(x => x.Id == wybranezamowienie.Id).IlośćKupionegoProduktu)
                    {
                        if (context.Kliencis.FirstOrDefault(x => x.Id == wybranezamowienie.Klienci.Id).IlośćPieniędzy >=
                        context.Transakcjes.FirstOrDefault(y => y.Id == wybranezamowienie.Id).Cena)
                        {
                            var produkt = context.Produktys.FirstOrDefault(x => x.Id == wybranezamowienie.Produkty.Id);
                            produkt.Ilość -= context.Transakcjes.FirstOrDefault(x => x.Id == wybranezamowienie.Id).IlośćKupionegoProduktu;
                            var klient = context.Kliencis.FirstOrDefault(x => x.Id == wybranezamowienie.Klienci.Id);
                            klient.IlośćPieniędzy -= context.Transakcjes.FirstOrDefault(x => x.Id == wybranezamowienie.Id).Cena;
                            var transakcja = context.Transakcjes.FirstOrDefault(x => x.Id == wybranezamowienie.Id);
                            transakcja.Pracownicy = context.Pracownicys.First(x => x.Id == Użytkownik.Id);
                            transakcja.StatusTransakcji = "Zrealizowana";
                            context.SaveChanges();
                            temp[context.Produktys.FirstOrDefault(x => x.Id == wybranezamowienie.Produkty.Id).Id - 1].Ilość = produkt.Ilość;
                            foreach (var t in temp)
                            {
                                ProduktyWSklepie.Add(t);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Klient nie ma tyle pieniędzy");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nie mamy tyle sztuk tego produktu");
                    }
                }
                Zamówienia.Remove(wybranezamowienie);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Odrzuć(object sender, RoutedEventArgs e)
        {
            try
            {
                var wybranezamowienie = Zamówienia[LBZamówienia.SelectedIndex];
                using (var context = new MyContext())
                {
                    var transakcja = context.Transakcjes.FirstOrDefault(x => x.Id == wybranezamowienie.Id);
                    transakcja.Pracownicy = context.Pracownicys.First(x => x.Id == Użytkownik.Id);
                    transakcja.StatusTransakcji = "Odrzucona";
                    context.SaveChanges();
                }
                Zamówienia.Remove(wybranezamowienie);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Dodaj_Do_dostawy(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new MyContext())
                {
                    var produkt = context.Produktys.First(x => x.Id == CBDostawa.SelectedIndex + 1);
                    int ilość = int.Parse(TBDostawa.Text);
                    if (0 < ilość)
                    {
                        var check = new CheckBox
                        {
                            Content = produkt.Nazwa + "Do zapłaty: " + (ilość * produkt.Cena / 2).ToString()
                        };
                        ListaZamówionych.Add(check);
                        ListaSztuk.Add(ilość);
                        ListaNazw.Add(produkt.Nazwa);
                        SPDostawa.Children.Add(check);

                    }
                    else
                    {
                        MessageBox.Show("Głupi czy co?");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Usuń_Z_Dostawy(object sender, RoutedEventArgs e)
        {
            SPDostawa.Children.Clear();
            for (int i = 0; i < ListaZamówionych.Count(); i++)
            {
                if (ListaZamówionych[i].IsChecked.GetValueOrDefault())
                {
                    ListaZamówionych.Remove(ListaZamówionych[i]);
                    ListaSztuk.Remove(ListaSztuk[i]);
                    ListaNazw.Remove(ListaNazw[i]);
                    i -= 1;
                }
                else
                {
                    SPDostawa.Children.Add(ListaZamówionych[i]);
                }
            }
        }
        private void Button_Zrób_Dostawę(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Produkty> temp = new ObservableCollection<Produkty>();
            foreach (var p in ProduktyWSklepie)
            {
                temp.Add(p);
            }

            ProduktyWSklepie.Clear();
            using (var context = new MyContext())
            {
                for (int i = 0; i < ListaSztuk.Count(); i++)
                {
                    string nazwa = ListaNazw[i];
                    var dostawa = new Dostawy
                    {
                        Ilość = ListaSztuk[i],
                        Pracownicy = context.Pracownicys.FirstOrDefault(x => x.Id == Użytkownik.Id),
                        Produkty = context.Produktys.FirstOrDefault(x => x.Nazwa == nazwa)
                    };
                    context.Dostawys.Add(dostawa);
                    var produkt = context.Produktys.FirstOrDefault(x => x.Nazwa == nazwa);
                    produkt.Ilość += ListaSztuk[i];
                    temp.FirstOrDefault(x => x.Id == produkt.Id).Ilość += ListaSztuk[i];
                    context.SaveChanges();
                    CBDostawa.Items[produkt.Id - 1] = temp[produkt.Id - 1].Nazwa + " (" + temp[produkt.Id - 1].Ilość + ")";
                }
            }
            foreach (var t in temp)
            {
                ProduktyWSklepie.Add(t);
            }
            CBDostawa.SelectedIndex = 0;
            SPDostawa.Children.Clear();
        }
        private void Button_Zatrudnij(object sender, RoutedEventArgs e)
        {
            using(var context = new MyContext())
            {
                var pracownicy = (from st in context.Pracownicys where st.Login == TBZatrudnij.Text select st);
                if (pracownicy.Count() == 0)
                {
                    var pracownik = new Pracownicy
                    {
                        Login = TBZatrudnij.Text,
                        Hasło = TBZatrudnij.Text,
                        Stanowisko = CBZatrudnij.Text,
                        Wynagrodzenie = 2000
                    };
                    context.Pracownicys.Add(pracownik);
                    context.SaveChanges();
                    var item = new ComboBoxItem
                    {
                        Content = TBZatrudnij.Text
                    };
                    CBZwolnij.Items.Add(item);
                }
                else
                {
                    MessageBox.Show("Ten pracownik już u nas pracuje");
                }
            }
        }
        private void Button_Zwolnij(object sender, RoutedEventArgs e)
        {
            MessageBoxButton button = MessageBoxButton.YesNo;
            string messageBoxText = "Czy na pewno chcesz usunąć konto?";
            MessageBoxResult result = MessageBox.Show(messageBoxText, null, button);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    using (var context = new MyContext())
                    {
                        context.Pracownicys.Remove(context.Pracownicys.FirstOrDefault(a => a.Id == 
                        context.Pracownicys.FirstOrDefault(y=> y.Login == CBZwolnij.Text).Id));
                        context.SaveChanges();
                    }
                    CBZwolnij.Items.Remove(CBZwolnij.SelectedItem);
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
        private void Button_Exportuj(object sender, RoutedEventArgs e)
        {
            SaveFileDialog plikdozapisu = new SaveFileDialog();
            plikdozapisu.Filter = "Plik csv(*.csv)|*.csv";
            if (plikdozapisu.ShowDialog() == true)
            {
                using (var context = new MyContext())
                {
                    var danedozapisu = (from st in context.Transakcjes  select st);
                    foreach (var transakcja in danedozapisu)
                    {
                        transakcja.Pracownicy = context.Pracownicys.FirstOrDefault(x => x.Id == context.Transakcjes.FirstOrDefault(y => y.Id == transakcja.Id).Pracownicy.Id);
                        transakcja.Klienci = context.Kliencis.FirstOrDefault(x => x.Id == context.Transakcjes.FirstOrDefault(y => y.Id == transakcja.Id).Klienci.Id);
                        transakcja.Produkty = context.Produktys.FirstOrDefault(x => x.Id == context.Transakcjes.FirstOrDefault(y => y.Id == transakcja.Id).Produkty.Id);
                        int len = transakcja.Klienci.Hasło.Length;
                        transakcja.Klienci.Hasło = "";
                        for (int i = 0; i < len; i++)
                        {
                            transakcja.Klienci.Hasło += "*";
                        }
                        if (transakcja.Pracownicy != null)
                        {
                            len = transakcja.Pracownicy.Hasło.Length;
                            transakcja.Pracownicy.Hasło = "";
                            for (int i = 0; i < len; i++)
                            {
                                transakcja.Pracownicy.Hasło += "*";
                            }
                        }
                    }
                    /*var danedozapisu = (from st in context.Kliencis select st);
                    foreach(var klient in danedozapisu)
                    {
                        int len = klient.Hasło.Length;
                        klient.Hasło = "";
                        for (int i = 0; i < len; i++)
                        {
                            klient.Hasło += "*";
                        }
                    }*/
                    using (var w = new StreamWriter(plikdozapisu.FileName))
                    using (var csv = new CsvWriter(w, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(danedozapisu);
                    }
                }
            }
        }
        private void Button_Importuj(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog plikdoodczytu = new OpenFileDialog();
                plikdoodczytu.Filter = "Plik csv(*.csv)|*.csv";
                if(plikdoodczytu.ShowDialog() == true)
                {
                    using (var reader = new StreamReader(plikdoodczytu.FileName))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var klienci = csv.GetRecords<Klienci>();
                        using (var context = new MyContext())
                        {
                            foreach (var klient in klienci)
                            {
                                //context.Kliencis.Attach(klient);
                                context.Kliencis.Add(klient);
                            }
                            context.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}