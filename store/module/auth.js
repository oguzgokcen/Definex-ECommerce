import { jwtDecode } from "jwt-decode";

const state = () => ({
  token: null,
  user: null
});

const mutations = {
  SET_TOKEN(state, token) {
    state.token = token;
  },
  SET_USER(state, user) {
    state.user = user;
  },
  CLEAR_AUTH(state) {
    state.token = null;
    state.user = null;
  }
};

const actions = {
  // Initialize auth state from localStorage
  initAuth({ commit }) {
    const token = localStorage.getItem('token');

    if (token) {
      try {
        const decodedToken = jwtDecode(token);
        // Check if token is expired
        if (decodedToken.exp * 1000 < Date.now()) {
          commit('CLEAR_AUTH');
          localStorage.removeItem('token');
          return;
        }
        commit('SET_TOKEN', token);
        commit('SET_USER', {
          ...decodedToken,
          roles: decodedToken.realm_access?.roles || []
        });
      } catch (error) {
        console.error('Error decoding token:', error);
        commit('CLEAR_AUTH');
        localStorage.removeItem('token');
      }
    }
  },

  // Login action
  login({ commit }, tokenData) {
    if (tokenData && tokenData.access_token) {
      try {
        const decodedToken = jwtDecode(tokenData.access_token);
        localStorage.setItem('token', tokenData.access_token);
        const roles = decodedToken.realm_access?.roles || [];
        commit('SET_TOKEN', tokenData.access_token);
        commit('SET_USER', {
          ...decodedToken, roles: roles
        });
        console.log("User roles (login):", roles);
      } catch (error) {
        console.error('Error decoding token:', error);
        commit('CLEAR_AUTH');
        localStorage.removeItem('token');
      }
    }
  },

  // Logout action
  logout({ commit }) {
    commit('CLEAR_AUTH');
    localStorage.removeItem('token');
    
  }
};

const getters = {
  isAuthenticated: (state) => {
    if (!state.token) return false;
    try {
      const decodedToken = jwtDecode(state.token);
      return decodedToken.exp * 1000 > Date.now();
    } catch (error) {
      return false;
    }
  },
  currentUser: (state) => state.user,
  isAdmin: (state) => state.user && state.user.roles && state.user.roles.includes('ADMIN')
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
  