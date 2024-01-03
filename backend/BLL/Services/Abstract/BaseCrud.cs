using AutoMapper;
using IgorBryt.Store.BLL;
using IgorBryt.Store.BLL.Interfaces;
using IgorBryt.Store.BLL.Models.Abstract;
using IgorBryt.Store.DAL.Entities.Abstract;
using IgorBryt.Store.DAL.Interfaces;

namespace Business.Services.Abstract
{
    public abstract class BaseCrud<TModel, TEntity> : BaseService, ICrud<TModel>
        where TModel : BaseModel
        where TEntity : BaseEntity
    {
        private readonly IRepository<TEntity> _repository;

        protected BaseCrud(IUnitOfWork unitOfWork, IMapper mapper, IRepository<TEntity> repository) : base(unitOfWork, mapper)
        {
            _repository = repository;
        }

        public async Task<TModel> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TModel>(entity);
        }

        public async Task AddAsync(TModel model)
        {
            if (model == null)
            {
                throw new StoreException($"The object {typeof(TModel).Name} cannot be null");
            }
            TEntity entity = _mapper.Map<TEntity>(model);
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            model.Id = entity.Id;
        }

        public async Task UpdateAsync(TModel model)
        {
            if (model == null)
            {
                throw new StoreException($"The object {nameof(TModel)} cannot be null");
            }

            TEntity entity = _mapper.Map<TEntity>(model);
            _repository.Update(entity);
            await _unitOfWork.SaveAsync();
        }

        public async virtual Task DeleteAsync(int modelId)
        {
            await _repository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAsync();
        }
    }
}
