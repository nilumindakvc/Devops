#!/bin/sh

# Generate env-config.js file with environment variables
cat <<EOF > /usr/share/nginx/html/env-config.js
window.ENV = {
  REACT_APP_API_BASE_URL: '${REACT_APP_API_BASE_URL}'
};
EOF

# Start nginx
nginx -g 'daemon off;'