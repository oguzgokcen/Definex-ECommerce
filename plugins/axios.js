export default function ({ $axios, store, redirect }) {
  $axios.interceptors.request.use(
    config => {
      const token = localStorage.getItem('token');
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    },
    error => {
      return Promise.reject(error);
    }
  );

  $axios.interceptors.response.use(
    response => response,
    error => {
      if (error.response) {
        switch (error.response.status) {
          case 401:
            store.dispatch('auth/logout');
            redirect('/login');
            break;
          case 403:
            console.error('Access forbidden');
            break;
          case 404:
            console.error('Resource not found');
            break;
          case 500:
            console.error('Server error');
            break;
          default:
            console.error('An error occurred');
        }
      } else if (error.request) {
        console.error('Network error');
      } else {
        console.error('Error:', error.message);
      }
      return Promise.reject(error);
    }
  );
} 