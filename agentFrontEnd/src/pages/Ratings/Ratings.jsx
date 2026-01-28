import { useEffect, useState } from "react";
import Footer from "../../components/Footer";
import "./Ratings.css";
import TopListCard from "./TopListCard";
import TopThreeCard from "./TopThreeCard";
import UserReviewCard from "./UserReviewCard";
import axios from "axios";
import ReviewAccording from "./ReviewAccording";
import Toast from "../Toast";
import { baseurl } from "../../config";

export default function Ratings({
  agenciesWithLogos,
  agencyReviews,
  agencies,
  userLogedIn,
  setAgencyReviews,
  isEverythingReady,
}) {
  // Debug logging
  console.log("Ratings - baseurl:", baseurl);
  console.log("Ratings - agenciesWithLogos:", agenciesWithLogos);
  console.log("Ratings - agenciesWithLogos length:", agenciesWithLogos?.length);
  console.log("Ratings - agencyReviews:", agencyReviews);
  console.log("Ratings - agencyReviews length:", agencyReviews?.length);
  console.log("Ratings - isEverythingReady:", isEverythingReady);

  // Validate baseurl
  if (!baseurl) {
    console.error('Ratings - baseurl is not set! Check environment variables.');
  }

  const getAllAgencyReviews = async () => {
    try {
      console.log('Ratings - Loading reviews from:', `${baseurl}/api/User/reveiws`);
      const result = await axios.get(`${baseurl}/api/User/reveiws`);
      setAgencyReviews(result.data);
      console.log('Ratings - Reviews loaded:', result.data?.length, 'reviews');
    } catch (err) {
      console.error('Ratings - Error loading reviews:', err.message);
      // Don't set empty array here since this is a refresh, not initial load
    }
  };

  const [reviewObject, setReviewObject] = useState({
    agencyId: "",
    serviceNumber: "",
    reviewText: "",
    userId: userLogedIn?.userId,
  });

  const [reviewToast, setReviewToast] = useState(false);

  const handleReviewSubmit = async () => {
    try {
      const result = await axios.post(
        `${baseurl}/api/User/review`,
        reviewObject,
      );
      console.log(result.data);
      setReviewToast(true);
      setReviewObject({
        agencyId: "",
        serviceNumber: "",
        reviewText: "",
        userId: userLogedIn?.userId,
      });
      await getAllAgencyReviews();
    } catch (err) {
      console.error("Error submitting review:", err);
    }
  };

  // console.log(agencyReviews);
  return (
    <div>
      {isEverythingReady == true ? (
        <>
          <div className="maincontainer_Ratings">
            <div className="main_title_R ">
              <h1 className="display-3" id="ratingPage_main_title">
                “Reviews That Build Trust”
              </h1>
            </div>
            <div className="sub_title_R">
              <h1 className="display-6 fw-lighter fs-4">
                Top Agencies of the month
              </h1>
            </div>

            <div className="one_two_three_container">
              {agenciesWithLogos && agenciesWithLogos.length >= 3 ? (
                <>
                  <div className="two  common_all">
                    <TopThreeCard
                      name={agenciesWithLogos[1]?.agencyName || "loading..."}
                      logoPic={agenciesWithLogos[1]?.logo}
                    />
                    <div className="number common_all display-5 bg-secondary w-100 text-light flex-grow-1">
                      #2
                    </div>
                  </div>
                  <div className="one  common_all">
                    <TopThreeCard
                      name={agenciesWithLogos[0]?.agencyName || "loading..."}
                      logoPic={agenciesWithLogos[0]?.logo}
                    />
                    <div className="number common_all display-5 w-100 bg-dark text-light flex-grow-1">
                      #1
                    </div>
                  </div>
                  <div className="three common_all">
                    <TopThreeCard
                      name={agenciesWithLogos[2]?.agencyName || "loading..."}
                      logoPic={agenciesWithLogos[2]?.logo}
                    />
                    <div className="number common_all bg-warning w-100 display-5 text-light flex-grow-1">
                      #3
                    </div>
                  </div>
                </>
              ) : (
                <div
                  className="d-flex justify-content-center align-items-center"
                  style={{ minHeight: "200px" }}
                >
                  <div className="text-center">
                    <div
                      className="spinner-border text-primary mb-2"
                      role="status"
                    >
                      <span className="visually-hidden">Loading...</span>
                    </div>
                    <p className="text-muted">Loading top agencies...</p>
                  </div>
                </div>
              )}
            </div>

            <div className="sub_title_R">
              <h1 className="display-6 fw-lighter fs-4 ">User Reviews</h1>
            </div>

            <div className="toplist ">
              <ReviewAccording
                agenciesWithLogos={agenciesWithLogos}
                agencyReviews={agencyReviews}
                userLogedIn={userLogedIn}
                setAgencyReviews={setAgencyReviews}
              />
            </div>

            <div className="sub_title_R">
              <h1 className="display-3 fw-lighter ">Write a Review</h1>
              <h1 className="display-3 fw-lighter fs-4">
                Have you worked with an agency recently?
              </h1>
              <h1 className="display-3 fw-lighter fs-4">
                Help others stay safe and informed
              </h1>
            </div>

            <div className="writing_review_section">
              <div className="mb-3 ">
                <div className="d-flex align-items-center mb-2">
                  <label
                    htmlFor="exampleFormControlTextarea1"
                    className="form-label"
                  >
                    review
                  </label>
                  <label
                    htmlFor="exampleFormControlTextarea1"
                    className="form-label ms-5"
                  >
                    regarding:
                  </label>
                  <select
                    id="agency_to_give_review"
                    className="form-select w-25 ms-1"
                    aria-label="Default select example"
                    value={reviewObject.agencyId}
                    onChange={(e) =>
                      setReviewObject({
                        ...reviewObject,
                        agencyId: e.target.value,
                      })
                    }
                  >
                    <option value="">Open this select menu</option>
                    {agencies ? (
                      agencies?.map((agency, index) => {
                        return (
                          <option value={agency.agencyId} key={index}>
                            {agency.agencyName}
                          </option>
                        );
                      })
                    ) : (
                      <p>" "</p>
                    )}
                  </select>

                  <label className="form-label ms-3">Service No:</label>
                  <div className="w-20">
                    <input
                      type="text"
                      className="form-control ms-1 "
                      value={reviewObject.serviceNumber}
                      onChange={(e) =>
                        setReviewObject({
                          ...reviewObject,
                          serviceNumber: e.target.value,
                        })
                      }
                      id="review_text_box"
                    />
                  </div>
                </div>

                <textarea
                  className="form-control"
                  id="exampleFormControlTextarea1"
                  rows="3"
                  value={reviewObject.reviewText}
                  onChange={(e) =>
                    setReviewObject({
                      ...reviewObject,
                      reviewText: e.target.value,
                    })
                  }
                ></textarea>
              </div>
              <div className="d-grid gap-2 d-md-block">
                <button type="button" className="btn btn-outline-primary me-2">
                  clear
                </button>
                <button
                  className="btn btn-primary"
                  type="button"
                  onClick={() => handleReviewSubmit()}
                  id="submit_your_comment"
                >
                  submit
                </button>
              </div>
            </div>
          </div>
          <Toast
            showToast={reviewToast}
            setShowToast={setReviewToast}
            message={"✅ your review is added!"}
            id="review_ok_toast"
          />
          <Footer />
        </>
      ) : (
        <>
          <div className="starter_agent bg-light d-flex flex-column justify-content-center align-items-center">
            <p className="text-warning fw-lighter text-primary-emphasis fs-4">
              Agent is on the way
            </p>
            <div className="d-flex justify-content-center gap-1">
              <div
                className="spinner-grow spinner-grow-sm text-primary"
                role="status"
              >
                <span className="visually-hidden">Loading...</span>
              </div>
              <div
                className="spinner-grow spinner-grow-sm text-primary"
                role="status"
              >
                <span className="visually-hidden">Loading...</span>
              </div>
              <div
                className="spinner-grow spinner-grow-sm text-primary"
                role="status"
              >
                <span className="visually-hidden">Loading...</span>
              </div>
            </div>
          </div>
        </>
      )}
    </div>
  );
}
