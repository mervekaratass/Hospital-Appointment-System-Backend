# Hospital Appointment System📝
Bu proje Tobeto ile gerçekleştirilen .NET & Angular Full Stack eğitiminin bitirme projesidir. 

🛠 Gereksinimler: 
- [x] Web projesi için: Asp.NET & Angular
- [x] Veri tabanı işlemleri için: MsSQL Server / PostgreSQL
- [x] Test işlemleri için: Postman,swagger vs.

📫 Nasıl bir proje oluşturduk?
<p>Bu proje, hastaların randevu almasını, geçmiş ve gelecek randevularını takip etmelerini ve doktorlar ile kolayca iletişim kurmalarını sağlayan, kullanıcı dostu bir hastane randevu sistemidir. </p>

<p> Üç tip kullanıcı bulunmaktadır: </p>

➡️ 1- Admin/Yönetici 

- [x] Hasta bilgilerini listeler,düzenler ve siler. Gerektiğinde yeni hasta ekleyebilir.
- [X] Hastaneye doktor ataması yapar. Doktor bilgilerini listeler, günceller ve siler. 
- [x] Mevcut branşları listeler,düzenler ve siler. Hastaneye branş eklemesi yapar.
- [x] Geçmiş ve gelecek tüm randevu detaylarını listeler. Yeni randevu oluşturabilir.
- [x] Yazılmış raporlar detaylarını (rapor içeriği hariç) görüntüleyebilir.
- [x] Kullanıcılar tarafından oluşturulan tüm öneri & şikayet geri bildirimlerini listeler. Geri bildirimi onaylama ve silme hakkına sahiptir.
- [x] Toplam randevu sayısı, toplam doktor sayısı ve toplam branş sayısı gibi metrikleri gösteren İstatistikleri görüntüleyebilir.
- [x] Kendi bilgilerini güncelleyebilir ve şifre değişikliği yapabilir.

➡️ 2- Doktor 
- [x] Bugünkü Randevularım - Yarınki Randevularım - Çalışma Takvimim - Hasta Raporları alanlarını içeren Özet sayfasını görüntüleyebilir.
- [x] Kendi çalışma takvimini oluşturabilir. Gerektiğinde çalışma takvimini güncelleyebilir veya silebilir.
- [x] Randevulu hastalarının bilgilerini görüntüleyebilir. 
- [x] Hastaların kendisinden aldığı randevuları Geçmiş Randevular & Gelecek Randevular alanında görüntüleyebilir.
- [x] Geçmiş randevular için rapor oluşturabilir.
- [x] Oluşturduğu raporların detaylarını görüntüleyebilir.
- [x] Öneri & Şikayet için oluşturduğu geri bildirimleri görüntüleyebilir ve silebilir. 
- [x] Kendi bilgilerini güncelleyebilir.

➡️ 2- Hasta 
- [x] Bugünkü Randevularım - Gelecek Randevularım - Raporlarım - Geri Bildirimlerim alanlarını içeren Özet sayfasını görüntüleyebilir.
- [x] İstediği branş ve doktora randevu alabilir. Gerektiğinde iptal edebilir.
- [x] Aldığı randevuları Geçmiş Randevular & Gelecek Randevular alanında görüntüleyebilir.
- [x] Doktorun oluşturduğu raporların detaylarını görüntüleyebilir.
- [x] Öneri & Şikayet için oluşturduğu geri bildirimleri görüntüleyebilir ve silebilir. 
- [x] Kendi bilgilerini güncelleyebilir.


## PROJE DETAYLARI📝

✎Öncelikle projemiz bir .Net ve Angular teknolojilerini içeren bir web projesidir .Projede veritabanı olarak MsSQL, dökümantasyon olarak Swagger kullanılmıştır. Ayrıca projemizde Narchgen mimarisi kullanılarak daha yönetilebilir bir sistem oluşturulmuştur. 

🎯Projede veri tabanı bağlantı yolunu appsetting.development.json içinde yazılmıştır. Bunu yaparak uygulama içerisine bağlantı kodlarımızı yazmak yerine daha genel bir yerde kolay bir şekilde yönetilmesini sağlanmıştır. Böylece bir havuzdaki musluklar gibi hangisini istenilirse o musluktan verilerin çekilmesi sağlanmıştır.

