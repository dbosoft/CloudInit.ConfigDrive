namespace Contiva.CloudInit.ConfigDrive
{
    public interface ICommandHandler<T>
    {
        void HandleCommand(T command);
    }
}