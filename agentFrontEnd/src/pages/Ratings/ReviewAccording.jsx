import axios from "axios";
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
        `${baseurl}/api/User/review/${userId}/${serviceNumber}`,
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

  return (
    <div
      className="accordion accordion-flush w-100 modern-accordion"
      id="reviewAccordion"
    >
      {agenciesWithLogos ? (
        agenciesWithLogos.map((agency, index) => {
          const reviews = agencyReviews
            ? agencyReviews.filter((a) => a.agencyId == agency.agencyId)
            : null;
          return (
            <div className="accordion-item modern-accordion-item" key={index}>
              <h2 className="accordion-header" id={`flush-heading-${index}`}>
                <button
                  className="accordion-button collapsed d-flex align-items-center"
                  type="button"
                  data-bs-toggle="collapse"
                  data-bs-target={`#flush-collapse-${index}`}
                  aria-expanded="false"
                  aria-controls={`flush-collapse-${index}`}
                >
                  <span className="agency-accordion-name">
                    {agency.agencyName}
                  </span>
                  <span className="agency-accordion-stars ms-3">
                    {"⭐".repeat(agency.averageRating)}
                  </span>
                  <span className="review-count-badge ms-auto me-3">
                    {reviews ? reviews.length : 0} reviews
                  </span>
                </button>
              </h2>
              <div
                id={`flush-collapse-${index}`}
                className="accordion-collapse collapse"
                aria-labelledby={`flush-heading-${index}`}
                data-bs-parent="#reviewAccordion"
              >
                <div className="accordion-body modern-accordion-body">
                  {reviews && reviews.length > 0 ? (
                    reviews.map((review, idx) => {
                      return (
                        <div className="review-card" key={idx}>
                          <div className="review-card-header">
                            <div className="review-user-info">
                              <div className="review-avatar">
                                {review.userName
                                  ? review.userName.charAt(0).toUpperCase()
                                  : "U"}
                              </div>
                              <div className="review-meta">
                                <span className="review-user-name">
                                  {review.userName || "User"}
                                </span>
                                <span className="review-service-no">
                                  Service #{review.serviceNumber}
                                </span>
                              </div>
                            </div>
                            {review.userId == userLogedIn.userId && (
                              <button
                                className="review-delete-btn"
                                onClick={() =>
                                  handleDeleteReview(
                                    review.userId,
                                    review.serviceNumber,
                                  )
                                }
                                title="Delete review"
                              >
                                ✕
                              </button>
                            )}
                          </div>
                          <p className="review-text">{review.reviewText}</p>
                        </div>
                      );
                    })
                  ) : (
                    <div className="no-reviews">
                      <p>No reviews yet for this agency</p>
                    </div>
                  )}
                </div>
              </div>
            </div>
          );
        })
      ) : (
        <div className="text-center py-4">
          <div className="spinner-border text-primary" role="status">
            <span className="visually-hidden">Loading...</span>
          </div>
        </div>
      )}
    </div>
  );
}
