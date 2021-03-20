using System;
using System.Timers;
using WindowsInput;
using WindowsInput.Native;

namespace DiscordBotInput
{
    class Input
    {
        /// <summary>
        /// for verify that does not need to say anything and just needs the Timer
        /// </summary>
        public Input() { }

        /// <summary>
        /// for saying commands
        /// </summary>
        /// <param name="command"></param>
        /// <param name="intervall"></param>
        public Input(string command, int intervall, Timer timer)
        {
            Command = command;
            Intervall = intervall;
            Timer = timer;
        }

        public string Command { get; set; }
        public int Intervall { get; set; }
        public Timer Timer { get; set; }

        /// <summary>
        /// sets up Command and Timer and starts it
        /// </summary>
        virtual public void Start()
        {
            TimerSay();
        }

        /// <summary>
        /// stops all Timers
        /// </summary>
        virtual public void Stop()
        {
            Timer.Stop();
        }

        void TimerSay()
        {
            Timer.Interval = Intervall;
            Timer.Elapsed += OnSayEvent;
            Timer.AutoReset = true;
            Timer.Enabled = true;
            Console.WriteLine("Started Timer!");
        }

        private void OnSayEvent(Object source, ElapsedEventArgs e)
        {
            SayCommand();
        }

        void SayCommand()
        {
            InputSimulator i = new InputSimulator();
            i.Keyboard.TextEntry(Command);
            i.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            Console.WriteLine($"Said {Command}!");
        }
    }
}
