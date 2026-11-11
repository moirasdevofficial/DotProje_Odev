**Ad Soyad:** Mustafa KÖROĞLUGİL 
**Öğrenci Numarası:** 2602020015


**Oyun özeti:**
Bir topu yuvarlanarak hareket ettirdiğimiz 3 seviyeden oluşan puan
toplamalı basit platform oyunu

**Kullandığım Scriptler ve görevleri;**

**Player3Script:** Karakter kontrolü, Trigger işlemleri, Sahne Kontrolü, UI update

**ColorManager3Script:** Enum şeklinde hızlıca material atama işlemi için

**Collectible3Script:** Puan sistemi için toplanabilir objelere atadığım script.
Başlangıçta rastgele yeşil veya kırmızı top seçerek uygun material ve tag atanır.


**Algoritma Açıklamaları:**
Nesne toplama işlemi ontrigger eventi ile gerçekleşir, karakter nesne ile etkileşime
girdiğinde nesnenin tagi kontrol edilir. Tagine göre score,ui,sfx işleelri yapıldıktan
sonra destroy edilerek o objenin toplandığı belli edilir.
Sahne geçiş işlemi içinse bölüm sonuna özel tage sahip bir box daha yerleştirilir
karakter boxa değdiği an o seviyeyi geçmek için gerekli puana sahip olup olmadığı
kontrol edilir. Eğer yeterli puana sahipse bir sonraki sahneyi değilse aynı sahneyi
yükleriz. Puanları tek tek sahnelere inspector ile atamak yerine karakter scripti içine
girilen indexe göre puan döndüren bir fonksiyon oluşturdum.

**Oynanış videosu:**
https://drive.google.com/file/d/10AXQHmq2KUEiPs8iKMQ5XLucObStLc6G/view?usp=sharing
