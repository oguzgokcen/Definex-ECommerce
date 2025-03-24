import Vue from 'vue'
import Router from 'vue-router'
import { normalizeURL, decode } from 'ufo'
import { interopDefault } from './utils'
import scrollBehavior from './router.scrollBehavior.js'

const _916dcee8 = () => interopDefault(import('..\\pages\\404.vue' /* webpackChunkName: "pages/404" */))
const _c4e2058c = () => interopDefault(import('..\\pages\\about-us.vue' /* webpackChunkName: "pages/about-us" */))
const _0ea4ca78 = () => interopDefault(import('..\\pages\\blog\\index.vue' /* webpackChunkName: "pages/blog/index" */))
const _64e056f4 = () => interopDefault(import('..\\pages\\cart\\index.vue' /* webpackChunkName: "pages/cart/index" */))
const _80fffeca = () => interopDefault(import('..\\pages\\coming-soon.vue' /* webpackChunkName: "pages/coming-soon" */))
const _339c265b = () => interopDefault(import('..\\pages\\contact-us\\index.vue' /* webpackChunkName: "pages/contact-us/index" */))
const _0885194b = () => interopDefault(import('..\\pages\\electronics.vue' /* webpackChunkName: "pages/electronics" */))
const _6339bb6a = () => interopDefault(import('..\\pages\\faq.vue' /* webpackChunkName: "pages/faq" */))
const _5d218574 = () => interopDefault(import('..\\pages\\furniture.vue' /* webpackChunkName: "pages/furniture" */))
const _0f5ca7e1 = () => interopDefault(import('..\\pages\\grocery.vue' /* webpackChunkName: "pages/grocery" */))
const _127b0186 = () => interopDefault(import('..\\pages\\login.vue' /* webpackChunkName: "pages/login" */))
const _01fa2564 = () => interopDefault(import('..\\pages\\lookbook.vue' /* webpackChunkName: "pages/lookbook" */))
const _514d4fcc = () => interopDefault(import('..\\pages\\my-account\\index.vue' /* webpackChunkName: "pages/my-account/index" */))
const _3ebb828f = () => interopDefault(import('..\\pages\\pharmacy.vue' /* webpackChunkName: "pages/pharmacy" */))
const _c58ba81a = () => interopDefault(import('..\\pages\\privacy-policy.vue' /* webpackChunkName: "pages/privacy-policy" */))
const _7225f47f = () => interopDefault(import('..\\pages\\register.vue' /* webpackChunkName: "pages/register" */))
const _a3f15a20 = () => interopDefault(import('..\\pages\\shop\\index.vue' /* webpackChunkName: "pages/shop/index" */))
const _0fafd9f7 = () => interopDefault(import('..\\pages\\vendor-dashboard\\index.vue' /* webpackChunkName: "pages/vendor-dashboard/index" */))
const _b1df4276 = () => interopDefault(import('..\\pages\\blog\\blog-2.vue' /* webpackChunkName: "pages/blog/blog-2" */))
const _b1c31374 = () => interopDefault(import('..\\pages\\blog\\blog-3.vue' /* webpackChunkName: "pages/blog/blog-3" */))
const _7216f6aa = () => interopDefault(import('..\\pages\\blog\\blog-single-2.vue' /* webpackChunkName: "pages/blog/blog-single-2" */))
const _29badb7e = () => interopDefault(import('..\\pages\\cart\\cart-2.vue' /* webpackChunkName: "pages/cart/cart-2" */))
const _299eac7c = () => interopDefault(import('..\\pages\\cart\\cart-3.vue' /* webpackChunkName: "pages/cart/cart-3" */))
const _29827d7a = () => interopDefault(import('..\\pages\\cart\\cart-4.vue' /* webpackChunkName: "pages/cart/cart-4" */))
const _4098819c = () => interopDefault(import('..\\pages\\cart\\empty-cart.vue' /* webpackChunkName: "pages/cart/empty-cart" */))
const _13115bd7 = () => interopDefault(import('..\\pages\\contact-us\\contact-us-2.vue' /* webpackChunkName: "pages/contact-us/contact-us-2" */))
const _78af642c = () => interopDefault(import('..\\pages\\my-account\\account-details.vue' /* webpackChunkName: "pages/my-account/account-details" */))
const _53dee3f1 = () => interopDefault(import('..\\pages\\my-account\\account-info-edit.vue' /* webpackChunkName: "pages/my-account/account-info-edit" */))
const _48866aea = () => interopDefault(import('..\\pages\\my-account\\addresses.vue' /* webpackChunkName: "pages/my-account/addresses" */))
const _6ed6df52 = () => interopDefault(import('..\\pages\\my-account\\checkout-1.vue' /* webpackChunkName: "pages/my-account/checkout-1" */))
const _6ee4f6d3 = () => interopDefault(import('..\\pages\\my-account\\checkout-2.vue' /* webpackChunkName: "pages/my-account/checkout-2" */))
const _3cb07ead = () => interopDefault(import('..\\pages\\my-account\\compare.vue' /* webpackChunkName: "pages/my-account/compare" */))
const _58fa6bb3 = () => interopDefault(import('..\\pages\\my-account\\downloads.vue' /* webpackChunkName: "pages/my-account/downloads" */))
const _b4818b84 = () => interopDefault(import('..\\pages\\my-account\\order-tracking.vue' /* webpackChunkName: "pages/my-account/order-tracking" */))
const _180a9b4d = () => interopDefault(import('..\\pages\\my-account\\orders.vue' /* webpackChunkName: "pages/my-account/orders" */))
const _43abaa2d = () => interopDefault(import('..\\pages\\my-account\\wishlist.vue' /* webpackChunkName: "pages/my-account/wishlist" */))
const _a76b61e4 = () => interopDefault(import('..\\pages\\product\\product-single-2.vue' /* webpackChunkName: "pages/product/product-single-2" */))
const _a74f32e2 = () => interopDefault(import('..\\pages\\product\\product-single-3.vue' /* webpackChunkName: "pages/product/product-single-3" */))
const _60d4886d = () => interopDefault(import('..\\pages\\shop\\shop-2.vue' /* webpackChunkName: "pages/shop/shop-2" */))
const _60e29fee = () => interopDefault(import('..\\pages\\shop\\shop-3.vue' /* webpackChunkName: "pages/shop/shop-3" */))
const _60f0b76f = () => interopDefault(import('..\\pages\\shop\\shop-4.vue' /* webpackChunkName: "pages/shop/shop-4" */))
const _60fecef0 = () => interopDefault(import('..\\pages\\shop\\shop-5.vue' /* webpackChunkName: "pages/shop/shop-5" */))
const _78c12aa8 = () => interopDefault(import('..\\pages\\vendor-dashboard\\add-product.vue' /* webpackChunkName: "pages/vendor-dashboard/add-product" */))
const _287a9a73 = () => interopDefault(import('..\\pages\\vendor-dashboard\\order.vue' /* webpackChunkName: "pages/vendor-dashboard/order" */))
const _79197854 = () => interopDefault(import('..\\pages\\vendor-dashboard\\product.vue' /* webpackChunkName: "pages/vendor-dashboard/product" */))
const _46cd6fe4 = () => interopDefault(import('..\\pages\\vendor-dashboard\\profile.vue' /* webpackChunkName: "pages/vendor-dashboard/profile" */))
const _6a8dbd96 = () => interopDefault(import('..\\pages\\vendor-dashboard\\setting.vue' /* webpackChunkName: "pages/vendor-dashboard/setting" */))
const _2d1adc26 = () => interopDefault(import('..\\pages\\index.vue' /* webpackChunkName: "pages/index" */))
const _12120d08 = () => interopDefault(import('..\\pages\\blog\\_slug.vue' /* webpackChunkName: "pages/blog/_slug" */))
const _145e2687 = () => interopDefault(import('..\\pages\\product\\_id.vue' /* webpackChunkName: "pages/product/_id" */))

