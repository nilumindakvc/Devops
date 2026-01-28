import Nav from "./components/Nav";
import "./App.css";
import Home from "./pages/Home/Home";
import ExploreGlobe from "./pages/exploreGlobe/ExploreGlobe";
import GlobalJobs from "./pages/GlobalJobs/GlobalJobs";
import AgencyRegistering from "./pages/AgencyRegistering";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Ratings from "./pages/Ratings/Ratings";
import { useEffect, useState } from "react";
import ScrollToTop from "./ScrollToTop";
import Login from "./pages/Login/Login";
import AgencyPage from "./pages/Agency/AgencyPage";
import axios from "axios";
import { baseurl } from "./config";

function App() {
  // Validate baseurl configuration
  console.log('App - baseurl configured as:', baseurl);
  if (!baseurl) {
    console.error('App - baseurl is not set! Check environment variables.');
    console.log('App - window.ENV:', window.ENV);
    console.log('App - import.meta.env:', import.meta.env);
  }

  const [signInState, setSignInState] = useState(1);
  const [commonSignIn_SignUp_state, set_common_signIn_signUp_state] =
    useState(1);
  const [userLogedIn, setUserLogedIn] = useState(null);

  const [urgentlyOpenedJobs, setUrgentlyOpenedJobs] = useState(null);
  const [jobList, setJobList] = useState(null);
  const [selectedJobFromJobPage, setSelectedJobFromJobPage] = useState(null);
  const [agencies, setAgencies] = useState([""]);
  const [allCountries, setAllCountries] = useState(null);
  const [agency_CountryPairList, setAgency_CountryPairList] = useState(null);
  const [selectedAgency, setSelectedAgency] = useState(null);
  const [agencyReviews, setAgencyReviews] = useState(null);
  const [isEverythingReady, setIsEverythingReady] = useState(false);

  const [canPublish, setCanPublish] = useState(false);

  const pitchMaker = () => {
    if (
      jobList &&
      agencies != [""] &&
      allCountries &&
      agency_CountryPairList &&
      agencyReviews
    ) {
      setIsEverythingReady(true);
    }
  };

  useEffect(() => {
    pitchMaker();
  }, [jobList, agencies, allCountries, agency_CountryPairList, agencyReviews]);

  const getAllAgencyReviews = async () => {
    try {
      console.log('Loading agency reviews from:', `${baseurl}/api/User/reveiws`);
      const result = await axios.get(`${baseurl}/api/User/reveiws`);
      setAgencyReviews(result.data);
      console.log('Agency reviews loaded:', result.data?.length, 'reviews');
    } catch (err) {
      console.error('Error loading agency reviews:', err.message);
      setAgencyReviews([]); // Set empty array as fallback
    }
  };
  const getAllAgency_contryPair = async () => {
    try {
      console.log('Loading agency-country pairs from:', `${baseurl}/api/Agency/AgencyCountries`);
      const response = await axios.get(`${baseurl}/api/Agency/AgencyCountries`);
      setAgency_CountryPairList(response.data);
      console.log('Agency-country pairs loaded:', response.data?.length, 'pairs');
    } catch (err) {
      console.error('Error loading agency-country pairs:', err.message);
      setAgency_CountryPairList([]); // Set empty array as fallback
    }
  };

  const getAllAgencies = async () => {
    try {
      console.log('Loading agencies from:', `${baseurl}/api/Agency`);
      const response = await axios.get(`${baseurl}/api/Agency`);
      const allAgencies = response.data;
      setAgencies(allAgencies);
      console.log('Agencies loaded:', allAgencies?.length, 'agencies');
    } catch (err) {
      console.error('Error loading agencies:', err.message);
      setAgencies([]); // Set empty array as fallback
    }
  };

  const getAllCountries = async () => {
    try {
      console.log('Loading countries from:', `${baseurl}/api/Country`);
      const response = await axios.get(`${baseurl}/api/Country`);
      setAllCountries(response.data);
      console.log('Countries loaded:', response.data?.length, 'countries');
    } catch (err) {
      console.error('Error loading countries:', err.message);
      setAllCountries([]); // Set empty array as fallback
    }
  };

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

  const [sortedAgenciesbyRating, setSortedAgenciesByRating] = useState(null);
  const [agenciesWithLogos, setAgenciesWithLogos] = useState([]);

  useEffect(() => {
    if (agencies.length > 0) {
      const result = [...agencies].sort(
        (a, b) => b.averageRating - a.averageRating,
      );
      setSortedAgenciesByRating(result);
    }
  }, [agencies]);

  useEffect(() => {
    const fetchAndUpdateLogos = async () => {
      if (sortedAgenciesbyRating && sortedAgenciesbyRating.length > 0) {
        try {
          const logos = await Promise.all(
            sortedAgenciesbyRating.map((agency) =>
              axios
                .get(`${baseurl}/api/Agency/Logo/${agency.licenseNumber}`)
                .then((res) => res.data)
                .catch((err) => {
                  console.warn(
                    `Logo failed for ${agency.agencyName}:`,
                    err.message,
                  );
                  return null; // Return null for failed logos
                }),
            ),
          );
          const updated = sortedAgenciesbyRating.map((agency, index) => ({
            ...agency,
            logo: logos[index],
          }));
          setAgenciesWithLogos(updated);
        } catch (error) {
          console.error("Error in fetchAndUpdateLogos:", error);
        }
      }
    };
    fetchAndUpdateLogos();
  }, [sortedAgenciesbyRating]);

  useEffect(() => {
    if (window.location.pathname != "/") {
      set_common_signIn_signUp_state(0);
    }
    const savedUser = localStorage.getItem("user");
    if (savedUser) {
      setUserLogedIn(JSON.parse(savedUser));
    }
    getAllJobs();
    getAllAgencies();
    getAllCountries();
    getAllAgency_contryPair();
    getAllAgencyReviews();
  }, []);

  // console.log(userLogedIn);
  // console.log(agencyReviews);
  console.log(agenciesWithLogos);
  return (
    <div className="main_container">
      <div className="navigation_container">
        <Nav
          signInState={signInState}
          setSignInState={setSignInState}
          commonSignIn_SignUp_state={commonSignIn_SignUp_state}
          setSelectedJobFromJobPage={setSelectedJobFromJobPage}
          set_common_signIn_signUp_state={set_common_signIn_signUp_state}
          userLogedIn={userLogedIn}
          setSelectedAgency={setSelectedAgency}
          canPublish={canPublish}
          setCanPublish={setCanPublish}
        />
      </div>

      <div className="page_container">
        <Routes>
          <Route
            path="/"
            element={
              <Login
                signInState={signInState}
                setSignInState={setSignInState}
                setUserLogedIn={setUserLogedIn}
                set_common_signIn_signUp_state={set_common_signIn_signUp_state}
                commonSignIn_SignUp_state={commonSignIn_SignUp_state}
              />
            }
          />

          <Route
            path="/Home"
            element={<Home isEverythingReady={isEverythingReady} />}
          />

          <Route
            path="/ExploreGlobe"
            element={
              <ExploreGlobe
                allCountries={allCountries}
                agency_CountryPairList={agency_CountryPairList}
                agencies={agencies}
                setSelectedAgency={setSelectedAgency}
              />
            }
          />

          <Route
            path="/GlobalJobs"
            element={
              <GlobalJobs
                urgentlyOpenedJobs={urgentlyOpenedJobs}
                jobList={jobList}
                setSelectedJobFromJobPage={setSelectedJobFromJobPage}
              />
            }
          />

          <Route
            path="/AgencyRegistering"
            element={
              <AgencyRegistering
                setAgencies={setAgencies}
                jobList={jobList}
                setJobList={setJobList}
                setUrgentlyOpenedJobs={setUrgentlyOpenedJobs}
                agencies={agencies}
                canPublish={canPublish}
                setCanPublish={setCanPublish}
              />
            }
          />

          <Route
            path="/Ratings"
            element={
              <Ratings
                isEverythingReady={isEverythingReady}
                sortedAgenciesbyRating={sortedAgenciesbyRating}
                userLogedIn={userLogedIn}
                agenciesWithLogos={agenciesWithLogos}
                agencyReviews={agencyReviews}
                agencies={agencies}
                setAgencyReviews={setAgencyReviews}
              />
            }
          />

          <Route
            path="/Agency"
            element={
              <AgencyPage
                selectedJobFromJobPage={selectedJobFromJobPage}
                agencies={agencies}
                selectedAgency={selectedAgency}
              />
            }
          />
        </Routes>
      </div>
    </div>
  );
}

export default App;
