import axios from 'axios'
import './AgencyPage.css'
import { useEffect, useRef, useState } from 'react'
import JobCard from './JobCard';
import Footer from "../../components/Footer";
import { use } from 'react';
import { baseurl } from '../../config';

export default function AgencyPage({selectedJobFromJobPage,agencies,selectedAgency}){

    // const [agencies,setAgencies]=useState(['']);
    const [agencyx,setAgencyx]=useState(null);
    const [logoGot,setLogoGot] =useState('');

    const [agencyJobList,setAgencyJobList]=useState([]);

    const target =useRef(null);

    const handleAgencies = async () => {
    try {
        
       if (selectedJobFromJobPage && agencies.length > 0) {
            const requiredAgency = agencies.find(a => a.agencyId == selectedJobFromJobPage.agencyId);
            setAgencyx(requiredAgency);
            await retriveLogo(requiredAgency.licenseNumber);
            await handleJobRetriew(requiredAgency.agencyId);

        } else if (selectedAgency && agencies.length > 0) {
            const requiredAgency = agencies.find(a => a.agencyId == selectedAgency.agencyId);
            setAgencyx(requiredAgency);
            await retriveLogo(requiredAgency.licenseNumber);
            await handleJobRetriew(requiredAgency.agencyId);
           
        }else if(agencies.length > 0){
            const firstAgency = agencies[0];
            setAgencyx(firstAgency);
            await retriveLogo(firstAgency.licenseNumber);
            await handleJobRetriew(firstAgency.agencyId);
        }

    } catch (error) {
        console.log(error);
    }
  };

    const handleSelect=async(e)=>{
        const agencyLNum = e.target.value;
        const  agencyObj = agencies.find(a=>a.licenseNumber==agencyLNum);
        setAgencyx(agencyObj);
        
        await retriveLogo(agencyObj.licenseNumber);
        await handleJobRetriew(agencyObj.agencyId);


    }

    const retriveLogo =async(license)=>{
        await axios.get(`${baseurl}/api/Agency/Logo/${license}`)
        .then(res=>setLogoGot(res.data))
        .catch(err=>console.log(err));
    }

    const handleJobRetriew=async(agencyId)=>{
        await axios.get(`${baseurl}/api/Job/jobsbyAgency/${agencyId}`)
        .then(res=>setAgencyJobList(res.data))
        .catch(err=>console.log(err));
    }

    const handlingScroll=()=>{
         if (target.current) {
         target.current.scrollIntoView({
           behavior: "smooth", 
           block: "start",    
      });
    }
    }
   

    useEffect(()=>{
        handleAgencies();      
    },[] );

    useEffect(() => {
        if (selectedJobFromJobPage && agencyJobList.length > 0) {
            handlingScroll();
        }
        }, [agencyJobList, selectedJobFromJobPage]);
    
    

    // console.log(agencies);
    // console.log(agencyx);
    console.log(selectedJobFromJobPage);


    return(
       <div className="agency_page_main_container">
        <div className="search_agencies mt-4">
         <select
            className="form-select w-50 me-2"
            aria-label="Default select example"
            value={agencyx?.licenseNumber || ""} 
            onChange={handleSelect}
            >
            {/* <option value="">Select agency</option> */}
            {agencies!=null?agencies.map((agency, index) => (
                <option value={agency.licenseNumber} key={index}>
                {agency.agencyName}
                </option>
            )):<p>...</p>}
         </select>
         
        </div>

        <div className='agency_description'>
        <div className="agency_logo_section">
            <div className="logo_agency_page">
                <img src={`data:image/jpeg;base64,${logoGot}`} class="img-fluid" alt="..."></img>
            </div>
            <div className="agency_name">
                  <h1 className="display-6 fw-lighter fs-4">{agencyx?agencyx.agencyName:""}</h1>
            </div>
        </div>
        <div className="agency_details_section">

            <div className="basic_details">
                <div className="title_column">
                <h1 className="display-6 fw-normal fs-6">Address</h1>
                <h1 className="display-6 fw-normal fs-6">Agency Name</h1>
                <h1 className="display-6 fw-normal fs-6">City</h1>
                <h1 className="display-6 fw-normal fs-6">Country</h1>
                <h1 className="display-6 fw-normal fs-6">Email</h1>
                <h1 className="display-6 fw-normal fs-6">License Number</h1>
                <h1 className="display-6 fw-normal fs-6">Phone</h1>
                <h1 className="display-6 fw-normal fs-6">Website</h1>

                
            </div>

            <div className="details_column">
                 <h1 className="display-6 fw-lighter fs-6">{agencyx?agencyx.address:""}</h1>
                 <h1 className="display-6 fw-lighter fs-6">{agencyx?agencyx.agencyName:""}</h1>
                 <h1 className="display-6 fw-lighter fs-6">{agencyx?agencyx.city:""}</h1>
                 <h1 className="display-6 fw-lighter fs-6">{agencyx?agencyx.country:""}</h1>
                 <h1 className="display-6 fw-lighter fs-6">{agencyx?agencyx.email:""}</h1>
                 <h1 className="display-6 fw-lighter fs-6">{agencyx?agencyx.licenseNumber:""}</h1>
                 <h1 className="display-6 fw-lighter fs-6">{agencyx?agencyx.phone:""}</h1>
                 <h1 className="display-6 fw-lighter fs-6">{agencyx?agencyx.website:""}</h1>

            </div>
              
            </div>

            <div className="sub_section">
                <div className="section_title">
                    <p className="display-6 text-warning fw-lighter">
                        Descrption
                    </p>
                </div>
                <div className="section_content">
                    <p className="fw-lighter fs-6 w-95">
                       {agencyx?agencyx.description:""} 
                    </p>
                </div>
            </div>

            <div className="sub_section">
                <div className="section_title">
                    <p className="display-6 text-warning fw-lighter">
                        Job Portral
                    </p>
                </div>
                <div className="section_content">
                    {agencyJobList.map((job,index)=>{

                    const jobRef = job.jobTitle === selectedJobFromJobPage?.jobTitle ? target : null;

                    return (
                     <div>   
                        <JobCard ref={jobRef}  jobTitle={job.jobTitle} description={job.jobDescription}
                        salary={job.salaryRange} requirements={job.requirement} deadline={job.deadline} category={job.categoryId}
                        country={job.countryName}  key={index} />
                     </div>)
                     
                    })}
                    <Footer/>
                </div>
            </div>

           
        </div>
        </div>
       </div>
    )
}