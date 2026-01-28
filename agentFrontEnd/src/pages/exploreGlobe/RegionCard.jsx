import axios from "axios";
import "./ExploreGlobe.css";
import { useEffect } from "react";

export default function RegionCard(props){

    return(
        <div className="sub_region">
          <figure class="figure">
            <img
              src={props.pic}
              class="figure-img img-fluid"
              alt="..."
              onClick={()=>props.setRegionSelected(props.regionId)}
            />
          </figure>
          <h1 class="display-6">{props.cap}</h1>
        </div>
    )
}