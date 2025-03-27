import Vue from 'vue'
import Vuex from 'vuex'
import products from './module/products'
import cart from './module/cart'
import auth from './module/auth'

Vue.use(Vuex)
const createStore = () => {
  return new Vuex.Store({
    modules: {
      products: products,
      cart: cart,
      auth: auth
    }
  })
}
export default createStore