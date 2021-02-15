using HockeyVenueManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HockeyVenueManagement.Services
{
    public interface IPitchService
    {
        Task<IEnumerable<Pitch>> GetGrassPitches();

        Task<IEnumerable<Pitch>> GetSyntheticPitches();
        Task<HashSet<int>> GetPitchIds();
    }
}