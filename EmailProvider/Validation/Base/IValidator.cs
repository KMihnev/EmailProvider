//Includes

namespace EmailProvider.Validation.Base
{
    //------------------------------------------------------
    //	IValidator
    //------------------------------------------------------

    /// <summary> Базов валидатор </summary>
    public interface IValidator
    {
        abstract bool Validate(bool bLog = false);
    }
}
