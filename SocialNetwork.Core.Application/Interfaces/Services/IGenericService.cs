namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, ViewModel, Model>
           where SaveViewModel : class
           where ViewModel : class
           where Model : class
    {
        Task<SaveViewModel> AddAsync(SaveViewModel vm);
        Task UpdateAsync(SaveViewModel vm, int id);
        Task DeleteAsync(int id);
        Task<SaveViewModel> GetByIdSaveViewModelAsync(int id);
        Task<List<ViewModel>> GetAllViewModelAsync();
    }
}
