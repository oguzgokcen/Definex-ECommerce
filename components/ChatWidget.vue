<template>
  <div class="chat-widget">
    <div class="chat-icon" @click="toggleChat">
      <i class="fas fa-comments"></i>
      <span v-if="unreadCount > 0" class="unread-badge">{{ unreadCount }}</span>
    </div>

    <div v-if="isOpen" class="chat-interface">
      <div class="chat-header">
        <h4>Customer Support</h4>
        <button class="close-btn" @click="toggleChat">
          <i class="fas fa-times"></i>
        </button>
      </div>
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
          placeholder="Type your message..."
          type="text"
        >
        <button @click="sendMessage">
          <i class="fas fa-paper-plane"></i>
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'ChatWidget',
  data() {
    return {
      isOpen: false,
      newMessage: '',
      messages: [
        {
          text: 'Hello! How can we help you today?',
          type: 'received'
        }
      ],
      unreadCount: 0
    }
  },
  methods: {
    toggleChat() {
      this.isOpen = !this.isOpen;
      if (this.isOpen) {
        this.unreadCount = 0;
        this.$nextTick(() => {
          this.scrollToBottom();
        });
      }
    },
    sendMessage() {
      if (this.newMessage.trim()) {
        this.messages.push({
          text: this.newMessage,
          type: 'sent'
        });
        this.newMessage = '';
        this.$nextTick(() => {
          this.scrollToBottom();
        });
        // Here you can add API call to send message to backend
        this.simulateResponse();
      }
    },
    scrollToBottom() {
      const container = this.$refs.messageContainer;
      container.scrollTop = container.scrollHeight;
    },
    simulateResponse() {
      setTimeout(() => {
        this.messages.push({
          text: 'Thank you for your message. Our team will get back to you soon.',
          type: 'received'
        });
        this.$nextTick(() => {
          this.scrollToBottom();
        });
      }, 1000);
    }
  }
}
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
  flex: 1;
  padding: 15px;
  overflow-y: auto;
}

.message {
  margin-bottom: 10px;
  max-width: 80%;
}

.message.sent {
  margin-left: auto;
}

.message-content {
  padding: 8px 12px;
  border-radius: 15px;
  display: inline-block;
}

.message.sent .message-content {
  background-color: #007bff;
  color: white;
  border-radius: 15px 15px 0 15px;
}

.message.received .message-content {
  background-color: #f1f1f1;
  color: #333;
  border-radius: 15px 15px 15px 0;
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
</style> 