using Microsoft.EntityFrameworkCore;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

Console.WriteLine("hello");

#region Veri Ekleme

#region Veri Nasıl Eklenir?

//ETicaretContext context = new();
//Urun urun = new()
//{
//    UrunAdi = "A Ürünü",
//    Fiyat = 1000
//};

#region context.AddAsync Fonksiyonu
//await context.AddAsync(urun);
#endregion
#region context.DbSet.AddAsync Fonksiyonu
//await context.Urunler.AddAsync(urun);
#endregion

//await context.SaveChangesAsync();

#endregion

#region SaveChanges Nedir?
//Insert, update ve delete sorgularını oluşturup bir transaction eşliğinde veritabanına gönderip execute eden fonksiyondur. Eğer ki oluşturulan sorgulardan herhangi biri başarısız olursa tüm işlemleri geri alır.
#endregion

#region EF Core Açısından Bir Verinin Eklenmesi Gerektiği Nasıl Anlaşılıyor?

//ETicaretContext context = new();
//Urun urun = new()
//{
//    UrunAdi = "B ürünü",
//    Fiyat = 200
//};

//Console.WriteLine(context.Entry(urun).State);
//await context.AddAsync(urun);
//Console.WriteLine(context.Entry(urun).State);
//await context.SaveChangesAsync();
//Console.WriteLine(context.Entry(urun).State);

#endregion

#region Birden Fazla Veri Eklerken Nelere Dikkat Edilmelidir?
#region SaveChanges'ı Verimli Kullanalım
//SaveChanges fonksiyonu her tetiklendiğinde bir transaction oluşturacağından dolayı EF Core ile yapılan her bir işleme özel kullanmaktan kaçınmalıyız! Çünkü her işleme özel transaction veritabanı açısından ekstradan maliyet demektir. O yüzden mümkün mertebe tüm işlemlerimizi tek bir transaction eşliğinde veritabanına gönderebilmek için SaveChanges'ı aşağıdaki gibi tek seferde kullanmak hem maliyet hem de yönetilebilirlik açısından katkıda bulunmuş olacaktır.

//ETicaretContext context = new();
//Urun urun1 = new()
//{
//    UrunAdi = "C ürünü",
//    Fiyat = 200
//};

//Urun urun2 = new()
//{
//    UrunAdi = "D ürünü",
//    Fiyat = 200
//};

//Urun urun3 = new()
//{
//    UrunAdi = "E ürünü",
//    Fiyat = 200
//};

//await context.AddAsync(urun1);
//await context.AddAsync(urun2);
//await context.AddAsync(urun3);
//await context.SaveChangesAsync();

#endregion
#region AddRange

//ETicaretContext context = new();
//Urun urun1 = new()
//{
//    UrunAdi = "C ürünü",
//    Fiyat = 200
//};

//Urun urun2 = new()
//{
//    UrunAdi = "D ürünü",
//    Fiyat = 200
//};

//Urun urun3 = new()
//{
//    UrunAdi = "E ürünü",
//    Fiyat = 200
//};

//await context.Urunler.AddRangeAsync(urun1, urun2, urun3);
//await context.SaveChangesAsync();

#endregion
#endregion

#region Eklenen Verinin Generate Edilen Id'sini Elde Etme

//ETicaretContext context = new();
//Urun urun = new()
//{
//    UrunAdi = "O ürünü",
//    Fiyat = 200
//};
//await context.AddAsync(urun);
//await context.SaveChangesAsync();
//Console.WriteLine(urun.Id);

#endregion

#endregion

#region Veri Güncelleme

#region Veri Nasıl Güncellenir?

//ETicaretContext context = new();
//Urun urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 3);
//urun.UrunAdi = "H Ürünü";
//urun.Fiyat = 999;
//await context.SaveChangesAsync();

#endregion

#region ChangeTracker Nedir? Kısaca!
//context üzerinden gelen verilerin takibinden sorumlu bir mekanizdmadır. Bu takip mekanizması sayesinde context üzerinden gelen verilerle ilgili işlemler neticesinde update yahut delete sorgularının oluşturulacağı anlaşılır.
#endregion

