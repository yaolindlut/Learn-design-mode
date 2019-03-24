namespace Command.Light
{
    public class LightOnCommand : ICommand
    {
        Light light;
        public LightOnCommand(Light light)
        {
            this.light = light;
        }
        public void execute()
        {
            light.On();
        }

        public void undo()
        {
            light.Off();
        }
    }
}