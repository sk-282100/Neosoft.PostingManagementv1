using Microsoft.EntityFrameworkCore;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Domain.Entities;

namespace PostingManagement.Persistence.Repositories
{
    public class TriggerRepository : ITriggerRepository
    {
        private readonly ApplicationDbContext _context;
        public TriggerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddTrigger(int scaleId, int Tenure, string mandatoryStatus)
        {
            Trigger triggerModel = new Trigger();
            triggerModel.ScaleId = scaleId;
            triggerModel.Tenure = Tenure;
            triggerModel.Mandatory = mandatoryStatus;
            await _context.TriggerTbl.AddAsync(triggerModel);

            return _context.SaveChanges() == 1 ? true : false;
        }

        public async Task<bool> DeleteTrigger(int triggerId)
        {
            var trigger = await _context.TriggerTbl.FirstOrDefaultAsync(x => x.TriggerId == triggerId);
            if(trigger != null)
            {
                _context.TriggerTbl.Remove(trigger);
                return _context.SaveChanges() == 1 ? true : false;
            }
            return false;
        }

        public async Task<List<TriggerVm>> GetAllTriggerDetails()
        {
            var trigger = await(from trigg in _context.TriggerTbl
                                    join scale in _context.ScaleTbl on trigg.ScaleId equals scale.ScaleId
                                    select new TriggerVm()
                                    {
                                       TriggerId = trigg.TriggerId,
                                       ScaleName = scale.Name ,
                                       Tenure = trigg.Tenure ,
                                       Mandatory = trigg.Mandatory
                                    }).OrderByDescending(x => x.TriggerId).ToListAsync();
            return trigger;
        }

        public async Task<Trigger?> GetTriggerDetailsById(int triggerId)
        {
            var trigger = await _context.TriggerTbl.FirstOrDefaultAsync(x => x.TriggerId == triggerId );
            return trigger;
        }

        public async Task<bool> UpdateTrigger(int triggerId, int scaleId, int tenure, string mandatoryStatus)
        {
            var trigger = await _context.TriggerTbl.FirstOrDefaultAsync(x => x.TriggerId == triggerId );
            trigger.Tenure = tenure;
            trigger.ScaleId = scaleId;
            trigger.Mandatory = mandatoryStatus;
            //Update
            _context.Entry(trigger).State = EntityState.Modified;
            return _context.SaveChanges() == 1 ? true : false;
        }
    }
}
