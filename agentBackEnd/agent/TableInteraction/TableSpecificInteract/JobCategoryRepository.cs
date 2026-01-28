using agent.entityClasses;
using agent.TableInteraction.generic;
using agent.TableInteraction.TableSpecificInerfaces;

namespace agent.TableInteraction.TableSpecificInteract
{
    public class JobCategoryRepository : TableOperations<JobCategory>, IJobCategory
    {
        private readonly agentDbContextSqlite _context;

        public JobCategoryRepository(agentDbContextSqlite context) : base(context)
        {
            _context = context;
        }

        public void func()
        { }
    }
}