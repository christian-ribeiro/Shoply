namespace Shoply.Domain.DTO.Base;

public class BaseValidateDTO
{
    public bool Invalid { get; private set; }

    public bool SetInvalid()
    {
        return Invalid = true;
    }
}