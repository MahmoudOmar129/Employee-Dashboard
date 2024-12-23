namespace Demo.BL.Interfaces
{
    public interface IGenericRep<TEntity> where TEntity : class
    {
        Task<int> CountAsync();
    }
}
