namespace WebInkLibrary.Core
{
    public interface IModule<T> where T : class
    {
        T GetId(int moduleId);
    }
}
