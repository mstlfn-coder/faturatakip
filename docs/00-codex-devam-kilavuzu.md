# Codex Devam Kılavuzu

Bu dosya, yeni Codex chatine geçildiğinde projeye kaldığı yerden devam etmek için ilk okunacak kılavuzdur.

## Proje Bilgisi

- Proje adı: Kurum Fatura Takip Programı
- Proje yolu: `C:\Users\Asus\Documents\FATURA TAKİP PROGRAMI`
- Kaynak planın eski konumu: `C:\Users\Asus\Desktop\Kurum_Fatura_Takip_Programi_Gelistirme_Plani.md`
- Proje içindeki plan kopyası: `docs/01-gelistirme-plani.md`
- Hedef teknoloji: C# / .NET / WPF / SQLite
- Uygulama tipi: Kurum içi masaüstü takip programı

## Yeni Chat Başlangıç Sırası

Yeni Codex chatinde şu sıra izlenmeli:

1. `README.md` oku.
2. `docs/03-devam-notlari.md` oku.
3. `ROADMAP.md` oku.
4. `REGRESYON.md` oku.
5. Gerekirse ayrıntılı plan için `docs/01-gelistirme-plani.md` oku.
6. `git status --short --branch` çalıştır.
7. Sadece roadmapte `Sıradaki` olan faza odaklan.

## Devam Komutu Örneği

Yeni chat açıldığında kullanıcı şu şekilde başlayabilir:

```text
Bu projeye kaldığımız yerden devam et. Önce README.md, docs/00-codex-devam-kilavuzu.md, docs/03-devam-notlari.md, ROADMAP.md ve REGRESYON.md dosyalarını oku. Sadece ROADMAP.md içindeki Sıradaki fazı uygula. Faz sonunda devam notlarını, roadmap'i ve regresyon dosyasını güncelle.
```

## Yol Güvenliği

Proje kök klasörü Türkçe karakter içerir. PowerShell tarafında yol sorunu yaşanırsa `-LiteralPath` kullanılmalı.

Önerilen yaklaşım:

```powershell
Set-Location -LiteralPath 'C:\Users\Asus\Documents\FATURA TAKİP PROGRAMI'
```

Yeni oluşturulacak proje içi dosya ve klasör adları mümkün olduğunca ASCII olmalı. Örneğin `docs`, `database`, `attachments`, `backups`, `logs`, `exports`.

## Geliştirme Disiplini

- Tek seferde büyük geliştirme yapılmayacak.
- Her faz küçük ve test edilebilir olacak.
- Önceki çalışan davranışlar regresyon listesiyle korunacak.
- PDF dosyaları veritabanına gömülmeyecek, dosya sisteminde saklanacak.
- Veritabanında dosya yolu, orijinal ad, hash ve yüklenme bilgileri tutulacak.
- Abonelik ana veri modeli olarak korunacak.

Ana ilişki:

```text
Fatura Türü -> Abonelik -> Fatura -> Ödeme -> Evraklar
```

## Her Faz Sonunda Güncellenecek Dosyalar

- `docs/03-devam-notlari.md`
- `ROADMAP.md`
- `REGRESYON.md`

Bu üç dosya güncellenmeden bir sonraki faza geçilmemeli.
