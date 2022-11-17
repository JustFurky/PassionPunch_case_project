# PassionPunch_case_project
 Projenin ana yapısı ve mimarisi local nesnelerde singleton ile veri alışverişi ve network üzerinden Pun.RPC komutu üzerinden işleniyor. Main obje olarak CharacterManager ve onun kontrolünde olan Character Controller en aktif kullanılan componentler. Genel olarak COP'ye bağlı kalmaya çalıştım. Silahlarımız GunScript abstract klasını miras alıyor ve scriptte belirlenen fonksiyon ve değişkeni kullanıyor. Projeye yeni silah eklenmek istendiği taktirde Gun scriptinin eklenmesi ve GunManager'e referans verilmesi yeterli. Karakter üzerinden silah kontrolünü ise GunManager scripti yapıyor. Silah ateşleme, aktif silahı ve mermi rengini seçme işlemini de GunManager'den yapıyorum. Targetler hit durumunda yapılacak işlemleri kendi üzerlerinde bulunan TargetScript'den alıyor ve target listeleri ve event timer TargetManager'da işleniyor. Her target IDamagable Interface'ini bulunduruyor.

 Karakterimiz için Character Component ve Character Movement isimli 2 farklı component bulunduruyor.
 UI için yapılan işlemler componentlere bölünmüş şekilde(EventData, PlayerData vb.).

 Klasör yapısı içinde barınan materyallere göre ayrılmış durumda. Örneğin Scriptler için Script ana klasörü ve Target, Character, Gun&Bullet, InGameUI gibi scriptlerin kullanıldıkları objelere göre ayrılmış durumdalar. Local İşlemlerde kullanılan prefablar Prefabs klasöründe bulunurken network tarafından işlem yapılan prefablar PhotonPrefab klasörüne dahil.

 Oyunun çalışma yolu ise; Kullanıcılar lobi üzerinden odaya katıldıktan ve oda sahibi oyunu başlattıktan sonra network üzerinden senkron bir şekilde oyun sahnesi çağırılıyor. Çağırılan sahnede karakterlerin hareketleri, targetler, skor ve renk değişim eventi server üzerinden işlenirken karakterlerin UI değişimleri ve hedefi vurduklarındaki kontroller lokal olarak yapılıyor ve sonrasında network üzerinden diğer oyunculara iletiliyor.

 Projede kullandığım network sistemi Photon Pun2 olarak biliniyor ve geliştiriciler tarafından sıkça kullanılan, Unity ile oldukça uyumlu yaygın bir network sistemi.

 Projede daha iyi bir yapı kurmak ve network işlemlerini, Input sistemini optimize ve sağlam mimaride yazmak mümkün. 

