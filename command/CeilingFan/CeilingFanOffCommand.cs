namespace Command.Ceil
{
    public class CeilingFanOffCommand : ICommand
    {
        CeilingFan ceilingFan;
        int preSpeed;  // 记录命令在执行前的状态
        public CeilingFanOffCommand(CeilingFan cf)
        {
            ceilingFan = cf;
            preSpeed = cf.getSpeed();
        }
        public void execute()
        {
            ceilingFan.Off();
        }

        public void undo()
        {
            switch (preSpeed)
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