using agent.entityClasses;
using agent.TableInteraction.generic;
using agent.TableInteraction.TableSpecificInerfaces;

namespace agent.TableInteraction.TableSpecificInteract
{
    public class JobApplicationRepository : TableOperations<JobApplication>, IJobApplication
    {
        private readonly agentDbContextSqlite _context;

        public JobApplicationRepository(agentDbContextSqlite context) : base(context)
        {
            _context = context;
        }

        public void func()
        { }
    }
}