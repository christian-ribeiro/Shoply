using Shoply.Arguments.Argument.Base;
using Shoply.Domain.DTO.Base;
using Shoply.Infrastructure.Persistence.EFCore.Entity.Base;
using Shoply.Infrastructure.Persistence.EFCore.Entity.Module.Registration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Shoply.Infrastructure.Entity.Base;

public abstract class BaseEntity<TEntity, TInputCreate, TInputUpdate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TEntity : BaseEntity<TEntity, TInputCreate, TInputUpdate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputUpdate : BaseInputUpdate<TInputUpdate>
    where TOutput : BaseOutput<TOutput>
    where TDTO : BaseDTO<TInputCreate, TInputUpdate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TInternalPropertiesDTO : BaseInternalPropertiesDTO<TInternalPropertiesDTO>, new()
    where TExternalPropertiesDTO : BaseExternalPropertiesDTO<TExternalPropertiesDTO>, new()
    where TAuxiliaryPropertiesDTO : BaseAuxiliaryPropertiesDTO<TAuxiliaryPropertiesDTO>, new()
{
    public long Id { get; set; }
    public virtual DateTime? CreationDate { get; set; }
    public virtual long? CreationUserId { get; set; }
    public virtual DateTime? ChangeDate { get; set; }
    public virtual long? ChangeUserId { get; set; }

    #region Virtual Properties
    #region Internal
    [NotMapped]
    public virtual User? CreationUser { get; set; }
    [NotMapped]
    public virtual User? ChangeUser { get; set; }
    #endregion
    #endregion

    public virtual Dictionary<(string? MethodName, string? CallAlias), CustomReturnPropertyDictionary> MethodReturnPropertyDictionary => [];

    public List<string> GetProperties(string? methodName, string? callAlias)
    {
        MethodReturnPropertyDictionary.TryGetValue((methodName, callAlias), out CustomReturnPropertyDictionary? dictionary);
        if (dictionary == null || dictionary.ListProperty.Count == 0)
        {
            return (from i in GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    where !i.IsDefined(typeof(NotMappedAttribute)) &&
                    (i.PropertyType.IsPrimitive || i.PropertyType.IsValueType || i.PropertyType == typeof(string))
                    select i.Name).ToList();
        }

        return (from i in dictionary.ListProperty
                select i.PropertyName).ToList(); ;
    }
}