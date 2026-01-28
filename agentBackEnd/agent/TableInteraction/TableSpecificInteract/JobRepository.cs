using agent.entityClasses;
using agent.TableInteraction.generic;
using agent.TableInteraction.TableSpecificInerfaces;

namespace agent.TableInteraction.TableSpecificInteract
{
    public class JobRepository : TableOperations<Job>, IJob
    {
        private readonly agentDbContextSqlite _context;

        public JobRepository(agentDbContextSqlite context) : base(context)
        {
            _context = context;
        }

        public void func()
        { }
    }
}