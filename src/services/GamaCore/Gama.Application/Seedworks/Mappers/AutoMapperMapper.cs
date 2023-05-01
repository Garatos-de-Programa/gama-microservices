using AutoMapper;
using Gama.Application.Contracts.Mappers;

namespace Gama.Application.Seedworks.Mappers
{
    internal class AutoMapperMapper : IEntityMapper
    {
        private readonly IMapper _mapper;

        public AutoMapperMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TResult Map<TResult, TEntity>(TEntity entity)
        {
            return _mapper.Map<TResult>(entity);
        }
    }
}
