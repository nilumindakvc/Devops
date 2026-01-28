using agent.entityClasses;
using agent.TableInteraction.generic;
using agent.TableInteraction.TableSpecificInerfaces;

namespace agent.TableInteraction.TableSpecificInteract
{
    public class AgencyReviewRepository : TableOperations<AgencyReview>, IAgencyReviews
    {
        private readonly agentDbContextSqlite _context;

        public AgencyReviewRepository(agentDbContextSqlite context) : base(context)
        {
            _context = context;
        }

        public void func()
        { }
    }
}