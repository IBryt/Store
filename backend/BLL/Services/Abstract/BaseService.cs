using AutoMapper;
using FluentValidation;
using IgorBryt.Store.DAL.Interfaces;

namespace Business.Services.Abstract
{
    public abstract class BaseService<TModel>
    {
        protected internal readonly IUnitOfWork _unitOfWork;
        protected internal readonly IMapper _mapper;
        protected internal IValidator<TModel> _validator;

        protected BaseService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<TModel> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }
    }
}
