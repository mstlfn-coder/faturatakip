# Devam Notları

Bu dosya, yeni Codex chatlerinde kaldığımız yeri hızlıca anlamak için tutulur.

## Güncel Kaldığımız Yer

- Tarih: 2026-05-30
- Aktif branch: `codex/v0.2-fatura-turleri`
- Son tamamlanan faz: `v0.2 - Fatura Türleri Yönetimi`
- Sıradaki faz: `v0.3 - Abonelik Yönetimi`
- İlk dokümantasyon commit'i: `e0de4f9 docs: initialize project planning and continuity notes`
- `v0.1` commit'i: `3b3e20a feat: initialize wpf sqlite project skeleton`
- `v0.2` değişiklikleri henüz commitlenmedi.

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
25. `v0.1` commit'i master içine fast-forward merge edildi.
26. `codex/v0.2-fatura-turleri` branch'i açıldı.
27. `invoice_types` tablosu için `0002` migration eklendi.
28. Başlangıç fatura türleri seed edildi: Elektrik, Su, Doğalgaz, Telefon, İnternet, Diğer.
29. Fatura türü modeli ve repository katmanı eklendi.
30. Fatura türü listeleme, ekleme, düzenleme ve aktif/pasif yapma UI akışı eklendi.
31. `--self-test` komutu eklendi ve fatura türü repository akışı geçici veritabanıyla doğrulandı.
32. `dotnet build FaturaTakip.sln` başarılı çalıştı.
33. `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --health-check` başarılı çalıştı.
34. `dotnet run --no-build --project src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test` başarılı çalıştı.
35. Uygulama kısa süreli başlatma testinden geçti.

## Bir Sonraki Codex Görevi

Sıradaki görev yalnızca `v0.3` kapsamını uygulamak olmalı.

Başlangıç talimatı:

```text
C# WPF + SQLite tabanlı kurum fatura takip programında v0.3 fazını uygula.

Önce README.md, docs/00-codex-devam-kilavuzu.md, docs/03-devam-notlari.md, ROADMAP.md ve REGRESYON.md dosyalarını oku.

Bu fazda yalnızca abonelik yönetimi yapılacak.

Abonelikler için tablo/migration, fatura türüne bağlama, listeleme, ekleme, düzenleme ve aktif/pasif yapma desteklenecek.

Fatura kaydı, ödeme, PDF evrak ekleme, rapor, dışa aktarım ve yedekleme yapılmayacak.

Faz sonunda uygulamayı çalıştır veya en azından derleme/test doğrulamasını yap; sonra ROADMAP.md, REGRESYON.md ve docs/03-devam-notlari.md dosyalarını güncelle.
```

## Dikkat Edilecekler

- Yol sorunlarına karşı PowerShell'de gerekirse `-LiteralPath` kullanılmalı.
- Uygulama kodu başlamadan önce .NET SDK durumu kontrol edilmeli.
- Yeni oluşturulacak klasör adları ASCII olmalı.
- Faz kapsamı aşılmamalı.
- Önce dokümantasyon okunmalı, sonra uygulama koduna geçilmeli.
