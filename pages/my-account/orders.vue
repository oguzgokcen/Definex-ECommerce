<template>
    <div class="myaccount-content">
        <!-- Success Popup -->
        <div v-if="showSuccessPopup" class="success-popup">
            <div class="success-content">
                <i class="fas fa-check-circle"></i>
                <h3>Sipariş Başarıyla Oluşturuldu!</h3>
                <p>Siparişiniz başarıyla oluşturuldu. Sipariş detaylarınızı aşağıda görebilirsiniz.</p>
                <button @click="closeSuccessPopup">Tamam</button>
            </div>
        </div>

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
            showSuccessPopup: false,
        }
    },
    async mounted() {
        // For scroll page top for every Route 
        window.scrollTo(0, 0);
        await this.fetchOrders();
        
        // Check for success parameter in route
        if (this.$route.query.success === 'true') {
            this.showSuccessPopup = true;
        }
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
                0: 'Ödeme Bekleniyor',
                1: 'Ödeme Tamamlandı',
                2: 'Sipariş Tamamlandı',
                3: 'Ödeme Başarısız'
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
                'PRE_PAYMENT': 'pending',
                'PAYMENT_FAILED': 'cancelled'
            };
            return statusClasses[status] || '';
        },
        closeSuccessPopup() {
            this.showSuccessPopup = false;
            // Remove the success parameter from URL
            this.$router.replace({ query: { ...this.$route.query, success: undefined } });
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

.success-popup {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    align-items: center;
    justify-content: center;
    z-index: 1000;
}

.success-content {
    background-color: white;
    padding: 30px;
    border-radius: 10px;
    text-align: center;
    max-width: 400px;
    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}

.success-content i {
    font-size: 48px;
    color: #28a745;
    margin-bottom: 20px;
}

.success-content h3 {
    color: #28a745;
    margin-bottom: 15px;
}

.success-content p {
    color: #666;
    margin-bottom: 20px;
}

.success-content button {
    background-color: #28a745;
    color: white;
    border: none;
    padding: 10px 20px;
    border-radius: 5px;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.success-content button:hover {
    background-color: #218838;
}
</style>