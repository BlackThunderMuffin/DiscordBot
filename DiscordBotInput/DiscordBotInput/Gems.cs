using System;
using System.Timers;
using System.Drawing;
using WindowsInput;
using System.Media;
using WindowsInput.Native;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using Timer = System.Timers.Timer;

namespace DiscordBotInput
{
	class Gems
	{
		public int[] anzahl = new int[7] { 0, 0, 0, 0, 0, 0, 0 };                           //Array für die Anzahl der Sets
		public int[] xhunts = new int[7] { 1, 35, 60, 85, 85, 110, 110 };                   //Anzahl der Hunts pro Gem mit einem Sicherheitswert von +10
		public int hunts = 0;                                                               //Int zum runterzählen der Hunts
		public int z = 0;                                                                                                                                   //Int zum wählen der gelisteten Gem Sets
		public int[,] myarray = new int[3, 7] { { 51, 52, 53, 54, 55, 56, 57, }, { 65, 66, 67, 68, 69, 70, 71 }, { 72, 73, 74, 75, 76, 77, 78 } };          //Zahlenwerte für die Gems
		public int mydelay = 4000;                                                          //auf 10000 hochsetzen++++++++++++++++++++++++++++++++
		public bool check = false;                                                          //boolscher Wert zum wechseln des Fensters
		public bool empty = false;                                                          //boolscher Wert zum checken ob Gems vorhanden sind/genutzt werden sollen
		public bool hunting = false;                                                        //boolscher Wert zum initiieren der der Hunt Zählung																									
		public bool noGems = true;                                                          //boolscher Wert zum checken ob Gems benutzt werden oder nicht
		public int i = 0;                                                                   //Int zum inkrementieren im Timer
		public Timer TimerG { get; set; }                                                   //Timer zum aufrufen der jeweiligen Gems

		void TimerSayG()                                                                    //Timer zum sagen von "Equip gem ...."
		{
			TimerG = new Timer																//kommentar 
			{
				Interval = 500                                                              //Interval auf 5000 setzen+++++++++++++++++++++++++
			};
			TimerG.Elapsed += OnSayGEvent;
			TimerG.AutoReset = true;
			TimerG.Enabled = true;
			Console.WriteLine("Gems werden hinzugefügt");
		}
		private void OnSayGEvent(Object source, ElapsedEventArgs e)
		{
			SayG();
		}

		void SayG()                                                                         //SayG zum ausgeben der 2D Array Werte
		{                                                                                   //für das jeweilige Gemset
			TimerG.Start();
			InputSimulator j = new InputSimulator();
			j.Keyboard.TextEntry("owo equip " + myarray[i, z]);
			j.Keyboard.KeyPress(VirtualKeyCode.RETURN);
			i++;
			if (i == 3)                                                                     //Wird der Wert i=3 erreicht, wird der Timer gestoppt 
			{                                                                               //um ein laufen ins unendliche zu verhindern.
				i = 0;
				TimerG.Stop();
			}

		}
		public void UseGem()                                                                //Methode zur Abfrage ob Gems verwendet werden sollen
		{                                                                                   //oder nicht

			string[] liste = new string[7] { "common", "uncommon", "rare", "epic", "mythic", "legendary", "fabled" };               //String Feld zum abfragen der Gemsets



			Console.WriteLine("Hast du Gems? (ja/j/y/yes/nein)");
			string input = Console.ReadLine();
			if (input == "ja" || input == "j" || input == "yes" || input == "y" || input == "Ja" || input == "J" || input == "Yes" || input == "Y")     //Alle ja Werte werden angenommen
			{                                                                                                                                               //aber jedes andere Zeichen = nein
				for (int i = 0; i < 7; i++)
				{

					{

						Console.WriteLine($"Wie viele { liste[i]} Sets hast du?");                  //Abfrage der Anzahl an Sets pro Gem
						string insert = Console.ReadLine();
						while (insert.Length == 0)
						{
							Console.WriteLine("Bitte einen gültigen Wert eingeben!");
							insert = Console.ReadLine();
						}
						bool worked = int.TryParse(insert, out anzahl[i]);
						while (!worked)
						{
							Console.WriteLine("Bitte einen gültigen Wert eingeben!");
							insert = Console.ReadLine();
						}

					}

				}
				for (int i = 0; i < 7; i++)
				{
					Console.WriteLine($"Du hast {anzahl[i]} { liste[i]} Sets");             //Ausgabe der vorhandenen Gemsets
				}
			}
			else
			{
				empty = true;                                                               //verhindern, dass die TypeGem() in OwO durchlaufen wird, falls keine Gems vorhanden sind
			}
		}

		public void TypeGem()                                                               //Methode zum equippen/use der Gem sets
		{
			if (!empty)                                                                     //falls keine gems vorhanden sind wird die if-Bedingung nicht ausgeführt
			{


				if (hunting)                                                                //runterzählen der Hunts um den Zeitraum für neue Gems zu bestimmen
				{
					hunts--;
					Console.WriteLine("Number of hunts: " + hunts);
				}

				while (!check)                                                                                      //Gewährung eines Zeitraums zum Fensterwechsel. 
				{                                                                                                   //einmalige Ausführung
					Console.WriteLine("Bitte Enter drücken und innerhalb von 10 Sekunden zu OwO wechseln");
					Console.ReadLine();
					Thread.Sleep(mydelay);
					check = true;                                                                                   //setzen von check auf true, um ein erneutes durchlaufen der Schleife zu verhindern
					hunting = true;                                                                                 //setzen von hunting auf true, um das runter zählen der Hunts zu initialisieren
					noGems = true;                                                                                  //setzen von noGems auf true, um ein einsetzen von neuen Gems zu ermöglichen

				}
				if (hunts == 0)                                                     //Bedingung um bei 0 hunts Gems einzusetzen
				{
					noGems = true;                                                  //noGems wird auf true gesetzt
					if (noGems == true && anzahl[z] != 0)                           //checken ob ein gem set vorhanden ist und keins eingesetzt
					{
						hunts = xhunts[z];                                          //setzen der auszuführenden Hunts bis zum neuen Gemset
																					//auf den im Feld xhunts[] hinterlegten Wert

						TimerSayG();                                                //aufrufen des Timers zum equippen der Gems

						noGems = false;                                             //setzen von nogems auf false, weil nun Gems eingesetzt sind
						anzahl[z]--;                                                //ein Gemset wurde genutzt, verringern der vorhandenen Sets um eins

					}
					else if (noGems == true && anzahl[z] == 0)                      //für den Fall das keine Gemsets genutzt werden, aber 
					{
						while (anzahl[z] == 0)                                      //Wechseln im Feld, bis ein Gemset auftaucht
						{
							z++;
							Console.WriteLine(xhunts[z]);
							if (z == 6 && anzahl[z] == 0)                           //wird das sechste Feld erreicht und ist dieses leer, wird
							{                                                       //empty auf true gesetzt und die anfängliche if-Bedingung
								empty = true;                                       //wird nicht mehr ausgeführt
							}
						}

						if (anzahl[z] != 0)                                         //ist der Wert im Feld ungleich 0 werden die Hunts
						{                                                           //auf den benötigte Hunts pro Gemset Wert gesetzt
							hunts = xhunts[z];
						}

						TimerSayG();                                                //aufrufen des Timers zum equippen der Gems


						noGems = false;                                             //setzen von nogems auf false, weil nun Gems eingesetzt sind
						anzahl[z]--;                                                //ein Gemset wurde genutzt, verringern der vorhandenen Sets um eins

					}
				}
			}
		}
	}
}