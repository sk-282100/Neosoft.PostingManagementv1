using Microsoft.EntityFrameworkCore;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Persistence.Repositories
{
    public class ScaleRepository : IScaleRepository
    {
        private readonly ApplicationDbContext _context;
        public ScaleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Scale>> GetAllScaleDetails()
        {
            var scaleList = await _context.ScaleTbl.OrderByDescending(x => x.ScaleId).ToListAsync();
            return scaleList;
        }
    }
}
