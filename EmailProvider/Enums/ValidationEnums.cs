//Includes

namespace EmailServiceIntermediate.Enums
{
    //------------------------------------------------------
    //	UserValidationTypes
    //------------------------------------------------------

    //Всички валидационни типове са задължителни, но за 5 - няма друго изискване
    public enum UserValidationTypes
    {
        ValidationTypeNone = 0,
        ValidationTypeName,
        ValidationTypePassword,
        ValidationTypeEmail,
        ValidationTypePhoneNumber,
        ValidationTypeCountry
    }

    //------------------------------------------------------
    //	EmailValidationTypes
    //------------------------------------------------------
    public enum EmailValidationTypes
    {
        ValidationTypeNone = 0,
        ValidationTypeSender,
        ValidationTypeReceiver,
        ValidationTypeSubject
    }
}
