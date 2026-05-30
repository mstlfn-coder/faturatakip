# Devam Notları

Bu dosya, yeni Codex chatlerinde kaldığımız yeri hızlıca anlamak için tutulur.

## Güncel Kaldığımız Yer

- Tarih: 2026-05-30
- Aktif branch: `codex/v0.1-proje-iskeleti`
- Son tamamlanan faz: `v0.1 - Proje İskeleti ve Veritabanı`
- Sıradaki faz: `v0.2 - Fatura Türleri Yönetimi`
- İlk dokümantasyon commit'i: `e0de4f9 docs: initialize project planning and continuity notes`
- `v0.1` değişiklikleri henüz commitlenmedi.

## Bu Oturumda Yapılanlar

1. Proje klasörü kontrol edildi.
2. Proje klasöründe başlangıçta yalnızca `.git` bulundu.
3. Mevcut `.md` dosyası olmadığı görüldü.
4. Masaüstündeki kaynak plan dosyası bulundu:
   `C:\Users\Asus\Desktop\Kurum_Fatura_Takip_Programi_Gelistirme_Plani.md`
5. Plan dosyasının UTF-8 okunması gerektiği doğrulandı.
6. `docs` klasörü oluşturuldu.
7. Kaynak plan proje içine kopyalandı:
   `docs/01-gelistirme-plani.md`
8. Yeni chat devam kılavuzu oluşturuldu:
   `docs/00-codex-devam-kilavuzu.md`
9. Roadmap oluşturuldu:
   `ROADMAP.md`
10. Regresyon kontrol dosyası oluşturuldu:
    `REGRESYON.md`
11. Proje kararları kaydı oluşturuldu:
    `docs/02-proje-kararlari.md`
12. Bu devam notları oluşturuldu.
13. `codex/v0.1-proje-iskeleti` branch'i açıldı.
14. `FaturaTakip.sln` oluşturuldu.
15. `src/FaturaTakip.App` WPF projesi oluşturuldu.
16. `Microsoft.Data.Sqlite` paketi eklendi.
17. `.gitignore` eklendi ve `FaturaTakip.App` klasörü için Windows'taki `*.app` ignore çakışması düzeltildi.
18. Runtime klasör yapısı `.gitkeep` dosyalarıyla takip edilebilir hale getirildi.
19. SQLite başlangıç altyapısı ve idempotent migration runner eklendi.
20. `database/fatura_takip.db` health-check sırasında oluşturuldu.
21. Sade boş dashboard ekranı bağlandı.
22. `dotnet build FaturaTakip.sln` başarılı çalıştı.
23. `dotnet run --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` iki kez başarılı çalıştı.
24. Uygulama kısa süreli başlatma testinden geçti.

## Bir Sonraki Codex Görevi

Sıradaki görev yalnızca `v0.2` kapsamını uygulamak olmalı.

Başlangıç talimatı:

```text
C# WPF + SQLite tabanlı kurum fatura takip programında v0.2 fazını uygula.

Önce README.md, docs/00-codex-devam-kilavuzu.md, docs/03-devam-notlari.md, ROADMAP.md ve REGRESYON.md dosyalarını oku.

Bu fazda yalnızca fatura türleri yönetimi yapılacak.

Fatura türleri için tablo/migration, başlangıç türleri, listeleme, ekleme, düzenleme ve aktif/pasif yapma desteklenecek.

Abonelik, fatura, ödeme, PDF evrak ekleme, rapor, dışa aktarım ve yedekleme yapılmayacak.

Faz sonunda uygulamayı çalıştır veya en azından derleme/test doğrulamasını yap; sonra ROADMAP.md, REGRESYON.md ve docs/03-devam-notlari.md dosyalarını güncelle.
```

## Dikkat Edilecekler

- Yol sorunlarına karşı PowerShell'de gerekirse `-LiteralPath` kullanılmalı.
- Uygulama kodu başlamadan önce .NET SDK durumu kontrol edilmeli.
- Yeni oluşturulacak klasör adları ASCII olmalı.
- Faz kapsamı aşılmamalı.
- Önce dokümantasyon okunmalı, sonra uygulama koduna geçilmeli.
