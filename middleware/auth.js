export default function ({ store, redirect, route }) {
  // Check if user is authenticated
  const isAuthenticated = store.getters['auth/isAuthenticated'];
  console.log("isauth", isAuthenticated)
  // If not authenticated and trying to access protected route
  if (!isAuthenticated) {
    // Redirect to login page with the attempted URL as query parameter
    return redirect({
      path: '/login',
      query: { redirect: route.fullPath }
    });
  }
} 