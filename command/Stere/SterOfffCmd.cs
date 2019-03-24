using System;

namespace Command.Stere
{
    public class StereOffCmd:ICommand
    {
        Stere stere;
        public StereOffCmd(Stere st)
        {
            this.stere = st;
        }

        public void execute()
        {
            Console.WriteLine("turn off stere");
        }

        public void undo()
        {
            throw new System.NotImplementedException();
        }
    }
}