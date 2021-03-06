# FACTORY DESIGN PATTERN 
 Factory design pattern, bir üst sınıfta nesneler oluşturmak için bir arayüz sağlayan, 
ancak alt sınıfların oluşturulacak nesnelerin türünü değiştirmesine izin veren creational design pattern modelidir. Bir nesne farklı amaçlarda aynı şekilde oluşturulacaksa ortak bir arayüzde oluşturmayı sağlar. Bu tasarım deseni bir nesne yaratmak için arayüz sağlar ve hangi sınıftan nesne yaratılacağını alt sınıfların belirlemesine olanak tanır.
Asıl amaç oluşturmak istediğimiz sınıfları “Creator” dediğimiz tek bir sınıf üzerinden yönetmektir. Nesne, yaratma sürecinden soyutlanmış olur ve bu sayede sadece kendi işlevlerine odaklanır.
.
Factory Pattern’i uygulamak için “abstract class” ya da “interface” yapılarından faydalanabiliriz.

### Abstract Class ve Interface

OOP’de çok karıştırılan ve yeni başlayanların “hangisini kullanmalıyım acaba?” diyerek kafa patlattığı bir konudur. Öncelikle bu arkadaşların görevi kabaca söylemek gerekirse, arkada dönen teknik olaylardan bizi ayırmak ve bizim sadece input-output ile ilgilenmemizi sağlamak. Diyelim ki önümüzde bir mikrofon var ve biz bu mikrofona doğru eğilip bir şeyler söylüyoruz. Bizim ilgilendiğimiz kısım, mikrofona doğru konuşmak ve bunun sonucunda insanların bizi duyması. Biz mikrofona doğru konuşurken sesin nasıl iletildiğini veya teknik detayları ile ilgilenmiyoruz. Bizim bir girdimiz ve bir çıktımız var ve bizim bütün ilgilendiğimiz konu bu. Başka bir sekilde ifade etmek gerekirse, sinemaya gittiniz ve bir seansa bilet alıp koltuğunuza oturdunuz. Sizin ilgilendiğiniz kısım filmi izlemek/salona giriş yapmak(input) ve izlemek(output). Siz filmi izlerken “acaba bu görüntü nasıl bu beyaz perdeye yansıyor da biz bunu kocaman görüyoruz?” düşüncesinde olmuyorsunuz. İlgilendiğiniz(aslında ilgilenmeniz gereken) tek olay izlemek oluyor. Bir tablo üzerinde bu arkadaşların kısaca artılarını ve eksilerini inceleyebiliriz.

<table style="width:100%">
  <tr>
    <th>Abstract Class</th>
    <th>Interface</th>
  </tr>
  <tr>
    <td class="tg-0pky">New anahtar sözcüğü ile yeni nesne üretilemez.</td>
    <td class="tg-0pky">New anahtar sözcüğü ile yeni nesne üretilemez.</td>
  </tr>
  <tr>
    <td class="tg-0pky">constructor/destructor tanımlanabilir.
</td>
    <td class="tg-0pky">constructor/destructor tanımlanamaz.</td>
  </tr>
  <tr>
    <td class="tg-0pky">İçerisinde kapsadığı her şey public olmak zorunda değildir.</td>
    <td class="tg-0pky">İçerisinde bulunan her şey public olarak kabul edilir.
</td>
  </tr>
  <tr>
    <td class="tg-0pky">İçerisinde abstract methodlar ve normal methodlar bulundurabilir.</td>
    <td class="tg-0pky">İçerisinde sadece method bildirimi yapılabilir.
</td>
  </tr>
  <tr>
    <td class="tg-0pky">Alt classlar abstract classta bulunan abstract methodları override etmek zorundadır.</td>
    <td class="tg-0pky">Interface’i miras alan class’lar interface içerisinde bildirilen bütün methodları kullanmak zorundadır.</td>
  </tr>
  <tr>
    <td class="tg-0pky">Bir sınıf aynı anda sadece bir tane abstract class’ı miras alabilir.</td>
    <td class="tg-0pky">Bir sınır aynı anda birden fazla Interface miras alabilir.</td>
  </tr>
  <tr>
    <td class="tg-0pky">Alt classlar ile arasında Is-a ilişkisi vardır.
</td>
    <td class="tg-0pky">Alt classlar ile arasında can-do ilişkisi vardır.
</td>
  </tr>
</table>

### Factory Pattern
Bu iki yapıyı anladıktan sonra Factory Pattern’e geri dönebiliriz. Yukarıda yazdığımız gibi, bizim amacımız sinemaya girip eğlenmek. Filmin perdeye nasıl yansıdığını, hangi açı ile perdeye geldiği vs konularıyla ilgilenmiyoruz. O halde başlayabiliriz.

Bu örneğimiz bir oyun örneği olsun. Mage ve Skeleton olmak üzere iki adet düşman tipimiz, bunları üretecek olan bir tane Spawner(Creator) ve aynı zamanda düşmanlarımızın Shout ve TakeDamage özellikleri olsun.

Burada dikkat etmemiz gereken konu, Mage ve Skeletonun ortak bir durumu var. İkisi de bir düşman! O halde biz bir adet ikisinin de miras alacağı bir Enemy Class’ı yazalım.

### Temel bir Factory Pattern yapısı şu adımlardan oluşur:

