import { useNavigate } from "react-router-dom";
import "./Login.css";
import { useState } from "react";
import axios from "axios";
import { baseurl } from "../../config";
export default function SignUp({ setSignInState,commonSignIn_SignUp_state }) {
  const navigate = useNavigate();
  
  const handleSignUp=async(e)=>{
    e.preventDefault();
    try{
      const response =await axios.post(`${baseurl}/api/User/user`,newUser);
      response.data?setSignInState(1):setSignInState(0);
    }catch(err){
      console.log(err);
    }
  }

  const [newUser,setNewUser]=useState({
    firstName:'',
    lastName:'',
    email:'',
    password:''
  })
  console.log(newUser);
  return (
    <form id="signUp_form_id">
      <div class="mb-3">
        <p className="display-5 mb-4">SignUp</p>
        <label for="exampleInputEmail1" class="form-label">
          First Name
        </label>
        <input
          type="text"
          class="form-control"
          id="firstName"
          aria-describedby="emailHelp"
          value={newUser.firstName}
          onChange={(e)=>setNewUser({...newUser,firstName:e.target.value})}
        />
        <label for="exampleInputPassword1" class="form-label">
          Last Name
        </label>
        <input
          type="text"
          class="form-control"
          id="lastName"
          value={newUser.lastName}
          onChange={(e)=>setNewUser({...newUser,lastName:e.target.value})}
        />
        <label for="exampleInputPassword1" class="form-label">
          email
        </label>
        <input
          type="text"
          class="form-control"
          id="emailOnSignUp"
          value={newUser.email}
          onChange={(e)=>setNewUser({...newUser,email:e.target.value})}
        />
      </div>
      <div class="mb-3">
        <label for="exampleInputPassword1" class="form-label">
          Password
        </label>
        <input
          type="password"
          class="form-control"
          id="passwordOnSignUp"
           value={newUser.password}
          onChange={(e)=>setNewUser({...newUser,password:e.target.value})}
        />
      </div>
      <div className="d-flex justify-content-center ">
        <button
          type="submit"
          class="btn btn-primary ps-5 pe-5 mt-2"
          onClick={(e) => handleSignUp(e)}
          id="signUp_button"
        >
          Submit
        </button>
      </div>
    </form>
  );
}
