import { Link, NavLink, useNavigate } from "react-router-dom";
import "./Nav.css";
import { useState, useEffect } from "react";
import "bootstrap-icons/font/bootstrap-icons.css";

export default function Nav({setSignInState,commonSignIn_SignUp_state,setSelectedJobFromJobPage,set_common_signIn_signUp_state,
  userLogedIn,setSelectedAgency,canPublish,setCanPublish
}) {

  const [theme, setTheme] = useState("light");
  const clearStateVariables=()=>{
    
      setSelectedJobFromJobPage(null);
      setSelectedAgency(null);
      setCanPublish(false);
    
  }

  const navigate =useNavigate();

  useEffect(() => {
    document.documentElement.setAttribute("data-bs-theme", theme);
  }, [theme]);

  return (
    <nav className={`navbar navbar-expand-lg bg-body-tertiary ${window.location.pathname=='/ExploreGlobe'||window.location.pathname=='/GlobalJobs'||
    window.location.pathname=='/Ratings'||canPublish==true?"navcss":""}`}>
      <div className="container-fluid bg-light">
        <NavLink className="navbar-brand " to="/Home">
          Agent
        </NavLink>
        <button
          className="navbar-toggler me-5 z-3"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarText"
          aria-controls="navbarText"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarText">
          <ul className="navbar-nav me-auto mb-2 mb-lg-0">
            <li className="nav-item me-3">
              <NavLink className={`nav-link ${commonSignIn_SignUp_state==1?"visually-hidden":" "}`} to="/Home" end>
                <span onClick={()=>clearStateVariables()}>Home</span>
              </NavLink>
            </li>
            <li className="nav-item me-3">
              <NavLink className={`nav-link ${commonSignIn_SignUp_state==1?"visually-hidden":" "}`} to="/ExploreGlobe">
                <span onClick={()=>clearStateVariables()}>Find Agent</span>
              </NavLink>
            </li>
            <li className="nav-item me-3">
              <NavLink className={`nav-link ${commonSignIn_SignUp_state==1?"visually-hidden":" "}`} to="/GlobalJobs">
                <span onClick={()=>clearStateVariables()}>Jobs</span>
              </NavLink>
            </li>
            <li className="nav-item me-3">
              <NavLink className={`nav-link ${commonSignIn_SignUp_state==1?"visually-hidden":" "}`} to="/AgencyRegistering">
                <span onClick={()=>clearStateVariables()}>Register & Publish</span>
              </NavLink>
            </li>
            <li className="nav-item me-3">
              <NavLink className={`nav-link ${commonSignIn_SignUp_state==1?"visually-hidden":" "}`} to="/Agency">
               <span onClick={()=>clearStateVariables()}>Agencies</span> 
              </NavLink>
            </li>
             <li className="nav-item me-3">
              <NavLink className={`nav-link ${commonSignIn_SignUp_state==1?"visually-hidden":" "}`} to="/Ratings">
                <span onClick={()=>clearStateVariables()}>Ratings</span>
              </NavLink>
            </li>
          </ul>
          <span className="navbar-text">
            <button type="button" className={`btn btn-primary me-1 ${commonSignIn_SignUp_state==0?"visually-hidden":" "} `} 
            onClick={()=>setSignInState(1)}>
               SignIn
            </button>
            <button type="button" className={`btn btn-outline-primary me-1 ${commonSignIn_SignUp_state==0?"visually-hidden":" "}`} 
             onClick={()=>setSignInState(0)} id="signup_button_id">
              SignUp
            </button>
            <div className="d-flex justify-content-center align-items-center gap-5">
              <p className={`fs-6 fw-lighter ${commonSignIn_SignUp_state==1?"visually-hidden":" "}`}>
                <i class="bi bi-person-circle"></i>
                <span className="ms-2 me-1">{userLogedIn?.firstName}</span>
                <span>{userLogedIn?.lastName}</span>
              </p>
            <button type="button" className={`btn btn-primary me-1 ${commonSignIn_SignUp_state==1?"visually-hidden":" "} `} 
             onClick={()=>{setSignInState(1);navigate('/');set_common_signIn_signUp_state(1)}}>
              Logout
            </button>
            </div>
            
            {/* <button
              type="button"
              className="btn btn-outline-secondary"
              onClick={() => setTheme(theme === "dark" ? "light" : "dark")}
            >
              {theme=="dark"?"dark":"light"}
            </button> */}
          </span>
        </div>
      </div>
    </nav>
  );
}
