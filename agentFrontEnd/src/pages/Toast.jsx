import React, { useState, useEffect } from "react";

export default function Toast({showToast,setShowToast,message}) {
 

  useEffect(() => {
    let timer;
    if (showToast) {
      // Hide toast after 3 seconds
      timer = setTimeout(() => {
        setShowToast(false);
      }, 5000);
    }
    return () => clearTimeout(timer);
  }, [showToast]);

  return (
    <div className="p-3">
      <div className="toast-container position-fixed bottom-0 end-0 p-3">
        <div
          className={`toast align-items-center text-bg-primary border-0 ${
            showToast ? "show" : "hide"
          }`}
          role="alert"
        >
          <div className="d-flex">
            <div className="toast-body">{message}</div>
            <button
              type="button"
              className="btn-close btn-close-white me-2 m-auto"
              onClick={() => setShowToast(false)}
            ></button>
          </div>
        </div>
      </div>
    </div>
  );
}
