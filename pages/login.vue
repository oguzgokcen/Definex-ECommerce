<template>
  <div>
    <!-- Banner Area -->
    <section id="common_banner_one">
        <div class="container ">
            <div class="row">
                <div class="col-lg-12">
                    <div class="common_banner_text">
                        <h2>{{this.title}}</h2>
                        <b-breadcrumb :items="breadcrumbItems" class="bg-transparent"></b-breadcrumb>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Login-Area -->
    <section id="login_area" class="ptb-100">
        <div class="container">
            <div class="row">
                <div class="col-lg-6 offset-lg-3 col-md-12 col-sm-12 col-12">
                    <div class="account_form">
                        <h3>Processing login...</h3>
                    </div>
                </div>
            </div>
        </div>
    </section>

  </div>
</template>

<script>
import { mapActions } from "vuex";

export default {
    name: 'Login',
    data() {
        return {
            title: 'Login',
            breadcrumbItems: [
                {
                    text: 'Home',
                    to: '/'
                },
                {
                    text: 'Login'
                }
            ]
        }
    },
    async mounted() {  
        window.scrollTo(0, 0)
        await this.handleKeycloakCallback();
    },
    methods: {
        ...mapActions({
            login: 'auth/login'
        }),

        async handleKeycloakCallback() {
            try {
                // Get the authorization code from URL
                const code = this.$route.query.code;
                
                if (!code) {
                    // If no code, redirect to Keycloak login
                    window.location.href = 'http://localhost:8080/realms/e-commerce/protocol/openid-connect/auth?client_id=public-client&response_type=code&scope=openid email&redirect_uri=http://localhost:3000/';
                    return;
                }

                // Exchange code for token
                const response = await this.$axios.post('http://localhost:8080/realms/e-commerce/protocol/openid-connect/token', 
                    new URLSearchParams({
                        grant_type: 'authorization_code',
                        client_id: 'public-client',
                        code: code,
                        redirect_uri: 'http://localhost:3000/login'
                    }), {
                        headers: {
                            'Content-Type': 'application/x-www-form-urlencoded'
                        }
                    }
                );

                if (response.data && response.data.access_token) {
                    await this.login(response.data);
                    console.log("redirecting")
                    const redirectUrl = '/';
                    this.$router.push(redirectUrl);
                } else {
                    throw new Error('Invalid token response');
                }
            } catch (error) {
                console.error('Login error:', error);
                // Redirect to home page on error
                this.$router.push('/');
            }
        }
    },
    head() {
      return {
        title: this.title,
        meta: [
          {
            hid: 'description',
            name: 'description',
            content: 'Login page - AndShop Ecommerce Vue js, Nuxt js Template'
          }
        ]
      }
    }
}
</script>

<style scoped>
.alert-danger {
    color: #721c24;
    background-color: #f8d7da;
    border-color: #f5c6cb;
    padding: 0.75rem 1.25rem;
    margin-bottom: 1rem;
    border: 1px solid transparent;
    border-radius: 0.25rem;
}

button:disabled {
    opacity: 0.7;
    cursor: not-allowed;
}
</style>