import axios from "axios";
import { useEffect, useState } from "react";
import ct2Image from "../../assets/ct2.png";
import ct3Image from "../../assets/ct3.png";
import ct4Image from "../../assets/ct4.png";
import ct5Image from "../../assets/ct5.png";
import ct6Image from "../../assets/ct6.png";

export default function JobCard(props) {
  const categoryImages = {
    2: ct2Image,
    3: ct3Image,
    4: ct4Image,
    5: ct5Image,
    6: ct6Image,
  };

  return (
    <div
      className="card mb-3 w-100 job-card-modern"
      idx={props.index}
      ref={props.ref}
    >
      <div className="row g-0">
        <div className="col-md-3 d-flex flex-column align-items-center justify-content-center job-card-image-section">
          <img
            src={categoryImages[props.category]}
            className="img-fluid rounded"
            style={{ maxWidth: "70px", height: "auto" }}
            alt="..."
          />
        </div>
        <div className="col-md-9">
          <div className="card-body">
            <h5 className="card-title job-card-title">{props.jobTitle}</h5>
            <p className="card-text job-card-description">
              {props.description}
            </p>
            <div className="job-card-details">
              <span className="job-detail-item">
                <span className="job-detail-icon">📍</span> {props.country}
              </span>
              <span className="job-detail-item">
                <span className="job-detail-icon">💰</span> {props.salary}
              </span>
              <span className="job-detail-item">
                <span className="job-detail-icon">📋</span> {props.requirements}
              </span>
              <span className="job-detail-item deadline">
                <span className="job-detail-icon">📅</span> {props.deadline}
              </span>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
