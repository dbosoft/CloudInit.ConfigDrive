namespace Contiva.CloudInit.ConfigDrive.Generator
{
    public class DummyCommandHandler<T> : ICommandHandler<T>
    {
        public void HandleCommand(T command)
        {
        }
    }
}