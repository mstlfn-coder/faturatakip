# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-14 / v1.133)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.133 - Akis Butonu Klavye Odak Vurgusu`
- Bu adimda Odemeler panelindeki akis butonlari icin klavye odaginda daha belirgin cerceve ve ton vurgusu eklendi.
- Boylece secili karttaki `Enter ile ac` ipucu, klavye odagi alindiginda buton yuzeyinde de daha net karsilik buluyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.132)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.132 - Aktif Kolon Kisayol Kapsulu`
- Bu adimda secili akis kartlarina, butondan hemen once gorunen kucuk bir `Enter ile ac` kapsulu eklendi.
- Boylece secili kartta fare aksiyonunun yani sira klavye ile ilerleme ipucu da gorunur hale geldi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.131)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.131 - Aksiyon Butonu Fiil Dili`
- Bu adimda Odemeler panelindeki akis butonlari daha net fiil diliyle guncellendi.
- Boylece secili karttaki hedef ve sonraki adim satirlariyla buton metni daha uyumlu hale geldi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.130)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.130 - Aktif Kolon Sonraki Adim Ipuclari`
- Bu adimda secili akis kartlarina, hedef satirinin altinda gorunen kisa bir `Sonraki adim:` ipucu eklendi.
- Boylece kullanici sadece nereye gidecegini degil, oraya gidince ilk ne yapacagini da kart uzerinden okuyabiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.129)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.129 - Aktif Kolon Hedef Satiri`
- Bu adimda secili akis kartinin dugmesinden hemen once gorunen kisa bir `Hedef:` satiri eklendi.
- Boylece kullanici tiklamadan hemen once o kartin hangi sonucu acacagini daha net okuyabiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.128)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.128 - Aktif Kolon Metin Tonu`
- Bu adimda secili akis kartinin yalnizca kendi baslik ve aciklama metinleri hafif ton farki alacak sekilde guncellendi.
- Boylece aktif kolon rozeti ve kart secimine ek olarak, kartin kendi icerigi de daha rafine bir odak hissi veriyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.127)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.127 - Aktif Kolon Rozeti`
- Bu adimda Odemeler panelindeki secili akis kartina kucuk bir `AKTIF KOLON` rozeti eklendi.
- Boylece aktif yol artik ust rozet, secili kart, alt not, buton vurgu ve kolon rozeti ile birlikte okunuyor.
- Ayrica bozulmus devam dosyasi temiz metin olarak yeniden kuruldu.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.126)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.126 - Akis Baslik Baglami`
- Bu adimda Odeme Is Akisi bolum basliginin altina, secili kolonu ve ilgili yonu ozetleyen dinamik bir baglam satiri eklendi.
- Boylece aktif yolun hangi akis kolonunda toplandigi panelin orta bolumunde de acikca okunabiliyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Son Guncelleme (2026-06-14 / v1.125)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.125 - Ozet Yonu Ipuclari`
- Bu adimda ust ozet kartlarin alt akis baglanti metinlerine asagi yon ipuclari eklendi.
- Boylece yukaridaki sayisal ozetlerden asagidaki akis kutularina gecis daha taranabilir hale geldi.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Odemeler panelindeki buton odagi da belirginlestigi icin, sonraki mantikli kucuk adim secili akis kartinin butonuna daha acik bir tooltip dili eklemek olabilir.
