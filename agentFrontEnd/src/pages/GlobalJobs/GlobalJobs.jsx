import { useEffect, useState } from "react";
import Footer from "../../components/Footer";
import Carsoul from "./Carsoul";
import "./GlobalJobs.css";
import JobCategory from "./JobCategory";
import UrgentOpenCard from "./UrgentOpenCard";
import SearchAleart from "./SearchAleart";
import axios from "axios";
import cleaningImage from "../../assets/cleaning.jpg";
import waiterImage from "../../assets/waiter.jpg";
import constructionImage from "../../assets/construction.jpg";
import logisticsImage from "../../assets/logistics.jpg";
import ct2Image from "../../assets/ct2.png";
import ct3Image from "../../assets/ct3.png";
import ct4Image from "../../assets/ct4.png";
import ct5Image from "../../assets/ct5.png";
import ct6Image from "../../assets/ct6.png";

export default function GlobalJobs({
  urgentlyOpenedJobs,
  jobList,
  setSelectedJobFromJobPage,
}) {
  const imageArray = [
    { img: cleaningImage },
    { img: waiterImage },
    { img: constructionImage },
    { img: logisticsImage },
  ];

  const categoryImages = {
    2: ct2Image,
    3: ct3Image,
    4: ct4Image,
    5: ct5Image,
    6: ct6Image,
  };

  const [searchedCountry, setSearchedCountry] = useState("");
  const [matchingJobs, setMatchingJobs] = useState(null);

  const filterJobsByCountry = () => {
    const matchedJobs = jobList.filter(
      (job) =>
        job.countryName ==
        searchedCountry.charAt(0).toUpperCase() + searchedCountry.slice(1),
    );
    setMatchingJobs(matchedJobs);
  };

  return (
    <>
      <div className="maincontainer_global_jobs">
        <div className="region_caption_global_jobs_1 mt-4 mb-3">
          <h1 className="display-3 mt-5 mb-4">"Discover Global Carreers"</h1>
        </div>
        {/* <div className="region_caption_global_jobs_1">
          <h1 class="display-5">Search latest</h1>
        </div> */}

        <div className="carsoul_container">
          <Carsoul imageArray={imageArray} />
        </div>

        <div className="region_caption_global_jobs_2">Carrer Categories</div>

        <div className="carrer_category_section ">
          <JobCategory
            jobList={jobList}
            setSelectedJobFromJobPage={setSelectedJobFromJobPage}
          />
        </div>

        <div className="region_caption_global_jobs_1  me-2 mt-5 mb-5 p-3 ">
          <h1 className="display-5 ">Urgent Openings</h1>
        </div>

        <div className="urgent_open mt-3 mb-5">
          {urgentlyOpenedJobs ? (
            urgentlyOpenedJobs.map((ujob, index) => (
              <UrgentOpenCard
                jobTitle={ujob.jobTitle}
                description={ujob.jobDescription}
                agency={ujob.agencyName}
                country={ujob.countryName}
                salary={ujob.salaryRange}
                image={categoryImages[ujob.categoryId]}
                index={index}
                setSelectedJobFromJobPage={setSelectedJobFromJobPage}
                ujob={ujob}
              />
            ))
          ) : (
            <>
              <div className="text-center">
                <div className="spinner-border" role="status">
                  <span className="visually-hidden">Loading...</span>
                </div>
              </div>
            </>
          )}
        </div>

        <div className="search-section mt-5">
          <h4 className="text-center mb-5">🌍 Search Jobs by Country</h4>
          <div className="search-box">
            <input
              className="form-control search-input"
              type="search"
              placeholder="Enter country name..."
              aria-label="Search"
              value={searchedCountry}
              onChange={(e) => setSearchedCountry(e.target.value)}
              onKeyDown={(e) => e.key === "Enter" && filterJobsByCountry()}
            />
            <button className="btn search-btn" onClick={filterJobsByCountry}>
              Search
            </button>
          </div>
        </div>

        <div className="search_result mt-4">
          {matchingJobs && matchingJobs.length > 0 ? (
            matchingJobs.map((job, index) => (
              <SearchAleart
                setSelectedJobFromJobPage={setSelectedJobFromJobPage}
                des={job.jobTitle}
                job={job}
                key={index}
              />
            ))
          ) : (
            <p>
              no matching result <span className="ms-4">(O_O)</span>
            </p>
          )}
        </div>
        <Footer />
      </div>
    </>
  );
}
