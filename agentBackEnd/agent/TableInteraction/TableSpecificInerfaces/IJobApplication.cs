using agent.entityClasses;
using agent.TableInteraction.generic;

namespace agent.TableInteraction.TableSpecificInerfaces
{
    public interface IJobApplication : ITableOperation<JobApplication>
    {
        public void func();
    }
}