import { useEffect, useState } from "react";
import Footer from "../../components/Footer";
import "./ExploreGlobe.css";
import RegionCard from "./RegionCard";
import RegionCountries from "./RegionCountries";
import TopAgency from "./TopAgency";
import axios from "axios";
import { baseurl } from "../../config";
import middleEastImage from "../../assets/middle east.jpg";
import europeImage from "../../assets/europe.jpg";
import eastAsiaImage from "../../assets/east asia.jpg";
import southKoriaImage from "../../assets/southkoria.jpg";

export default function ExploreGlobe({
  allCountries,
  agency_CountryPairList,
  agencies,
  setSelectedAgency,
}) {
  const [regionCountries, setRegionCountries] = useState([]);

  const [middleEastCountries, setMiddleEastCountries] = useState(null);
  const [europeCountries, setEuropeCountries] = useState(null);
  const [eastAsianCountries, setEastAsianCountries] = useState(null);
  const [southKorea, setSouthKorea] = useState(null);

  const [regionSelected, setRegionSelected] = useState(1);

  const [sortedAgenciesbyRating, setSortedAgenciesByRating] = useState(null);

  const sortingFunc = () => {
    if (agencies.length > 0) {
      const result = [...agencies]
        .sort((a, b) => b.averageRating - a.averageRating)
        .slice(0, 4);
      setSortedAgenciesByRating(result);
    }
  };

  const filterByRegion = () => {
    if (allCountries != null) {
      setMiddleEastCountries(allCountries.filter((a) => a.regionId == 1));
      setEuropeCountries(allCountries.filter((a) => a.regionId == 2));
      setEastAsianCountries(allCountries.filter((a) => a.regionId == 3));
      setSouthKorea(allCountries.filter((a) => a.regionId == 4));
    }
  };

  const determineRegionCountries = () => {
    if (allCountries != null) {
      if (regionSelected == 1) {
        setRegionCountries(middleEastCountries);
      }
      if (regionSelected == 2) {
        setRegionCountries(europeCountries);
      }
      if (regionSelected == 3) {
        setRegionCountries(eastAsianCountries);
      }
      if (regionSelected == 4) {
        setRegionCountries(southKorea);
      }
    }
  };

  const fetchAndUpdateLogos = async () => {
    if (
      sortedAgenciesbyRating &&
      sortedAgenciesbyRating.length === 4 &&
      !sortedAgenciesbyRating[0].logo
    ) {
      const logos = await Promise.all(
        sortedAgenciesbyRating.map((agency) =>
          axios
            .get(`${baseurl}/api/Agency/Logo/${agency.licenseNumber}`)
            .then((res) => res.data),
        ),
      );
      const updated = sortedAgenciesbyRating.map((agency, index) => ({
        ...agency,
        logo: logos[index],
      }));
      setSortedAgenciesByRating(updated);
    }
  };

  useEffect(() => {
    filterByRegion();
  }, [allCountries]);

  useEffect(() => {
    determineRegionCountries();
  }, [southKorea, regionSelected]);

  useEffect(() => {
    sortingFunc();
  }, [agencies]);

  useEffect(() => {
    fetchAndUpdateLogos();
  }, [sortedAgenciesbyRating]);

  // console.log(agency_CountryPairList);
  return (
    <div className="maincontainer_globe_explore">
      <div className="headline">
        <h1 className="display-4">“Your Dream Job Abroad Starts</h1>
        <h1 className="display-4">with the Right Guidance.”</h1>
      </div>
      <div className="region_caption ">Available Regions</div>
      <div className="regions">
        <RegionCard
          pic={middleEastImage}
          cap="Middle East"
          setRegionSelected={setRegionSelected}
          regionId={1}
        />
        <RegionCard
          pic={europeImage}
          cap="Europe"
          setRegionSelected={setRegionSelected}
          regionId={2}
        />
        <RegionCard
          pic={eastAsiaImage}
          cap="MEast Asia"
          setRegionSelected={setRegionSelected}
          regionId={3}
        />
        <RegionCard
          pic={southKoriaImage}
          cap="South Koria"
          setRegionSelected={setRegionSelected}
          regionId={4}
        />
      </div>

      <div className="region_caption">Available countries</div>
      <div className="countries_available">
        <RegionCountries
          accordian_object={regionCountries}
          agency_CountryPairList={agency_CountryPairList}
          agencies={agencies}
          setSelectedAgency={setSelectedAgency}
        />
      </div>
      <div className="region_caption display-5 mb-3">Top Agencies</div>
      <div className="topajencies">
        {sortedAgenciesbyRating ? (
          sortedAgenciesbyRating.map((agency, index) => {
            return (
              <TopAgency
                name={agency.agencyName}
                description={agency.description}
                logoGot={agency.logo}
                index={index}
                setSelectedAgency={setSelectedAgency}
                agency={agency}
              />
            );
          })
        ) : (
          <p>loading...</p>
        )}
      </div>
      <Footer />
    </div>
  );
}
