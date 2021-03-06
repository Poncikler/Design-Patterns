# SINGLETON DESIGN PATTERN 
 İsminden de anlaşılacağı üzere Singleton design pattern, bir Class’a ait tek bir nesne yaratmaya ve oluşturulan tek bir nesneyi tüm programda kullanmamızı sağlar. Bu işlem süresince bir daha aynı nesne oluşturulamaz ya da türetilemez.

Ayrıca singletonun design pattern, benzer yapıda işlemleri, her bir işlem için farklı obje kullanmak yerine aynı obje üzerinden kullanarak belleğin gereksiz birikimini azaltmak, performansı artırmak ve multi thread çalışan bir sistemde hatalı işlemlerin önüne geçmek için kullanılır.

### Singleton tasarım deseni hakkında oluşturulabilecek farkı örnekler

- Çekiliş yapmak; Bir hediye çekiliş yapılırken çekilişe katılacak her kişi için teker teker çekiliş sırasına girme metodu oluşturmak yerine tek obje üzerinden tüm kişiler sırasıyla çekilişe eklenebilir.
- Fatura kesmek: Toplu fatura kesilme işleminde değerlerin anlık takip edilebilmesi ve hatalı işlem yapılmaması için tek bir nesne oluşturulup bu nesne üzerinden her fatura teker teker kesilebilir.
- Bankada işlem sırası alma: Aynı anda gerçekleşebilecek işlemler için aynı veri dönebilir. Bu da sıralamada hatalara yol açabilir. Bu tarz durumlar için yalnız bir nesne üzerinden işlemlerin yapılması gerekir.
- Hesap makinesi ve sorular: Farz edelim ki önümüzde 20 farklı problem var ve bu problemler için hesap makinesi kullanmamız gerekiyor. Bu durumda her bir soru için farklı hesap makinesi kullanmamıza gerek var mıdır? Öyle bir kişi varsa bul beni yiğidim. Mantıklı düşünecek olursak, gerek yoktur. Her bir sorunun çözümü için aynı hesap makinesini kullanabiliriz. İşte singleton yapısı tam da bu tür problemlerde imdadımıza yetişir.
- Biraz daha nerd arkadaşlar için örnek vermek gerekirse: Farz edelim ki bir otomasyon yazıyorsunuz ve çeşitli yerlerde veritabanı işlemleri   gerçekleştiriyorsunuz. C# ve SQL özelinde örnek vermek gerekirse; her insert/update/delete işlemleri için bağlantı açıp kapattığınız bir Connect nesneniz olduğunu düşünün. Eğer farklı classlar içerisinde sürekli bu nesneyi tekrar tekrar oluşturup kullanıyorsanız, sizi seviyorum sevgili canlarım. Bundan sonra böyle yapmayın olur mu? Bu bağlantı nesnesini bir singleton class yapısına dönüştürün ve öyle kullanın. 

#### Singleton design pattern araştırdıkça karşımıza farklı implementasyon yöntemleri çıkmaktadır.

- Thread kontrolü olmayan en temel Singleton yöntemi
- Multi Thread kontrolü olan Singleton yöntemi

#### Ayrıca nesne oluşturma yöntemine bağlı olarak iki farklı yönteme ayrılmaktadır
- Lazy Singleton
- Eager Singleton

Bu tipleri bir veritabanı örneğinde inceleyelim. Bir adet Database Class’ımız olsun ve biz bu class’tan sadece bir tane bağlantı nesnesi oluşturulmasını istiyoruz. İşlemlerimizi yaptığımız ana class’ta ise iki farklı işlem olsun ve bu iki işlem de Database Class’ındaki bağlantı nesnesine ihtiyaç duysun. Bizim amacımız farklı senaryolara sahip olan bu örneğe Singleton tasarım desenini bu senaryolara uygun olacak şekilde implemente etmek olsun. Unutmayın, hedefimiz kaç tane işlem olursa olsun, bütün işlemlerin aynı nesneyi kullanması ve birden fazla nesne üretilmemesi olacak!

İlk olarak en ilkel diyebileceğimiz, neredeyse her kaynakta bulunabilen türü implemente edelim.

