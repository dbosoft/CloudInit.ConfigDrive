namespace Dbosoft.CloudInit.ConfigDrive
{
    // ReSharper disable once TypeParameterCanBeVariant
    public interface ICommandHandler<T>
    {
        void HandleCommand(T command);
    }
}