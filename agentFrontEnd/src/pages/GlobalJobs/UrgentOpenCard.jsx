import { useNavigate } from "react-router-dom";
import "./GlobalJobs.css";

export default function UrgentOpenCard(Props){

   const navigate =useNavigate();
   
   const handleJobClick=(job)=>{
       Props.setSelectedJobFromJobPage(job)
       navigate("/Agency");
    }
    return(
         
            <div class="card d-flex flex-column align-items-center justify-content-center p-4 urgent_open_card "  key={Props.index}>
              <img
                src={Props.image}
                class="card-img-top mb-3"
                alt="..."
                style={{ maxWidth: '60px', height: 'auto' }}
              />
              <div class="card-body">
                <h5 class="card-title">{Props.jobTitle}</h5>
                <p class="card-text">{Props.country}</p>
                <p class="card-text">{Props.salary}</p> 
              </div>
              <a  class="btn btn-outline-primary" onClick={()=>handleJobClick(Props.ujob)}>
                  Visit Agent
              </a>
            </div>
          
    )
}