```c#
  "AllowedHosts": "*",
  "ConnectionStrings": {  
   "BaseDb": "Server=DESKTOP-Q270QVE\\SQLEXPRESS;Database=Hospital;Trusted_Connection=True;Trust Server Certificate=True;"    
  }
```

🔒 Projemizin katmanları aşağıda gösterilmiştir:

![image](https://github.com/user-attachments/assets/46eb0125-6318-4c26-be4f-a2b17c9f318f)

-----------------------------------------------------------------------
### 🌱DOMAIN KATMANI

✎ Entityler Domain katmanında oluşturulmuştur. Aşağıda örnek olarak Branch Entity dosyasını görebilirsiniz. Her class için gereksiz kod tekrarını önlemek adına her class Entity sınıfından miras alır. Diğer entityleri projenin içerisinde inceleyebilirsiniz.

Oluşturulan Entityler

- ⚡Appointment, randevu bilgilerini tutar.
- ⚡Branch, branş bilgilerini tutar.
- ⚡Doctor, doktor bilgilerini tutar.
- ⚡DoctorSchedule, doktor çalışma takvimi bilgilerini tutar.
- ⚡EmailAuthenticator, mail doğrulama bilgilerini tutar.
- ⚡Feedback, geri bildirim bilgilerini tutar.
- ⚡Manager, yönetici bilgilerini tutar.
- ⚡OperationClaim, rol bilgilerini tutar.
- ⚡OtpAuthenticator, SMS yollama bilgilerini tutar.
- ⚡Patient, hasta bilgilerini tutar.
- ⚡RefreshToken, token bilgilerini tutar.
- ⚡Report, rapor bilgilerini tutar.
- ⚡User, kullanıcı bilgilerini tutar.
- ⚡UserOperationClaim, kullanıcı rol bilgilerini tutar.
```c#

public class Branch : Entity<int>
{
    public Branch()
    {
    }

    public Branch(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Name { get; set; }
    public virtual ICollection<Doctor> Doctors { get; set; }
}
```

### 🌱PERSISTENCE KATMANI

✎ Oluşturulan Entity sınıflarını veri tabanında gösterebilmek için BaseDbContext sınıfı oluşturulmuştur.Sınıfımız Narchgen tarafından sağlanan DbContext sınıfından kalıtım alarak veritabanında modellerimize karşılık gelecek olan tabloların oluşmasını sağlar.

```c#
public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<EmailAuthenticator> EmailAuthenticators { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<OtpAuthenticator> OtpAuthenticators { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Report> Reports { get; set; }

    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<DoctorSchedule> DoctorSchedules { get; set; }

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration)
        : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
```

🖋 Code First yaklaşımı ile kullanılan veri tabanı modellerini(entity) ve ilişkilerinin yapılandırılmasını sağlamak için bir yol olan Fluent Api ile modellerin konfigürasyonlarını gerçekleştirilmiştir. Örnek olarak yukarıda verilen Branch sınıfın konfigürasyon kodları gösterilmiştir.

```c#
public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branches");

        builder.Property(d => d.Id).HasColumnName("Id").IsRequired();
        builder.Property(d => d.Name).HasColumnName("Name").IsRequired();
        builder.Property(d => d.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(d => d.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(d => d.DeletedDate).HasColumnName("DeletedDate");
    }
}
```

İşlemler tamamlandıktan sonra migration işlemi yapılarak modeller veri tabanına yansıtılmıştır.
📊 Veri tabanında tablolarımı oluşturuyorum. Aşağıda oluşturulan tabloların diyagramı gösterilmiştir.

![image](https://github.com/user-attachments/assets/badeff0b-32eb-43cb-80a5-01fb2e9aba7e)
-----------------------------------------------------------------------
### 🌱APPLICATION KATMANI

🌕 Projemizde Fluent Validation ile requestler için kurallar oluşturulmuştur. Peki fluent validation nedir? Fluent Validation bir veri doğrulama kütüphanesidir. Fluent Validation ve benzeri ürünlerin kullanılması, verilerin doğru şekilde yani verilerin oluştururken konulmuş kısıtlamaları sağlayarak kurallara uyumlu halde olmasını ve kullanıcı ya da sistem kaynaklı hataların oluşmasını engeller.

📃 Bunun için Application katmanına Fluent Validation için gerekli kütüphane indirilip kurallar oluşturulmuştur.

```c#
public class CreateBranchCommandValidator : AbstractValidator<CreateBranchCommand>
{
    public CreateBranchCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("İsim alanı boş olamaz.");
        RuleFor(c => c.Name).MinimumLength(5).WithMessage("İsim alanı minimum 5 karakter olmalı.");
    }
}
```
🔎 Böylece daha Controller tarafında istek atılmadan requestlerin istenilen kurallara uygun olup olmadığı kontrol edilir.

-----------------------------------------------------------------------
### 🌱WEBAPI KATMANI

⚓ Bu katmanda işlemlerin gerçekleştirildiği Controller sınıfları oluşturulur. Aşağıda BranchController dosyasının kodları örnek olarak gösterilmiştir.

```c#
[Route("api/[controller]")]
[ApiController]
public class BranchesController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedBranchResponse>> Add([FromBody] CreateBranchCommand command)
    {        
        CreatedBranchResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);        
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedBranchResponse>> Update([FromBody] UpdateBranchCommand command)
    {       
        UpdatedBranchResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedBranchResponse>> Delete([FromRoute] int id , [FromQuery] PageRequest  pageRequest)
    {        
        DeleteBranchCommand command = new() { Id = id };

        DeletedBranchResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdBranchResponse>> GetById([FromRoute] int id)
    {
        GetByIdBranchQuery query = new() { Id = id };

        GetByIdBranchResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListBranchQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBranchQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListBranchListItemDto> response = await Mediator.Send(query);


        return Ok(response);
    }


    [HttpGet("GetByName/{name}")]
    public async Task<ActionResult<GetByNameBranchResponse>> GetByName([FromRoute] string name)
    {
        GetByNameBranchQuery query = new() { Name = name };

        GetByNameBranchResponse response = await Mediator.Send(query);

        return Ok(response);
    }
}
   //diğer metotlara proje kodlarından ulaşabilirsiniz.
```

Projede 14 adet Controller sınıfı bulunmaktadır.Bunlardan bazıları Narchgen mimarisi ile hazır gelen Controller sınıfları olup aşağıda gösterilmiştir.

- ⚡ AuthController, yetkilendirme işlemlerinin gerçekleştirildiği sınıftır.
- ⚡ BaseController, diğer Controller sınıflarının miras aldığı Base yapı amacıyla kullanılır.
- ⚡ OperationClaimsController, rollerinin ayarlandığı sınıftır.
- ⚡ SmsSimulationController, SMS yollama işlemlerinin gerçekleştirildiği sınıftır.
- ⚡ UsersController, kullanıcı işlemlerinin gerçekleştirildiği sınıftır.
- ⚡ UserOperationClaimsController, kullanıcı rollerinin ayarlandığı sınıftır.
  
Proje isterlerine göre eklenen Controller sınıfları ise şunlardır;

- ⚡ AppointmentsController, randevu işlemlerinin gerçekleştirildiği sınıftır.
- ⚡ BranchesController, branş işlemlerinin gerçekleştirildiği sınıftır.
- ⚡ DoctorSchedulesController, doktor çalışma takvimi işlemlerinin gerçekleştirildiği sınıftır.
- ⚡ DoctorsController, doktor işlemlerinin gerçekleştirildiği sınıftır.
- ⚡ FeedbacksController, geri bildirim işlemlerinin gerçekleştirildiği sınıftır.
- ⚡ ManagersController, yönetici işlemlerinin gerçekleştirildiği sınıftır.
- ⚡ PatientsController, hasta işlemlerinin gerçekleştirildiği sınıftır.
- ⚡ ReportsController, rapor işlemlerinin gerçekleştirildiği sınıftır.

-----------------------------------------------------------------------
Anlatacaklarım bu kadar. Umarım açık olmuştur. 🧕🏻 Görüşürüz 🎉

## Badges

Add badges from somewhere like: [shields.io](https://shields.io/)

[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)
[![GPLv3 License](https://img.shields.io/badge/License-GPL%20v3-yellow.svg)](https://opensource.org/licenses/)
[![AGPL License](https://img.shields.io/badge/license-AGPL-blue.svg)](http://www.gnu.org/licenses/agpl-3.0)
