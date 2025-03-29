<template>
  <div class="chat-container">
    <div class="chat-sidebar">
      <div class="chat-sidebar-header">
        <h3>Mesajlar</h3>
        <div class="search-box">
          <input type="text" placeholder="Kullanıcı ara..." v-model="searchQuery" />
          <i class="fas fa-search"></i>
        </div>
      </div>
      
      <div class="conversations-list">
        <div v-if="loading" class="loading-state">
          <i class="fas fa-spinner fa-spin"></i>
          <p>Konuşmalar yükleniyor...</p>
        </div>
        <div v-else-if="error" class="error-state">
          <i class="fas fa-exclamation-circle"></i>
          <p>{{ error }}</p>
        </div>
        <div v-else-if="conversations.length === 0" class="empty-state">
          <i class="fas fa-comments"></i>
          <p>Henüz hiç konuşma yok</p>
        </div>
        <div 
          v-else
          v-for="conversation in filteredConversations" 
          :key="conversation.id"
          class="conversation-item"
          :class="{ active: selectedConversation?.id === conversation.id }"
          @click="selectConversation(conversation)"
        >
          <div class="conversation-info">
            <div class="conversation-name">{{ conversation.name }}</div>
            <div class="conversation-preview">{{ conversation.lastMessage }}</div>
          </div>
          <div class="conversation-time">{{ conversation.lastMessageTime }}</div>
        </div>
      </div>
    </div>

    <div class="chat-main">
      <div v-if="selectedConversation" class="chat-window">
        <div class="chat-header">
          <div class="chat-user-info">
            <div>
              <h4>{{ selectedConversation.name }}</h4>
            </div>
          </div>
        </div>

        <div class="chat-messages" ref="messagesContainer">
          <div 
            v-for="message in selectedConversation.messages" 
            :key="message.id"
            class="message"
            :class="{ 'message-sent': message.sent }"
          >
            <div class="message-content">
              {{ message.content }}
            </div>
            <div class="message-time">{{ message.time }}</div>
          </div>
        </div>

        <div class="chat-input">
          <input 
            type="text" 
            v-model="newMessage" 
            placeholder="Mesajınızı yazın..."
            @keyup.enter="sendMessage"
          >
          <button @click="sendMessage">
            <i class="fas fa-paper-plane"></i>
          </button>
        </div>
      </div>

      <div v-else class="no-conversation">
        <i class="fas fa-comments"></i>
        <p>Mesajlaşmak için bir konuşma seçin</p>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex'

export default {
  name: 'AdminChat',
  layout: 'admin-layout',
  middleware: ['admin'],
  data() {
    return {
      searchQuery: '',
      newMessage: '',
      selectedConversation: null,
      conversations: [],
      loading: false,
      error: null
    }
  },
  computed: {
    ...mapState({
      token: state => state.auth.token
    }),
    filteredConversations() {
      if (!this.searchQuery) return this.conversations
      return this.conversations.filter(conv => 
        conv.name.toLowerCase().includes(this.searchQuery.toLowerCase())
      )
    }
  },
  async mounted() {
    await this.fetchConversations()
    this.initializeSignalR()
  },
  beforeDestroy() {
    this.$signalR.stopConnection()
  },
  methods: {
    initializeSignalR() {
      this.$signalR.startConnection()

      this.$signalR.onMessageReceived((message) => {
        console.log(message)
        let conversation = this.conversations.find(c => c.id === message.conversationId)
        
        if (!conversation) {
          conversation = {
            id: message.conversationId,
            name: message.userName,
            lastMessage: message.message,
            lastMessageTime: new Date().toLocaleTimeString('tr-TR', { hour: '2-digit', minute: '2-digit' }),
            messages: []
          }
          this.conversations.unshift(conversation)
        }

        // Add the new message
        const newMessage = {
          id: Date.now(),
          content: message.content,
          time: new Date().toLocaleTimeString('tr-TR', { hour: '2-digit', minute: '2-digit' }),
          sent: false
        }

        conversation.messages.push(newMessage)
        conversation.lastMessage = message.message
        conversation.lastMessageTime = newMessage.time

      })

      this.$signalR.onErrorReceived((errorMessage) => {
        console.error('SignalR Error:', errorMessage)
        this.error = 'Bağlantı hatası oluştu. Lütfen sayfayı yenileyin.'
      })
    },
    async fetchConversations() {
      try {
        this.loading = true
        this.error = null
        const response = await this.$axios.get('/chat/conversations', {
          headers: {
            'Authorization': `Bearer ${this.token}`
          }
        })
        this.conversations = response.data.map(conv => ({
          id: conv.id,
          userId: conv.userId,
          name: conv.userName,
          lastMessage: conv.lastMessage,
          lastMessageTime: new Date(conv.lastMessageTime).toLocaleTimeString('tr-TR', { hour: '2-digit', minute: '2-digit' }),
          messages: conv.messages.map(msg => ({
            id: msg.id,
            content: msg.content,
            time: new Date(msg.time).toLocaleTimeString('tr-TR', { hour: '2-digit', minute: '2-digit' }),
            sent: msg.senderId === 'admin'
          }))
        }))
      } catch (error) {
        this.error = 'Konuşmalar yüklenirken bir hata oluştu.'
        console.error('Error fetching conversations:', error)
      } finally {
        this.loading = false
      }
    },
    selectConversation(conversation) {
      this.selectedConversation = conversation
    },
    async sendMessage() {
      if (!this.newMessage.trim() || !this.selectedConversation) return
      console.log(this.selectedConversation)
        const message = {
          id: Date.now(),
          content: this.newMessage,
          time: new Date().toLocaleTimeString('tr-TR', { hour: '2-digit', minute: '2-digit' }),
          sent: true
        }

        this.selectedConversation.messages.push(message)
        this.selectedConversation.lastMessage = this.newMessage
        this.selectedConversation.lastMessageTime = message.time

        await this.$signalR.sendMessageToUser(this.selectedConversation.userId, this.newMessage)

        this.newMessage = ''
    },
  }
}
</script>

