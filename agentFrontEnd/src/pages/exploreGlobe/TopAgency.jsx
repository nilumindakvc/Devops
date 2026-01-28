import { useNavigate } from "react-router-dom";
import "./ExploreGlobe.css";
export default function TopAgency(prop) {
  const navigate = useNavigate();

  const handleAgencyClick = (agency) => {
    navigate("/Agency");
    prop.setSelectedAgency(agency);
  };
  return (
    <div className="card_container_top_As position-relative " key={prop.index}>
      <div class="d-flex flex-column align-items-center justify-content-center p-3">
        <img
          src={`data:image/jpeg;base64,${prop.logoGot}`}
          alt="..."
          style={{ maxWidth: "150px", height: "auto" }}
        />
        <div class="card-body d-flex flex-column align-items-center justify-content-center mt-5">
          <h5 class="card-title">{prop.name}</h5>
          <p class="card-text mt-1">{prop.description}</p>
          <button
            href="#"
            class="btn btn-outline-secondary mt-3 position-absolute bottom-0"
            onClick={() => handleAgencyClick(prop.agency)}
          >
            visit Agent
          </button>
        </div>
      </div>
    </div>
  );
}
