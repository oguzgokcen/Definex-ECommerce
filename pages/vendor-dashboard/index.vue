<template>

    <div id="vendor_dashboard">
        <div class="row">
            <div class="col-lg-4 col-md-4 col-sm-6 col-12">
                <div class="vendor_top_box">
                <h2>25</h2>
                <h4>Total Products</h4>
                </div>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-6 col-12">
                <div class="vendor_top_box">
                <h2>2552</h2>
                <h4>Total Sales</h4>
                </div>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-6 col-12">
                <div class="vendor_top_box">
                <h2>50</h2>
                <h4>Order Pending</h4>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-6">
                <div class="mychart_area" id="linechart">
                    <apexchart type="line" height="315" :options="options" :series="series1"></apexchart>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="mychart_area" id="chart">
                    <apexchart type="bar" height="315" :options="chartOptions" :series="series"></apexchart>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-6 col-md-6 col-sm-12 col-12">
                <div class="vendor_order_boxed">
                    <h4>Pending Products</h4>
                    <table class="table pending_table">
                        <thead class="thead-light">
                            <tr>
                                <th scope="col">Image</th>
                                <th scope="col">Product Name</th>
                                <th scope="col">Price</th>
                                <th scope="col">Sales</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td scope="row"><img :src="require('@/assets/img/common/1.jpg')" alt="img"></td>
                                <td>neck velvet dress</td>
                                <td>$205</td>
                                <td>1000</td>
                            </tr>
                            <tr>
                                <td scope="row"><img :src="require('@/assets/img/common/1.jpg')" alt="img"></td>
                                <td>neck velvet dress</td>
                                <td>$205</td>
                                <td>1000</td>
                            </tr>
                            <tr>
                                <td scope="row"><img :src="require('@/assets/img/common/1.jpg')" alt="img"></td>
                                <td>neck velvet dress</td>
                                <td>$205</td>
                                <td>1000</td>
                            </tr>
                            <tr>
                                <td scope="row"><img :src="require('@/assets/img/common/1.jpg')" alt="img"></td>
                                <td>neck velvet dress</td>
                                <td>$205</td>
                                <td>1000</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12 col-12">
                <div class="vendor_order_boxed">
                    <h4>Recent Orders</h4>
                    <table class="table pending_table">
                        <thead class="thead-light">
                            <tr>
                            <th scope="col">Order Id</th>
                            <th scope="col">Product Details</th>
                            <th scope="col">Status</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                            <td scope="row">#21515</td>
                            <td>Neck Velvet Dress</td>
                            <td>Confrimed</td>
                            </tr>
                            <tr>
                            <td scope="row">#78153		</td>
                            <td>Belted Trench Coat</td>
                            <td>Shipped</td>
                            </tr>
                            <tr>
                            <td scope="row">#51512		</td>
                            <td>Man Print Tee</td>
                            <td>Pending</td>
                            </tr>
                            <tr>
                            <td scope="row">#78153	</td>
                            <td>	Belted Trench Coat</td>
                            <td>Shipped</td>
                            </tr>
                            <tr>
                            <td scope="row">#78153		</td>
                            <td>Belted Trench Coat</td>
                            <td>Shipped</td>
                            </tr>
                            <tr>
                            <td scope="row">#51512		</td>
                            <td>Man Print Tee</td>
                            <td>Pending</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'VendorDashboard',
    layout: 'admin-layout',
    data() {
        return {
            title: 'Vendor',

            // Breadcrumb Items Data
            breadcrumbItems: [
                {
                    text: 'Home',
                    to: '/'
                },
                {
                    text: 'Vendor',
                }
            ],
            options: {
                chart: {
                id: 'linechart'
                },
                xaxis: {
                categories: [1991, 1992, 1993, 1994, 1995, 1996, 1997, 1998]
                }
            },
            series1: [
                {
                    name: 'Total Revenue',
                    data: [44, 55, 57, 56, 61, 58, 63, 60, 66]
                }, 
                {
                    name: 'Point movments',
                    data: [76, 85, 101, 98, 87, 105, 91, 114, 94]
                },
                {
                    name: 'Revenue',
                    data: [35, 41, 36, 26, 45, 48, 52, 53, 41]
                }
            ],


            series: [
                {
                    name: 'Recent Orders',
                    data: [44, 55, 57, 56, 61, 58, 63, 60, 66]
                }, 
                {
                    name: 'Pending Payments',
                    data: [76, 85, 101, 98, 87, 105, 91, 114, 94]
                },
                {
                    name: 'Revenue',
                    data: [35, 41, 36, 26, 45, 48, 52, 53, 41]
                }
            ],
          chartOptions: {
            chart: {
              type: 'bar',
            },
            plotOptions: {
              bar: {
                horizontal: false,
                columnWidth: '55%',
                endingShape: 'rounded'
              },
            },
            dataLabels: {
              enabled: false
            },
            stroke: {
              show: true,
              width: 2,
              colors: ['transparent']
            },
            xaxis: {
              categories: ['Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct'],
            },
            yaxis: {
              title: {
                text: '$ (thousands)'
              }
            },
            fill: {
              opacity: 1
            },
            tooltip: {
              y: {
                formatter: function (val) {
                  return "$ " + val + " thousands"
                }
              }
            }
          },

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
            content: 'Vendor Dashboard - AndShop Ecommerce Vue js, Nuxt js Template'
          }
        ]
      }
    }
}
</script>