<style scoped>
.chat-container {
  display: flex;
  height: calc(100vh - 60px);
  background: #fff;
  border-radius: 8px;
  overflow: hidden;
}

.chat-sidebar {
  width: 300px;
  border-right: 1px solid #eee;
  display: flex;
  flex-direction: column;
}

.chat-sidebar-header {
  padding: 20px;
  border-bottom: 1px solid #eee;
}

.chat-sidebar-header h3 {
  margin: 0 0 15px 0;
  color: #333;
}

.search-box {
  position: relative;
}

.search-box input {
  width: 100%;
  padding: 8px 35px 8px 15px;
  border: 1px solid #ddd;
  border-radius: 20px;
  font-size: 14px;
}

.search-box i {
  position: absolute;
  right: 15px;
  top: 50%;
  transform: translateY(-50%);
  color: #999;
}

.conversations-list {
  flex: 1;
  overflow-y: auto;
  padding: 10px;
}

.conversation-item {
  display: flex;
  align-items: center;
  padding: 10px;
  border-radius: 8px;
  cursor: pointer;
  transition: background-color 0.2s;
}

.conversation-item:hover {
  background-color: #f5f5f5;
}

.conversation-item.active {
  background-color: #e3f2fd;
}

.conversation-avatar {
  position: relative;
  margin-right: 15px;
}

.conversation-avatar img {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  object-fit: cover;
}

.conversation-info {
  flex: 1;
}

.conversation-name {
  font-weight: 500;
  margin-bottom: 2px;
}

.conversation-preview {
  font-size: 12px;
  color: #666;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.conversation-time {
  font-size: 12px;
  color: #999;
}

.chat-main {
  flex: 1;
  display: flex;
  flex-direction: column;
}

.chat-window {
  display: flex;
  flex-direction: column;
  height: 100%;
}

.chat-header {
  padding: 20px;
  border-bottom: 1px solid #eee;
}

.chat-user-info {
  display: flex;
  align-items: center;
}

.chat-user-info img {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  margin-right: 15px;
}

.chat-user-info h4 {
  margin: 0;
  color: #333;
}

.status {
  font-size: 12px;
  color: #4caf50;
}

.chat-messages {
  flex: 1;
  padding: 20px;
  overflow-y: auto;
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.message {
  max-width: 70%;
  padding: 10px 15px;
  border-radius: 15px;
  background: #f5f5f5;
  position: relative;
}

.message-sent {
  background: #e3f2fd;
  align-self: flex-end;
  border-bottom-right-radius: 5px;
}

.message-sent .message-time {
  text-align: right;
}

.message-time {
  font-size: 11px;
  color: #999;
  margin-top: 5px;
}

.chat-input {
  padding: 20px;
  border-top: 1px solid #eee;
  display: flex;
  gap: 10px;
}

.chat-input input {
  flex: 1;
  padding: 10px 15px;
  border: 1px solid #ddd;
  border-radius: 20px;
  font-size: 14px;
}

.chat-input button {
  background: var(--main-theme-color);
  color: white;
  border: none;
  border-radius: 50%;
  width: 40px;
  height: 40px;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  transition: background-color 0.2s;
}

.chat-input button:hover {
  background: var(--main-theme-color-dark);
}

.no-conversation {
  flex: 1;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  color: #999;
}

.no-conversation i {
  font-size: 48px;
  margin-bottom: 15px;
}

.no-conversation p {
  font-size: 16px;
}

.loading-state,
.error-state,
.empty-state {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 20px;
  color: #999;
}

.loading-state i,
.error-state i,
.empty-state i {
  font-size: 24px;
  margin-bottom: 10px;
}

.error-state {
  color: #dc3545;
}

.loading-state i {
  color: var(--main-theme-color);
}
</style> 