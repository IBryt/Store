using AutoMapper;
using IgorBryt.Store.DAL.Interfaces;

namespace Business.Services.Abstract
{
    public abstract class BaseService
    {
        protected internal readonly IUnitOfWork _unitOfWork;
        protected internal readonly IMapper _mapper;

        protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
