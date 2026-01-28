// Runtime environment variable injected by Docker or Vite
export const baseurl = window.ENV?.REACT_APP_API_BASE_URL || import.meta.env.VITE_API_BASE_URL || 'http://localhost:5203';
