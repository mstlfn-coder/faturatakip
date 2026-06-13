# Devam Notlari

Bu dosya, yeni Codex chatlerinde kaldigimiz yeri hizlica anlamak icin tutulur.

## Son Guncelleme (2026-06-13 / v1.95)

- Aktif branch: `codex/v1.08-review-context-issue-action`
- Son tamamlanan faz: `v1.95 - Yardim Durum Satiri Klavye Odak Cercevesi`
- Bu adimda secili yardim durum satirina daha belirgin klavye odak cercevesi eklendi.
- Yardim satiri odak aldiginda sinir ve arka plan birlikte vurgulaniyor.
- Smoke test: `dotnet build .\FaturaTakip.sln -c Release`
- Smoke test: `dotnet run -c Release --no-build --project .\src\FaturaTakip.App\FaturaTakip.App.csproj -- --self-test`

## Sonraki Mantikli Kucuk Adim

- Yardim durum satiri tooltip metnini daha kisa ve daha yonlendirici hale getirip `yeniden calistir` ifadesini tek dilde netlestirebiliriz.