#region Takip Edilemeyen Nesneler Nasıl Güncellenir?
//ChangeTracker mekanizması tarafından takip edilemeyen nesnelerin güncellenebilmesi için Update fonksiyonu kullanılır!
//Update fonksiyonunu kullanabilmek için kesinlikle ilgili nesnede Id değeri verilmelidir!

//ETicaretContext context = new();
//Urun urun = new()
//{
//    Id = 3,
//    UrunAdi = "Yeni Ürün",
//    Fiyat = 123
//};
//context.Urunler.Update(urun);
//await context.SaveChangesAsync();

#endregion

#region EntityState Nedir?
//Bir entity instance'ının durumunu ifade eden bir referanstır.

//ETicaretContext context = new();
//Urun urun = new();
//Console.WriteLine(context.Entry(urun).State);

#endregion

#region EF Core Açısından Bir Verinin Güncellenmesi Gerektiği Nasıl Anlaşılıyor?

//ETicaretContext context = new();
//Urun urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 3);
//Console.WriteLine(context.Entry(urun).State);

//urun.UrunAdi = "Hilmi";
//Console.WriteLine(context.Entry(urun).State);

//await context.SaveChangesAsync();
//Console.WriteLine(context.Entry(urun).State);

#endregion

#region Birden Fazla Veri Güncellenirken Nelere Dikkat Edilmelidir?

//ETicaretContext context = new();
//var urunler = await context.Urunler.ToListAsync();

//foreach (var urun in urunler)
//{
//    urun.UrunAdi += "*";
//}

//await context.SaveChangesAsync();

#endregion

#endregion

#region Veri Silme

#region Veri Nasıl Silinir?

//ETicaretContext context = new();
//Urun urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 4);
//context.Urunler.Remove(urun);
//await context.SaveChangesAsync();

#endregion

#region Silme İşleminde ChangeTracker'ın Rolü
//context üzerinden gelen verilerin takibinden sorumlu bir mekanizdmadır. Bu takip mekanizması sayesinde context üzerinden gelen verilerle ilgili işlemler neticesinde update yahut delete sorgularının oluşturulacağı anlaşılır.
#endregion

#region Takip Edilemeyen Nesneler Nasıl Silinir?

//ETicaretContext context = new();
//Urun urun = new()
//{
//    Id = 3
//};

//context.Urunler.Remove(urun);
//await context.SaveChangesAsync();

#region EntityState ile Silme İşlemi

//ETicaretContext context = new();
//Urun urun = new()
//{
//    Id = 2
//};
//context.Entry(urun).State = EntityState.Deleted;
//await context.SaveChangesAsync();

#endregion

#endregion

#region Birden Fazla Veri Silinirken Nelere Dikkat Edilmelidir?
#region SaveChanges'ı Verimli Kullanalım



#endregion
#region RemoveRange

//ETicaretContext context = new();
//List<Urun> urunler = await context.Urunler.Where(u => u.Id >= 6 && u.Id <= 8).ToListAsync();

//context.Urunler.RemoveRange(urunler);
//await context.SaveChangesAsync();

#endregion
#endregion

#endregion

#region Temel Sorgulama
//ETicaretContext context = new();

#region En Temel Basit Bir Sorgulama Nasıl Yapılır?
#region Method Syntax

//var urunler = await context.Urunler.ToListAsync();

#endregion
#region Query Syntax

//var urunler2 = await (from urun in context.Urunler
//                      select urun).ToListAsync();

#endregion
#endregion

#region Sorguyu Execute Etmek İçin Ne Yapmamız Gerekmektedir?
#region ToListAsync
#region Method Syntax

//var urunler = await context.Urunler.ToListAsync();

#endregion
#region Query Syntax

//var urunler = await (from urun in context.Urunler
//                     select urun).ToListAsync();

#endregion
#endregion

#region Foreach

