using agent.entityClasses;
using agent.TableInteraction.generic;

namespace agent.TableInteraction.TableSpecificInerfaces
{
    public interface IAgency : ITableOperation<Agency>
    {
        public AgencyCountry createAgencyCountry(AgencyCountry newAgencyCountry);
    }
}