namespace Shoply.Domain.Interface.Mapper;

public interface IBaseEntity<TEntity, TDTO>
{
    TDTO GetDTO(TEntity entity);
    TEntity GetEntity(TDTO dto);
}