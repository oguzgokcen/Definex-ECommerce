export default function ({ store, redirect, error }) {
  if (!store.getters['auth/isAuthenticated']) {
    return redirect('/login');
  }

  const user = store.state.auth.user || JSON.parse(localStorage.getItem('user'));

  if (!user || !user.roles || !user.roles.includes('admin')) {
    return error({ statusCode: 403, message: 'Access denied. Admin privileges required.' });
  }
} 