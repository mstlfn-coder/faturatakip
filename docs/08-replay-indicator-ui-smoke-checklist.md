# Replay Indicator UI Smoke Checklist

Son guncelleme: 2026-06-13

Bu belge, Faturalar ekranindaki odeme yardimi ve PDF yardimi replay indicator akislarini hizlica elle dogrulamak icin tutulur.
Amac, replay mini isareti, tooltip dili ve helper ozetlerinin birlikte dogru davrandigini kisa bir turla kontrol etmektir.

## Hazirlik

1. `UygulamayiCalistir.bat` ile uygulamayi ac.
2. `Faturalar` ekranina git.
3. Odeme kaydi olan bir fatura sec.
4. Odeme yardimi ve PDF yardimi alanlari gorunur olsun.

## Odeme Yardimi Kontrolu

1. Bir odeme yardimi aksiyonu calistir:
   - `Kalan Tutar`
   - `Son Aciklama`
   - `Secili Odeme`
2. Replay ozet satirinda ilgili prefix gorunur.
3. Mini indicator aksiyon varsa cizgi agirlikli gorunur.
4. Prefix veya mini indicator ustune gel:
   - tooltip action adini gosterir
   - `replay ayari` ve `vurgu` bilgisini gosterir
5. Ayni aksiyonu tekrar calistir:
   - son aksiyon satiri `yeniden tetiklendi` dilini alir
   - tooltip aktif durumda da bu dili tasir

## PDF Yardimi Kontrolu

1. Bir PDF yardimi aksiyonu calistir:
   - `PDF Sec`
   - `PDF Ac`
2. Replay ozet satirinda ilgili prefix gorunur.
3. Mini indicator aksiyon varsa cizgi agirlikli gorunur.
4. Prefix veya mini indicator ustune gel:
   - tooltip action adini gosterir
   - `replay ayari` ve `vurgu` bilgisini gosterir
5. Ayni aksiyonu tekrar calistir:
   - son aksiyon satiri `yeniden tetiklendi` dilini alir
   - tooltip aktif durumda da bu dili tasir

## Bos Durum Kontrolu

1. Henuz replay baglami olusmamis bir yardim alanina bak.
2. Mini indicator daha sakin gorunur.
3. Tooltip:
   - `Replay ayari hazir` dilini kullanir
   - action secilirse replay yardiminin hazirlanacagini soyler

## Cikis Kriteri

Asagidakiler birlikte saglaniyorsa replay indicator UX halkasi smoke seviyesinde temiz kabul edilir:

- Odeme yardimi replay indicator gorunuyor
- PDF yardimi replay indicator gorunuyor
- Tooltip action adini ve replay ayarini dogru anlatiyor
- Tekrar tetikleme dili satir ve tooltip arasinda tutarli
- Bos durum dili yonlendirici ve sakin

## Komut Destegi

- `dotnet build .\FaturaTakip.sln -c Release`
- `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`
