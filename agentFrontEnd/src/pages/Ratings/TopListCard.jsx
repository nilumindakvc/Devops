export default function TopListCard({name,rating,logo,stars}){
    return(
        <>
        <div
            class="alert alert-warning w-100 d-flex  align-items-center rounded-end-pill"
            role="alert"
          >
            <h1 className="display-6 fw-lighter fs-5">{name}</h1>
            {/* <img src={`data:image/jpeg;base64,${logo}`} style={{ maxWidth: '60px', height: 'auto' }} className="ms-5 me-5" alt="..." />  */}
            <p className="ms-4">Ratings:{rating}</p>
            <p>{stars}</p>
          </div>
        </>
    )
}