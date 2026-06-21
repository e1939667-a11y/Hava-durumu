# 🌤️ Hava Durumu Uygulaması

Windows masa üstü uygulaması - Anlık, 5 günlük ve saatlik hava durumu gösterimi.

## ✨ Özellikler

✅ **Anlık hava durumu** - Sıcaklık, nem, rüzgar hızı
✅ **5 günlük tahmin** - Maksimum ve minimum sıcaklık
✅ **Saatlik tahmin** - Saatlik detaylı hava durumu (büyük görünümde)
✅ **Sağ tık menüsü** - Tema, şeffaflık ve görünüm ayarları
✅ **Animasyonlu hava ikonları** - Dinamik görsel geri bildirim
✅ **3 görünüm modu**:
   - 🔹 **Küçük** = O günün hava durumu
   - 🔹 **Orta** = O gün + 5 gün tahmin
   - 🔹 **Büyük** = O gün + 5 gün + saatlik bar
✅ **Ay evresini gösterim** - Tüm görünüm modlarında
✅ **Tema rengi seçimi** - Koyu Gri, Lacivert, Yeşil, Mürekkep
✅ **Şeffaflık ayarı** - Sadece arka planda (%50-%100)
✅ **Veriler %100 görünür** - Tüm bilgiler her zaman okunabilir
✅ **Modüler yapı** - APK/PKG için hazırlı

## 🛠️ Teknoloji

- **WPF** (Windows Presentation Foundation) - C#
- **OpenWeatherMap API** - Hava durumu verileri
- **.NET 6.0+** - Framework
- **Newtonsoft.Json** - JSON işlemi

## 📋 Sistem Gereksinimleri

- Windows 10 veya üstü
- .NET 6.0 Runtime
- İnternet bağlantısı

## 🚀 Kurulum ve Kullanım

### 1. Klonla
```bash
git clone https://github.com/e1939667-a11y/Hava-durumu.git
cd Hava-durumu
```

### 2. OpenWeatherMap API Key Ayarla

1. [openweathermap.org](https://openweathermap.org/) adresine git
2. Ücretsiz hesap oluştur
3. API Key'i al
4. `src/Services/WeatherService.cs` dosyasında `_apiKey` değişkenini güncelle

### 3. Çalıştır

**Visual Studio ile:**
```bash
Visual Studio'da HavaDurumu.sln dosyasını aç
F5 tuşuna bas
```

**Komut satırından:**
```bash
dotnet run
```

## 🎮 Kullanım

### Sağ Tık Menüsü
- 🎨 **Tema Rengi** - Arka plan rengini değiştir
- 👁️ **Şeffaflık** - %50 ile %100 arasında ayarla (sadece arka plan)
- 📺 **Görünüm** - Küçük/Orta/Büyük mod seç
- ❌ **Kapat** - Uygulamayı kapat

### Görünüm Modları

**Küçük (280px yükseklik)**
- Anlık hava durumu
- Ay evresini

**Orta (420px yükseklik)**
- Anlık hava durumu
- 5 günlük tahmin
- Ay evresini

**Büyük (600px yükseklik)**
- Anlık hava durumu
- 5 günlük tahmin
- Saatlik tahmin (alt bar)
- Ay evresini

## 📁 Proje Yapısı

```
Hava-durumu/
├── src/
│   ├── App.xaml(.cs)              # Ana uygulama dosyası
│   ├── MainWindow.xaml(.cs)       # Ana pencere (UI & logik)
│   ├── Models/
│   │   ├── WeatherModel.cs        # Veri modelleri
│   │   └── AppSettings.cs         # Ayarlar yönetimi
│   ├���─ Services/
│   │   └── WeatherService.cs      # API iletişimi
│   └── Resources/
│       └── Animations.xaml        # Animasyonlar
├── HavaDurumu.csproj              # Proje dosyası
├── README.md                      # Bu dosya
└── .gitignore
```

## 🔧 Geliştirme

### Yeni Özellik Eklemek

1. Yeni branch oluştur: `git checkout -b feature/yeni-ozellik`
2. Değişiklikleri yap
3. Commit et: `git commit -m 'Açıklama'`
4. Push et: `git push origin feature/yeni-ozellik`
5. Pull Request aç

### APK/PKG Oluşturma

Modüler yapı sayesinde mobil platform için:
- `src/Models/` - Paylaşılabilir veri modelleri
- `src/Services/` - Paylaşılabilir API servisleri

İhtiyaç halinde sil-baştan yapmadan bu dosyalar yeni projede kullanılabilir.

## 📝 Lisans

MIT License - Detaylar için LICENSE dosyasına bakın

## 👤 Yazar

**e1939667-a11y**

## 🤝 Katkı

Katkılardan mutlu olacağız! İssue açabilir veya PR gönderebilirsin.

## 📞 İletişim

Sorular ve öneriler için GitHub Issues'ı kullan.

---

⭐ Projeyi beğendiysen star atabilirsin!
