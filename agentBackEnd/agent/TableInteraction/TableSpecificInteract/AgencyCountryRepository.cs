using agent.entityClasses;
using agent.TableInteraction.generic;
using agent.TableInteraction.TableSpecificInerfaces;

namespace agent.TableInteraction.TableSpecificInteract
{
    public class AgencyCountryRepository : TableOperations<AgencyCountry>, IAgencyCountry
    {
        private readonly agentDbContextSqlite _context;

        public AgencyCountryRepository(agentDbContextSqlite context) : base(context)
        {
            _context = context;
        }

        //public AgencyCountry createAgencyCountry(AgencyCountry newAgencyCountry)
        //{
        //    _context.AgencyCountries.Add(newAgencyCountry);
        //    _context.SaveChanges();
        //    return newAgencyCountry;
        //}
    }
}