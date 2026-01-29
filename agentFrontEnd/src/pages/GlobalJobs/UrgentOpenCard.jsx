import { useNavigate } from "react-router-dom";
import "./GlobalJobs.css";

export default function UrgentOpenCard(Props) {
  const navigate = useNavigate();

  const handleJobClick = (job) => {
    Props.setSelectedJobFromJobPage(job);
    navigate("/Agency");
  };

  return (
    <div
      className="card urgent-card p-4"
      key={Props.index}
      onClick={() => handleJobClick(Props.ujob)}
    >
      <div className="d-flex justify-content-center align-items-center mb-4">
        <img
          src={Props.image}
          alt={Props.jobTitle}
          style={{ width: "65px", height: "65px", objectFit: "contain" }}
        />
      </div>

      <div className="text-center flex-grow-1">
        <h6 className="fw-bold mb-3">{Props.jobTitle}</h6>
        <p className="text-muted small mb-2">📍 {Props.country}</p>
        <p className="text-success fw-medium mb-0">{Props.salary}</p>
      </div>

      <button className="btn view-btn btn-sm w-100 mt-4">View Details</button>
    </div>
  );
}
