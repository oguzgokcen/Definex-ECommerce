<template>
  <div class="chat-widget">
    <div class="chat-icon" @click="toggleChat">
      <i class="fas fa-comments"></i>
      <span v-if="unreadCount > 0" class="unread-badge">{{ unreadCount }}</span>
    </div>

    <div v-if="isOpen" class="chat-interface">
      <div class="chat-header">
        <h4>Müşteri Hizmetleri</h4>
        <button class="close-btn" @click="toggleChat">
          <i class="fas fa-times"></i>
        </button>
      </div>
      
      <div v-if="!isAuthenticated" class="auth-message">
        <i class="fas fa-lock"></i>
        <p>Mesajlaşmak için giriş yapmanız gerekmektedir</p>
        <a :href="keycloakLoginUrl" class="login-btn">
          <i class="fas fa-sign-in-alt"></i>
          Giriş Yap
        </a>
      </div>

      <template v-else>
        <div class="chat-messages" ref="messageContainer">
          <div v-for="(message, index) in messages" :key="index" 
               :class="['message', message.type]">
            <div class="message-content">
              {{ message.text }}
            </div>
          </div>
        </div>
        <div class="chat-input">
          <input 
            v-model="newMessage" 
            @keyup.enter="sendMessage"
            placeholder="Mesajınızı giriniz..."
            type="text"
          >
          <button @click="sendMessage">
            <i class="fas fa-paper-plane"></i>
          </button>
        </div>
      </template>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  name: 'ChatWidget',
  data() {
    return {
      isOpen: false,
      newMessage: '',
      messages: [],
      unreadCount: 0,
      keycloakLoginUrl: 'http://localhost:8080/realms/e-commerce/protocol/openid-connect/auth?client_id=public-client&response_type=code&redirect_uri=http://localhost:3000/login'
    };
  },
  computed: {
    ...mapGetters({
      isAuthenticated: 'auth/isAuthenticated'
    })
  },
  mounted() {
    if (this.isAuthenticated) {
      this.$signalR.startConnection();

      this.$signalR.onMessageReceived((message) => {
        this.messages.push({
          text: message.content,
          type: 'received',
        });
        if (!this.isOpen) {
          this.unreadCount++;
        }
      });

      this.$signalR.onErrorReceived((errorMessage) => {
        alert(errorMessage);
      });

      // Initial message
      this.messages.push({
        text: 'Merhaba, nasıl yardımcı olabiliriz?',
        type: 'received',
      });
    }
  },
  beforeDestroy() {
    if (this.isAuthenticated) {
      this.$signalR.stopConnection();
    }
  },
  methods: {
    toggleChat() {
      this.isOpen = !this.isOpen;
      if (this.isOpen) {
        this.unreadCount = 0;
      }
    },
    sendMessage() {
      if (this.newMessage.trim() && this.isAuthenticated) {
        const message = {
          text: this.newMessage,
          type: 'sent',
        };
        this.messages.push(message);
        this.$signalR.sendMessageToAdmin(this.newMessage);
        this.newMessage = '';
      }
    }
  },
};
</script>

<style scoped>
.chat-widget {
  position: fixed;
  bottom: 20px;
  right: 20px;
  z-index: 1000;
}

.chat-icon {
  width: 60px;
  height: 60px;
  background-color: #007bff;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  box-shadow: 0 2px 10px rgba(0,0,0,0.2);
  position: relative;
  transition: transform 0.3s ease;
}

.chat-icon:hover {
  transform: scale(1.1);
}

.chat-icon i {
  color: white;
  font-size: 24px;
}

.unread-badge {
  position: absolute;
  top: -5px;
  right: -5px;
  background-color: #dc3545;
  color: white;
  border-radius: 50%;
  width: 20px;
  height: 20px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 12px;
}

.chat-interface {
  position: absolute;
  bottom: 80px;
  right: 0;
  width: 400px;
  height: 400px;
  background-color: white;
  border-radius: 10px;
  box-shadow: 0 5px 15px rgba(0,0,0,0.2);
  display: flex;
  flex-direction: column;
}

.chat-header {
  padding: 15px;
  background-color: #007bff;
  color: white;
  border-radius: 10px 10px 0 0;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.chat-header h4 {
  margin: 0;
  font-size: 16px;
}

.close-btn {
  background: none;
  border: none;
  color: white;
  cursor: pointer;
  font-size: 18px;
}

.chat-messages {
  display:flex;
  flex-direction:column;
  flex: 1;
  padding: 15px;
  overflow-y: auto;
}

.message {
  display: flex;
  justify-content: flex-start;
  margin-bottom: 10px;
  max-width: 80%;
}

.message.sent {
  align-self: self-end; 
}

.message-content {
  padding: 8px 12px;
  border-radius: 15px;
  display: inline-block;
  max-width: 100%; 
  word-wrap: break-word;
  overflow-wrap: break-word; 
  white-space: normal;
}

.message.sent .message-content {
  background-color: #007bff;
  color: white;
  border-radius: 15px 15px 0 15px;
  text-align: right;
}

.chat-input {
  padding: 15px;
  border-top: 1px solid #eee;
  display: flex;
  gap: 10px;
}

.chat-input input {
  flex: 1;
  padding: 8px 12px;
  border: 1px solid #ddd;
  border-radius: 20px;
  outline: none;
}

.chat-input button {
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 50%;
  width: 35px;
  height: 35px;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.chat-input button:hover {
  background-color: #0056b3;
}

.auth-message {
  flex: 1;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 20px;
  text-align: center;
  background-color: #f8f9fa;
}

.auth-message i {
  font-size: 48px;
  color: #dc3545;
  margin-bottom: 15px;
}

.auth-message p {
  color: #666;
  margin-bottom: 20px;
  font-size: 16px;
}

.login-btn {
  display: flex;
  align-items: center;
  gap: 8px;
  padding: 10px 20px;
  background-color: var(--main-theme-color);
  color: white;
  border-radius: 20px;
  text-decoration: none;
  transition: background-color 0.3s ease;
}

.login-btn:hover {
  background-color: var(--main-theme-color-dark);
}

.login-btn i {
  font-size: 16px;
  color: white;
}
</style> 