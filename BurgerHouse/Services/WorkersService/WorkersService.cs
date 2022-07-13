using Data;

namespace BurgerHouse.Services.WorkersService
{
    public class WorkersService : IWorkersService
    {
        private ApplicationDbContext _context;

        public int GetRestrauntIdByWorkerId(int workerId)
        {
            return _context.WorkersAndRestraunts.First(x => x.WorkerId == workerId).RestrauntId;
        }

        public WorkersService(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
