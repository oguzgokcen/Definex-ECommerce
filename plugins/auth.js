export default function ({ store }) {
  // Initialize auth state from localStorage
  store.dispatch('auth/initAuth');
} 