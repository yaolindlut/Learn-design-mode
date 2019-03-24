using System;

namespace Command
{
    public class NoCommand : ICommand
    {
        public void execute()
        {
            Console.WriteLine("do nothing");
        }

        public void undo()
        {
            throw new System.NotImplementedException();
        }
    }
}