namespace JOWheels.Models.Repository
{
    public interface ICRUDAction<T> where T : class    // <T> is Generics => allow you to write code that works with any data type &&  improving performance
    {
        public IEnumerable<T> List();
        public T GetBy(int id);
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(int id);
    }
}
