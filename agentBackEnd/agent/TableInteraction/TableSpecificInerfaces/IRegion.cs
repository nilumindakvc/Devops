using agent.DTOs;
using agent.entityClasses;
using agent.TableInteraction.generic;

namespace agent.TableInteraction.TableSpecificInerfaces
{
    public interface IRegion : ITableOperation<Region>
    {
        public List<CountryComDTO> getCountriesbyRegion(int regionId);
    }
}