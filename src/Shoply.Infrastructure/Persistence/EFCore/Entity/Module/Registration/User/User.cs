using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Infrastructure.Entity.Base;

namespace Shoply.Infrastructure.Persistence.EFCore.Entity.Module.Registration;

public class User : BaseEntity<User, InputCreateUser, InputUpdateUser, OutputUser, UserDTO, InternalPropertiesUserDTO, ExternalPropertiesUserDTO, AuxiliaryPropertiesUserDTO>
{
    public string Name { get; private set; } = String.Empty;
    public string Email { get; private set; } = String.Empty;
    public string Password { get; private set; } = String.Empty;
    public EnumLanguage Language { get; private set; }
    public string? RefreshToken { get; private set; }
    public Guid? LoginKey { get; private set; }
    public string? PasswordRecoveryCode { get; private set; }

    #region Inheritance
    #region ProductImage
    public virtual List<ProductImage>? ListCreationUserProductImage { get; private set; }
    #endregion

    #region Product
    public virtual List<Product>? ListCreationUserProduct { get; private set; }
    public virtual List<Product>? ListChangeUserProduct { get; private set; }
    #endregion

    #region MeasureUnit
    public virtual List<MeasureUnit>? ListCreationUserMeasureUnit { get; private set; }
    public virtual List<MeasureUnit>? ListChangeUserMeasureUnit { get; private set; }
    #endregion

    #region ProductCategory
    public virtual List<ProductCategory>? ListCreationUserProductCategory { get; private set; }
    public virtual List<ProductCategory>? ListChangeUserProductCategory { get; private set; }
    #endregion

    #region Brand
    public virtual List<Brand>? ListCreationUserBrand { get; private set; }
    public virtual List<Brand>? ListChangeUserBrand { get; private set; }
    #endregion

    #region User
    public virtual List<User>? ListCreationUserUser { get; private set; }
    public virtual List<User>? ListChangeUserUser { get; private set; }
    #endregion

    #region Customer
    public virtual List<Customer>? ListCreationUserCustomer { get; private set; }
    public virtual List<Customer>? ListChangeUserCustomer { get; private set; }
    #endregion    

    #region CustomerAddress
    public virtual List<CustomerAddress>? ListCreationUserCustomerAddress { get; private set; }
    public virtual List<CustomerAddress>? ListChangeUserCustomerAddress { get; private set; }
    #endregion
    #endregion

    public User() { }

    public User(string name, string email, string password, EnumLanguage language, string? refreshToken, Guid? loginKey, string? passwordRecoveryCode)
    {
        Name = name;
        Email = email;
        Password = password;
        Language = language;
        RefreshToken = refreshToken;
        LoginKey = loginKey;
        PasswordRecoveryCode = passwordRecoveryCode;
    }
}
