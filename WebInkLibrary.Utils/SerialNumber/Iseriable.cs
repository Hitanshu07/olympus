namespace WebInkLibrary.Utils.SerialNumber
{
    public interface ISerialNoService<T> where T: class
    {
        T AddSerialNumber(T entity, int srNo);
        T RemoveSerialNumber(T entity);
        T MoveSerialNumber(int targetSerialNo, T entity);
    }
}
