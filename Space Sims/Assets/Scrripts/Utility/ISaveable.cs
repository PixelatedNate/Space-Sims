public interface ISaveable<T>
{
    public T Save();
    public void Load(string Path);
    public void Load(T data);

}
