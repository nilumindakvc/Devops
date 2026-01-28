using agent.entityClasses;
using agent.TableInteraction.generic;

namespace agent.TableInteraction.TableSpecificInerfaces
{
    public interface ICountry : ITableOperation<Country>
    {
        public void func();
    }
}