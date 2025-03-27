<template>

    <div class="myaccount-content">
        <h4 class="title">Dashboard </h4>
        <p>From your account dashboard. you can easily check &amp; view your <nuxt-link to="/my-account/orders">recent
            orders</nuxt-link>, manage your <nuxt-link to="/my-account/addresses">shipping and billing addresses</nuxt-link> and <nuxt-link
            to="/my-account/account-details">Edit your password and account details.</nuxt-link>
        </p>
    </div>
                        
</template>

<script>
import { mapGetters } from 'vuex';

export default {
    name: 'Dashboard',
    layout: 'my-account',
    data() {
        return {
            title: 'My Account',

            // Breadcrumb Items Data
            breadcrumbItems: [
                {
                    text: 'Home',
                    to: '/'
                },
                {
                    text: 'Dashboard',
                }
            ],

        }
    },
    mounted() {
        // For scroll page top for every Route 
        window.scrollTo(0, 0)
    },
    // Page head() Title, description for SEO 
    head() {
      return {
        title: this.title,
        meta: [
          {
            hid: 'description',
            name: 'description',
            content: 'My Account page - AndShop Ecommerce Vue js, Nuxt js Template'
          }
        ]
      }
    }
}
</script>