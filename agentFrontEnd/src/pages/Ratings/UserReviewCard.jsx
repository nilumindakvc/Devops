import "./Ratings.css";
import aryaImage from "../../assets/arya.jpg";

export default function UserReviewCard({ name, des }) {
  return (
    <div className="card_container">
      <div class="card">
        <img src={aryaImage} class="card-img-top" alt="..." />
        <div class="card-body">
          <p class="card-text">{name}</p>
          <p class="card-text">{des}</p>
        </div>
      </div>
    </div>
  );
}
