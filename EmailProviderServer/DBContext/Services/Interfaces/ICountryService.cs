//Includes


namespace EmailProviderServer.DBContext.Services.Base
{
    //------------------------------------------------------
    //	ICountryService
    //------------------------------------------------------
    public interface ICountryService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();
        Task<T> GetByIdAsync<T>(int nId);
        Task<T> GetByNameAsync<T>(string strName);
        Task<T> GetByPhoneCodeAsync<T>(string strPhoneCode);
    }
}
