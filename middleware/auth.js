export default function ({ store, redirect, route }) {
  const isAuthenticated = store.getters['auth/isAuthenticated'];
  console.log("isauth", isAuthenticated)
  if (!isAuthenticated) {
    return redirect({
      path: '/login',
      query: { redirect: route.fullPath }
    });
  }
} 