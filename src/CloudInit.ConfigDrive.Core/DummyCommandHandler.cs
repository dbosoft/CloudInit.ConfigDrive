namespace Dbosoft.CloudInit.ConfigDrive
{
    public class DummyCommandHandler<T> : ICommandHandler<T>
    {
        public void HandleCommand(T command)
        {
        }
    }
}