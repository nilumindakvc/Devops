import { useEffect, useState } from "react";
import AccordianHeader from "./AccordianHeader";
import "./GlobalJobs.css";

export default function JobCategory({ jobList, setSelectedJobFromJobPage }) {
  const [healthcare, setHealthcare] = useState(null);
  const [Construction, setConstruction] = useState(null);
  const [Hospitality, setHospitality] = useState(null);
  const [Logistic, setLogistic] = useState(null);
  const [CleanAndDomestic, setCleanAndDomestics] = useState(null);

  const filteringFunc = (jobList) => {
    if (jobList != null) {
      setHealthcare(
        jobList.filter((item) => item.categoryName == "Healthcare")
      );
      setConstruction(
        jobList.filter((item) => item.categoryName == "Construction")
      );
      setHospitality(
        jobList.filter((item) => item.categoryName == "Hospitality")
      );
      setLogistic(jobList.filter((item) => item.categoryName == "Logistics"));
      setCleanAndDomestics(
        jobList.filter((item) => item.categoryName == "Clean and Domestic")
      );
    }
  };

  useEffect(() => filteringFunc(jobList), [jobList]);

  useEffect(() => {
    // Simple fix: ensure accordion elements are reset after render
    if (
      healthcare ||
      Construction ||
      Hospitality ||
      Logistic ||
      CleanAndDomestic
    ) {
      setTimeout(() => {
        const accordionElement = document.getElementById(
          "jobCategoryAccordion"
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
  }, [healthcare, Construction, Hospitality, Logistic, CleanAndDomestic]);

  console.log(healthcare);
  console.log(CleanAndDomestic);

  return (
    <div
      className="accordion accordion-flush w-100  "
      id="jobCategoryAccordion"
    >
      <AccordianHeader
        jobList={healthcare}
        categoryName={"Healthcare"}
        setSelectedJobFromJobPage={setSelectedJobFromJobPage}
      />
      <AccordianHeader
        jobList={Construction}
        categoryName={"Construction"}
        setSelectedJobFromJobPage={setSelectedJobFromJobPage}
      />
      <AccordianHeader
        jobList={Hospitality}
        categoryName={"Hospitality"}
        setSelectedJobFromJobPage={setSelectedJobFromJobPage}
      />
      <AccordianHeader
        jobList={Logistic}
        categoryName={"Logistic"}
        setSelectedJobFromJobPage={setSelectedJobFromJobPage}
      />
      <AccordianHeader
        jobList={CleanAndDomestic}
        categoryName={"CleanAndDomestic"}
        setSelectedJobFromJobPage={setSelectedJobFromJobPage}
      />
    </div>
  );
}
