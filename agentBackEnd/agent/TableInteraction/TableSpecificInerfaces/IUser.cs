using agent.TableInteraction.generic;
using agent.entityClasses;

namespace agent.TableInteraction.TableSpecificInerfaces
{
    public interface IUser : ITableOperation<User>
    {
        public void UserSpecificFunc();
    }
}