//foreach (Urun urun in urunler)
//{
//    Console.WriteLine(urun.UrunAdi);
//}

#region Deferred Execution (Ertelenmiş Çalışma)
//IQueryable çalışmalarında ilgili kod yazıldığı noktada tetiklenmez/çalıştırılmaz. Yani ilgili kod yazıldığı noktada sorguyu generate etmez! Nerede eder? Çalıştırıldığı/Execute edildiği noktada tetiklenir. Bu duruma ertelenmiş çalışma denir.
#endregion
#endregion

#region IQueryable ve IEnumerable Nedir? Basit Olarak!

//int urunId = 5;
//string urunAdi = "2";

//var urunler = from urun in context.Urunler
//              where urun.Id > urunId && urun.UrunAdi.Contains(urunAdi)
//              select urun;

//urunId = 200;
//urunAdi = "4";

//foreach (Urun urun in urunler)
//{
//    Console.WriteLine(urun.UrunAdi);
//}

//await urunler.ToListAsync();

#region IQueryable
//Sorguya karşılık gelir
//EF Core üzerinden yapılmış olan sorgunun execute edilmemiş halini ifade eder.
#endregion
#region IEnumerable
//Sorgunun çalıştırılıp verilerin in memorye yüklenmiş halini ifade eder.
#endregion
#endregion

#region Çoğul Veri Getiren Sorgulama Fonksiyonları

#region ToListAsync
//Üretilen sorguyu execute ettirmemizi sağlayan fonksiyondur.

#region Method Syntax

//var urunler = await context.Urunler.ToListAsync();

#endregion
#region Query Syntax

//var urunler = (from urun in context.Urunler
//               select urun);
//var datas = await urunler.ToListAsync();

#endregion
#endregion

#region Where
//Oluşturulan sorguya where şartı eklememizi sağlayan bir fonksiyondur.

#region Method Syntax
//var urunler = await context.Urunler.Where(u => u.Id > 500).ToListAsync();
//var urunler2 = await context.Urunler.Where(u => u.UrunAdi.StartsWith("a")).ToListAsync();
#endregion
#region Query Syntax
//var urunler = from urun in context.Urunler
//              where urun.Id > 500 && urun.UrunAdi.EndsWith("7")
//              select urun;
//var data = await urunler.ToListAsync();
#endregion
#endregion

#region OrderBy
//Sorgu üzerinde sıralama yapmamızı sağlayan bir fonksiyondur. (Ascending)

#region Method Syntax
//var urunler = context.Urunler.Where(u => u.Id > 500 || u.UrunAdi.EndsWith("2")).OrderBy(u => u.UrunAdi);
#endregion
#region Query Syntax
//var urunler2 = from urun in context.Urunler
//               where urun.Id > 500 || urun.UrunAdi.StartsWith("2")
//               orderby urun.UrunAdi
//               select urun;
#endregion
//await urunler.ToListAsync();
//await urunler2.ToListAsync();
#endregion

#region ThenBy
//OrderBy üzerinde yapılan sıralama işlemini farklı kolonlara da uygulamamızı sağlayan fonksiyondur. (Ascending)

//var urunler = context.Urunler.Where(u => u.Id > 500 || u.UrunAdi.EndsWith("2")).OrderBy(u => u.UrunAdi).ThenBy(u => u.Fiyat).ThenBy(u => u.Id);

//await urunler.ToListAsync();
#endregion

#region OrderByDescending
//Descending olarak sıralama yapmamızı sağlayan bir fonksiyondur

#region Method Syntax
//var urunler = await context.Urunler.OrderByDescending(u => u.Fiyat).ToListAsync();
#endregion
#region Query Syntax
//var urunler = await (from urun in context.Urunler
//               orderby urun.Fiyat descending
//               select urun).ToListAsync();
#endregion

#endregion

#region ThenByDesceding
//OrderByDescending üzerinde yapılan sıralama işlemini farklı kolonlara da uygulamamızı sağlayan fonksiyondur. (Ascending)

