namespace IgorBryt.Store.BLL.Interfaces;

public interface ICrud<TModel> where TModel : class
{
    Task<TModel> GetByIdAsync(int id);

    Task AddAsync(TModel model);

    Task UpdateAsync(TModel model);

    Task DeleteAsync(int modelId);
}
