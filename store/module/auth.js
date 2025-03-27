const state = () => ({
  token: null
});

const mutations = {
  SET_TOKEN(state, token) {
    state.token = token;
  },
  CLEAR_AUTH(state) {
    state.token = null;
  }
};

const actions = {
  // Initialize auth state from localStorage
  initAuth({ commit }) {
    const token = localStorage.getItem('token');

    if (token) {
      commit('SET_TOKEN', token);
    }
  },

  // Login action
  login({ commit }, tokenData) {
    if (tokenData && tokenData.access_token) {
      localStorage.setItem('token', tokenData.access_token);
      commit('SET_TOKEN', tokenData.access_token);
    }
  },

  // Logout action
  logout({ commit }) {
    // Clear local state and storage
    commit('CLEAR_AUTH');
    localStorage.removeItem('token');

  }
};

const getters = {
  isAuthenticated: (state) => !!state.token,
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
  