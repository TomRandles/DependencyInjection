using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HockeyVenueManagement.DataAccess;
using HockeyVenueManagement.Models;
using HockeyVenueManagement.Services;
using HockeyVenueManagement.Services.Audit;
using Microsoft.EntityFrameworkCore;

namespace TennisBookings.Web.Services
{
    public class PitchService : IPitchService
    {
        private readonly HockeyBookingDbContext _dbContext;
        private readonly IAuditor<PitchService> _auditor;

        public PitchService(HockeyBookingDbContext dbContext, IAuditor<PitchService> auditor)
        {
            _dbContext = dbContext;
            _auditor = auditor;
        }
        
        public async Task<IEnumerable<Pitch>> GetGrassPitches() =>
            await GetQueryablePitches().Where(c => c.Type == PitchType.grass).ToListAsync();

        public async Task<IEnumerable<Pitch>> GetSyntheticPitches() =>
            await GetQueryablePitches().Where(c => c.Type == PitchType.grass).ToListAsync(); 

        public async Task<HashSet<int>> GetPitchIds()
        {
            _auditor.RecordAction("Test");

            var ids = await GetQueryablePitches().Select(c => c.Id).OrderBy(c => c).ToListAsync();
            return ids.ToHashSet();
        }

        private IQueryable<Pitch> GetQueryablePitches() => _dbContext.Pitches.AsNoTracking();
    }
}