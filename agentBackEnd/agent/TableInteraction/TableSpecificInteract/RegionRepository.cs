using agent.DTOs;
using agent.entityClasses;
using agent.TableInteraction.generic;
using agent.TableInteraction.TableSpecificInerfaces;
using Microsoft.EntityFrameworkCore;

namespace agent.TableInteraction.TableSpecificInteract
{
    public class RegionRepository : TableOperations<Region>, IRegion
    {
        private readonly agentDbContextSqlite _context;

        public RegionRepository(agentDbContextSqlite context) : base(context)
        {
            _context = context;
        }

        public List<CountryComDTO> getCountriesbyRegion(int regionId)
        {
            List<CountryComDTO> regionCountries = _context.Countries.Where(x => x.RegionId == regionId)
                                                              .Select(country => new CountryComDTO
                                                              {
                                                                  CountryName = country.CountryName,
                                                                  RegionId = country.RegionId,
                                                                  AgencyList = country.AgencyCountries.Select(ac => new AgencyOutDTO
                                                                  {
                                                                      AgencyName = ac.Agency.AgencyName
                                                                  }
                                                                  ).ToList()
                                                              }).ToList();
            return regionCountries;
        }
    }
}