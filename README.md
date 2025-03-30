## E-commerce Frontend Kullanılan teknolojiler:
### Axios: http requestleri için
### microsoft@signalR : mesajlajma işlevi için

- Admin url: /vendor-dashboard
### Auth.js : kullanıcıyi server tarafında autorize etmeyi sağlayan verileri tutar. Ayrıca jwt-decode ile kullanıcıların rolüne göre admin sayfasına access verebilir.
### Middleware : Kullanıcılara belli sayfalarda erişimi kısıtlamak için middleware kullanılmıştır. İki tür middleware vardır : admin ve kullanıcı.
### Chatting: Kullanıcı tarafında components/chatwidget.vue , admin tarafında vendor-dashboard/chat.vue sayfaları signalr clientı kullanarak mesajlaşma işlevini sağlar.