//var urunler = await context.Urunler.OrderByDescending(u => u.Id).ThenByDescending(u => u.Fiyat).ThenBy(u => u.UrunAdi).ToListAsync();
#endregion

#endregion

#region Tekil Veri Getiren Sorgulama Fonksiyonları

//Yapılan sorguda sade ve sadece tek bir verinin gelmesi amaçlanıyorsa Single ya da SingleOrDefault fonksiyonu kullanılabilir.
#region SingleAsync
//Eğer ki, sorgu neticesinde birden fazla veri geliyorsa ya da hiç gelmiyorsa her iki durumda da exception fırlatır.
#region Tek Kayıt Geldiğinde
//var urun = await context.Urunler.SingleAsync(u => u.Id == 5);
#endregion
#region Hiç Kayıt Gelmediğinde
//var urun = await context.Urunler.SingleAsync(u => u.Id == 5555);
#endregion
#region Çok Kayıt Geldiğinde
//var urun = await context.Urunler.SingleAsync(u => u.Id > 5);
#endregion
#endregion

#region SingleOrDefaultAsync
//Eğer ki, sorgu neticesinde birden fazla veri geliyorsa exception fırlatır, hiç veri gelmiyorsa null döner.
#region Tek Kayıt Geldiğinde
//var urun = await context.Urunler.SingleOrDefaultAsync(u => u.Id == 5);
#endregion
#region Hiç Kayıt Gelmediğinde
//var urun = await context.Urunler.SingleOrDefaultAsync(u => u.Id == 5555);
#endregion
#region Çok Kayıt Geldiğinde
//var urun = await context.Urunler.SingleOrDefaultAsync(u => u.Id > 5);
#endregion
#endregion

//Yapılan sorguda tek bir verinin gelmesi amaçlanıyorsa First ya da FirstOrDefault fonksiyonu kullanılabilir
#region FirstAsync
//Soru neticesinde elde edilen verilerden ilkini getirir. Eğer ki hiç veri gelmiyorsa hata fırlatır.
#region Tek Kayıt Geldiğinde
//var urun = await context.Urunler.FirstAsync(u => u.Id == 5);
#endregion
#region Hiç Kayıt Gelmediğinde
//var urun = await context.Urunler.FirstAsync(u => u.Id == 5555);
#endregion
#region Çok Kayıt Geldiğinde
//var urun = await context.Urunler.FirstAsync(u => u.Id > 5);
#endregion
#endregion

#region FirstOrDefaultAsync
//Sorgu neticesinde elde edilen verilerden ilkini getirir. Eğer ki hiç veri gelmiyorsa null değer döndürür.

#region Tek Kayıt Geldiğinde
//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 5);
#endregion
#region Hiç Kayıt Gelmediğinde
//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 5555);
#endregion
#region Çok Kayıt Geldiğinde
//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id > 5);
#endregion
#endregion

#region SingleAsync, SingleOrDefaultAsync, FirstAsync, FirstOrDefaultAsync Fonkisyonlarının Karşılaştırması
#endregion

#region FindAsync
//Find fonksiyonu, primary key kolonuna özel gızlı bir şekilde sorgulama yapmamızı sağlayan bir fonksiyondur.

//Urun urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 5);
//Urun urun = await context.Urunler.FindAsync(5);

#region Composite Primary Key Durumu
//UrunParca up = await context.UrunParca.FindAsync(2, 5);
#endregion

#endregion

#region FindAsync ile SingleAsync, SingleOrDefaultAsync, FirstAsync, FirstOrDefaultAsync Fonkisyonlarının Karşılaştırması
#endregion

#region LastAsync
//First ile aynı sadece son veriyi alır
#endregion

#region LastOrDefaulAsync
//FirstOrDefault ile aynı sadece son veriyi alır
#endregion

#endregion

#region Diğer Sorgulama Fonksiyonları

#region CountAsync
//Oluşturulan sorgunun execute edilmesi neticesinde kaç adet satırın elde edileceğini sayısal olarak (int) bizlere bildiren fonksiyondur.

