using agent.entityClasses;
using agent.TableInteraction.generic;
using agent.TableInteraction.TableSpecificInerfaces;

namespace agent.TableInteraction.TableSpecificInteract
{
    public class AgencyDocumentRepository : TableOperations<AgencyDocument>, IAgencyDocument
    {
        private readonly agentDbContextSqlite _context;

        public AgencyDocumentRepository(agentDbContextSqlite context) : base(context)
        {
            _context = context;
        }

        public void func()
        { }
    }
}