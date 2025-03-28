<template>
    <div class="myaccount-content">
        <h4 class="title">Orders</h4>
        <div class="table_page table-responsive">
            <table>
                <thead>
                    <tr>
                        <th>Order</th>
                        <th>Date</th>
                        <th>Status</th>
                        <th>Total</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="order in orders" :key="order.id">
                        <td>#{{ order.id }}</td>
                        <td>{{ formatDate(order.createdOnUtc) }}</td>
                        <td>
                            <span :class="getStatusClass(order.status)">{{ order.status }}</span>
                        </td>
                        <td>${{ order.price }} for {{ order.items.length }} items</td>
                    </tr>
                    <tr v-if="orders.length === 0 || errors">
                        <td colspan="4" class="text-center">
                            <div v-if="errors" class="error-message">
                                {{ errors }}
                            </div>
                            <div v-else>
                                No orders found
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script>
export default {
    name: 'Orders',
    layout: 'my-account',
    data() {
        return {
            title: 'Orders',
            orders: [],
            errors: null,
            loading: false,
            // Breadcrumb Items Data
            breadcrumbItems: [
                {
                    text: 'Home',
                    to: '/'
                },
                {
                    text: 'Orders',
                }
            ],
        }
    },
    async mounted() {
        // For scroll page top for every Route 
        window.scrollTo(0, 0);
        await this.fetchOrders();
    },
    methods: {
        async fetchOrders() {
            try {
                this.loading = true;
                this.errors = null;
                const response = await this.$axios.get('/order/myorders', {
                    headers: {
                        'Authorization': `Bearer ${localStorage.getItem('token')}`
                    }
                });
                this.orders = response.data.orders.map(order => ({
                    price: order.price,
                    createdOnUtc: order.createdOnUtc,
                    status: this.getOrderStatus(order.orderStatus),
                    items: order.basketItems
                }));
                if (this.orders.length === 0) {
                    this.errors = "No orders found";
                }
            } catch (error) {
                if (error.response) {
                    this.errors = error.response.data.message || 'Failed to fetch orders. Please try again later.';
                } else {
                    this.errors = 'An unexpected error occurred. Please try again later.';
                }
                this.orders = []; 
            } finally {
                this.loading = false;
            }
        },
        getOrderStatus(orderStatus) {
            const statusMap = {
                0: 'PENDING',
                1: 'PROCESSING',
                2: 'COMPLETED',
                3: 'CANCELLED'
            };
            return statusMap[orderStatus] || 'UNKNOWN';
        },
        formatDate(dateString) {
            const date = new Date(dateString);
            return date.toLocaleDateString('en-US', {
                year: 'numeric',
                month: 'long',
                day: 'numeric'
            });
        },
        getStatusClass(status) {
            const statusClasses = {
                'COMPLETED': 'success',
                'PROCESSING': 'processing',
                'PENDING': 'pending',
                'CANCELLED': 'cancelled'
            };
            return statusClasses[status] || '';
        },
    },
    // Page head() Title, description for SEO 
    head() {
      return {
        title: this.title,
        meta: [
          {
            hid: 'description',
            name: 'description',
            content: 'Orders page - AndShop Ecommerce Vue js, Nuxt js Template'
          }
        ]
      }
    }
}
</script>

<style scoped>
.success {
    color: #28a745;
    font-weight: 600;
}
.processing {
    color: #ffc107;
    font-weight: 600;
}
.pending {
    color: #17a2b8;
    font-weight: 600;
}
.cancelled {
    color: #dc3545;
    font-weight: 600;
}
.error-message {
    color: #dc3545;
    font-weight: 500;
    padding: 10px;
}
</style>