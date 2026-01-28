import { useNavigate } from "react-router-dom";
import { useEffect } from "react";
import "./ExploreGlobe.css";

export default function RegionCountries({
  accordian_object,
  agency_CountryPairList,
  agencies,
  setSelectedAgency,
}) {
  const navigate = useNavigate();
  const handleAgencyClick = (agency) => {
    navigate("/Agency");
    setSelectedAgency(agency);
  };

  useEffect(() => {
    // Simple fix: ensure accordion elements are reset after render
    if (accordian_object && accordian_object.length > 0) {
      setTimeout(() => {
        const accordionElement = document.getElementById(
          "regionCountriesAccordion"
        );
        if (accordionElement) {
          // Remove any stale Bootstrap state and let it re-initialize naturally
          const collapseElements = accordionElement.querySelectorAll(
            ".accordion-collapse"
          );
          collapseElements.forEach((element) => {
            element.classList.remove("show");
            element.setAttribute("aria-expanded", "false");
          });
          const buttons =
            accordionElement.querySelectorAll(".accordion-button");
          buttons.forEach((button) => {
            button.classList.add("collapsed");
            button.setAttribute("aria-expanded", "false");
          });
        }
      }, 50);
    }
  }, [accordian_object]);

  return (
    <div
      className="accordion accordion-flush w-100 "
      id="regionCountriesAccordion"
    >
      {accordian_object ? (
        accordian_object.map((countryItem, index) => {
          const agencyList = agency_CountryPairList
            ? agency_CountryPairList.filter(
                (ac) => ac.countryId == countryItem.countryId
              )
            : null;

          return (
            <div className="accordion-item " key={index}>
              <h2 className="accordion-header " id={`flush-heading${index}`}>
                <button
                  className="accordion-button collapsed "
                  type="button"
                  data-bs-toggle="collapse"
                  data-bs-target={`#flush-collapse${index}`}
                  aria-expanded="false"
                  aria-controls={`flush-collapse${index}`}
                >
                  {countryItem.countryName}
                </button>
              </h2>
              <div
                id={`flush-collapse${index}`}
                className="accordion-collapse collapse"
                aria-labelledby={`flush-heading${index}`}
                data-bs-parent="#regionCountriesAccordion"
              >
                <div className="accordion-body ">
                  {agencyList ? (
                    agencyList.map((agency, idx) => {
                      return (
                        <p key={idx}>
                          {
                            agencies.find((a) => a.agencyId == agency.agencyId)
                              .agencyName
                          }
                          <span className="float-end">
                            <button
                              type="button"
                              class="btn btn-outline-info ps-4 pe-4 z-1"
                              onClick={() => handleAgencyClick(agency)}
                            >
                              visit
                            </button>
                          </span>
                        </p>
                      );
                    })
                  ) : (
                    <p>loading...</p>
                  )}
                </div>
              </div>
            </div>
          );
        })
      ) : (
        <p>loading...</p>
      )}
    </div>
  );
}
