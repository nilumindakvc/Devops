import { useNavigate } from "react-router-dom";
import "./Login.css";
import { useState } from "react";
import axios from "axios";
import { baseurl } from "../../config";


export default function SignIn({set_common_signIn_signUp_state,setUserLogedIn}) {
    const navigate =useNavigate();
    const [loginErrorMessage,setLoginErrorMessage]=useState(false);


    const [loginUser,setLoginUser] =useState({
      email:"",
      password:""
    });

    const handleLogin=async(e)=>{
      e.preventDefault();
      try{
        const response =await axios.post(`${baseurl}/api/User/login`,loginUser);
        if(response.data){
          set_common_signIn_signUp_state(0);
          console.log(response.data)
          setUserLogedIn(response.data);
          localStorage.setItem("user",JSON.stringify(response.data));
          navigate("/Home");
          
        }
        if(!response.data){
          setLoginErrorMessage(true);
        }
      }catch(err){
        console.log(err);
      }
        
    }

    console.log(baseurl)
  return (
    <form id="signIn_form_id">
      <div class="mb-3">
        <p className="display-5 mb-4">SignIn</p>
        <label for="exampleInputEmail1" class="form-label" >
          Email address
        </label>
        <input
          type="email"
          class="form-control"
          id="email"
          aria-describedby="emailHelp"
          value={loginUser.email}
          onChange={(e)=>setLoginUser({...loginUser,email:e.target.value})}
        />
      </div>
      <div class="mb-3">
        <label for="exampleInputPassword1" class="form-label">
          Password
        </label>
        <input
          type="password"
          class="form-control"
          id="password"
          value={loginUser.password}
          onChange={(e)=>setLoginUser({...loginUser,password:e.target.value})}
        />
      </div>
       <div class="mb-3">
        <label for="exampleInputPassword1" class="form-label text-danger">
          {loginErrorMessage && "invalid credentials"}
        </label>
      </div>
      <div className="d-flex justify-content-center ">
        <button type="submit" class="btn btn-primary ps-5 pe-5 mt-2" id="signIn_button"
         onClick={(e)=>handleLogin(e)} >
          Sign In
        </button>
      </div>
    </form>
  );
}
