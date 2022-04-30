using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teltonika.DataModels;

namespace Teltonika.Interfaces
{
    public interface ICovid19CaseService
    {
        Task<IEnumerable<Covid19Case>> GetCovid19Case();
        Task<Covid19Case> GetCovid19CaseById(int id);
        Task AddCovid19Case(Covid19Case cc);
        Task UpdateCovid19Case(Covid19Case cc);
        Task DeleteCovid19Case(int id);
    }
}