1. Ilk olarak bir Database Class’ı oluşturmamız gerekiyor.
```csharp
sealed class Database //sealed anahtar kelimesinin işlevini yazının sonundaki Keywords başlığında bulabilirsiniz
{

}
```
 

2. Sıradaki işlemimiz ise _instance adını vereceğimiz, programın çalışma süresi boyunca ne olursa olsun sadece bir tane üretilecek olan nesnemizi eklemek.

```csharp
sealed class Database //sealed anahtar kelimesinin işlevini yazının sonundaki Keywords başlığında bulabilirsiniz
{
    private static Database _instance = null; // null anahtar kelimesinin işlevini yazının sonundaki Keywords başlığında bulabilirsiniz
}
```

3. Sıradaki işlemimiz bu class’ın constructor’ını private yapmak. Bunu yapmamızın sebebi, programımızda global olarak new anahtar kelimesi ile bu class’tan yeni bir nesne üretilmesini engellemek.
```csharp
sealed class Database //sealed anahtar kelimesinin işlevini yazının sonundaki Keywords başlığında bulabilirsiniz
{
    private static Database _instance = null; // null anahtar kelimesinin işlevini yazının sonundaki Keywords başlığında bulabilirsiniz
    private Database() { Console.WriteLine("Instance created"); }
}
```

4. Sıradaki işlemimiz ise bu _instance nesnesinin null olup olmadığını ve sonuca göre işlem yapacağımız alanı kodlamak. Bu adım için iki farklı yol bulunmakta:
- Fonksiyon ile yazmak
- Property ile yazmak

Biz bu yazımızda Property ile yapmayı tercih ettik. Sebebi ise tamamen can sıkıntısı.//Property anahtar kelimesinin işlevini yazının sonundaki Keywords başlığında bulabilirsiniz

Şimdi property yöntemini kullanarak implementasyona devam edelim.
```csharp
sealed class Database //sealed anahtar kelimesinin işlevini yazının sonundaki Keywords başlığında bulabilirsiniz
{
    private static Database _instance = null; // null anahtar kelimesinin işlevini yazının sonundaki Keywords başlığında bulabilirsiniz
    private Database() { Console.WriteLine("Instance created"); }
    public static Database Instance
    {
        get
        {
            if (_instance == null) //_instance null mu ?
            {
                _instance = new Database(); // Eğer null ise yeni bir nesne oluştur.
            }
            return _instance; // _instance’yi geri döndür.
        }
    }
}
```

5. Son olarak control amaçlı bir fonksiyon daha oluşturalım.

```csharp
sealed class Database //sealed anahtar kelimesinin işlevini yazının sonundaki Keywords başlığında bulabilirsiniz
{
    private static Database _instance = null; // null anahtar kelimesinin işlevini yazının sonundaki Keywords başlığında bulabilirsiniz
    private Database() { Console.WriteLine("Instance created"); }
    public static Database Instance
    {
        get
        {
            if (_instance == null) //_instance null mu ?
            {
                 _instance = new Database(); // Eğer null ise yeni bir nesne oluştur.
            }
            return _instance; // _instance’yi geri döndür.
        }
    }
public void Connection(string Name)
{
    Console.WriteLine("{0} has connected", Name);
}
    

```
Şu an ilkel bir Singleton yapısı oluşturduk. Birazdan değineceğimiz farklı senaryolar dışında, şu an programın çalışma süresi boyunca bu class’tan sadece bir adet nesne oluşturulacaktır. 

6. Şimdi main fonksiyonumuzu yazalım ve örneğimizi tamamlayalım.
```csharp
class Program
{
    static void Main(string[] args)
    {
        Database db = Database.Instance;
        db.Connection("First Proccess");
        Database db2 = Database.Instance;
        db2.Connection("Second Proccess");
    }
}
```
Bu örnekte main içerisinde iki farklı işlem yapılıyor fakat kurduğumuz yapı sayesinde her işlem için yeni bir nesne oluşturmak yerine ilk işlem için sıfırdan bir nesne oluşturduktan sonra aynı nesneyi ikinci işlem için kullanıyoruz. Ekran çıktısı ise şu şekilde olacaktır.

