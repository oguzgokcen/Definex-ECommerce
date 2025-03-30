![ChatGPT Image 30 Mar 2025 22_24_45](https://github.com/user-attachments/assets/32f54b9e-c3c1-472c-aa89-aafdcdd8f1a8)
# Kullanılan Teknolojiler:
- ### Marten : Transactional Document DB and Event Store on PostgreSQL
- ### Mediatr : Bileşenler arasında bağımsız iletişim (decoupling) sağlar. Aynı zamanda CQRS (Command Query Responsibility Segregation) desenini uygulamak ve cross-cutting concerns (validation,logging) implementasyonu için de kullanılır.
- ### Fluent Validation : Veri doğrulama kurallarını belirlemek için kullanılır.
- ### SignalR : Gerçek zamanlı çift yönlü iletişim sağlar 
- ### Docker Compose : Servislerin kullandığı databaseleri ve keycloak instancelarını oluşturmamızı sağlar.
- ### Keycloak : Frontent uygulamasında kullanıcıya giriş yapma ve kayıt olma ekranlarını sunar. Kullanıcıyı girilen bilgilere göre authorize eder ve servislere authorizasyon için gerekli metadataları sağlar.


## Catalog Api:
- E ticaret sitesindeki ürünler için crud operasyonlarını gerçekleştirir.
- Database olarak postgresql document db kullanır.
## Order Api:
- Sipariş ve ödeme süreçlerini yönetir. Kullanıcının sipariş oluşturması için gerekli metodları sağlar.
- Verilen siparileri İlişkisel veritabanında tutar.
## Chat Api:
- Kullanıcıların müşteri hizmetleriyle mesajlajması için gerekli operasyonları gerçekleştirir.
- Mesajları Postrgesql document dbde tutar.

## Çalıştırmak için:
docker compose up komutu ile docker dosyasını çalıştırın.
Daha sonra gateway https diğer servisleri http config'inde çalıştırın. 
Keycloak config :
Realm adı : e-commerce
Client: public-client

Admin rolü için realm rolesta kullanıcıya "admin" rolü ekleyin. 
