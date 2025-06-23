using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Models.Serializables
{
    public class StatisticsViewModel
    {
        public StatisticsViewModel()
        {
            
        }

        public int numberOfUsers { get; set; }

        public int numberOfIncomingEmails { get; set; }

        public int numberOfOutgoingEmails { get; set; }
    }
}