//var urunler = (await context.Urunler.ToListAsync()).Count();
//var urunler = await context.Urunler.CountAsync();
#endregion

#region LongCountAsync
//Oluşturulan sorgunun execute edilmesi neticesinde kaç adet satırın elde edileceğini sayısal olarak (long) bizlere bildiren fonksiyondur.

//var urunler = await context.Urunler.LongCountAsync(u => u.Fiyat > 150);
#endregion

#region AnyAsync
//Sorgu neticesinde verinin gelip gelmediğini bool türünde dönen fonksiyondur.

//var urunler = await context.Urunler.Where(u => u.UrunAdi.Contains("1")).AnyAsync();
//var urunler = await context.Urunler.AnyAsync(u => u.UrunAdi.Contains("1"));
#endregion

#region MaxAsync
//Verilen kolondaki maksimum değeri getirir.

//var fiyat = await context.Urunler.MaxAsync(u => u.Fiyat);
#endregion

#region MinAsync
//Verilen kolondaki minimum değeri getirir

//var fiyat = await context.Urunler.MinAsync(u => u.Fiyat);
#endregion

#region Distinct
//Sorguda mükerrer kayıtlar varsa bunları tekilleştiren bir işleve sahiptir.

//var urunler = await context.Urunler.Distinct().ToListAsync();
#endregion

#region AllAsync
//Bir soru neticesinde gelen verilerin verilen şarta uyup uymadığını kontrol etmektedir. Eğer ki tüm veriler şarta uyuyorsa true, uymuyorsa false dönecektir.

//var m = await context.Urunler.AllAsync(u => u.Fiyat > 150);
#endregion

#region SumAsync
//Toplam fonksiyonudur

//var fiyatToplama = await context.Urunler.SumAsync(u => u.Fiyat);
#endregion

#region AverageAsync
//Vermiş olduğumuz sayısal property'nin aritmetik ortalamasını alır.

//var aritmetikOrtalama = await context.Urunler.AverageAsync(u => u.Fiyat);
#endregion

#region ContainsAsync
//Like ''%...%' sorgusu oluşturmamızı sağlar

//var urunler = await context.Urunler.Where(u => u.UrunAdi.Contains("7")).ToListAsync();
#endregion

#region StartsWith
//Like '...%' sorgusu oluşturmamızı sağlar

//var urunler = await context.Urunler.Where(u => u.UrunAdi.StartsWith("7")).ToListAsync();
#endregion

#region EndsWith
//Like '%...' sorgusu oluşturmamızı sağlar

//var urunler = await context.Urunler.Where(u => u.UrunAdi.EndsWith("7")).ToListAsync();
#endregion


#endregion

#region Sorgu Sonucu Dönüşüm Fonksiyonları
//Bu fonksiyonlar ile sorgu neticesinde elde edilen verileri isteğimiz doğrultusunda farklı türlerde projecsiyon edebiliyoruz.

#region ToDictionaryAsync
//Sorgu neticesinde gelecek olan veriyi bir dictionary olarak elde etmek/tutmak/karşılamak istiyorsak kullanılır.

//var urunler = await context.Urunler.ToDictionaryAsync(u => u.UrunAdi, u => u.Fiyat);

//ToList ile aynı amaca hizmet etmektedir. Yani, oluşturulan sorguyu execute edip neticesini alırlar.
//ToList: Gelen sorgu neticesinde entity türünden bir koleksiyona(List<TEntity>) dönüştürmekteyken,
//ToDictionary ise: Gelen sorgu neticesini Dictionary türünden bir koleksiyona dönüştürecektir.
#endregion

#region ToArrayAsync
//Oluşturulan sorguyu dizi olarak elde eder.
//ToList ile muadil amaca hizmet eder. Yani sorguyu execute eder lakin gelen sonucu entity dizisi olarak elde eder.

//var urunler = await context.Urunler.ToArrayAsync();
#endregion

