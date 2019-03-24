namespace Command.Ceil
{
    public class CeilingFanLowCommand : ICommand
    {
        CeilingFan ceilingFan;
        int preSpeed;  
        public CeilingFanLowCommand(CeilingFan cf)
        {
            ceilingFan = cf;
        }

        public void execute()
        {
            preSpeed = ceilingFan.getSpeed();
            ceilingFan.Low(); 
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