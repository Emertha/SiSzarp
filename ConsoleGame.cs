using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{

    public class Pytanie
    {
        string TrescPytania;
        string OdpowiedzA;
        string OdpowiedzB;
        string OdpowiedzC;
        string OdpowiedzD;
        string OdpowiedzPoprawna;
        bool OdpowiedzUzyta;

        //konstruktor, ktory bedzie miec tylko potrzebne parametry
        public Pytanie(string Tresc, string A, string B, string C, string D, string Prawidlowa)
        {
            TrescPytania = Tresc;
            OdpowiedzA = A;
            OdpowiedzB = B;
            OdpowiedzC = C;
            OdpowiedzD = D;
            OdpowiedzPoprawna = Prawidlowa;
            OdpowiedzUzyta = false; //tworze pytanie, wiec jest nieuzyte wiec false, zmieni sie dopiero przy losowaniu i pobraniu

        }
        public string BierzemyPoprawna()
        {
            return OdpowiedzPoprawna; //ona sobie istnieje gdzies tam

        }
        public bool CzyUzyte()
        {
            return OdpowiedzUzyta;

        }
        public void WypiszPytanie()
        { // posluguje sie obiektem a nie kontruktorem
            Console.WriteLine(TrescPytania);
            Console.WriteLine(OdpowiedzA);
            Console.WriteLine(OdpowiedzB);
            Console.WriteLine(OdpowiedzC);
            Console.WriteLine(OdpowiedzD);
            
        }
        public void OznaczUzyte ()
        {
            OdpowiedzUzyta = true;


        }



    }
    
    public class BazaPytan
    {
        //lista (tablica) o typie Pytanie
        //public List<Pytanie> Baza;
        List<Pytanie>  Baza = new List<Pytanie>();
        //konstruktor
        public BazaPytan()
        {

        }

        //dodaje obiekt typu pytanie do tablicy, ale nic nie zwracam bo to siedzi gdzies tam w pamieci
        public void DodajPytanieDoTablicy(string Tresc, string A, string B, string C, string D, string Prawidlowa)

        {//robie nowy obiekt, ktory zapisze sie do tablicy
            Pytanie DoTablicy = new Pytanie(Tresc, A, B, C, D, Prawidlowa);
            // teraz dodaje do tablicy
            Baza.Add(DoTablicy);
        }
        public Pytanie ZwrocPytanieZBazy(int n)

        {
           return Baza[n];

        }
        public Pytanie LosujPytanieZBazy()

        {// jezeli jest uzyte to losuje dalej (true), a jesli nieuzyte to przerywa (break), oznacza jako zuzyte i zwraca , ignoruje to zdanie gdzie jest true

            Pytanie Wylosowane;
            do
            {
                Random Losowa = new Random();
                int l = Losowa.Next(0, Baza.Count() );
                Wylosowane = Baza[l];
                if (Wylosowane.CzyUzyte() == false) { break;  }
            } while (Wylosowane.CzyUzyte() == true);
            Wylosowane.OznaczUzyte();

            return Wylosowane;
            
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            BazaPytan mojaBaza = new BazaPytan();

            Console.WriteLine("\n********************************************************************************" +
                "\n" +
                "Witaj w grze '10 pytań'! Celem gry jest udzielenie poprawnej odpowiedzi na zadane pytanie. Do wyboru masz cztery odpowiedzi - 1, 2, 3, 4." + "\n" + 
                "\n" +
                "Proszę o wpisywanie jedyne jednej z tych liter, w innym przypadku dostaniesz 0 punktów! " +
                "\n" +
                "Po 10 pytaniach dowiesz się, na ile pytań odpowiedziałeś poprawnie. Przy każdym pytanie otrzymasz też informację, jaka odpowiedź była poprawna. " +
                "\n" +
                "Powodzenia! Wciśniej enter aby wyświetlić pierwsze pytanie." +
                "\n********************************************************************************");
            Console.ReadLine();
            Console.OutputEncoding = Encoding.GetEncoding("Windows-1250");
            using (StreamReader sr = File.OpenText("Pytania.txt"))
            {
                string s = String.Empty; //lecimy pokolei po linijkach w notatniku
                while ((s = sr.ReadLine()) != null)
                {

                    mojaBaza.DodajPytanieDoTablicy(s, sr.ReadLine(), sr.ReadLine(), sr.ReadLine(), sr.ReadLine(), sr.ReadLine());
                    //przekazujemy kazda linijke z notatnika
                    sr.ReadLine();

                }


                
           
                string wpis;
                int wynik = 0; int numer = 1;

                while (numer <= 10)
                {

                    Pytanie NaszePytanie = mojaBaza.LosujPytanieZBazy();
                    NaszePytanie.WypiszPytanie();
                    wpis = Console.ReadLine();
                    Console.WriteLine("Poprawna to " + NaszePytanie.BierzemyPoprawna());

                    if (wpis == NaszePytanie.BierzemyPoprawna())
                    {
                        Console.WriteLine("Brawo! Zdobywasz punkt!");
                        wynik = wynik + 1;
                    }
                    else Console.WriteLine("Niestety, to była niepoprawna odpowiedź");
                    numer = numer + 1;
                }

                Console.WriteLine("To już koniec gry! Twój wynik to: " + wynik + " Gratulacje!");
            } 
           


            Console.ReadKey(true);

        }
    }
}
