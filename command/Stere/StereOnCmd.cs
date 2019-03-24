namespace Command.Stere
{
    public class StereOnCommand : ICommand
    {
        Stere stere;
        public StereOnCommand(Stere stere)
        {
            this.stere = stere;
        }
        public void execute()
        {
            stere.on();
            stere.setCD();
            stere.setVolume(200);
        }

        public void undo()
        {
            throw new System.NotImplementedException();
        }
    }
}