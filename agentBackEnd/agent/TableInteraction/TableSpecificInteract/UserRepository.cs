using agent.entityClasses;
using agent.TableInteraction.generic;
using agent.TableInteraction.TableSpecificInerfaces;

namespace agent.TableInteraction.TableSpecificInteract
{
    public class UserRepository : TableOperations<User>, IUser
    {
        private readonly agentDbContextSqlite _context;

        public UserRepository(agentDbContextSqlite context) : base(context)
        {
            _context = context;
        }

        public void UserSpecificFunc()
        {
            //this function is only to deal with user table,not common
        }
    }
}