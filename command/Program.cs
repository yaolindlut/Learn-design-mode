using System;
using Command.Light;
using Command.Stere;
using Command.Ceil;

namespace Command
{
    class Program
    {
        static void Main(string[] args)
        {
            CeilingFan cf = new CeilingFan();
            CeilingFanHighCommand cfh = new CeilingFanHighCommand(cf);
            CeilingFanLowCommand cfl = new CeilingFanLowCommand(cf);

            Control ctr = new Control();
            // 假设 button0, button1 分别为高速低速
            ctr.SetCommand(0, cfh);
            ctr.SetCommand(1, cfl);

            ctr.OnButtonWasPressed(0);
            ctr.OnButtonWasPressed(1);
            ctr.UndoButtonWasPressed();
            ctr.RedoButtonWasPressed();
        }
    }
}
