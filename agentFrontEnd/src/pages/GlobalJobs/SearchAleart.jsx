import { useNavigate } from "react-router-dom";

export default function SearchAleart({des,job,setSelectedJobFromJobPage,index}){

  const navigate =useNavigate();
   
   const handleJobClick=(job)=>{
       setSelectedJobFromJobPage(job)
       navigate("/Agency");
    }
    return(
          <div className="alert_button_section" key={index}>
            <div class="alert alert-light w-75" role="alert">
              {des}
            </div>
            <button type="button" class="btn btn-outline-primary" onClick={()=>handleJobClick(job)}>
              Agent
            </button>
          </div>
    )
}