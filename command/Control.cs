using System.Collections.Generic;

namespace Command
{
    public class Control
    {
        ICommand[] onCommands;
        Stack<ICommand> undoCommands;
        Stack<ICommand> redoCommands;  // 记录前一个命令, 便于 undo

        public Control()
        {
            onCommands = new ICommand[2];
            undoCommands = new Stack<ICommand>();
            redoCommands = new Stack<ICommand>();
            for(int i = 0; i<2; i++)
            {
                onCommands[i] = null;
            }
        }

        public void SetCommand(int slot, ICommand onCmd)
        {
            onCommands[slot] = onCmd;
        }

        public void OnButtonWasPressed(int slot)
        {
            if (onCommands[slot] != null)
            {
                onCommands[slot].execute();
                undoCommands.Push(onCommands[slot]);
            }
        }

        public void UndoButtonWasPressed()
        {
            if (undoCommands.Count > 0)
            {
                ICommand cmd = undoCommands.Pop();
                redoCommands.Push(cmd);
                cmd.undo();
            }
        }

        public void RedoButtonWasPressed()
        {
            if(redoCommands.Count > 0)
            {
                ICommand cmd = redoCommands.Pop();
                undoCommands.Push(cmd);
                cmd.execute();
            }
        }
    }
}