using AutoMapper;
using Data.Repository;
using Entity.Model;
using Microsoft.Extensions.Logging;

namespace Bussines.serviceRepository;
public abstract class servicesBase <TDto, TEntity> where TEntity : class
{

    private DataGeneric<TEntity> _data;

    private ILogger _logger;

    private IMapper _mapper;


    public servicesBase(DataGeneric<TEntity> data,ILogger logger, IMapper mapper)
    {
        this._data = data;
        this._logger = logger;
        this._mapper = mapper;
    }


    public async Task<IEnumerable<TDto>> GetAll() 
    {
        try
        {
            var Entity = await _data.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(Entity);
        }
        catch (Exception ex) 
        {
            _logger.LogError("error", ex);
            throw;
        }
    }

    public async Task<TDto> GetAllById(int id)
    {
        try
        {
            var Entity = await _data.GetById(id);
            return _mapper.Map<TDto>(Entity);
        }catch (Exception ex)
        {
            _logger.LogError("error", ex);
            throw;
        }
    }

    public async Task<TDto?> AddAsync(TDto dto)
    {
        try
        {
            var Entity = _mapper.Map<TEntity>(dto);
            var create = await _data.AddAsync(Entity);
            return _mapper.Map<TDto>(Entity);
        }
        catch (Exception ex)
        {
            _logger.LogError("error", ex);
            throw;
        }
    }

    public async Task<bool> updateAsync(TDto dto)
    {
        try
        {
            var Entity = _mapper.Map<TEntity>(dto);
            return await _data.updateAsync(Entity);
        }
        catch (Exception ex)
        {
            _logger.LogError("error", ex);
            throw;
        }
    }

    public async Task<bool> delete(int id)
    {
        try
        {
            return await _data.deleteAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError("error", ex);
            throw;
        }
    }
}

