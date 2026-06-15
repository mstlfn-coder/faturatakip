# Kurum Fatura Takip Programi

Kurum aboneliklerine ait fatura, PDF evrak, odeme kaydi ve raporlama sureclerini tek yerde takip etmek icin gelistirilen WPF masaustu uygulamasi.

## Son Durum

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.208 - Reset Dugmesi Tipografi Ayari`
- Son smoke test:
  - `dotnet build .\FaturaTakip.sln -c Release`
  - `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Projenin Amaci

Bu uygulama su isleri duzenli hale getirmek icin gelistiriliyor:

- Abonelik kayitlarini tutmak
- Gelen faturalari islemek
- Fatura PDF dosyalarini saklamak
- Odeme kayitlari ve odeme PDF evraklarini baglamak
- Odenmemis, gecikmis veya evragi eksik faturalari ayiklamak
- Excel ve PDF raporlari uretmek
- Yeni Codex chatlerinde kaldigimiz yerden devam edebilmek icin surekli dokumantasyon tutmak

## Teknoloji Yapisi

- `.NET 8`
- `WPF`
- `SQLite`
- Dosya sistemi tabanli ek evrak klasorleri

Ana solution dosyasi:

- `FaturaTakip.sln`

Uygulama projesi:

- `src/FaturaTakip.App/FaturaTakip.App.csproj`

## Klasor Yapisi

```text
attachments/   Fatura ve odeme PDF ekleri
backups/       Yedek dosyalari
config/        Yerel tercih ve ayarlar
database/      SQLite veritabani
docs/          Plan, devam notlari ve handoff dosyalari
exports/       Excel, PDF, TXT, JSON disa aktarma ciktilari
logs/          Uygulama loglari
src/           Kaynak kod
tools/         Yardimci script ve araclar
```

## Calistirma

Gelisim icin:

```powershell
dotnet build .\FaturaTakip.sln
dotnet run --project .\src\FaturaTakip.App\FaturaTakip.App.csproj
```

Self-test icin:

```powershell
dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test
```

Windows uzerinden hizli acilis icin repo kokunde su dosyalar bulunur:

- `UygulamayiCalistir.bat`
- `SelfTestCalistir.bat`

## Once Okunacak Dosyalar

Yeni bir Codex chatinde devam edilecekse once su dosyalara bakilmasi yeterlidir:

1. `docs/03-devam-notlari.md`
2. `docs/06-guncel-durum-ozeti.md`
3. `REGRESYON.md`
4. `ROADMAP.md`

## Ana Dokumanlar

- `docs/01-gelistirme-plani.md`  
  Projenin ana kapsam ve mimari planı.

- `docs/03-devam-notlari.md`  
  Son tamamlanan faz ve sonraki mantikli adim.

- `docs/06-guncel-durum-ozeti.md`  
  Hizli handoff ozeti.

- `REGRESYON.md`  
  Her adim sonunda calistirilan kontroller.

- `ROADMAP.md`  
  Son tamamlanan isler ve genel gelisim akisi.

## Devam Kuralimiz

Bu projede sureklilik onemli. Bu yuzden her anlamli adimdan sonra:

- ilgili kod veya dokuman guncellenir
- smoke test varsa calistirilir
- `REGRESYON.md` guncellenir
- `ROADMAP.md` guncellenir
- `docs/03-devam-notlari.md` guncellenir
- gerekiyorsa `docs/06-guncel-durum-ozeti.md` guncellenir

Boylece yeni bir chat acildiginda kaldigimiz yeri hizlica toparlamak kolay olur.

## Guncel Gelisim Ekseni

Su an agirlikli olarak bu akislar guclendiriliyor:

- Faturalar ekranindaki inceleme yardimlari
- Secili aksiyon ozetleri ve kisayol geri bildirimleri
- PDF/evrak kontrol akisi
- Raporlama ve continuity dokumantasyonu

## Not

Yerel kullanici tercih dosyalari (ornegin `config/` altindaki bazi json dosyalari) ortam bazli olabilir. Gereksiz yere commitlenmemelidir.