- Kullanacağımız alt classların genel tanımlamasının yapılacağı bir abstract ana class oluşturulur. Bu class içerisinde alt classların içereceği özellikler tanımlanır.(Interface için biraz daha farklı durumlar söz konusu fakat bu örneğimizde interface kullanmayacağız.)
- İçerisinde alt classların türetileceği bir fabrika class’ı oluşturulur.
- Alt classlar oluşturulur ve ana class'ta tanımlanmış olan özellikler burada özelleştirilir.
- Aynı zamanda alt class’lar ana class’ı miras alır.

İlkel bir Factory Pattern yapısı bu üç gerekliliği sağlamalıdır. 

1) İlk olarak alt class’ların miras alacağı ana class’ı yazalım.
```csharp
public abstract class Enemy
    {
        public Enemy(int health, string name, int damage, int id)
        {
            Health = health;
            Name = name;
            Damage = damage;
            ID = id;
        }

        public int Health { get; private set; }
        public string Name { get; private set; }
        public int Damage { get; private set; }
        public int ID { get; private set; }

        public void TakeDamage(Enemy enemy)
        {
            Health -= enemy.Damage;
            Console.WriteLine(Name + " hasar alıyor. Aaaaaaah bu acıttı! Kalan canım : " + Health);
        }
        public abstract void Shout();
    }
```

Senaryomuzda düşman olan iki tane farklı karakter var ve bu karakterlerin ortak yönü düşman olmasıdır. Bundan dolayı ana class’ımız Enemy olacak. Bu Class içerisinde bütün Enemy’lere ait olacak olan özellikleri yazdık.<br/>

> Dikkat: Bu class’ı miras alan alt classlar TakeDamage methodunu veya tanımlanmış olan property’leri kullanmak zorunda değiller fakat Shout methodunu override etmek zorundalar çünkü kendisi bir abstract method.

2) Şimdi sırada Enemy class’ını miras alacak olan alt class’ları tanımlayalım. Bu senaryoda bir Mage ve bir Skeleton düşman var.

```csharp
class Skeleton : Enemy
    {
        public Skeleton(int health,string name,int damage,int id) : base(health,name,damage,id)
        {
            Console.WriteLine("Skeleton is ready!");
        }

        public override void Shout()
        {
            Console.WriteLine("TASTE MY BONE!");
        }
    }
    class Mage : Enemy
    {
        public Mage(int health, string name, int damage, int id) : base(health, name, damage, id)
        {
            Console.WriteLine("Mage is ready!");
        }

        public override void Shout()
        {
            Console.WriteLine("Kul e'ûzü birabbinnâs".ToUpper());
        }
    }
```

Bu iki class Enemy class’ını miras alıyor ve onun özelliklerinden faydalanabiliyor. base keyword’ü sayesinde alt class’ın constructor’ında aldığımız parametreleri üst class’a gönderebiliyoruz. Aynı zamanda bu iki class Shout methodunu kendisi için override ediyor.

3) Son adım olarak bu class’lardan nesnelerin türetileceği bir fabrika class’ı oluşturacağız. Bu senaryoda bu class’ın adı Spawner olacak.

```csharp
public enum Type
    {
        Meele,
        Range
    }

    public class Spawner
    {      
        public static Enemy SpawnEnemy(int health, string name, int damage, int id, Type type)
        {
            Enemy enemy = null;

            switch (type)
            {
                case Type.Meele:
                    enemy = new Skeleton(health, name, damage, id);
                    break;
                case Type.Range:
                    enemy = new Mage(health, name, damage, id);
                    break;
            }

            return enemy;
        }
    }
```

Enum yapısını kullanarak sadece belirlenen spesifik tipler ile çalışmak istiyoruz. Spawner içerisinde SpawnEnemy() methodunda şu işlemler yapılıyor:
1) Bana enemy’lerin statlarını ve türünü ver
2) Switch case ile hangi nesnenin türetileceğine karar ver
3) Türetilen nesneyi geri döndür.

> Not: Static olmasının sebebi, bu class’ı bir nesne türetmeden kullanmak istememiz. Dönüş tipinin Enemy olmasının sebebi ise, bizim bir Enemy nesnesini bu fonksiyona eşitleyip bir nesne türetmek istememiz.



Son olarak main içerisine bakalım.


```csharp
static void Main(string[] args)
        {
            Enemy pitircik = Spawner.SpawnEnemy(70, "Iskelet Abi", 30, 1, Type.Meele);
            pitircik.Shout();

            Enemy imam = Spawner.SpawnEnemy(100, "Imam Abi", 50, 2, Type.Range);
            imam.Shout();

            pitircik.TakeDamage(imam);

            imam.TakeDamage(pitircik);         

            Console.ReadLine();
        }

```

![alt text](https://github.com/UlascanKilic/Design-Patterns/blob/main/Factory/ss1.png)

```csharp
Enemy pitircik = Spawner.SpawnEnemy(70, "Iskelet Abi", 30, 1, Type.Meele);
```
Burada yapılan işlem adım adım şu şekildedir:
1) Spawner class’ındaki SpawnEnemy methoduna git ve verdiğim parametreleri gönder.
SpawnEnemy içerisine verilen Type değerine göre bir Mage veya Skeleton nesnesi türetiliyor ve dönüş tipi Enemy olduğu için bu türetilen nesneyi return ile geri döndürüyor. Döndürülen nesne pitircik tipindeki Enemy nesnesinde tutuluyor.

#### Kullanım amacı 
Aynı tipten nesneleri tek bir class üzerinden türetmek ve kontrol etmek.

#### Örnekler
Sisteme öğrenci ekleme. Araba üretmek, Bir oyuna düşman yaratmak.

#### Çözdüğü problem
Üretim sırasındaki oluşan karmaşayı engellemek.

#### Kaynakça 
Kalbimiz <3
