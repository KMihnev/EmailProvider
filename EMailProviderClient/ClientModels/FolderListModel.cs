using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailProviderClient.ClientModels
{
    public enum SystemFolders
    {
        Incoming = 0, 
        Outgoing = 1, 
        Drafts = 2, 
    }

    public class FolderListModel
    {
        public FolderListModel()
        {
            FolderType = SystemFolders.Incoming;
            FolderID = 0;
        }

        public SystemFolders FolderType { get; set; }

        public string Name { get; set; }

        public int FolderID { get; set; }
    }
}