const emptyFn = () => {}

Vue.use(Router)

export const routerOptions = {
  mode: 'history',
  base: '/',
  linkActiveClass: 'nuxt-link-active',
  linkExactActiveClass: 'nuxt-link-exact-active',
  scrollBehavior,

  routes: [{
    path: "/404",
    component: _916dcee8,
    name: "404"
  }, {
    path: "/about-us",
    component: _c4e2058c,
    name: "about-us"
  }, {
    path: "/blog",
    component: _0ea4ca78,
    name: "blog"
  }, {
    path: "/cart",
    component: _64e056f4,
    name: "cart"
  }, {
    path: "/coming-soon",
    component: _80fffeca,
    name: "coming-soon"
  }, {
    path: "/contact-us",
    component: _339c265b,
    name: "contact-us"
  }, {
    path: "/electronics",
    component: _0885194b,
    name: "electronics"
  }, {
    path: "/faq",
    component: _6339bb6a,
    name: "faq"
  }, {
    path: "/furniture",
    component: _5d218574,
    name: "furniture"
  }, {
    path: "/grocery",
    component: _0f5ca7e1,
    name: "grocery"
  }, {
    path: "/login",
    component: _127b0186,
    name: "login"
  }, {
    path: "/lookbook",
    component: _01fa2564,
    name: "lookbook"
  }, {
    path: "/my-account",
    component: _514d4fcc,
    name: "my-account"
  }, {
    path: "/pharmacy",
    component: _3ebb828f,
    name: "pharmacy"
  }, {
    path: "/privacy-policy",
    component: _c58ba81a,
    name: "privacy-policy"
  }, {
    path: "/register",
    component: _7225f47f,
    name: "register"
  }, {
    path: "/shop",
    component: _a3f15a20,
    name: "shop"
  }, {
    path: "/vendor-dashboard",
    component: _0fafd9f7,
    name: "vendor-dashboard"
  }, {
    path: "/blog/blog-2",
    component: _b1df4276,
    name: "blog-blog-2"
  }, {
    path: "/blog/blog-3",
    component: _b1c31374,
    name: "blog-blog-3"
  }, {
    path: "/blog/blog-single-2",
    component: _7216f6aa,
    name: "blog-blog-single-2"
  }, {
    path: "/cart/cart-2",
    component: _29badb7e,
    name: "cart-cart-2"
  }, {
    path: "/cart/cart-3",
    component: _299eac7c,
    name: "cart-cart-3"
  }, {
    path: "/cart/cart-4",
    component: _29827d7a,
    name: "cart-cart-4"
  }, {
    path: "/cart/empty-cart",
    component: _4098819c,
    name: "cart-empty-cart"
  }, {
    path: "/contact-us/contact-us-2",
    component: _13115bd7,
    name: "contact-us-contact-us-2"
  }, {
    path: "/my-account/account-details",
    component: _78af642c,
    name: "my-account-account-details"
  }, {
    path: "/my-account/account-info-edit",
    component: _53dee3f1,
    name: "my-account-account-info-edit"
  }, {
    path: "/my-account/addresses",
    component: _48866aea,
    name: "my-account-addresses"
  }, {
    path: "/my-account/checkout-1",
    component: _6ed6df52,
    name: "my-account-checkout-1"
  }, {
    path: "/my-account/checkout-2",
    component: _6ee4f6d3,
    name: "my-account-checkout-2"
  }, {
    path: "/my-account/compare",
    component: _3cb07ead,
    name: "my-account-compare"
  }, {
    path: "/my-account/downloads",
    component: _58fa6bb3,
    name: "my-account-downloads"
  }, {
    path: "/my-account/order-tracking",
    component: _b4818b84,
    name: "my-account-order-tracking"
  }, {
    path: "/my-account/orders",
    component: _180a9b4d,
    name: "my-account-orders"
  }, {
    path: "/my-account/wishlist",
    component: _43abaa2d,
    name: "my-account-wishlist"
  }, {
    path: "/product/product-single-2",
    component: _a76b61e4,
    name: "product-product-single-2"
  }, {
    path: "/product/product-single-3",
    component: _a74f32e2,
    name: "product-product-single-3"
  }, {
    path: "/shop/shop-2",
    component: _60d4886d,
    name: "shop-shop-2"
  }, {
    path: "/shop/shop-3",
    component: _60e29fee,
    name: "shop-shop-3"
  }, {
    path: "/shop/shop-4",
    component: _60f0b76f,
    name: "shop-shop-4"
  }, {
    path: "/shop/shop-5",
    component: _60fecef0,
    name: "shop-shop-5"
  }, {
    path: "/vendor-dashboard/add-product",
    component: _78c12aa8,
    name: "vendor-dashboard-add-product"
  }, {
    path: "/vendor-dashboard/order",
    component: _287a9a73,
    name: "vendor-dashboard-order"
  }, {
    path: "/vendor-dashboard/product",
    component: _79197854,
    name: "vendor-dashboard-product"
  }, {
    path: "/vendor-dashboard/profile",
    component: _46cd6fe4,
    name: "vendor-dashboard-profile"
  }, {
    path: "/vendor-dashboard/setting",
    component: _6a8dbd96,
    name: "vendor-dashboard-setting"
  }, {
    path: "/",
    component: _2d1adc26,
    name: "index"
  }, {
    path: "/blog/:slug",
    component: _12120d08,
    name: "blog-slug"
  }, {
    path: "/product/:id?",
    component: _145e2687,
    name: "product-id"
  }],

  fallback: false
}

export function createRouter (ssrContext, config) {
  const base = (config._app && config._app.basePath) || routerOptions.base
  const router = new Router({ ...routerOptions, base  })

  // TODO: remove in Nuxt 3
  const originalPush = router.push
  router.push = function push (location, onComplete = emptyFn, onAbort) {
    return originalPush.call(this, location, onComplete, onAbort)
  }

  const resolve = router.resolve.bind(router)
  router.resolve = (to, current, append) => {
    if (typeof to === 'string') {
      to = normalizeURL(to)
    }
    return resolve(to, current, append)
  }

  return router
}
