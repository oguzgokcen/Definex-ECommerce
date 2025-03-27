<template>
  <div>
    <Header />
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

        <!-- My Account-Area -->
        <section id="my-account_area" class="ptb-100">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12 col-md-3 col-lg-3">
                        <!-- Nav tabs -->
                        <div class="dashboard_tab_button">
                            <b-navbar-nav>
                                <b-nav-item to="/my-account"><i class="fas fa-tachometer-alt"></i>Dashboard</b-nav-item>
                                <b-nav-item to="/my-account/orders"><i class="fas fa-cart-arrow-down"></i>Orders</b-nav-item>
                                <b-nav-item to="/my-account/downloads"><i class="fas fa-cloud-download-alt"></i>Downloads</b-nav-item>
                                <b-nav-item to="/my-account/addresses"><i class="fas fa-map-marker-alt"></i>Addresses</b-nav-item>
                                <b-nav-item to="/my-account/account-details"><i class="fas fa-user"></i>Account details</b-nav-item>
                                <b-nav-item to="/login"><i class="fas fa-sign-out-alt"></i>Logout</b-nav-item>
                            </b-navbar-nav>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-9 col-lg-9">
                        <div class="dashboard_content">
                            <nuxt />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <Footer />
  </div>
</template>

<script>
export default {
  name: 'MyAccountLayout',
  middleware: 'auth'
}
</script> 