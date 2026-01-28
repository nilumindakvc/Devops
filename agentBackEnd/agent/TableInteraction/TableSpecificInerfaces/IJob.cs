using agent.entityClasses;
using agent.TableInteraction.generic;

namespace agent.TableInteraction.TableSpecificInerfaces
{
    public interface IJob : ITableOperation<Job>
    {
        public void func();
    }
}