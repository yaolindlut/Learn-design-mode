namespace Command.Ceil
{
    public class CeilingFanHighCommand : ICommand
    {
        CeilingFan ceilingFan;
        int preSpeed;  
        public CeilingFanHighCommand(CeilingFan cf)
        {
            ceilingFan = cf;
        }

        public void execute()
        {
            preSpeed = ceilingFan.getSpeed();
            ceilingFan.High(); 
        }

        public void undo()
        {
            switch(preSpeed)
            {
                case CeilingFan.HIGH:
                    ceilingFan.High();
                    break;
                case CeilingFan.MEDIUM:
                    ceilingFan.Medium();
                    break;
                case CeilingFan.LOW:
                    ceilingFan.Low();
                    break;
                default:
                    ceilingFan.Off();
                    break;
            }
        }
    }
}