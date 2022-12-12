using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PostingManagement.Application.Contracts.Persistence;
using PostingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostingManagement.Persistence.Repositories
{
    public class JobFamilyRepository : BaseRepository<JobFamilies>, IJobFamilyRepository
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _applicationDbcontext; 
        public JobFamilyRepository(ApplicationDbContext dbContext, ILogger<JobFamilies> logger) : base(dbContext, logger)
        {
            _logger = logger;
            _applicationDbcontext = dbContext;
        }

        public async Task<bool> AddAsync(string jobFamilyName)
        {
            JobFamilies jobFamilies = new JobFamilies { JobFamilyName = jobFamilyName };
            _applicationDbcontext.JobFamilyTable.Add(jobFamilies);
            return await _applicationDbcontext.SaveChangesAsync() == 1? true : false;    
        }

        public async Task<List<JobFamilies>> GetAllJobFamily()
        {
            return await _applicationDbcontext.JobFamilyTable.OrderByDescending(x => x.JobFamilyId).ToListAsync();
        }

        public async Task<JobFamilies> GetJobFamilyById(int id)
        {
            return await _applicationDbcontext.JobFamilyTable.Where(x => x.JobFamilyId == id).FirstOrDefaultAsync();
        }

        public async Task <JobFamilies> GetJobFamilyByName(string jobFamilyName)
        {
            return await _applicationDbcontext.JobFamilyTable.Where(x => x.JobFamilyName == jobFamilyName).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAsync(JobFamilies jobFamilies)
        {
            _applicationDbcontext.JobFamilyTable.Remove(jobFamilies);   
            return await _applicationDbcontext.SaveChangesAsync() == 1? true: false;
        }

        public async Task<bool> UpdateAsync(JobFamilies jobFamilies)
        {
            _applicationDbcontext.JobFamilyTable.Update(jobFamilies);
            return await _applicationDbcontext.SaveChangesAsync() == 1 ? true : false;
        }
        public Task<bool> IsJobFamilyAlreadyExist(string jobFamilyName)
        {
            return _applicationDbcontext.JobFamilyTable.AnyAsync(x => x.JobFamilyName == jobFamilyName);
        }
    }
}
