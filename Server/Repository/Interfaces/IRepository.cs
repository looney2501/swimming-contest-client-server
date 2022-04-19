namespace Server.Repository;

public interface IRepository<Tid, T>
{
    int Add(T elem);
    void Delete(T elem);
    void Update(T elem, Tid id);
    T FindById(Tid id);
}