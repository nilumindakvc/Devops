import axios from "axios";
import { useEffect } from "react";
import { baseurl } from "../../config";

export default function ReviewAccording({
  agencyReviews,
  agenciesWithLogos,
  userLogedIn,
  setAgencyReviews,
}) {
  const handleDeleteReview = async (userId, serviceNumber) => {
    try {
      const response = await axios.delete(
        `${baseurl}/api/User/review/${userId}/${serviceNumber}`
      );
      await getAllAgencyReviews();
    } catch (err) {
      console.log(err);
    }
  };

  const getAllAgencyReviews = async () => {
    const result = await axios.get(`${baseurl}/api/User/reveiws`);
    setAgencyReviews(result.data);
  };

  useEffect(() => {
    // Simple fix: ensure accordion elements are reset after render
    if (agenciesWithLogos && agenciesWithLogos.length > 0) {
      setTimeout(() => {
        const accordionElement = document.getElementById("reviewAccordion");
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
  }, [agenciesWithLogos]);

  return (
    <div className="accordion accordion-flush w-100 " id="reviewAccordion">
      {agenciesWithLogos ? (
        agenciesWithLogos.map((agency, index) => {
          const reviews = agencyReviews
            ? agencyReviews.filter((a) => a.agencyId == agency.agencyId)
            : null;
          return (
            <div className="accordion-item " key={index}>
              <h2 className="accordion-header" id={`flush-heading-${index}`}>
                <button
                  className="accordion-button collapsed d-flex"
                  type="button"
                  data-bs-toggle="collapse"
                  data-bs-target={`#flush-collapse-${index}`}
                  aria-expanded="false"
                  aria-controls={`flush-collapse-${index}`}
                >
                  <p>{agency.agencyName}</p>
                  <p className="ms-2">{"⭐".repeat(agency.averageRating)}</p>
                </button>
              </h2>
              <div
                id={`flush-collapse-${index}`}
                className="accordion-collapse collapse"
                aria-labelledby={`flush-heading-${index}`}
                data-bs-parent="#reviewAccordion"
              >
                <div className="accordion-body ">
                  {reviews ? (
                    reviews.map((review, idx) => {
                      return (
                        <div
                          className="alert alert-success "
                          role="alert"
                          key={idx}
                        >
                          <div className="position-relative">
                            <p>{review.reviewText}</p>

                            {review.userId == userLogedIn.userId ? (
                              <button
                                className="btn btn-outline-primary-subtle position-absolute top-0 end-0 "
                                onClick={() =>
                                  handleDeleteReview(
                                    review.userId,
                                    review.serviceNumber
                                  )
                                }
                              >
                                x
                              </button>
                            ) : (
                              ""
                            )}
                          </div>
                          <hr />
                          <p>Service No:{review.serviceNumber}</p>
                        </div>
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
