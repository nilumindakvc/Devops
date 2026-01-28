import { useEffect, useState } from "react";
import "./AgencyRegistering.css";
import "bootstrap-icons/font/bootstrap-icons.css";
import Footer from "../components/Footer";
import axios from "axios";
import Toast from "./Toast";
import JobCard from "./Agency/JobCard";
import EJobCard from "./EJobCard";
import { baseurl } from "../config";

export default function AgencyRegistering({
  setJobList,
  setAgencies,
  setUrgentlyOpenedJobs,
  agencies,
  jobList,
  canPublish,
  setCanPublish
}) {
  const [progress, setprogress] = useState(0);
  const [createOrUpdate, setCreateOrUpdate] = useState("create");

  const [logo, setlogo] = useState(null);
  const [doc1, setdoc1] = useState(null);
  const [doc2, setdoc2] = useState(null);

  const [agencyLicenseToUpdate, setAgencyLicenseToUpdate] = useState("");
  const [agencyPasswordToUpdate, setAgencyPasswordToUpdate] = useState("");

  const [licenseToPublish, setLicenseToPublish] = useState("");
  const [publishingAgencyJobList, setPublishingAgencyJobList] = useState(null);
  const [passwordToPublish, setPasswordToPublish] = useState();

  const [countriesAvailable, setCountriesAvailable] = useState(null);
  const [countryx, setCountryX] = useState(null);

  const [categoriesAvailable, setCategoriesAvailable] = useState(null);
  const [categoryx, setCategoryx] = useState();

  const [allRegions, setAllRegions] = useState(null);
  const [regionx, setRegionx] = useState();

  const [toastJobPublish, setToastJobPublish] = useState(false);
  const [toastRegisterST1, setToastRegisterST1] = useState(false);
  const [toastRegisterST2, setToastRegisterST2] = useState(false);
  const [toastRegisterST3, setToastRegisterST3] = useState(false);
  const [toastUpdate, setToastUpdate] = useState(false);

  const [agencyToRegister, setAgencyToRegister] = useState({
    agencyName: "",
    licenseNumber: "",
    email: "",
    phone: "",
    address: "",
    city: "",
    country: "",
    website: "",
    description: "",
    password: "",
  });
  const [fetchedAgency, setFetchedAgency] = useState({
    agencyName: "",
    licenseNumber: "",
    email: "",
    phone: "",
    address: "",
    city: "",
    country: "",
    website: "",
    description: "",
    password: "",
  });

  const [jobToPost, setJobToPost] = useState({
    jobTitle: "",
    jobDescription: "",
    categoryId: "",
    agencyId: "",
    countryId: "",
    salaryRange: "",
    requirements: "",
    isUrgent: false,
    deadLine: null,
    regionId: "",
    openedUrgently: false,
  });

  const [startCreation, setStartCreation] = useState(false);

  // console.log(agencyLicense);
  // console.log(agencyPassword);
  // console.log(categoryx);
  // console.log(agencyOfPublishing);
  console.log(jobToPost);

  const getAllJobs = async () => {
    try {
      const result = await axios.get(`${baseurl}/api/Job`);
      setJobList(result.data);

      const urgentlyOpens = result.data
        .filter((a) => a.openedUrgently == true)
        .slice(0, 5);
      setUrgentlyOpenedJobs(urgentlyOpens);
    } catch (err) {
      console.log(err);
    }
  };

  useEffect(() => {
    const jobPublishingAgency = agencies?.find(
      (a) => a.licenseNumber == licenseToPublish
    );
    setPublishingAgencyJobList(
      jobList?.filter((a) => a.agencyId == jobPublishingAgency?.agencyId)
    );
  }, [jobList]);

  const getAllAgencies = async () => {
    const response = await axios.get(`${baseurl}/api/Agency`);
    const allAgencies = response.data;
    setAgencies(allAgencies);
  };

  const handleSelectCountry = (e) => {
    const countryName = e.target.value;
    const country = countriesAvailable.find(
      (a) => a.countryName == countryName
    );
    setCountryX(country);
    setJobToPost({ ...jobToPost, countryId: country.countryId });
  };

  const handleSelectCategory = (e) => {
    const categoryName = e.target.value;
    const category = categoriesAvailable.find(
      (a) => a.categoryName == categoryName
    );
    setCategoryx(category);
    setJobToPost({
      ...jobToPost,
      categoryId: category.categoryId,
    });
  };

  const handleSelectRegion = (e) => {
    const region_name = e.target.value;
    const region = allRegions.find((a) => a.regionName == region_name);
    setRegionx(region);
    setJobToPost({
      ...jobToPost,
      regionId: region.regionId,
    });
  };

  const fetchAgency = async () => {
    await axios
      .get(`${baseurl}/api/Agency/${agencyLicenseToUpdate}`)
      .then((res) => setFetchedAgency(res.data))
      .catch((err) => console.log(err));

    console.log(fetchedAgency);
  };

  const handleUpdate = async () => {
    axios
      .put(
        `${baseurl}/api/Agency/${agencyLicenseToUpdate}`,
        fetchedAgency
      )
      .then((res) => {
        console.log("successfully Updated", res.data);
        setToastUpdate(true);
        setFetchedAgency({
          agencyName: "",
          licenseNumber: fetchedAgency.licenseNumber,
          email: "",
          phone: "",
          address: "",
          city: "",
          country: "",
          website: "",
          description: "",
          password: "",
        });
        getAllAgencies();
      })
      .catch((err) => console.log(err));
  };

  async function handleUploads(
    filename_1,
    filename_2,
    filename_3,
    file_1,
    file_2,
    file_3,
    diterminent
  ) {
    if (!file_1 || !file_2 || !file_3) {
      return;
    }

    const formdata = new FormData();
    formdata.append(filename_1, file_1);
    formdata.append(filename_2, file_2);
    formdata.append(filename_3, file_3);

    try {
      if (diterminent == "update") {
        const res = await axios.post(
          `${baseurl}/api/Agency/Uploads/${fetchedAgency.licenseNumber}`,
          formdata,
          {
            headers: {
              "Content-Type": "multipart/form-data",
            },
          }
        );
        console.log(res.data);
        setToastUpdate(true);
      }

      if (diterminent == "create") {
        const res = await axios.post(
          `${baseurl}/api/Agency/Uploads/${agencyToRegister.licenseNumber}`,
          formdata,
          {
            headers: {
              "Content-Type": "multipart/form-data",
            },
          }
        );
        console.log(res.data);
        setToastRegisterST3(true);
      }
    } catch (err) {
      console.error("upload erro", err);
    }
  }

  const handleNewAgencyRegistering = async () => {
    const response = await axios.post(
      `${baseurl}/api/Agency/createAgency`,
      agencyToRegister
    );
    setAgencyToRegister({
      agencyName: "",
      licenseNumber: agencyToRegister.licenseNumber,
      email: "",
      phone: "",
      address: "",
      city: "",
      country: "",
      website: "",
      description: "",
      password: "",
    });
    getAllAgencies();
    console.log(response.data);
  };

  const checkForPublishing = async () => {
    axios
      .get(`${baseurl}/api/Agency/${licenseToPublish}`)
      .then((res) => {
        if (res.data.password==passwordToPublish) {
          setCanPublish(true);
          const requiredAgency = agencies?.find(
            (a) => a.licenseNumber == licenseToPublish
          );
          setJobToPost({ ...jobToPost, agencyId: requiredAgency.agencyId });
          setPublishingAgencyJobList(
            jobList?.filter((a) => a.agencyId == requiredAgency.agencyId)
          );
        } else {
          setCanPublish(false);
        }
      })
      .catch((err) => console.log(err));

    getAvailableCountries();
    getAvailableCategories();
    getAvailableRegions();
  };

  const getAvailableCountries = async () => {
    axios
      .get(`${baseurl}/api/Country`)
      .then((res) => setCountriesAvailable(res.data))
      .catch((err) => console.log(err));

    console.log(countriesAvailable);
  };

  const getAvailableCategories = async () => {
    axios
      .get(`${baseurl}/api/Job/categories`)
      .then((res) => setCategoriesAvailable(res.data))
      .catch((err) => console.log(err));
  };

  const getAvailableRegions = async () => {
    try {
      const regions = await axios.get(`${baseurl}/api/Region`);
      setAllRegions(regions.data);
    } catch (err) {
      console.log(err);
    }
  };
  const handleJobPosting = async () => {
    axios
      .post(`${baseurl}/api/Job`, jobToPost)
      .then((res) => {
        setToastJobPublish(true);
        console.log(res.data);
        setJobToPost({
          jobTitle: "",
          jobDescription: "",
          categoryId: "",
          agencyId: "",
          countryId: "",
          salaryRange: "",
          requirements: "",
          isUrgent: false,
          deadLine: null,
          regionId: "",
          openedUrgently: false,
        });
        getAllJobs();
      })
      .catch((err) => console.log(err));
  };

  // console.log(jobToPost);
  console.log(fetchedAgency);
  // console.log(allRegions);
  return (
    <>
      <div className="maincontainer_agency_registering">
        <div className="main_title_AR bg-secondary-subtle">
          <h1 class="display-3">“Join As a Verified Agency”</h1>
          <p class="fw-lighter fs-3">
            Reach thousands of overseas job seekers and build trust with
            verified credentials
          </p>
        </div>
        <div className="select_create_update">
          <button
            className="btn btn-secondary w-50 h-100 rounded-0"
            onClick={() => setCreateOrUpdate("create")}
          >
            {" "}
            Create your Agency
          </button>
          <button
            className="btn btn-warning w-50 h-100 rounded-0"
            onClick={() => setCreateOrUpdate("update")}
          >
            Update
          </button>
        </div>
        {createOrUpdate == "create" ? (
          <>
            <div className="step_progress_sec1 ">
              <div className="icon">
                <i class="bi bi-flag fs-3"></i>
                <p class="fs-6">Create Account</p>
              </div>
              <div className="icon">
                <i class="bi bi-file-text-fill fs-3"></i>
                <p class="fs-6">Agency Information</p>
              </div>
              <div className="icon">
                <i class="bi bi-file-earmark-medical fs-3"></i>
                <p class="fs-6">Certification Upload</p>
              </div>
              <div className="icon">
                <i class="bi bi-search fs-3"></i>
                <p class="fs-6">Approval</p>
              </div>
              <div className="icon">
                <i class="bi bi-unlock2-fill fs-3"></i>
                <p class="fs-6">Publish</p>
              </div>
            </div>
            <div className="step_progress_sec2 ">
              <div
                class="progress"
                role="progressbar"
                aria-label="Example 20px high"
                aria-valuenow={progress}
                aria-valuemin="0"
                aria-valuemax="100"
                style={{ height: "15px" }}
              >
                <div
                  class="progress-bar bg-secondary"
                  style={{ width: `${progress}%` }}
                ></div>
              </div>
            </div>
            {startCreation == true ? (
              <>
                <div className="d-flex justify-content-end">
                  <button
                    className="btn  bg-primary-subtle "
                    onClick={() => setStartCreation(false)}
                  >
                    Cancel Registering
                  </button>
                </div>
                <div className="sub_title_AR">
                  <h1 class="display-6 fw-lighter fs-4">Create Account</h1>
                </div>
                <div className="account_creation bg-secondary-subtle">
                  <div className="account_creation_coloumn">
                    <div class="mb-3">
                      <label for="exampleInputPassword1" class="form-label">
                        Email
                      </label>
                      <input
                        type="email"
                        class="form-control"
                        id="exampleInputPassword1"
                        value={agencyToRegister.email}
                        onChange={(e) =>
                          setAgencyToRegister({
                            ...agencyToRegister,
                            email: e.target.value,
                          })
                        }
                      />
                    </div>
                    <div class="mb-3">
                      <label for="exampleInputPassword1" class="form-label">
                        Phone
                      </label>
                      <input
                        type="text"
                        class="form-control"
                        id="exampleInputPassword1"
                        value={agencyToRegister.phone}
                        onChange={(e) =>
                          setAgencyToRegister({
                            ...agencyToRegister,
                            phone: e.target.value,
                          })
                        }
                      />
                    </div>
                  </div>
                  <div className="account_creation_coloumn">
                    <div class="mb-3">
                      <label for="exampleInputPassword1" class="form-label">
                        Password
                      </label>
                      <input
                        type="password"
                        class="form-control"
                        id="exampleInputPassword1"
                      />
                    </div>
                    <div class="mb-3">
                      <label for="exampleInputPassword1" class="form-label">
                        Site URL
                      </label>
                      <input
                        type="text"
                        class="form-control"
                        id="exampleInputPassword1"
                        value={agencyToRegister.website}
                        onChange={(e) =>
                          setAgencyToRegister({
                            ...agencyToRegister,
                            website: e.target.value,
                          })
                        }
                      />
                    </div>
                  </div>
                  <div className="account_creation_coloumn">
                    <div class="mb-3">
                      <label for="exampleInputPassword1" class="form-label">
                        Confirm Password
                      </label>
                      <input
                        type="password"
                        class="form-control"
                        id="exampleInputPassword1"
                        value={agencyToRegister.password}
                        onChange={(e) =>
                          setAgencyToRegister({
                            ...agencyToRegister,
                            password: e.target.value,
                          })
                        }
                      />
                    </div>
                  </div>
                </div>
                <div className="button_group">
                  <button type="button" class="btn btn-outline-secondary">
                    Clear
                  </button>
                  <button
                    type="button"
                    class="btn btn-secondary ms-2"
                    onClick={() => {
                      if (
                        agencyToRegister.email != "" &&
                        agencyToRegister.phone != "" &&
                        agencyToRegister.password != "" &&
                        agencyToRegister.website != ""
                      ) {
                        setprogress(20);
                        setToastRegisterST1(true);
                      } else {
                        setprogress(0);
                      }
                    }}
                  >
                    Confirm
                  </button>
                </div>

                <div className="sub_title_AR">
                  <h1 class="display-6 fw-lighter fs-4">Agency Information</h1>
                </div>
                <div className="account_creation bg-secondary-subtle">
                  <div className="account_creation_coloumn">
                    <div class="mb-3">
                      <label for="exampleInputPassword1" class="form-label">
                        Legal Name
                      </label>
                      <input
                        type="text"
                        class="form-control"
                        id="exampleInputPassword1"
                        value={agencyToRegister.agencyName}
                        onChange={(e) =>
                          setAgencyToRegister({
                            ...agencyToRegister,
                            agencyName: e.target.value,
                          })
                        }
                      />
                    </div>
                    <div class="mb-3">
                      <label for="exampleInputPassword1" class="form-label">
                        Office Address
                      </label>
                      <input
                        type="text"
                        class="form-control"
                        id="exampleInputPassword1"
                        value={agencyToRegister.address}
                        onChange={(e) =>
                          setAgencyToRegister({
                            ...agencyToRegister,
                            address: e.target.value,
                          })
                        }
                      />
                    </div>
                  </div>
                  <div className="account_creation_coloumn">
                    <div class="mb-3">
                      <label for="exampleInputPassword1" class="form-label">
                        City
                      </label>
                      <input
                        type="text"
                        class="form-control"
                        id="exampleInputPassword1"
                        value={agencyToRegister.city}
                        onChange={(e) =>
                          setAgencyToRegister({
                            ...agencyToRegister,
                            city: e.target.value,
                          })
                        }
                      />
                    </div>
                    <div class="mb-3">
                      <label for="exampleInputPassword1" class="form-label">
                        Licence Number
                      </label>
                      <input
                        type="text"
                        class="form-control"
                        id="exampleInputPassword1"
                        value={agencyToRegister.licenseNumber}
                        onChange={(e) =>
                          setAgencyToRegister({
                            ...agencyToRegister,
                            licenseNumber: e.target.value,
                          })
                        }
                      />
                    </div>
                  </div>
                  <div className="account_creation_coloumn">
                    <div class="mb-3">
                      <label for="exampleInputPassword1" class="form-label">
                        Country of Registered
                      </label>
                      <input
                        type="text"
                        class="form-control"
                        id="exampleInputPassword1"
                        value={agencyToRegister.country}
                        onChange={(e) =>
                          setAgencyToRegister({
                            ...agencyToRegister,
                            country: e.target.value,
                          })
                        }
                      />
                    </div>
                    <div class="mb-3">
                      <label for="exampleInputPassword1" class="form-label">
                        Description
                      </label>
                      <input
                        type="text"
                        class="form-control"
                        id="exampleInputPassword1"
                        value={agencyToRegister.description}
                        onChange={(e) =>
                          setAgencyToRegister({
                            ...agencyToRegister,
                            description: e.target.value,
                          })
                        }
                      />
                    </div>
                  </div>
                </div>
                <div className="button_group">
                  <button type="button" class="btn btn-outline-secondary">
                    Clear
                  </button>
                  <button
                    type="button"
                    class="btn btn-secondary ms-2"
                    onClick={() => {
                      if (
                        agencyToRegister.agencyName != "" &&
                        agencyToRegister.address != "" &&
                        agencyToRegister.city != "" &&
                        agencyToRegister.licenseNumber != "" &&
                        agencyToRegister.country != "" &&
                        agencyToRegister.description != ""
                      ) {
                        setprogress(40);
                        setToastRegisterST2(true);
                        handleNewAgencyRegistering();
                      } else {
                        setprogress(20);
                      }
                    }}
                  >
                    Confirm
                  </button>
                </div>
                <div className="sub_title_AR">
                  <h1 class="display-6 fw-lighter fs-4">
                    Certification upload
                  </h1>
                </div>
                <div className="account_creation bg-secondary-subtle">
                  <div className="account_creation_coloumn">
                    <div class="input-group mb-3">
                      <label class="input-group-text" for="inputGroupFile01">
                        Logo in JPG
                      </label>
                      <input
                        type="file"
                        class="form-control"
                        id="inputGroupFile01"
                        onChange={(e) => setlogo(e.target.files[0])}
                      />
                    </div>
                    <div class="input-group mb-3">
                      <label class="input-group-text" for="inputGroupFile02">
                        Doc2
                      </label>
                      <input
                        type="file"
                        class="form-control"
                        id="inputGroupFile02"
                        onChange={(e) => setdoc1(e.target.files[0])}
                      />
                    </div>
                    <div class="input-group mb-3">
                      <label class="input-group-text" for="inputGroupFile03">
                        Doc2
                      </label>
                      <input
                        type="file"
                        class="form-control"
                        id="inputGroupFile03"
                        onChange={(e) => setdoc2(e.target.files[0])}
                      />
                    </div>
                  </div>
                </div>
                <div className="button_group">
                  <button type="button" class="btn btn-outline-secondary">
                    Clear
                  </button>
                  <button
                    type="button"
                    class="btn btn-secondary ms-2"
                    onClick={() =>
                      handleUploads(
                        "logo",
                        "doc1",
                        "doc2",
                        logo,
                        doc1,
                        doc2,
                        "create"
                      )
                    }
                  >
                    Upload
                  </button>
                </div>
                <Toast
                  showToast={toastRegisterST1}
                  setShowToast={setToastRegisterST1}
                  message={"✅ acccount creation is ok!"}
                />
                <Toast
                  showToast={toastRegisterST2}
                  setShowToast={setToastRegisterST2}
                  message={"✅ agency information is ok!"}
                />
                <Toast
                  showToast={toastRegisterST3}
                  setShowToast={setToastRegisterST3}
                  message={"✅ document uploaded is ok!"}
                />
              </>
            ) : (
              <>
                <div className="d-flex justify-content-end">
                  <button
                    className="btn  bg-primary-subtle "
                    onClick={() => setStartCreation(true)}
                  >
                    Start Registering
                  </button>
                </div>
              </>
            )}
            <div className="last_title_AR ">
              <h1 class="display-5">
                ⚠️ Important Notice to All Registering Agencies
              </h1>
              <p class="fw-lighter fs-6 w-95">
                To maintain the integrity, transparency, and safety of our
                platform for all users, it is mandatory that all agencies submit
                only valid, verifiable, and up-to-date documents during the
                registration process. This includes official business
                registration certificates, government-issued recruitment
                licenses, and any other relevant certifications. All uploaded
                documents will undergo strict manual verification by our
                compliance team. Any attempt to submit forged, expired, or
                misleading information will result in immediate rejection of the
                registration and may lead to blacklisting of the agency on our
                platform. By continuing, you acknowledge that: You are
                authorized to represent the agency you are registering. All
                provided information and documents are true and legally valid.
                You understand that approved agencies will be publicly visible
                and subject to user reviews. Misrepresentation may lead to legal
                consequences under relevant laws. We appreciate your cooperation
                in helping us build a trustworthy environment for global job
                seekers and partners.
              </p>
            </div>
            <div className="sub_title_AR">
              <h1 class="display-6 fw-lighter fs-4 ">Publish Section</h1>
            </div>
            <div className="publish_section ">
              <div className="sub_publish_section bg-success-subtle">
                <div class="mb-4 w-25">
                  <label class="form-label">License Number</label>
                  <input
                    type="text"
                    class="form-control"
                    value={licenseToPublish}
                    onChange={(e) => setLicenseToPublish(e.target.value)}
                  />
                </div>
                <div class="mb-4 w-25">
                  <label class="form-label">Password</label>
                  <input
                    type="password"
                    class="form-control"
                    value={passwordToPublish}
                    onChange={(e) => setPasswordToPublish(e.target.value)}
                  />
                </div>
                <button
                  type="submit"
                  class="btn btn-secondary w-25 mb-4"
                  onClick={checkForPublishing}
                >
                  Submit
                </button>
              </div>
            </div>
            {canPublish ? (
              <>
                <div className="sub_title_AR">
                  <h1 class="display-6 fw-lighter fs-4">Job Posting</h1>
                </div>
                <>
                  <div className="account_creation bg-secondary-subtle">
                    <div className="account_creation_coloumn">
                      <div class="mb-3">
                        <label for="exampleInputPassword1" class="form-label">
                          Job Title
                        </label>
                        <input
                          type="text"
                          class="form-control"
                          value={jobToPost.jobTitle}
                          onChange={(e) =>
                            setJobToPost({
                              ...jobToPost,
                              jobTitle: e.target.value,
                            })
                          }
                        />
                      </div>
                      <div class="mb-3">
                        <label for="exampleInputPassword1" class="form-label">
                          description
                        </label>
                        <input
                          type="text"
                          class="form-control"
                          value={jobToPost.jobDescription}
                          onChange={(e) => {
                            return setJobToPost({
                              ...jobToPost,
                              jobDescription: e.target.value,
                            });
                          }}
                        />
                      </div>
                      <div class="mb-3">
                        <label for="exampleInputPassword1" class="form-label">
                          category ID
                        </label>
                        <select
                          className="form-select w-100 "
                          aria-label="Default select example"
                          value={categoryx?.categoryName || ""}
                          onChange={handleSelectCategory}
                        >
                          <option value="">Select Category</option>
                          {categoriesAvailable!=null?categoriesAvailable.map((category, index) =>{
                             return (
                            <option value={category.categoryName} key={index}>
                              {category.categoryName}
                            </option>
                          )}):<option>...</option>}
                        </select>
                      </div>
                    </div>
                    <div className="account_creation_coloumn">
                      <div class="mb-3">
                        <label for="exampleInputPassword1" class="form-label">
                          country ID
                        </label>
                        <select
                          className="form-select w-100 "
                          aria-label="Default select example"
                          value={countryx?.countryName || ""}
                          onChange={handleSelectCountry}
                        >
                          <option value="">Select country</option>
                          {countriesAvailable!=null?countriesAvailable.map((country, index) =>{ 
                            return(
                            <option value={country.countryName} key={index}>
                              {country.countryName}
                            </option>
                          )}):<option>...</option>}
                        </select>
                      </div>
                      <div class="mb-3">
                        <label for="exampleInputPassword1" class="form-label">
                          Salary Range
                        </label>
                        <input
                          type="text"
                          class="form-control"
                          value={jobToPost.salaryRange}
                          onChange={(e) =>
                            setJobToPost({
                              ...jobToPost,
                              salaryRange: e.target.value,
                            })
                          }
                        />
                      </div>

                      <div class="mb-3">
                        <label for="exampleInputPassword1" class="form-label">
                          Requirements
                        </label>
                        <input
                          type="text"
                          class="form-control"
                          value={jobToPost.requirements}
                          onChange={(e) =>
                            setJobToPost({
                              ...jobToPost,
                              requirements: e.target.value,
                            })
                          }
                        />
                      </div>
                    </div>
                    <div className="account_creation_coloumn">
                      <div class="mb-3">
                        <label for="exampleInputPassword1" class="form-label">
                          Urgent Vacancy
                        </label>

                        <input
                          type="checkbox"
                          value={jobToPost.isUrgent}
                          className="form-check-input ms-4"
                          onChange={(e) =>
                            setJobToPost({
                              ...jobToPost,
                              isUrgent: e.target.checked,
                            })
                          }
                        />
                      </div>
                      <div class="mb-3">
                        <label for="exampleInputPassword1" class="form-label">
                          Newly Opened
                        </label>

                        <input
                          type="checkbox"
                          value={jobToPost.openedUrgently}
                          className="form-check-input ms-4"
                          onChange={(e) =>
                            setJobToPost({
                              ...jobToPost,
                              openedUrgently: e.target.checked,
                            })
                          }
                        />
                      </div>

                      <div class="mb-3">
                        <label for="exampleInputPassword1" class="form-label">
                          Deadline
                        </label>
                        <input
                          type="date"
                          class="form-control"
                          value={jobToPost.deadLine}
                          onChange={(e) =>
                            setJobToPost({
                              ...jobToPost,
                              deadLine: e.target.value,
                            })
                          }
                        />
                      </div>
                      <label for="exampleInputPassword1" class="form-label">
                        Region Id
                      </label>
                      <select
                        className="form-select w-50 "
                        aria-label="Default select example"
                        value={regionx?.regionName || ""}
                        onChange={handleSelectRegion}
                      >
                        <option value="">Select Region</option>
                        {allRegions
                          ? allRegions.map((region, index) => (
                              <option value={region.regionName} key={index}>
                                {region.regionName}
                              </option>
                            ))
                          : "loading"}
                      </select>
                    </div>
                  </div>

                  <div className="button_group">
                    <button type="button" class="btn btn-outline-warning">
                      Clear
                    </button>
                    <button
                      type="button"
                      class="btn btn-warning ms-2"
                      onClick={handleJobPosting}
                    >
                      Post
                    </button>
                  </div>
                </>
                <div className="sub_title_AR">
                  <h1 class="display-6 fw-lighter fs-4 mb-5">Posted Jobs</h1>
                </div>
                <div className="d-flex flex-column align-items-start gap-2 w-75 h-auto mt-3">
                  {publishingAgencyJobList?.map((job, index) => {
                    return (
                      <div>
                        <EJobCard
                          jobTitle={job.jobTitle}
                          description={job.jobDescription}
                          salary={job.salaryRange}
                          requirements={job.requirement}
                          deadline={job.deadline}
                          category={job.categoryId}
                          country={job.countryName}
                          idx={index}
                          deletebutton={true}
                          setJobList={setJobList}
                          setUrgentlyOpenedJobs={setUrgentlyOpenedJobs}
                          jobId={job.jobId}
                          setPublishingAgencyJobList={
                            setPublishingAgencyJobList
                          }
                          agencies={agencies}
                          licenseToPublish={licenseToPublish}
                          jobList={jobList}
                        />
                      </div>
                    );
                  })}
                </div>
                <div className="w-75 h-auto">
                  <Toast
                    showToast={toastJobPublish}
                    setShowToast={setToastJobPublish}
                    message={"✅ your job is published successfully!"}
                  />
                </div>
              </>
            ) : (
              <>
                <h1 class="display-5 mb-4">🔒 No publishing is available</h1>
              </>
            )}
          </>
        ) : (
          <>
            <div class="mb-4 w-25">
              <label class="form-label">License Number</label>
              <input
                type="text"
                class="form-control"
                value={agencyLicenseToUpdate}
                onChange={(e) => setAgencyLicenseToUpdate(e.target.value)}
              />
            </div>
            <div class="mb-4 w-25">
              <label class="form-label">Password</label>
              <input
                type="password"
                class="form-control"
                value={agencyPasswordToUpdate}
                onChange={(e) => setAgencyPasswordToUpdate(e.target.value)}
              />
            </div>

            <button
              type="submit"
              class="btn btn-secondary w-25 mb-4"
              onClick={fetchAgency}
            >
              Submit
            </button>
            <div className="sub_title_AR">
              <h1 class="display-6 fw-lighter fs-4">Agency Information</h1>
            </div>
            <>
              <div className="account_creation bg-secondary-subtle">
                <div className="account_creation_coloumn">
                  <div class="mb-3">
                    <label for="exampleInputPassword1" class="form-label">
                      Legal Name
                    </label>
                    <input
                      type="text"
                      class="form-control"
                      value={fetchedAgency.agencyName}
                      onChange={(e) =>
                        setFetchedAgency({
                          ...fetchedAgency,
                          agencyName: e.target.value,
                        })
                      }
                    />
                  </div>
                  <div class="mb-3">
                    <label for="exampleInputPassword1" class="form-label">
                      Office Address
                    </label>
                    <input
                      type="text"
                      class="form-control"
                      value={fetchedAgency.address}
                      onChange={(e) =>
                        setFetchedAgency({
                          ...fetchedAgency,
                          address: e.target.value,
                        })
                      }
                    />
                  </div>
                  <div class="mb-3">
                    <label for="exampleInputPassword1" class="form-label">
                      Email
                    </label>
                    <input
                      type="text"
                      class="form-control"
                      value={fetchedAgency.email}
                      onChange={(e) =>
                        setFetchedAgency({
                          ...fetchedAgency,
                          email: e.target.value,
                        })
                      }
                    />
                  </div>
                </div>
                <div className="account_creation_coloumn">
                  <div class="mb-3">
                    <label for="exampleInputPassword1" class="form-label">
                      City
                    </label>
                    <input
                      type="text"
                      class="form-control"
                      value={fetchedAgency.city}
                      onChange={(e) =>
                        setFetchedAgency({
                          ...fetchedAgency,
                          city: e.target.value,
                        })
                      }
                    />
                  </div>
                  <div class="mb-3">
                    <label for="exampleInputPassword1" class="form-label">
                      Licence Number
                    </label>
                    <input
                      type="text"
                      class="form-control"
                      value={fetchedAgency.licenseNumber}
                      onChange={(e) =>
                        setFetchedAgency({
                          ...fetchedAgency,
                          licenseNumber: e.target.value,
                        })
                      }
                    />
                  </div>

                  <div class="mb-3">
                    <label for="exampleInputPassword1" class="form-label">
                      Website
                    </label>
                    <input
                      type="text"
                      class="form-control"
                      value={fetchedAgency.website}
                      onChange={(e) =>
                        setFetchedAgency({
                          ...fetchedAgency,
                          website: e.target.value,
                        })
                      }
                    />
                  </div>
                </div>
                <div className="account_creation_coloumn">
                  <div class="mb-3">
                    <label for="exampleInputPassword1" class="form-label">
                      Country of Registered
                    </label>
                    <input
                      type="text"
                      class="form-control"
                      value={fetchedAgency.country}
                      onChange={(e) =>
                        setFetchedAgency({
                          ...fetchedAgency,
                          country: e.target.value,
                        })
                      }
                    />
                  </div>
                  <div class="mb-3">
                    <label for="exampleInputPassword1" class="form-label">
                      Contact Person
                    </label>
                    <input
                      type="text"
                      class="form-control"
                      value={fetchedAgency.phone}
                      onChange={(e) =>
                        setFetchedAgency({
                          ...fetchedAgency,
                          phone: e.target.value,
                        })
                      }
                    />
                  </div>

                  <div class="mb-3">
                    <label for="exampleInputPassword1" class="form-label">
                      Description
                    </label>
                    <input
                      type="text"
                      class="form-control"
                      value={fetchedAgency.description}
                      onChange={(e) =>
                        setFetchedAgency({
                          ...fetchedAgency,
                          description: e.target.value,
                        })
                      }
                    />
                  </div>
                </div>
              </div>
              <div className="button_group">
                <button type="button" class="btn btn-outline-warning">
                  Clear
                </button>
                <button
                  type="button"
                  class="btn btn-warning ms-2"
                  onClick={() => handleUpdate()}
                >
                  Confirm
                </button>
              </div>
              <div className="sub_title_AR">
                <h1 class="display-6 fw-lighter fs-4">Certification upload</h1>
              </div>
              <div className="account_creation bg-secondary-subtle">
                <div className="account_creation_coloumn">
                  <div class="input-group mb-3">
                    <label class="input-group-text" for="inputGroupFile01">
                      Logo in JPG
                    </label>
                    <input
                      type="file"
                      class="form-control"
                      id="inputGroupFile01"
                      onChange={(e) => setlogo(e.target.files[0])}
                    />
                  </div>
                  <div class="input-group mb-3">
                    <label class="input-group-text" for="inputGroupFile02">
                      Doc2
                    </label>
                    <input
                      type="file"
                      class="form-control"
                      id="inputGroupFile02"
                      onChange={(e) => setdoc1(e.target.files[0])}
                    />
                  </div>
                  <div class="input-group mb-3">
                    <label class="input-group-text" for="inputGroupFile03">
                      Doc2
                    </label>
                    <input
                      type="file"
                      class="form-control"
                      id="inputGroupFile03"
                      onChange={(e) => setdoc2(e.target.files[0])}
                    />
                  </div>
                </div>
              </div>
              <div className="button_group">
                <button type="button" class="btn btn-outline-warning">
                  Clear
                </button>
                <button
                  type="button"
                  class="btn btn-warning ms-2"
                  onClick={() =>
                    handleUploads(
                      "logo",
                      "doc1",
                      "doc2",
                      logo,
                      doc1,
                      doc2,
                      "update"
                    )
                  }
                >
                  Upload
                </button>
              </div>
            </>
          </>
        )}
      </div>
      <Toast
        showToast={toastUpdate}
        setShowToast={setToastUpdate}
        message={"✅ Updation is ok!"}
      />
      <Footer />
    </>
  );
}
