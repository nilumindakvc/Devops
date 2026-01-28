import { useEffect } from "react";
import { Carousel } from "bootstrap";
import "./GlobalJobs.css";

export default function Carsoul({ imageArray }) {

  useEffect(() => {
  const element = document.querySelector("#carouselExampleSlidesOnly");
  if (element) {
    const carousel = new Carousel(element, {
      interval: 3000,
      ride: "carousel"
    });
    return () => carousel.dispose(); 
  }
 }, []);

  return (
    
    <div
      id="carouselExampleSlidesOnly"
      class="carousel slide "
      data-bs-ride="carousel"
    >
      <div class="carousel-inner ">
        {imageArray.map((image,index) => {
          return (
            <div  className={`carousel-item ${index === 0 ? "active" : ""}`} key={index}>
              <img
                src={image.img}
                class="d-block w-100"
                alt="..."
              />
            </div>
          );
        })}
      </div>
    </div>
  );
}

