namespace WebAppSemesterProject3.DTO
{
    public interface IDeserializer<T>
    {
        Task<T> GetObject(int id);
        Task<IEnumerable<T>> GetList();
    }
}
