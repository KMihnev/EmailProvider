//Includes


using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Services.Base
{
    //------------------------------------------------------
    //	IFolderService
    //------------------------------------------------------
    public interface IFolderService
    {
        IEnumerable<Folder> GetAll(int? nCount = null);

        Folder GetById(int nId);

        Folder GetByName(string strName);
    }
}
