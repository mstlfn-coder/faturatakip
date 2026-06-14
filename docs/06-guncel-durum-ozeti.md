# Guncel Durum Ozeti

Son guncelleme tarihi: 2026-06-14

## Hemen Okunacak Ozet

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan temiz faz: `v1.129 - Aktif Kolon Hedef Satiri`
- Bu dalin ana odagi: Odemeler gecis panelinde aktif akis baglamini katman katman daha okunur hale getirmek
- Dogrulama rutini: her kucuk adim sonunda `dotnet build` ve `--self-test`

## Son Tamamlanan Fazlar

- `v1.129` Aktif Kolon Hedef Satiri
- `v1.127` Aktif Kolon Rozeti
- `v1.128` Aktif Kolon Metin Tonu
- `v1.126` Akis Baslik Baglami
- `v1.125` Ozet Yonu Ipuclari
- `v1.124` Akis Ozet Geri Baglantisi
- `v1.123` Ozet Akis Baglanti Metni
- `v1.122` Aktif Yol Buton Vurgusu
- `v1.121` Aktif Yol Alt Notu
- `v1.120` Son Yol Kart Secimi
- `v1.119` Son Yol Rozeti
- `v1.118` Dinamik Odeme Ipucu
- `v1.117` Odeme Kart Hover Vurgusu
- `v1.116` Ust Yonlendirme Netligi
- `v1.115` Odeme Ozet Baglami
- `v1.114` Odeme Akisi Rozetleri
- `v1.113` Odemeye Hedefli Gecis
- `v1.112` Odemeler Gecis Paneli

## Bu Fazda Ne Yapildi

1. Secili akis kartlarinin dugmesinden hemen once gorunen `Hedef:` satiri eklendi.
2. Bu satir yalnizca aktif kolonda gorunuyor; diger kartlar notr ve daha sakin kaliyor.
3. Kart ici odak artik secili zemin, aktif kolon rozeti, mikro metin tonu ve hedef satiri ile birlikte okunuyor.
4. Dogrulama rutini build ve self-test ile tekrar temiz gecti.

## Son Dogrulanan Testler

- `dotnet build .\FaturaTakip.sln -c Release`
- `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Secili kolon kartinin altinda kisa bir "sonraki adim" ipucu vererek kullanici aksiyon zincirini bir tik daha akiskan hale getirmek

## Yeni Chat Icin Kisa Talimat

```text
Once docs/06-guncel-durum-ozeti.md ve docs/03-devam-notlari.md dosyalarini oku.
Ardindan README.md, ROADMAP.md ve REGRESYON.md icindeki en ust guncel bolumu kontrol et.
Sonra aktif branch uzerinde siradaki mantikli kucuk adimi uygula; build + self-test ve dokuman guncellemesini unutma.
```
