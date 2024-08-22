# Kütüphane Yönetim Sistemi API'si

Bu proje, bir kütüphane yönetim sistemi için geliştirilmiş **ASP.NET Core Web API** uygulamasıdır.
Kitaplar, yazarlar, kategoriler, diller, ödünç alma kayıtları, çalışanlar ve üyeler gibi çeşitli kütüphane varlıklarını yönetmeyi sağlar.
JWT tabanlı kimlik doğrulama ve yetkilendirme kullanılmaktadır.

## Özellikler



### Kullanılan Teknolojiler

- **ASP.NET Core**: .NET Core platformu üzerinde Web API geliştirmek için kullanıldı.
- **Entity Framework Core**: Veritabanı yönetimi için kullanıldı. Code-first yaklaşımı ile modeller ve ilişkiler tanımlandı.
- **Identity Framework**: Kullanıcı kimlik doğrulama ve yetkilendirme işlemleri için kullanıldı.
- **JWT Authentication**: API için token tabanlı kimlik doğrulama sağlamak amacıyla kullanıldı.
- **SQL Server**: Veritabanı olarak kullanıldı.
- **Swagger**: API dökümantasyonu ve test arayüzü sağlamak için kullanıldı.


- 
### Kitap Modeli (Book)
Kitap modeli, kütüphane yönetim sisteminde bir kitabı temsil eder ve aşağıdaki özelliklere sahiptir:

- **Id**: Kitabın benzersiz kimliği.
- **ISBN**: Kitabın uluslararası standart kitap numarası (ISBN), 10-13 karakter uzunluğunda.
- **Başlık (Title)**: Kitabın adı, maksimum 2000 karakter.
- **Sayfa Sayısı (PageCount)**: Kitabın sayfa sayısı, 1 ile maksimum değer arasında bir aralıkta.
- **Yayın Yılı (PublishingYear)**: Kitabın yayımlandığı yıl, -4000 ile 2100 arasında bir değer alabilir.
- **Açıklama (Description)**: Kitabın açıklaması, maksimum 5000 karakter.
- **Baskı Sayısı (PrintCount)**: Kitabın kaçıncı baskısı olduğu.
- **Yasaklı (Banned)**: Kitabın yasaklı olup olmadığını belirten bir değer.
- **Derecelendirme (Rating)**: Kitabın kullanıcılar tarafından derecelendirilmesi, 0 ile 5 arasında bir değer.
- **Yazarlar (Authors)**: Kitabın yazarlarıyla olan ilişkisi (AuthorBook tablosu ile).
- **Diller (Languages)**: Kitabın dil ilişkileri (LanguageBook tablosu ile).
- **Alt Kategoriler (SubCategories)**: Kitabın alt kategori ilişkileri (SubCategoryBook tablosu ile).
- **Kitap Kopyaları (BookCopies)**: Kitabın mevcut kopyaları.
- **Yayınevi (Publisher)**: Kitabın yayınevi ile olan ilişkisi.
- **Raf Konumu (LocationShelf)**: Kitabın kütüphanedeki fiziksel raf konumu.



## API İşlevselliği

### Kitap İşlemleri (Books)

#### Get All Books
Tüm kitapları listelemek için kullanılır.
- **HTTP GET**: `/api/Books`
- **Yetki Gereksinimi**: Çalışan (Employee) rolü.

#### Get Book by Id
Belirli bir kitabı ID ile getirmek için kullanılır.
- **HTTP GET**: `/api/Books/{id}`
- **Yetki Gereksinimi**: Çalışan (Employee) rolü.

#### Create Book
Yeni bir kitap eklemek için kullanılır.
- **HTTP POST**: `/api/Books`
- **Yetki Gereksinimi**: Çalışan (Employee) rolü.

#### Update Book
Mevcut bir kitabı güncellemek için kullanılır.
- **HTTP PUT**: `/api/Books/{id}`
- **Yetki Gereksinimi**: Çalışan (Employee) rolü.

#### Delete Book
Mevcut bir kitabı silmek için kullanılır.
- **HTTP DELETE**: `/api/Books/{id}`
- **Yetki Gereksinimi**: Çalışan (Employee) rolü.






### Ödünç Alma İşlemleri (Borrows)

#### Get All Borrows
Tüm ödünç alma kayıtlarını listeler.
- **HTTP GET**: `/api/Borrows`
- **Yetki Gereksinimi**: Çalışan (Employee) rolü.

#### Get Borrow by Id
Belirli bir ödünç alma kaydını getirir.
- **HTTP GET**: `/api/Borrows/{id}`
- **Yetki Gereksinimi**: Çalışan (Employee) rolü.

