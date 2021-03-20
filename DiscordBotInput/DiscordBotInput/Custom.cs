using System;
using System.Timers;


namespace DiscordBotInput
{
    class Custom
    {
        private Timer timerVerify;
        private Input s;

        public Custom(string command, int intervall, int timeVerify)
        {
            Command = command;
            Intervall = intervall;
            TimeVerify = timeVerify;
        }
        public string Command { get; set; }
        public int Intervall { get; set; }
        public int TimeVerify { get; set; }

        public void Start()
        {
            timerVerify = new Timer(TimeVerify);
            s = new Input(Command, Intervall, new Timer());

            //start the verify Timer
            TimerVerify(TimeVerify);
            //start the say command and timer
            s.Start();
        }

        /// <summary>
        /// stops all Command Timer and Verify Timer
        /// </summary>
        public void Stop()
        {
            s.Timer.Stop();
            timerVerify.Stop();
        }

        void TimerVerify(int timeVerify)
        {
            if (timeVerify == 0)
            {
                timeVerify = 60000 * 25; //25min
            }
            Timer timer = new Timer(timeVerify);
            timer.Elapsed += OnVerifyEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
            Console.WriteLine("Started Verify Timer!");
        }

        private void OnVerifyEvent(Object source, ElapsedEventArgs e)
        {
            s.Stop();
            Console.WriteLine("Verify might occur! \n Timer stopped! \n Press Enter to resume!");
            Console.ReadLine();
            s.Timer.Start();
        }
    }
}
