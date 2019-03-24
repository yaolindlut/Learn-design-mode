using System;

namespace Command.Ceil
{
    public class CeilingFan
    {
        public const int HIGH = 3;
        public const int MEDIUM = 2;
        public const int LOW = 1;
        public const int OFF = 0;
        int speed;

        public CeilingFan()
        {
            speed = OFF;
        }

        public void High()
        {
            speed = HIGH;
            Console.WriteLine("turn ceilfan to high speed!");
        }

        public void Medium()
        {
            speed = MEDIUM;
            Console.WriteLine("turn ceifan to medium speed!");
        }

        public void Low()
        {
            speed = LOW;
            Console.WriteLine("turn ceifan to low speed!");
        }

        public void Off()
        {
            speed = OFF;
            Console.WriteLine("turn off ceilfan");
        }

        public int getSpeed()
        {
            return speed;
        }
    }
}