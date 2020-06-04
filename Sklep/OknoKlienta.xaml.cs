using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for OknoKlienta.xaml
    /// </summary>
    public partial class OknoKlienta : Window
    {
        public ObservableCollection<Transakcje> WTrakcie { get; set; }
        public ObservableCollection<Transakcje> Zrealizowane { get; set; }
        public ObservableCollection<Transakcje> Odrzucone { get; set; }
        public MainWindow Okno { get; set; }
        public Klienci Użytkownik { get; set; }
        public List<CheckBox> ListaZamówionych { get; set; }
        public List<int> ListaSztuk { get; set; }
        public List<string> ListaNazw { get; set; }
        public double Saldo
        {
            get
            {
                return saldo;
            }
            set
            {
                if (saldo != value)
                {
                    saldo = value;
                    LSaldo.Content = Saldo.ToString();
                    using (var context = new MyContext())
                    {
                        var klient = context.Kliencis.First(x => x.Login == Użytkownik.Login);
                        klient.IlośćPieniędzy = Saldo;
                        context.SaveChanges();
                    }
                }
            }
        }
        double saldo;
        public OknoKlienta(Klienci użytkownik, MainWindow window)
        {
            DataContext = this;
            InitializeComponent();
            WTrakcie = new ObservableCollection<Transakcje>();
            Zrealizowane = new ObservableCollection<Transakcje>();
            Odrzucone = new ObservableCollection<Transakcje>();
            ListaZamówionych = new List<CheckBox>();
            ListaSztuk = new List<int>();
            ListaNazw = new List<string>();
            Okno = window;
            Okno.TBHasło.Password = "";
            Okno.TBHasłoR.Text = "";
            Okno.TBLogin.Text = "";
            Okno.TBLoginR.Text = "";
            Użytkownik = użytkownik;
            LInfo.Content += użytkownik.Login;
            saldo = użytkownik.IlośćPieniędzy;
            LSaldo.Content = Saldo.ToString();
            using (var context = new MyContext())
            {
                var produkty = (from st in context.Produktys
                         select st);
                foreach (var produkt in produkty)
                {
                    var item = new ComboBoxItem
                    {
                        Content = produkt.Nazwa + " (" + produkt.Ilość.ToString() + ")"
                    };
                    CBNoweZamówienie.Items.Add(item);
                }
                CBNoweZamówienie.SelectedIndex = 0;
                var transakcje = (from st in context.Transakcjes where st.Klienci.Id == Użytkownik.Id select st);
                foreach( var transakcja in transakcje)
                {
                    transakcja.Klienci = context.Kliencis.First(x => x.Id == Użytkownik.Id);
                    transakcja.Pracownicy= context.Pracownicys.FirstOrDefault(x => x.Id == 
                    context.Transakcjes.FirstOrDefault(y=>y.Id == transakcja.Id).Pracownicy.Id);
                    if (transakcja.StatusTransakcji == "W trakcie realizacji")
                    {
                        WTrakcie.Add(transakcja);
                    }
                    if(transakcja.StatusTransakcji == "Zrealizowana")
                    {
                        Zrealizowane.Add(transakcja);
                    }
                    if (transakcja.StatusTransakcji == "Odrzucona")
                    {
                        Odrzucone.Add(transakcja);
                    }
                }
            }
            LBAktualne.ItemsSource = WTrakcie;
            LBOdrzucone.ItemsSource = Odrzucone;
            LBZrealizowane.ItemsSource = Zrealizowane;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Close();
            Okno.Show();
        }
        private void Button_Dodaj(object sender, RoutedEventArgs e)
        {
            try
            {
                double liczba = double.Parse(TBDodaj.Text);
                if (liczba >= 0)
                {
                    Saldo += liczba;
                }
                else
                {
                    TBDodaj.Text = "Wpisz liczbę nieujemną";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Zabierz(object sender, RoutedEventArgs e)
        {
            try
            {
                double liczba = double.Parse(TBZabierz.Text);
                if (liczba >= 0)
                {
                    if (liczba <= Saldo)
                    {
                        Saldo -= liczba;
                    }
                    else
                    {
                        MessageBox.Show("Za dużo chcesz zabrać z konta");
                    }
                }
                else
                {
                    TBZabierz.Text = "Wpisz liczbę nieujemną";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Usuń(object sender, RoutedEventArgs e)
        {
            MessageBoxButton button = MessageBoxButton.YesNo;
            string messageBoxText = "Czy na pewno chcesz usunąć konto?";
            MessageBoxResult result = MessageBox.Show(messageBoxText, null, button);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    using (var context = new MyContext())
                    {
                        context.Kliencis.Attach(Użytkownik);
                        context.Kliencis.Remove(Użytkownik);
                        context.SaveChanges();
                        Window_Closed(null, null);
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
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
                        var klient = context.Kliencis.First(x => x.Login == Użytkownik.Login);
                        klient.Hasło = Użytkownik.Hasło;
                        context.SaveChanges();
                    }
                    MessageBox.Show("Pomyślnie zmieniono hasło");
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
        private void Button_Dodaj_Do_Zamówienia(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new MyContext())
                {
                    var produkt = context.Produktys.First(x => x.Id == CBNoweZamówienie.SelectedIndex + 1);
                    int ilość = int.Parse(TBZamowienie.Text);
                    if (0 <ilość && ilość <= produkt.Ilość)
                    {
                        var check = new CheckBox
                        {
                            Content = produkt.Nazwa + " Do zapłaty: " + (ilość * produkt.Cena).ToString()
                        };
                        ListaZamówionych.Add(check);
                        ListaSztuk.Add(ilość);
                        ListaNazw.Add(produkt.Nazwa);
                        SPZamowienie.Children.Add(check);
                    }
                    else
                    {
                        MessageBox.Show("Nie mamy tyle towaru");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Usuń_Z_Zamówienia( object sender, RoutedEventArgs e)
        {
            SPZamowienie.Children.Clear();
            for (int i=0; i < ListaZamówionych.Count();i++)
            {
                if(ListaZamówionych[i].IsChecked.GetValueOrDefault())
                {
                    ListaZamówionych.Remove(ListaZamówionych[i]);
                    ListaSztuk.Remove(ListaSztuk[i]);
                    ListaNazw.Remove(ListaNazw[i]);
                    i -= 1;
                }
                else
                {
                    SPZamowienie.Children.Add(ListaZamówionych[i]);
                }
            }
        }
        private void Button_Złóż_Zamówienie(object sender, RoutedEventArgs e)
        {
            using (var context = new MyContext())
            {
                for (int i = 0; i < ListaSztuk.Count(); i++) {
                    string nazwa = ListaNazw[i];
                    var transakcja = new Transakcje
                    {
                        Klienci = context.Kliencis.First(x=>x.Id == Użytkownik.Id),
                        IlośćKupionegoProduktu = ListaSztuk[i],
                        StatusTransakcji = "W trakcie realizacji",
                        Produkty = context.Produktys.First(x=>x.Nazwa == nazwa),
                        Cena = context.Produktys.First(x => x.Nazwa == nazwa).Cena * ListaSztuk[i]
                    };
                    context.Transakcjes.Add(transakcja);
                    WTrakcie.Add(transakcja);
                    context.SaveChanges();
                }
            }
            SPZamowienie.Children.Clear();
        }
    }
}