![alt text](https://github.com/UlascanKilic/Design-Patterns/blob/main/Singleton/ss1.png)

7. Şimdi konuşmamız gereken bazı durumlar var. Bu implementasyon, single thread bir yapıda düzgün çalışacaktır. Peki ya multi threading bir yapı kullanılacaksa ne yapıcaz? Bu sefer sadece main içerisinde değişiklik yaparak bu oluşturduğumuz Singleton yapısını bir de multi thread ile kullanalım. Database class’ını aynı bırakıyorum ve main içerisine şu kodları ekliyorum:
```csharp
class Program
{
    static void Main(string[] args)
    {
        Parallel.Invoke(() => FirstProccess(), () => SecondProccess());//Parallel.Invoke() anahtar kelimesinin işlevini yazının sonundaki Keywords başlığında bulabilirsiniz
        Console.ReadLine();
    }

    static void FirstProccess()
    {
        Database db = Database.Instance;
        db.Connection("First Proccess");
    }
    static void SecondProccess()
    {
        Database db = Database.Instance;
        db.Connection("Second Proccess");
    }
}
```
Yukarıdaki kodda yaptığımız işlem basitçe bir multi threading oluşturmaktır. Multi thread bir işlem yaptığımızda, Database class’ına “T” zamanında “N” adet işlem yapılabilir. Kısacası birden çok iş parçacığının aynı anda farklı kesimlerde çalışabilmesi için bölümlenmesini sağlıyoruz.

Burada fark edilmesi gereken sorun ise şu; Aynı anda farklı kesimlerde çalıştırdığımız işlemler yine aynı anda Database class’ından nesne oluşturmaya çalışacaktır ve bizim yazdığımız ilkel Singleton kodu *bumm* diye patlayacaktır. Örnek ekran çıktısını aşağıda inceleyelim.

![alt text](https://github.com/UlascanKilic/Design-Patterns/blob/main/Singleton/ss2.png)


T zamanında iki adet parça Database Class’ına gidip bir nesne oluşturmaya çalıştı ve bizim akıllı Class’ımız ikisine de aynı anda yanıt verdiği için ikisi için de yeni bir nesne oluşturmuş oldu. Günlük bir diyalog üzerinden basit bir örnek vermek gerekirse:
X kişisi ve Y kişisi bir bakkal amcaya aynı anda soru sorsun.

> X: Bakkal amca bakkal amca kalemin var mı? | Y: Bakkal amca bakkal amca kalemin var mı? 
>
> Bakkal amca: Evet var, al bakalım.

Bu örnekte bakkal amca aynı anda iki kişiye de cevap verdiği için iki kişiye de farklı kalemler vermiş oluyor. Bizim istediğimiz ise bir adet kalem alıp onu önce X kişisine, sonra da aynı kalemi Y kişisine vermesi. Peki bu problemin önüne nasıl geçebiliriz ?

8.Burada Multi thread Singleton yapısı ile tanışıyoruz. Yapı olarak mimarimizde köklü bir değişiklik yapmayacağız. Class’ımıza sadece ufak bir kontrol daha ekleyeceğiz.
```csharp
sealed class Database //sealed anahtar kelimesinin işlevini yazının sonundaki Keywords başlığında bulabilirsiniz
{
    private static Database _instance = null; // null anahtar kelimesinin işlevini yazının sonundaki Keywords başlığında bulabilirsiniz
    private static readonly object threadSafety = new object(); //readonly anahtar kelimesinin işlevini yazının sonundaki Keywords başlığında bulabilirsiniz

    private Database() { Console.WriteLine("Instance created"); }
    public static Database Instance
    {
        get
        {
            lock (threadSafety) //lock anahtar kelimesinin işlevini yazının sonundaki Keywords başlığında bulabilirsiniz
            {
                if (_instance == null)
                {
                    _instance = new Database();
                }
                return _instance;
            }
        }
    }

    public void Connection(string Name)
    {
        Console.WriteLine("{0} has connected", Name);
    }
}

```

Burada kullandığımız lock anahtar kelimesi sayesinde blok içerisine aldığı kod satırları single thread olarak gerçekleşir ve bu sayede multi thread yapılardaki karmaşıklığı engellemiş oluruz.
Az önceki bakkal amcaya geri dönelim.

> X: Bakkal amca bakkal amca kalemin var mı? | Y: Bakkal amca bakkal amca kalemin var mı?
> 
> Bakkal amca: Çocuklar! Böyle olmaz! Sıraya girin hemen!
> 
> X: Bakkal amca bakkal amca kalemin var mı?
> 
> Bakkal amca: Al bakalım yavrum.
> 
> Y: Bakkal amca bakkal amca kalemin var mı?
> 
> Bakkal amca: Aynı kalemi sen de kullan evladım.

Bakkal amcamızın ekran çıktısına bakalım.

![alt text](https://github.com/UlascanKilic/Design-Patterns/blob/main/Singleton/ss3.png)


9. Son olarak değineceğimiz iki farklı konu kaldı. Lazy Singleton ve Eager Singleton.

<b>Lazy Singleton:</b> Üretmek istediğimiz nesne sadece ilgili class çağrıldığında üretilir.</br>
<b>Eager Singleton: </b> Program başlar başlamaz class içerisinde bir nesne üretilir.

Hangisini kullanacağınız sizin kendi senaryonuza göre değişiklik gösterecektir.
Bir nesneyi üretmenin maliyeti bizim için yüksekse ve bu nesne çok nadir kullanılacaksa Lazy Singleton yapısı kullanmamız daha da doğru olacaktır. Aksi durumlarda ise Eager Singleton yapısına başvurabiliriz. Bu iki yapıyı kısaca örnek kodlarla inceleyelim

#### Lazy Singleton
```csharp
sealed class Database //sealed anahtar kelimesinin işlevini yazının sonundaki Keywords başlığında bulabilirsiniz
{
    private static Database _instance = null; // null anahtar kelimesinin işlevini yazının sonundaki Keywords başlığında bulabilirsiniz
       
    private Database() { Console.WriteLine("Instance created"); }
    public static Database Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Database();
            }
            return _instance;
        }
    }     
}
```
_instance ilk olarak null şeklinde tanımlanır ve Property içerisinde null olup olmadığına göre işlem yaparız. Aynı zamanda Lazy Singleton yapısında single/multi thread kontrolü yapmamız gerekir. Yukarıdaki örnek single thread için yapılmıştır. Multi thread kontrollü Lazy Singleton için az önce kullandığımız lock yapısını kullanabilirsiniz.

#### Eager Singleton
```csharp
sealed class Database //sealed anahtar kelimesinin işlevini yazının sonundaki Keywords başlığında bulabilirsiniz
{
    private static Database _instance = new Database(); 
       
    private Database() { Console.WriteLine("Instance created"); }       
    public static Database Instance
    {
        get { return _instance; }
    }
}
```
Eager Singleton yapısında ise nesnemiz class oluştuğu anda otomatik olarak oluşturulur. Ayrıca Eager Singleton yapısında multi thread için ekstra kontrol yapmaya gerek yoktur. Single/Multi thread için aynı şekilde kodlanır ve iki durumda da doğru şekilde çalışır.

Son olarak, araştırma yaparken double-check locking gibi bir kavram görebilirsiniz. Anlatmaya gerek bile duymuyoruz. Boşverin onu.

### Keywords 

#### Lock
Multi Thread programlamada Thread’lerin senkronize bir şekilde çalışmasını sağlar. Lock anahtar kelimesi bloğunun içine yazılan işlemler Single Thread olarak işlem görür.

#### Property 
Program akışı içerisinde her yerden erişilmesini, fütursuzca değiştirilmesini istemediğimiz değişkenlerin class dışarısından güvenli şekilde erişilmesi ve değiştirilmesini istediğimiz durumlarda başvurduğumuz yapılardır.

Bir property iki farklı bloktan oluşur<br>
Get : değişkenin değerinin dışarıdan okunduğu blok<br>
Set : değişkenin değerinin dışarıdan değiştirildiği blok.

#### Parallel.Invoke 
Bir programda oluşma sırası önemli olmadan çoklu işlem yapmanın(Multi Thread) en basit yoludur.


#### Readonly 
Constructor dışında sadece okuma yetkisi vermek istediğiniz veriler için kullanılır. Static bir ifade değildir. Bu yüzden constdan farklıdır.



#### Kaynakça:


https://sourcemaking.com/design_patterns/singleton <br>
https://csharpindepth.com/articles/singleton

