# Kütüphane Yönetim Sistemi API'si

Bu proje, bir kütüphane yönetim sistemi için geliştirilmiş **ASP.NET Core Web API** uygulamasıdır. Kitaplar, yazarlar, kategoriler ve diller gibi çeşitli kütüphane varlıklarını yönetmeyi sağlar. JWT tabanlı kimlik doğrulama ve yetkilendirme kullanılmaktadır.

## Özellikler

### Kitap Modeli (Book)
Kitap modeli, kütüphane yönetim sisteminde bir kitabı temsil eder ve aşağıdaki özelliklere sahiptir:

- **Id**: Kitabın benzersiz kimliği.
- **ISBN**: Kitabın uluslararası standart kitap numarası (ISBN), 10-13 karakter uzunluğunda.
- **Başlık (Title)**: Kitabın adı, maksimum 2000 karakter.
- **Sayfa Sayısı (PageCount)**: Kitabın sayfa sayısı, 1 ile kısa max değer arasında bir aralıkta.
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

### Kullanılan Teknolojiler

- **ASP.NET Core**: .NET Core platformu üzerinde Web API geliştirmek için kullanıldı.
- **Entity Framework Core**: Veritabanı yönetimi için kullanıldı. Code-first yaklaşımı ile modeller ve ilişkiler tanımlandı.
- **Identity Framework**: Kullanıcı kimlik doğrulama ve yetkilendirme işlemleri için kullanıldı.
- **JWT Authentication**: API için token tabanlı kimlik doğrulama sağlamak amacıyla kullanıldı.
- **SQL Server**: Veritabanı olarak kullanıldı.
- **Swagger**: API dökümantasyonu ve test arayüzü sağlamak için kullanıldı.
  
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

## Kurulum

Projeyi kendi makinenizde çalıştırmak için şu adımları izleyin:

1. **Gereksinimler**:
   - [.NET 7 SDK](https://dotnet.microsoft.com/download)
   - [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
   - [Postman](https://www.postman.com/) (isteğe bağlı)

2. **Proje Klonlama**:
   ```bash
   git clone https://github.com/AmmarAlp/CompletdProjects.git 
   gh repo clone AmmarAlp/CompletdProjects
