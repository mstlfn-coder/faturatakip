# Codex Devam Kilavuzu

Bu dosya, yeni Codex chatine gecildiginde projeye kaldigimiz yerden devam etmek icin ilk okunacak temiz kilavuzdur.

## Proje Bilgisi

- Proje adi: Kurum Fatura Takip Programi
- Proje yolu: `C:\Users\Asus\Documents\FATURA TAKİP PROGRAMI`
- Proje ici plan: `docs/01-gelistirme-plani.md`
- Temiz guncel durum ozeti: `docs/06-guncel-durum-ozeti.md`
- Hedef teknoloji: C# / .NET / WPF / SQLite
- Uygulama tipi: kurum ici masaustu takip programi

## Yeni Chat Baslangic Sirasi

Yeni Codex chatinde su sira izlenmeli:

1. `docs/06-guncel-durum-ozeti.md` oku.
2. `README.md` icindeki en alttaki son faz notlarini oku.
3. `ROADMAP.md` icindeki en alttaki `Son Guncelleme` bloklarini oku.
4. `REGRESYON.md` icindeki en alttaki son faz bloklarini oku.
5. Gerekirse ayrintili plan icin `docs/01-gelistirme-plani.md` oku.
6. `git status --short --branch` calistir.
7. Yalnizca bir sonraki mantikli kucuk faza odaklan.

## Onemli Not

Eski tarihsel dokumanlarda encoding kalintilari bulunabilir.
Guncel ve guvenilir handoff noktasi olarak once `docs/06-guncel-durum-ozeti.md` esas alinmalidir.

## Devam Komutu Ornegi

Yeni chat acildiginda kullanici su sekilde baslayabilir:

```text
Bu projeye kaldigimiz yerden devam et. Once docs/06-guncel-durum-ozeti.md, sonra README.md, ROADMAP.md ve REGRESYON.md dosyalarinin en guncel bolumlerini oku. Review baglam cipi UX'iyle ilgili bir is varsa docs/07-review-context-ui-smoke-checklist.md dosyasini da kontrol et. Sonraki mantikli kucuk adimi uygula; sonunda smoke testleri ve dokuman guncellemelerini yap.
```

## Yol Guvenligi

Proje kok klasoru Turkce karakter icerir. PowerShell tarafinda yol sorunu yasanirsa `-LiteralPath` kullanilmalidir.

```powershell
Set-Location -LiteralPath 'C:\Users\Asus\Documents\FATURA TAKİP PROGRAMI'
```

Yeni olusturulacak proje ici dosya ve klasor adlari mumkun oldugunca ASCII tutulmalidir.

## Her Faz Sonunda Guncellenecek Dosyalar

- `docs/06-guncel-durum-ozeti.md`
- `README.md`
- `ROADMAP.md`
- `REGRESYON.md`

Bu dosyalar guncellenmeden bir sonraki faza gecilmemelidir.

