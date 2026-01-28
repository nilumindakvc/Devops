import "./Ratings.css";
export default function TopThreeCard({ name, logoPic }) {
  return (
    <>
      <div className="logo common_all">
        {logoPic ? (
          <img
            src={`data:image/jpeg;base64,${logoPic}`}
            className="card-img-top"
            alt={name || "Agency logo"}
          />
        ) : (
          <div
            className="card-img-top d-flex align-items-center justify-content-center bg-light"
            style={{ minHeight: "100px" }}
          >
            <span className="text-muted">No Logo</span>
          </div>
        )}
      </div>
      <div className="display-6 fw-lighter fs-4 bg-light w-100 p-2">{name}</div>
      {/* <p className="bg-light w-100 d-flex justify-content-center p-2">⭐⭐⭐⭐</p> */}
    </>
  );
}
