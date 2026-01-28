using agent.entityClasses;
using agent.TableInteraction.generic;

namespace agent.TableInteraction.TableSpecificInerfaces
{
    public interface IJobCategory : ITableOperation<JobCategory>
    {
        public void func();
    }
}