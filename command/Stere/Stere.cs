using System;

namespace Command.Stere
{
    public class Stere
    {
        public void on()
        {
            Console.WriteLine("turn on stere");
        }

        public void setCD()
        {
            Console.WriteLine("set CD Player Mode");
        }

        public void setVolume(int vol)
        {
            Console.WriteLine("turn the volume to "  + vol.ToString());
        }
    }
}