#region Select
//Select fonksiyonunun işlevsel olarak birden fazla davranışı söz konusudur.
//1. Select fonksiyonu, genarete edilecek sorgunun çekilecek kolonlarını ayarlamamızı sağlar
//var urunler = await context.Urunler.Select(u => new Urun
//{
//    Id = u.Id,
//    Fiyat = u.Fiyat,
//}).ToListAsync();

//2. Select fonksiyonu, gelen verileri farklı türlerde karşılamamızı sağar. T, anonim
//var urunler = await context.Urunler.Select(u => new
//{
//    Id = u.Id,
//    Fiyat = u.Fiyat,
//}).ToListAsync();

//var urunler = await context.Urunler.Select(u => new UrunDetay
//{
//    Id = u.Id,
//    Fiyat = u.Fiyat,
//}).ToListAsync();

#endregion

#region SelectMany
//Select ile aynı amaca hizmet eder. Lakin, ilişkisel tablolar neticesinde gelen koleksiyonel verileri de tekilleştirip projeksiyon etmemizi sağlar.

//var urunler = await context.Urunler.Include(u => u.Parcalar).SelectMany(u => u.Parcalar, (u,p) => new
//{
//    u.Id,
//    u.Fiyat,
//    p.ParcaAdi
//}).ToListAsync();

#endregion

#endregion

#region GroupBy Fonksiyonu
//Gruplama yapmamızı sağlayan fonksiyondur.
#region Method Syntax
//var datas = await context.Urunler.GroupBy(u => u.Fiyat).Select(group => new
//{
//    Count = group.Count(),
//    Fiyat = group.Key
//}).ToListAsync();
#endregion
#region Query Syntax
//var datas = await (from urun in context.Urunler
//             group urun by urun.Fiyat
//             into @group
//            select new
//            {
//                Fiyat = @group.Key,
//                Count = @group.Count(),
//            }).ToListAsync();
#endregion
#endregion

#region Foreach Fonksiyonu
//Bir sorgulama fonksiyonu felan değildir!
//Sorgulama neticesinde elde edilen koleksiyonel veriler üzerinde iterasyonel olarak dönmemizi ve teker teker verileri elde edip işlemler yapabilmemizi sağlayan bir fonksiyondur. foreach döngüsünün method halidir.

//foreach (var u in datas)
//{

//}

//datas.ForEach(x =>
//{

//});

#endregion

#endregion

#endregion

public class ETicaretContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }
    public DbSet<Parca> Parcalar { get; set; }
    public DbSet<UrunParca> UrunParca { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Provider
        //ConnectionString
        //Lazy Loading
        optionsBuilder.UseSqlServer("Server=localhost; Database=ETicaretDB; uid=sa; pwd=123; TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UrunParca>().HasKey(up => new { up.UrunId, up.ParcaId });
    }
}

public class Urun
{
    public int Id { get; set; }
    //public int ID { get; set; }
    //public int UrunId { get; set; }
    //public int UrunID { get; set; }
    public string UrunAdi { get; set; }
    public int Fiyat { get; set; }

    public ICollection<Parca> Parcalar { get; set; }
}

public class Parca
{
    public int Id { get; set; }
    public string ParcaAdi { get; set; }
}

public class UrunParca
{
    public int UrunId { get; set; }
    public int ParcaId { get; set; }
    public Urun Urun { get; set; }
    public Parca Parca { get; set; }
}

public class UrunDetay
{
    public int Id { get; set; }
    public int Fiyat { get; set; }
}

#region OnConfiguration ile Konfigürasyon Ayarlarını Gerçekleştirmek
//EF Core tool'unu yapılandırmak için kullandığımız bir methottur.
//Context nesnesinde override edierek kullanılmaktadır.
#endregion
#region Basit Düzeyde Entity Tanımlama Kuralları
//EF Core, her tablonun default olarak bir primary key kolonu olması gerektiğini kabul eder
//Haliyle, bu kolonu temsil eden bir property tanımlamadığımız takdirde hata verecektir!
#endregion
#region Tablo Adını Belirleme

#endregion