#### Create Borrow
Yeni bir ödünç alma kaydı oluşturur.
- **HTTP POST**: `/api/Borrows`
- **Yetki Gereksinimi**: Çalışan (Employee) rolü.

#### Update Borrow
Ödünç alınan kitabı geri getirir ve ilgili bilgileri günceller. Cezalar, gecikme gün sayısına göre hesaplanır ve üye hesabına eklenir.
- **HTTP PUT**: `/api/Borrows`
- **Yetki Gereksinimi**: Çalışan (Employee) rolü.

#### Delete Borrow
Belirli bir ödünç alma kaydını siler.
- **HTTP DELETE**: `/api/Borrows/{id}`
- **Yetki Gereksinimi**: Çalışan (Employee) rolü.






### Çalışan İşlemleri (Employees)

#### Get All Employees
Tüm çalışanları listeler.
- **HTTP GET**: `/api/Employees`
- **Yetki Gereksinimi**: Çalışan (Employee) rolü.

#### Get Employee by Id
Belirli bir çalışanı getirir.
- **HTTP GET**: `/api/Employees/{id}`
- **Yetki Gereksinimi**: Çalışan (Employee) rolü.

#### Create Employee
Yeni bir çalışan ekler.
- **HTTP POST**: `/api/Employees`
- **Yetki Gereksinimi**: Çalışan (Employee) rolü.

#### Update Employee
Belirli bir çalışanın bilgilerini günceller.
- **HTTP PUT**: `/api/Employees/{id}`
- **Yetki Gereksinimi**: Çalışan (Employee) rolü.

#### Delete Employee
Belirli bir çalışanı siler.
- **HTTP DELETE**: `/api/Employees/{id}`
- **Yetki Gereksinimi**: Çalışan (Employee) rolü.





### Üye İşlemleri (Members)

#### Get All Members
Tüm üyeleri listeler.
- **HTTP GET**: `/api/Members`
- **Yetki Gereksinimi**: Çalışan (Employee) rolü.

#### Get Member by Id
Belirli bir üyeyi getirir.
- **HTTP GET**: `/api/Members/{id}`
- **Yetki Gereksinimi**: Çalışan (Employee) rolü.

#### Create Member
Yeni bir üye ekler.
- **HTTP POST**: `/api/Members`
- **Yetki Gereksinimi**: Çalışan (Employee) rolü.

#### Update Member
Belirli bir üyenin bilgilerini günceller.
- **HTTP PUT**: `/api/Members/{id}`
- **Yetki Gereksinimi**: Çalışan (Employee) rolü.

#### Delete Member
Belirli bir üyeyi siler.
- **HTTP DELETE**: `/api/Members/{id}`
- **Yetki Gereksinimi**: Çalışan (Employee) rolü.





### Kayıt İşlemleri (Registrations)

#### Login
Kullanıcıyı giriş yapar ve JWT token döner.
- **HTTP POST**: `/api/Registrations/Login`

#### Logout
Kullanıcıyı çıkış yaptırır.
- **HTTP GET**: `/api/Registrations/Logout`
- **Yetki Gereksinimi**: Kullanıcı giriş yapmış olmalıdır.





## Program.cs

`Program.cs`, ASP.NET Core uygulamanızın yapılandırmasını ve başlatılmasını içerir. Aşağıda, bu dosyanın temel işlevleri özetlenmiştir:

- **DbContext Konfigürasyonu**: `LibraryAPIsContext` ile SQL Server veritabanı bağlantısı yapılandırılır.
- **Identity Konfigürasyonu**: `ApplicationUser` ve `IdentityRole` kullanılarak kullanıcı ve rol yönetimi yapılır.
- **JWT Authentication**: JWT token doğrulama ve kimlik doğrulama yapılandırması yapılır.
- **Swagger Konfigürasyonu**: API dokümantasyonu için Swagger yapılandırması yapılır.
- **Migration**: Veritabanı şeması güncellenir.
- **Admin Kullanıcı Oluşturma**: Eğer "Admin" rolü ve kullanıcısı yoksa, oluşturulur.





## Kurulum

Projeyi kendi makinenizde çalıştırmak için şu adımları izleyin:

1. **Gereksinimler**:
   - [.NET 7 SDK](https://dotnet.microsoft.com/download)
   - [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
   - [Postman](https://www.postman.com/) (isteğe bağlı)

2. **Proje Klonlama**:
   ```bash
   git clone https://github.com/AmmarAlp/CompletdProjects.git
