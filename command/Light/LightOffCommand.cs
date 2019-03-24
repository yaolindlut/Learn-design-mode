namespace Command.Light
{
    public class LightOffCommand : ICommand
    {
        Light light;

        public LightOffCommand(Light light)
        {
            this.light = light;
        }
        public void execute()
        {
            this.light.Off();
        }

        public void undo()
        {
            this.light.On();
        }
    }
}