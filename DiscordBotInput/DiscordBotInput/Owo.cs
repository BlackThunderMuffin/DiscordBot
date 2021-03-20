using System;
using System.Timers;
using WindowsInput;
using WindowsInput.Native;
using IronOcr;
using System.Drawing;
using System.Media;

namespace DiscordBotInput
{
    class Owo
    {

        public Owo(int timeVerify)
        {
            TimeVerify = timeVerify;
        }
        public int Interval { get; set; }
        public int TimeVerify { get; set; }

        public Timer TimerVerify { get; set; }
        public Timer TimerH { get; set; }
        public Timer TimerB { get; set; }

        Gems t = new Gems();
        

        public void Start()
        {
            t.UseGem();
            t.TypeGem();
            TimerVer();
            TimerSayH();
            TimerSayB();
        }

        /// <summary>
        /// stops all Timers
        /// </summary>
        public void Stop()
        {
            TimerH.Stop();
            TimerB.Stop();
            TimerVerify.Stop();
        }

        void TimerSayH()
        {
            TimerH = new Timer
            {
                Interval = 16000 //TODO: nicht hardcoded
            };
            TimerH.Elapsed += OnSayHEvent;
            TimerH.AutoReset = true;
            TimerH.Enabled = true;
            Console.WriteLine("Started OwO HTimer!");
        }

        void TimerSayB()
        {
            TimerB = new Timer
            {
                Interval = 16000 //TODO: nicht hardcoded
            };
            TimerB.Elapsed += OnSayBEvent;
            TimerB.AutoReset = true;
            TimerB.Enabled = true;
            Console.WriteLine("Started OwO BTimer!");
        }

        private void TimerVer()
        {
            if (TimeVerify == 0)
            {
                TimeVerify = 60000 * 25; //25min
            }
            TimerVerify = new Timer(TimeVerify);
            TimerVerify.Elapsed += OnVerifyEvent;
            TimerVerify.AutoReset = true;
            TimerVerify.Enabled = true;
            Console.WriteLine("Started Runtime Timer!");
        }

        private void OnSayHEvent(Object source, ElapsedEventArgs e)
        {
            SayH();
            //14000 bis 20000
            var _rand = new Random();
            int rand = _rand.Next(14000, 25000);
            TimerH.Interval = rand;
        }

        private void OnSayBEvent(Object source, ElapsedEventArgs e)
        {
            SayB();
            //14000 bis 20000
            var _rand = new Random();
            int rand = _rand.Next(14000, 25000);
            TimerB.Interval = rand;
        }

        private void OnVerifyEvent(Object source, ElapsedEventArgs e)
        {
            TimerH.Stop();
            TimerB.Stop();
            Console.WriteLine("Intervall is over! \n Command Timer stopped! \n Press Enter to resume!");
            Console.ReadLine();
            TimerB.Start();
            TimerH.Start();
        }

        void SayH()
        {
            InputSimulator i = new InputSimulator();
            i.Keyboard.TextEntry("owoh");
            i.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            Console.WriteLine($"Said owoh!");
        }
        void SayB()
        {
            InputSimulator i = new InputSimulator();
            i.Keyboard.TextEntry("owob");
            i.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            Console.WriteLine($"Said owob!");
            CheckForVerify();
            
        }
        void CheckForVerify()
        {
            CaptureScreen();
        }
        public void CaptureScreen()
        {
            //screenshot into png

            //string dir = AppDomain.CurrentDomain.BaseDirectory;
            Screenshot ps = new Screenshot();
            Image i = ps.CaptureScreen();

            //png into txt
            var Ocr = new IronTesseract();
            string r = "";
            using (var Input = new OcrInput(i))
            {
                //TODO: this Read is having problems!!
                var Result = Ocr.Read(Input);
                r = Result.Text;
            }
            Console.WriteLine($"Result: {r}");

            if (r.Contains("captcha") || r.Contains("Captcha"))
            {
                Stop();
                Console.WriteLine("\nVerify detected, stopped all timers!");
                
            }
            else
            {
                Console.WriteLine("\nNo Verify detected.");
            }

        }
    }
}

