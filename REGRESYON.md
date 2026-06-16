# Regresyon Kontrol Listesi

## v1.220 - Reset Kontrast Yumusatma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk satirinda kontrast yumusadi | OK | Reset dugmesinin yazi tonu yardim metnine daha sakin geciyor |
| Son odeme satirinda kontrast yumusadi | OK | Reset dugmesinin yazi tonu yardim metnine daha sakin geciyor |
| Okunurluk korundu | OK | Kontrast azalirken reset dugmesi rahatca secilebilir kaldi |
| 2026-06-16 | v1.220 | OK | Reset dugmesinin genel kontrasti hafifce yumusatildi |

## v1.219 - Reset Arka Plan Tonu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk satirinda arka plan yumusadi | OK | Reset dugmesinin zemin tonu yardim satirina daha sakin baglaniyor |
| Son odeme satirinda arka plan yumusadi | OK | Reset dugmesinin zemin tonu yardim satirina daha sakin baglaniyor |
| Aksiyon okunurlugu korundu | OK | Dugme arka plani hafiflesse de reset aksiyonu belirgin kaldi |
| 2026-06-16 | v1.219 | OK | Reset dugmesinin arka plan tonu hafifce yumusatildi |

## v1.218 - Reset Kenarlik Tonu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk satirinda kenarlik yumusadi | OK | Reset dugmesinin kenarlik tonu yardim satirina daha sakin baglaniyor |
| Son odeme satirinda kenarlik yumusadi | OK | Reset dugmesinin kenarlik tonu yardim satirina daha sakin baglaniyor |
| Dugme ayrimi korundu | OK | Kenarlik yumusasa da reset aksiyonu ayirt edilir kaliyor |
| 2026-06-16 | v1.218 | OK | Reset dugmesinin kenarlik tonu hafifce yumusatildi |

## v1.217 - Reset Renk Gecisi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk satirinda renk gecisi yumusadi | OK | Reset dugmesinin yazi tonu yardim metnine daha yakin hale geldi |
| Son odeme satirinda renk gecisi yumusadi | OK | Reset dugmesinin yazi tonu yardim metnine daha yakin hale geldi |
| Komut hissi korundu | OK | Dugme tonu yumusasa da ayri bir aksiyon olarak okunmaya devam ediyor |
| 2026-06-16 | v1.217 | OK | Reset dugmesinin yazi tonu yardim metnine yaklastirildi |

## v1.216 - Reset Tipografi Akrabaligi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk satirinda tipografik aile yaklasti | OK | Reset dugmesi ana aksiyon dugmesiyle ayni puntoya gelerek daha akraba hissediyor |
| Son odeme satirinda tipografik aile yaklasti | OK | Reset dugmesi ana aksiyon dugmesiyle ayni puntoya gelerek daha akraba hissediyor |
| Hiyerarsi korundu | OK | Reset dugmesi agirligi normale alindigi icin dugmeler benzesirken vurgu farki kaybolmadi |
| 2026-06-16 | v1.216 | OK | Reset dugmesinin tipografisi ana aksiyon ailesine yaklastirildi |

## v1.215 - Aksiyon Dugmesi Agirlik Dengesi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk aksiyon dugmesi yumusadi | OK | Dikey ic bosluk azaltilarak yardim metniyle agirlik farki hafifletildi |
| Son odeme aksiyon dugmesi yumusadi | OK | Dikey ic bosluk azaltilarak yardim metniyle agirlik farki hafifletildi |
| Aksiyon belirginligi korundu | OK | Dugmeler hala birincil komut hissini kaybetmeden okunuyor |
| 2026-06-15 | v1.215 | OK | Aksiyon dugmelerinin dikey ic boslugu hafifce azaltildi |

## v1.214 - Reset Dugmesi Satir Yuksekligi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk satiri sakinlesti | OK | Reset dugmesinin dikey ic boslugu azaltilarak toplam satir yuksekligi hafifletildi |
| Son odeme satiri sakinlesti | OK | Reset dugmesinin dikey ic boslugu azaltilarak toplam satir yuksekligi hafifletildi |
| Ortak stil korundu | OK | Dikey bosluk iki dugmede ortak stil uzerinden yonetiliyor |
| 2026-06-15 | v1.214 | OK | Reset dugmesinin dikey ic boslugu hafifce azaltildi |

## v1.213 - Reset Dugmesi Dikey Hiz
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk satirinda merkez hissi dengeli | OK | Reset dugmesi yardim metniyle ayni satirda daha dengeli hizalaniyor |
| Son odeme satirinda merkez hissi dengeli | OK | Reset dugmesi yardim metniyle ayni satirda daha dengeli hizalaniyor |
| Hiz davranisi ortaklasti | OK | Dikey hiz ortak stil anahtarina tasinarak iki dugmede ayni hale getirildi |
| 2026-06-15 | v1.213 | OK | Reset dugmesinin dikey hizasi ortak stil uzerinden sabitlendi |

## v1.212 - Yardim Metni Sarmalama Siniri
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk satiri sarmalamasi kontrollu | OK | Yardim metni genislik siniri ile daha ongorulebilir kiriliyor |
| Son odeme satiri sarmalamasi kontrollu | OK | Yardim metni genislik siniri ile daha ongorulebilir kiriliyor |
| Yan yana ritim korundu | OK | Yardim metni ile geri donus dugmesi arasindaki denge daha stabil kaldi |
| 2026-06-15 | v1.212 | OK | Yardim metinlerine kontrollu sarmalama siniri eklendi |

## v1.211 - Yardim Metni Satir Hizi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk yardim satiri hizlandi | OK | Yardim metninin ust boslugu azaltilarak reset dugmesiyle daha yakin ritim kuruldu |
| Son odeme yardim satiri hizlandi | OK | Yardim metninin ust boslugu azaltilarak reset dugmesiyle daha yakin ritim kuruldu |
| Okunabilirlik korundu | OK | Satir ici toparlama yapilirken metin sarmalama davranisi bozulmadi |
| 2026-06-15 | v1.211 | OK | Yardim metni ust boslugu hafifce azaltildi |

## v1.210 - Reset Dugmesi Sol Mesafe
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk satiri daha toplu | OK | Yardim metni ile geri donus dugmesi arasindaki sol bosluk hafifce azaldi |
| Son odeme satiri daha toplu | OK | Yardim metni ile geri donus dugmesi arasindaki sol bosluk hafifce azaldi |
| Margin ortaklasti | OK | Sol mesafe stil anahtarina tasinarak iki dugmede ayni hale getirildi |
| 2026-06-15 | v1.210 | OK | Reset dugmesinin sol mesafesi ortak stil uzerinden dengelendi |

## v1.209 - Reset Dugmesi Ic Boslugu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk geri donus ritmi toparlandi | OK | Kuyruk satirindaki dugme daha kompakt bir boslukla yardim metnine daha hafif baglaniyor |
| Son odeme geri donus ritmi toparlandi | OK | Son odeme satirindaki dugme daha kompakt bir boslukla yardim metnine daha hafif baglaniyor |
| Ic bosluk ortaklasti | OK | Padding stil anahtarina tasinarak iki dugmede ayni hale getirildi |
| 2026-06-15 | v1.209 | OK | Reset dugmesinin ic boslugu ortak stil uzerinden kompaktlastirildi |

## v1.208 - Reset Dugmesi Tipografi Ayari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk geri donus tipografisi dengeli | OK | Kuyruk satirindaki dugme yardim metniyle daha akraba bir olcekte gorunuyor |
| Son odeme geri donus tipografisi dengeli | OK | Son odeme satirindaki dugme yardim metniyle daha akraba bir olcekte gorunuyor |
| Stil ortaklasti | OK | Font boyutu stil anahtarina tasinarak iki dugmede ortak kullaniliyor |
| 2026-06-15 | v1.208 | OK | Reset dugmesinin tipografi ayari ortak stil uzerinden duzenlendi |

## v1.207 - Reset Dugmesi Tonu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk geri donus tonu yumusadi | OK | Kuyruk satirindaki `Hepsini Goster` dugmesi yardim metniyle daha uyumlu gorunuyor |
| Son odeme geri donus tonu yumusadi | OK | Son odeme satirindaki `Hepsini Goster` dugmesi yardim metniyle daha uyumlu gorunuyor |
| Aksiyon gorunurlugu korundu | OK | Dugme daha sakin olsa da tiklanabilir komut hissini kaybetmiyor |
| 2026-06-15 | v1.207 | OK | Reset dugmesinin tonu yardim satirina yaklastirildi |

## v1.206 - Reset Dugmesi Yatay Bosluk
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk komut satiri toplandi | OK | Yardim metni ile geri donus dugmesi daha yakin duruyor |
| Son odeme komut satiri toplandi | OK | Yardim metni ile geri donus dugmesi daha yakin duruyor |
| Yatay nefes korundu | OK | Bosluk azaltildi ama satir sikismadi |
| 2026-06-15 | v1.206 | OK | Reset dugmesinin yatay boslugu hafifce azaltildi |

## v1.205 - Yardim Metni Satir Yuksekligi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk yardim akisi dengeli | OK | Kuyruk yardim metninde satir yuksekligi daha sakin akiyor |
| Son odeme yardim akisi dengeli | OK | Son odeme yardim metninde satir yuksekligi daha sakin akiyor |
| Satir ritmi temiz | OK | Iki satirli senaryolarda komut satiri daha duzgun gorunuyor |
| 2026-06-15 | v1.205 | OK | Yardim metinlerinin satir yuksekligi dengelendi |

## v1.204 - Yardim Metni Tonu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk yardim tonu yumusadi | OK | Kuyruk yardim metni daha sakin renge ve daha kucuk tipografiye alindi |
| Son odeme yardim tonu yumusadi | OK | Son odeme yardim metni daha sakin renge ve daha kucuk tipografiye alindi |
| Komut hiyerarsisi korundu | OK | Yardim metni geri planda kalirken okunabilirligini koruyor |
| 2026-06-15 | v1.204 | OK | Yardim metinlerinin tonu ve boyutu yumusatildi |

## v1.203 - Yardim Metni Yakinlastirma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk yardim satiri toplandi | OK | Kuyruk aksiyon satirinda buton ve yardim metni daha yakin duruyor |
| Son odeme yardim satiri toplandi | OK | Son odeme aksiyon satirinda buton ve yardim metni daha yakin duruyor |
| Mikro ritim korundu | OK | Metin yakinlasti ama satir sikismadi |
| 2026-06-15 | v1.203 | OK | Yardim metinlerinin ust boslugu hafifce azaltildi |

## v1.202 - Rozet Aksiyon Yakinlastirma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk dikey ritmi toparlandi | OK | Aktif kuyruk rozeti ile komut satiri biraz daha yakinlasti |
| Son odeme dikey ritmi toparlandi | OK | Aktif son odeme rozeti ile komut satiri biraz daha yakinlasti |
| Mikro denge korundu | OK | Bosluk azaltildi ama katmanlar carpmadi |
| 2026-06-15 | v1.202 | OK | Rozet ve alt aksiyon satiri arasindaki bosluk hafifce azaltildi |

## v1.201 - Rozet Ust Bosluk Sikilastirma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk ritmi yakinlasti | OK | Filtre ile aktif kuyruk rozeti arasindaki bosluk azaltildi |
| Son odeme ritmi yakinlasti | OK | Filtre ile aktif son odeme rozeti arasindaki bosluk azaltildi |
| Mikro denge korundu | OK | Siklik arttirildi ama satirlar carpmadi |
| 2026-06-15 | v1.201 | OK | Aktif rozetin ust boslugu hafifce azaltildi |

## v1.200 - Aktif Rozet Baglam Metni
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk rozeti acik | OK | Kisa metin dogrudan aktif kuyruk baglamini soyluyor |
| Son odeme rozeti acik | OK | Kisa metin dogrudan aktif son odeme baglamini soyluyor |
| Yan yana okuma hizlandi | OK | Iki rozet bir arada daha net ayirt ediliyor |
| 2026-06-15 | v1.200 | OK | Aktif filtre rozetlerinin baglam metni netlestirildi |

## v1.199 - Aktif Rozet Tooltip Dili
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk tooltipi dinamik | OK | Secili kuyruk gorunumu ve sonuc sayisi hover aninda aciklaniyor |
| Son odeme tooltipi dinamik | OK | Secili son odeme gorunumu ve sonuc sayisi hover aninda aciklaniyor |
| Detay dili net | OK | Kisa rozet metni korunurken ek baglam tooltipte veriliyor |
| 2026-06-15 | v1.199 | OK | Aktif filtre rozetlerinin tooltip dili dinamik hale getirildi |

## v1.198 - Aktif Filtre Rozet Tonu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk rozet tonu dinamik | OK | Genel, Acil ve PDF Eksik gorunumleri farkli tonlarla ayirt ediliyor |
| Son odeme rozet tonu dinamik | OK | PDF Eksik gorunumu rozet tonuyla daha belirginlesiyor |
| Ton ayrimi sakin | OK | Renk ayrimi bilgi veriyor ama panel dengesini bozmuyor |
| 2026-06-15 | v1.198 | OK | Aktif filtre rozetlerine secili gorunume gore ton farki eklendi |

## v1.197 - Donus Dugmesi Ton Ayrimi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk donus dugmesi ayrisiyor | OK | Bos kuyruk durumundaki geri donus dugmesi daha gorunur ton aliyor |
| Son odeme donus dugmesi ayrisiyor | OK | Bos son odeme durumundaki geri donus dugmesi daha gorunur ton aliyor |
| Ton dengesi korundu | OK | Ayrim sakin tutuldu, birincil aksiyon hissine tasinmadi |
| 2026-06-15 | v1.197 | OK | Hepsini Goster dugmelerine sakin ton ayrimi eklendi |

## v1.196 - Bos Filtre Fiil Dili
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk bos metni yonlendirici | OK | Bos kuyruk durumunda geri donus ve filtre degisimi acik anlatiliyor |
| Son odeme bos metni yonlendirici | OK | Bos son odeme durumunda geri donus ve filtre degisimi acik anlatiliyor |
| Fiil dili net | OK | Metinler kullaniciya sonraki hareketi acik bicimde soyluyor |
| 2026-06-15 | v1.196 | OK | Bos filtre metinleri daha yonlendirici hale getirildi |

## v1.195 - Bos Filtre Donus Aksiyonu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk donus dugmesi hazir | OK | Sonucsuz kuyruk filtresinde Hepsini Goster gorunuyor |
| Son odeme donus dugmesi hazir | OK | Sonucsuz son odeme filtresinde Hepsini Goster gorunuyor |
| Donus davranisi kosullu | OK | Dugmeler yalnizca all disi filtre ve bos sonuc kombinasyonunda gorunuyor |
| 2026-06-15 | v1.195 | OK | Sonucsuz odeme filtrelerine geri donus aksiyonu eklendi |

## v1.194 - Aktif Filtre Sonuc Ozeti
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk rozet sayisi gorunur | OK | Aktif kuyruk filtresi gorunen kayit sayisini ozetliyor |
| Son odeme rozet sayisi gorunur | OK | Aktif son odeme filtresi gorunen kayit sayisini ozetliyor |
| Rozet sayisi dinamik | OK | Filtre degisikliginde sonuc sayisi listeyle birlikte guncelleniyor |
| 2026-06-15 | v1.194 | OK | Aktif filtre rozetlerine sonuc sayisi eklendi |

## v1.193 - Odemeler Filtre Sonuc Sayilari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk sayilari gorunur | OK | Hepsi, Acil ve PDF Eksik dugmeleri sonuc sayilarini gosteriyor |
| Son odeme sayilari gorunur | OK | Hepsi ve PDF Eksik dugmeleri sonuc sayilarini gosteriyor |
| Sayi kapsami acik | OK | Tooltipler sayilarin en yakin veya en son bes kaydi temsil ettigini acikliyor |
| 2026-06-15 | v1.193 | OK | Odemeler filtrelerine sonuc sayilari eklendi |

## v1.192 - Odemeler Aktif Filtre Rozeti
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk filtresi gorunur | OK | Secili kuyruk gorunumu AKTIF rozetiyle ozetleniyor |
| Son odeme filtresi gorunur | OK | Secili son odeme gorunumu AKTIF rozetiyle ozetleniyor |
| Rozet dinamik yenileniyor | OK | Filtre degisikliginde rozet metni veriyle birlikte guncelleniyor |
| 2026-06-15 | v1.192 | OK | Odemeler listelerine aktif filtre rozetleri eklendi |

## v1.191 - Odemeler Klavye Odak Vurgusu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Filtre odagi gorunur | OK | Filtre dugmeleri klavye odaginda belirgin cerceve aliyor |
| Aksiyon odagi gorunur | OK | Hizli ve satir ici acis dugmeleri odakta daha belirgin hale geliyor |
| Satir odagi gorunur | OK | Satir ici dugmeye odak gelince ilgili kaydin tamami vurgulaniyor |
| 2026-06-15 | v1.191 | OK | Odemeler listesinde klavye odak akisi belirginlestirildi |

## v1.190 - Odemeler Liste Filtreleri
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk filtreleri calisiyor | OK | Hepsi, Acil ve PDF Eksik secimleri kuyruk listesini daraltiyor |
| Son odeme filtreleri calisiyor | OK | Hepsi ve PDF Eksik secimleri son odeme listesini daraltiyor |
| Filtre ozeti gorunur | OK | Ozet metni ve aksiyon ipucu secili filtreye gore yenileniyor |
| 2026-06-15 | v1.190 | OK | Odemeler listesine gorunum filtreleri ve filtre baglam metinleri eklendi |

## v1.189 - Odemeler Liste Hover Erisimi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Hover vurgusu gorunur | OK | Kuyruk ve son odeme satirlari hover durumunda daha belirgin cerceve ve arka plan aliyor |
| Acis amaci netlesti | OK | Satir dugmelerinin tooltip metinleri hedefli acis davranisini acik bicimde anlatiyor |
| 2026-06-15 | v1.189 | OK | Odemeler listesine hover erisimi ve tooltip netligi eklendi |

## v1.188 - One Cikan Kayit Ozeti
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odak karti gorunur | OK | Ust alanda one cikan kaydi baslik, meta, durum ve tutar ile gosteriyor |
| Hizli acis korunuyor | OK | Odak kartindan ilgili fatura odeme alanina tek tikla acilabiliyor |
| 2026-06-15 | v1.188 | OK | Odemeler paneline one cikan kayit ozeti eklendi |

## v1.187 - Odemeler Durum Rozetleri
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruk durum rozetleri gorunur | OK | Odenmedi ve kismi durumlari satir icinde belirgin rozetlerle ayrisiyor |
| Son odeme PDF rozetleri gorunur | OK | PDF hazir ve PDF eksik durumlari tek bakista secilebiliyor |
| 2026-06-15 | v1.187 | OK | Odemeler paneline satir ici durum rozetleri eklendi |

## v1.186 - Odemeler Hizli Aksiyon Satiri
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Oncelikli kayit hizli aciliyor | OK | Odenmemis kuyrugunun ilk kaydi hizli aksiyon butonu ile odeme alanina geciyor |
| Son odeme hizli aciliyor | OK | Son odeme faturasina hizli aksiyon satirindan dogrudan donulebiliyor |
| 2026-06-15 | v1.186 | OK | Odemeler paneline hizli aksiyon satiri eklendi |

## v1.185 - Odemeler Ozeti Hedefli Gecis
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kuyruktan hedefli gecis var | OK | Bekleyen odeme kaydindan ilgili faturayi odeme alaninda ac butonu ile acmak mumkun |
| Son odemeden hedefli gecis var | OK | Son odeme listesinden ilgili fatura odeme alanina tek tikla donulebiliyor |
| 2026-06-15 | v1.185 | OK | Odemeler panelinde hedefli gecis dugmeleri eklendi |

## v1.184 - Odemeler Operasyon Ozeti
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme kuyrugu gorunur | OK | Odenmemis faturalardan en yakin kayitlar Odemeler sekmesinde listeleniyor |
| Son odemeler gorunur | OK | En yeni odeme kayitlari ve PDF durumu ayni panelde izlenebiliyor |
| 2026-06-15 | v1.184 | OK | Odemeler paneline operasyon ozeti eklendi |

## v1.183 - Mikro Hiyerarsi Seri Kapanisi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Butunsel ritim kontrolu tamamlandi | OK | Panel genelinde ton, bosluk ve vurgu katmanlari birlikte yeniden degerlendirildi |
| Ek piksel duzeltmesine gerek kalmadi | OK | Son gorunum dengeli bulundugu icin seri kontrollu bicimde kapatildi |
| 2026-06-15 | v1.183 | OK | Odemeler panelindeki mikro hiyerarsi ince ayar serisi kapatildi |

## v1.182 - Kisayol Aksiyon Son Ritim
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Alt komut ritmi toparlandi | OK | Kisayol kapsulu ile aksiyon dugmesi arasindaki bosluk azaltildigi icin blok daha toplu okunuyor |
| Komut agirligi korundu | OK | Aksiyon dugmeleri hala belirgin ve kolay secilebilir durumda |
| 2026-06-15 | v1.182 | OK | Odemeler panelinde kisayol aksiyon son ritim ayari tamamlandi |

## v1.181 - Hedef Kisayol Bosluk Ayari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Gecis boslugu dengelendi | OK | Hedef satiri ile kisayol kapsulu arasindaki bosluk azaltildigi icin alt akis daha toplu gorunuyor |
| Katman okunurlugu korundu | OK | Hedef, kisayol ve aksiyon katmanlari birbirine karismadan okunabiliyor |
| 2026-06-15 | v1.181 | OK | Odemeler panelinde hedef kisayol bosluk ayari tamamlandi |

## v1.180 - Sonraki Adim Ton Ayrimi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Ton hiyerarsisi netlesti | OK | Sonraki adim metinleri yumusatildigi icin hedef satiri daha belirgin ana yon oldu |
| Akis okunurlugu korundu | OK | Destekleyici metinler hala rahat okunuyor ve baglam kaybi olusmadi |
| 2026-06-15 | v1.180 | OK | Odemeler panelinde sonraki adim ton ayrimi tamamlandi |

## v1.179 - Aktif Yol Gecis Yumusatma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Aktif yol gecisi yumusadi | OK | Aktif yol notlarinin tonu ve ust boslugu azaltildigi icin aciklama ile gecis daha akici oldu |
| Hiyerarsi korundu | OK | Aktif yol ipucu hala secili akis bilgisini net bicimde ayiriyor |
| 2026-06-15 | v1.179 | OK | Odemeler panelinde aktif yol gecis yumusatma tamamlandi |

## v1.178 - Yardim Aksiyon Yakinlastirma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Yardim ve aksiyon dengelendi | OK | Yardim kapsulu ile aksiyon dugmesi arasindaki bosluk azaltildigi icin komut alani daha butun gorunuyor |
| Komut okunurlugu korundu | OK | Dugmeler ayrisikligini koruyor ve tiklanabilirlik algisi zayiflamadi |
| 2026-06-15 | v1.178 | OK | Odemeler panelinde yardim aksiyon yakinlastirma tamamlandi |

## v1.177 - Aktif Rozet Oran Ayari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Aktif rozet boyutu dengelendi | OK | Aktif kolon rozetlerinin ic boslugu azaltildigi icin yardim kapsulu ile daha yakin oran yakalandi |
| Katman okunurlugu korundu | OK | Secili akis vurgusu hala net ve yardim katmanlariyla karismiyor |
| 2026-06-15 | v1.177 | OK | Odemeler panelinde aktif rozet oran ayari tamamlandi |

## v1.176 - Hover Kenarlik Tonu Ayari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Hover kenarligi daha sakin | OK | Hover kenarlik tonu hafifce yumusatilarak cerceve vurgusu daha kontrollu hale getirildi |
| Vurgu okunurlugu korundu | OK | Kart hover durumu hala belirgin ve panel akisi kolay takip ediliyor |
| 2026-06-15 | v1.176 | OK | Odemeler panelinde hover kenarlik tonu ayari tamamlandi |

## v1.175 - Hover Arka Plan Tonu Ayari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Hover tonu daha sakin | OK | Hover arka plan tonu hafifce yumusatilarak kart vurgusu daha kontrollu hale getirildi |
| Etkilesim okunurlugu korundu | OK | Hover durumu hala kolay fark ediliyor ve aktif kart hissi kaybolmadi |
| 2026-06-15 | v1.175 | OK | Odemeler panelinde hover arka plan tonu ayari tamamlandi |

## v1.174 - Hover Buyume Orani Ayari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Hover buyumesi daha sakin | OK | Mikro olcek buyumesi azaltildigi icin kart tepkisi daha kontrollu hissediliyor |
| Hareket okunurlugu korundu | OK | Hover animasyonu hala fark edilir ve akisin yon hissi kaybolmadi |
| 2026-06-15 | v1.174 | OK | Odemeler panelinde hover buyume orani ayari tamamlandi |

## v1.173 - Hover Donus Hiz Ayari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Hover donusu daha cevik | OK | Hover cikis sureleri kisaltilarak temel hale daha hizli donus saglandi |
| Sakin ton korundu | OK | Kartlar daha cevik donse de yumusak panel dili bozulmadi |
| 2026-06-15 | v1.173 | OK | Odemeler panelinde hover donus hiz ayari tamamlandi |

## v1.172 - Hover Opaklik Dengeleme
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Hover opaklik daha sakin | OK | Dinlenim ve hover opaklik farki azaltildi |
| Kart tonu daha kararli | OK | Kartlar panel icinde daha istikrarli bir gorunum veriyor |
| 2026-06-15 | v1.172 | OK | Odemeler panelinde hover opaklik dengeleme tamamlandi |

## v1.171 - Hover Gecisi Yumusatma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Hover daha sakin | OK | Hover kenarligi ve mikro buyume yeni ton dengesine gore yumusatildi |
| Kart hareketi korundu | OK | Kartlar hala hissedilir ama daha rafine bir geri bildirim veriyor |
| 2026-06-15 | v1.171 | OK | Odemeler panelinde hover gecisi yumusatma tamamlandi |

## v1.170 - Birincil Odak Tonu Yumusatma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Birincil odak daha sakin | OK | Birincil dugmenin klavye odak tonu panel dengesiyle daha uyumlu hale geldi |
| Ana komut rolu korundu | OK | Odeme alanini ac dugmesi hala en belirgin ana aksiyon olarak kaliyor |
| 2026-06-15 | v1.170 | OK | Odemeler panelinde birincil odak tonu yumusatma tamamlandi |

## v1.169 - Renk Ailesi Agirlik Dengeleme
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Renk aileleri daha dengeli | OK | Mavi ve turuncu rozet tonlari yesil akisla daha yakin algisal agirlikta |
| Uc akis karti daha butunlu | OK | Kartlar ayni panel dili icinde daha esit vurgu dagitiyor |
| 2026-06-14 | v1.169 | OK | Odemeler panelinde renk ailesi agirlik dengeleme tamamlandi |

## v1.168 - Ikincil Odak Tonu Yumusatma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Ikincil odak daha sakin | OK | Klavye odak vurgusu yardimci rolu koruyacak sekilde yumusatildi |
| Birincil hiyerarsi korundu | OK | Birincil komut odakli akis daha temiz okunuyor |
| 2026-06-14 | v1.168 | OK | Odemeler panelinde ikincil odak tonu yumusatma tamamlandi |

## v1.167 - Ikincil Aksiyon Tonu Dengeleme
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Ikincil aksiyon daha sakin | OK | Ikincil buton tonlari yardimci rolune daha uygun hale geldi |
| Birincil aksiyon onde | OK | Odeme calisma dugmesi panelde daha net ana komut gibi gorunuyor |
| 2026-06-14 | v1.167 | OK | Odemeler panelinde ikincil aksiyon tonu dengeleme tamamlandi |

## v1.166 - Kisayol Tonu Yumusatma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kisayol daha sakin | OK | Kisayol kapsulu metinleri daha yardimci tonda okunuyor |
| Hiyerarsi korundu | OK | Aksiyon dugmesi ana komut olarak onde kalmaya devam ediyor |
| 2026-06-14 | v1.166 | OK | Odemeler panelinde kisayol tonu yumusatma tamamlandi |

## v1.165 - Sonraki Adim Tonu Yumusatma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Sonraki adim daha sakin | OK | Sonraki adim satirlari daha yardimci tonda okunuyor |
| Hiyerarsi korundu | OK | Hedef satiri ve aksiyon dugmesi ana odakta kalmaya devam ediyor |
| 2026-06-14 | v1.165 | OK | Odemeler panelinde sonraki adim tonu yumusatma tamamlandi |

## v1.164 - Aktif Yol Tonu Yumusatma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Aktif yol daha sakin | OK | Aktif yol notlari daha yardimci tonda okunuyor |
| Hiyerarsi korundu | OK | Hedef ve sonraki adim satirlari ana odakta kalmaya devam ediyor |
| 2026-06-14 | v1.164 | OK | Odemeler panelinde aktif yol tonu yumusatma tamamlandi |

## v1.163 - Baglam Cumlesi Tonu Yumusatma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Baglam cumlesi daha sakin | OK | Baglam cumleleri daha yardimci ton ve agirlikla okunuyor |
| Hiyerarsi korundu | OK | Baslik, aktif yol ve hedef satirlari ana odakta kalmaya devam ediyor |
| 2026-06-14 | v1.163 | OK | Odemeler panelinde baglam cumlesi tonu yumusatma tamamlandi |

## v1.162 - Aciklama Tonu Yumusatma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Aciklama daha sakin | OK | Aciklama satirlari daha yardimci bir tonla okunuyor |
| Metin hiyerarsisi korundu | OK | Aktif durum ve yonlendirme satirlari ana odakta kalmaya devam ediyor |
| 2026-06-14 | v1.162 | OK | Odemeler panelinde aciklama tonu yumusatma tamamlandi |

## v1.161 - Baslik Ust Bosluk Dengeleme
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Baslik daha bagli | OK | Basligin ust boslugu azaltildi ve aktif kolon rozetine yakinlasti |
| Ust giris daha butunlu | OK | Secili kartin ust blogu daha yekpare okunuyor |
| 2026-06-14 | v1.161 | OK | Odemeler panelinde baslik ust bosluk dengeleme tamamlandi |

## v1.160 - Rozet Ust Bosluk Dengeleme
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Rozet daha bagli | OK | Rozetin ust boslugu azaltildi ve kartin ust bloguna yakinlasti |
| Ust durum zinciri daha butunlu | OK | Secili durum girisi daha kompakt okunuyor |
| 2026-06-14 | v1.160 | OK | Odemeler panelinde rozet ust bosluk dengeleme tamamlandi |

## v1.159 - Aksiyon Butonu Yakinlastirma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Buton daha bagli | OK | Butonun ust boslugu azaltildi ve kisayol kapsulune yakinlasti |
| Alt eylem zinciri daha butunlu | OK | Kapsul ve buton daha tek blok gibi okunuyor |
| 2026-06-14 | v1.159 | OK | Odemeler panelinde aksiyon butonu yakinlastirma tamamlandi |

## v1.158 - Kisayol Kapsulu Yakinlastirma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kisayol kapsulu daha bagli | OK | Kapsulun ust boslugu azaltilarak sonraki adim satirina yakinlastirildi |
| Alt yardim zinciri daha toplu | OK | Sonraki adim ve kapsul daha tek akis gibi okunuyor |
| 2026-06-14 | v1.158 | OK | Odemeler panelinde kisayol kapsulu yakinlastirma tamamlandi |

## v1.157 - Sonraki Adim Yakinlastirma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Sonraki adim daha bagli | OK | Sonraki adim satiri hedef satirina yakinlastirildi |
| Alt yonlendirme blogu daha toplu | OK | Hedef ve sonraki adim daha tek blok gibi okunuyor |
| 2026-06-14 | v1.157 | OK | Odemeler panelinde sonraki adim yakinlastirma tamamlandi |

## v1.156 - Hedef Satiri Yakinlastirma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Hedef satiri daha bagli | OK | Hedef satirinin ust boslugu azaltildi ve aktif yol notuna yakinlasti |
| Secili akis zinciri daha akici | OK | Ust bilgi blogundan hedef satirina gecis daha tek parca hissediliyor |
| 2026-06-14 | v1.156 | OK | Odemeler panelinde hedef satiri yakinlastirma tamamlandi |

## v1.155 - Aktif Yol Notu Yakinlastirma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Aktif yol notu daha bagli | OK | Ust bosluk azaltilarak baglam satirina daha yakin okunur hale geldi |
| Secili durum geri bildirimi daha akici | OK | Ust bilgi blogu icindeki yardimci not daha dogal bir devam hissi veriyor |
| 2026-06-14 | v1.155 | OK | Odemeler panelinde aktif yol notu yakinlastirma tamamlandi |

## v1.154 - Aciklama Baglam Bosluk Ayari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Aciklama ve baglam daha bagli | OK | Baglam cumlesi ust boslugu azaltilarak aciklama satirina yakinlastirildi |
| Ust bilgi blogu daha butunlu | OK | Rozet-baslik-aciklama-baglam zinciri daha akici okunuyor |
| 2026-06-14 | v1.154 | OK | Odemeler panelinde aciklama-baglam bosluk ayari tamamlandi |

## v1.153 - Baslik Aciklama Ritim Sikilastirma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Baslik ve aciklama daha bagli | OK | Aciklama ust boslugu azaltilarak ust bilgi blogu sikilastirildi |
| Ilk bakis ritmi daha akici | OK | Rozet-baslik-aciklama zinciri daha tek parca hissediliyor |
| 2026-06-14 | v1.153 | OK | Odemeler panelinde baslik-aciklama ritim sikilastirma tamamlandi |

## v1.152 - Rozet Baslik Yakinlastirma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Rozet ile baslik daha bagli | OK | Baslik ust boslugu azaltilarak secili durum etiketiyle yakinlastirildi |
| Ust giris ritmi daha toplu | OK | Kartin ilk bilgi blogu daha birlesik okunuyor |
| 2026-06-14 | v1.152 | OK | Odemeler panelinde rozet-baslik yakinlastirma tamamlandi |

## v1.151 - Mikro Rozet Animasyon Ritmi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Aktif rozet fade ritmi daha tutarli | OK | Rozet fade baslangici ve suresi yardim kapsuluyle hizalandi |
| Secim geri bildirimi daha butunlu | OK | Kart icindeki mikro durum isaretleri ayni tempoda aciliyor |
| 2026-06-14 | v1.151 | OK | Odemeler panelinde mikro rozet animasyon ritmi duzenlendi |

## v1.150 - Mikro Rozet Agirlik Yumusatma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Aktif kolon rozet metni daha sakin | OK | Rozet metinleri normal agirlikla daha yumusak okunuyor |
| Renkli vurgu korunuyor | OK | Kapsul arka plani aktif baglami belirgin gostermeye devam ediyor |
| 2026-06-14 | v1.150 | OK | Odemeler panelinde mikro rozet agirlik yumusatma tamamlandi |

## v1.149 - Mikro Rozet Olcek Dengeleme
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Aktif kolon rozetleri daha dengeli | OK | Rozet padding ve yazi boyutu yardim kapsullerine yaklastirildi |
| Mikro etiket ailesi daha butunlu | OK | Rozet ile kapsul ayni kart dilinin parcalari gibi daha tutarli gorunuyor |
| 2026-06-14 | v1.149 | OK | Odemeler panelinde mikro rozet olcek dengeleme tamamlandi |

## v1.148 - Yardim Kapsulu Agirlik Yumusatma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Yardim kapsulu daha hafif gorunuyor | OK | `Enter ile ac` metni normal agirlikla daha yardimci bir katman gibi okunuyor |
| Buton ana aksiyon olarak onde kaliyor | OK | Kapsul ile buton arasindaki agirlik farki daha net |
| 2026-06-14 | v1.148 | OK | Odemeler panelinde yardim kapsulu agirlik yumusatma tamamlandi |

## v1.147 - Hedef Sonraki Adim Aralik Ayari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Hedef ve sonraki adim daha toplu | OK | Aralik azaltilarak iki satir daha bagli okunur hale geldi |
| Alt yardim ritmi korundu | OK | Sikisma olmadan daha birlesik bir yonlendirme blogu elde edildi |
| 2026-06-14 | v1.147 | OK | Odemeler panelinde hedef-sonraki adim aralik ayari tamamlandi |

## v1.146 - Ust Bilgi Mikro Bosluk Ayari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Ust bilgi blogu daha yakin | OK | `Aktif yol` notunun ust boslugu hafifce azaltildi |
| Okunabilirlik korundu | OK | Satirlar birbirine girmeden daha toplu bir akis veriyor |
| 2026-06-14 | v1.146 | OK | Odemeler panelinde ust bilgi mikro bosluk ayari tamamlandi |

## v1.145 - Ilk Aciklama Tonu Sakinlestirme
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Ilk aciklama satiri daha sakin | OK | Secili kartta aciklama tonu baslik ve hedefe gore yumusatildi |
| Metin hiyerarsisi net | OK | Baslik, hedef ve aciklama rolleri daha kolay ayrisiyor |
| 2026-06-14 | v1.145 | OK | Odemeler panelinde ilk aciklama tonu sakinlestirme tamamlandi |

## v1.144 - Ust Ritim Dengeleme
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Rozet ve baslik daha dengeli | OK | Ust bosluk hafifce azaltilarak ilk blog daha tek parca gorunuyor |
| Ilk bakis taramasi iyilesti | OK | Kartin ust akisinda gereksiz nefes azaltilip ritim toparlandi |
| 2026-06-14 | v1.144 | OK | Odemeler panelinde ust ritim dengeleme tamamlandi |

## v1.143 - Buton Ust Bosluk Dengeleme
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Buton ile kapsul arasi daha dengeli | OK | Ust bosluk azaltildigi icin iki mikro yuzey daha tutarli okunuyor |
| Alt blok ritmi korundu | OK | Sikisma olmadan daha yakin bir akıs elde edildi |
| 2026-06-14 | v1.143 | OK | Odemeler panelinde buton ust bosluk dengeleme tamamlandi |

## v1.142 - Yardim Kapsulu Icerik Sikilastirma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Yardim kapsulleri daha kompakt | OK | Padding ve yazi boyutu biraz azaltilarak daha rafine bir mikro yuzey elde edildi |
| Okunabilirlik korundu | OK | `Enter ile ac` metni hala rahat seciliyor |
| 2026-06-14 | v1.142 | OK | Odemeler panelinde yardim kapsulu icerik sikilastirma tamamlandi |

## v1.141 - Yardim Metni Satir Yuksekligi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Yardim satirlari daha duzgun akiyor | OK | `Hedef` ve `Sonraki adim` satirlari sabit line-height ile daha temiz hizada |
| Uzun metin ritmi korundu | OK | Sarma durumunda satirlar daha dengeli boslukla okunuyor |
| 2026-06-14 | v1.141 | OK | Odemeler panelinde yardim metni satir yuksekligi tamamlandi |

## v1.140 - Aktif Yol Tipografi Ayrimi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Aktif yol notu daha yardimci gorunuyor | OK | Boyut ve agirlik azaltildigi icin `Hedef` satiriyle karismiyor |
| Mikro hiyerarsi netlesti | OK | Aktif yol notu, hedef ve sonraki adim arasindaki rol farki daha belirgin |
| 2026-06-14 | v1.140 | OK | Odemeler panelinde aktif yol tipografi ayrimi tamamlandi |

## v1.139 - Ton Hiyerarsisi Dengeleme
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Yardim kapsulleri daha sakin ton kullaniyor | OK | Arka plan ve cerceve tonlari bir tik daha hafifletildi |
| Secili metinlerle uyum korundu | OK | Aciklama ve kapsul tonlari ayni renk ailesinde daha dengeli okunuyor |
| 2026-06-14 | v1.139 | OK | Odemeler panelinde ton hiyerarsisi dengelemesi tamamlandi |

## v1.138 - Aktif Kolon Rozeti Gorunme Gecisi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Aktif kolon rozeti yumusak sekilde gorunuyor | OK | `AKTIF KOLON` rozeti secili kartta hafif opacity gecisiyle beliriyor |
| Kisayol kapsuluyla uyum korundu | OK | Iki yardimci mikro unsur ayni sakin geri bildirim diline yaklasti |
| 2026-06-14 | v1.138 | OK | Odemeler panelinde aktif kolon rozeti gorunme gecisi tamamlandi |

## v1.137 - Kisayol Kapsulu Gorunme Canlanmasi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kisayol kapsulu yumusak sekilde gorunuyor | OK | `Enter ile ac` kapsulu secili oldugunda hafif opacity canlanmasi aliyor |
| Geri bildirim dili sakin kaldi | OK | Etki kisa ve dusuk siddette tutuldu |
| 2026-06-14 | v1.137 | OK | Odemeler panelinde kisayol kapsulu gorunme canlanmasi tamamlandi |

## v1.136 - Akis Karti Hover Gecisi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Akis kartlari hover aninda yumusak gecis veriyor | OK | Opacity ve mikro scale animasyonu ile kart hafifce canlaniyor |
| Genel sakin ton korundu | OK | Gecis kisa ve dusuk siddette kaldigi icin panel sakinligini bozmuyor |
| 2026-06-14 | v1.136 | OK | Odemeler panelinde akis karti hover gecisi tamamlandi |

## v1.135 - Akis Karti Dikey Ritim Ayari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Alt yonlendirme bloku daha toparli | OK | Hedef, sonraki adim, kisayol kapsulu ve buton araliklari daha kompakt |
| Okunabilirlik korundu | OK | Metinler ve butonlar birbirine girmeden daha tek parca okunuyor |
| 2026-06-14 | v1.135 | OK | Odemeler panelinde akis karti dikey ritim ayari tamamlandi |

## v1.134 - Akis Butonu Tooltip Dili
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Akis butonlarinda tooltip gorunuyor | OK | Her akis dugmesi acacagi hedefi ve `Enter` ipucunu hover aninda gosteriyor |
| Hover dili kart diliyle uyumlu | OK | Tooltip metinleri hedef ve sonraki adim aciklamalariyla tutarli |
| 2026-06-14 | v1.134 | OK | Odemeler panelinde akis butonu tooltip dili tamamlandi |

## v1.133 - Akis Butonu Klavye Odak Vurgusu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Akis butonlari klavye odaginda belirginlesiyor | OK | Secili karttaki butonlar odakta daha net cerceve ve ton vurgu aliyor |
| Kisayol ipucuyla uyum korundu | OK | `Enter ile ac` kapsulu ile buton odagi ayni aksiyon dilini destekliyor |
| 2026-06-14 | v1.133 | OK | Odemeler panelinde akis butonu klavye odak vurgusu tamamlandi |

## v1.132 - Aktif Kolon Kisayol Kapsulu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Secili akis kartinda kisayol kapsulu gorunuyor | OK | `Enter ile ac` kapsulu secili kartta butondan once gorunuyor |
| Diger kartlar sakin kaliyor | OK | Secili olmayan kartlarda kisayol kapsulu gorunmuyor |
| 2026-06-14 | v1.132 | OK | Odemeler panelinde aktif kolon kisayol kapsulu tamamlandi |

## v1.131 - Aksiyon Butonu Fiil Dili
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Buton metinleri daha net fiil dili kullaniyor | OK | `Odeme Alanini Ac`, `Eksik Evrak Raporunu Ac`, `Odenmemisler Raporunu Ac` ifadeleri gorunuyor |
| Kart ici dil ile uyum korundu | OK | Hedef ve sonraki adim satirlariyla buton komutu ayni aksiyon tonunda okunuyor |
| 2026-06-14 | v1.131 | OK | Odemeler panelinde aksiyon butonu fiil dili tamamlandi |

## v1.130 - Aktif Kolon Sonraki Adim Ipuclari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Secili akis kartinda sonraki adim ipucu gorunuyor | OK | `Sonraki adim:` satiri secili kartta hedef bilgisinin altinda beliriyor |
| Diger kartlar sakin kaliyor | OK | Secili olmayan kartlarda bu ipucu gorunmuyor |
| 2026-06-14 | v1.130 | OK | Odemeler panelinde aktif kolon sonraki adim ipuclari tamamlandi |

## v1.129 - Aktif Kolon Hedef Satiri
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Secili akis kartinda hedef satiri gorunuyor | OK | `Hedef:` satiri secili kartta dugmeden hemen once beliriyor |
| Diger kartlar sakin kaliyor | OK | Secili olmayan akis kartlarinda hedef satiri gorunmuyor |
| 2026-06-14 | v1.129 | OK | Odemeler panelinde aktif kolon hedef satiri tamamlandi |

## v1.128 - Aktif Kolon Metin Tonu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Secili akis karti metin tonu aliyor | OK | Aktif kolonun baslik ve aciklama satiri kendi renk ailesine hafifce yaklasiyor |
| Diger kartlar notr kaliyor | OK | Secim kalktiginda title ve description renkleri varsayilan gorunume donuyor |
| 2026-06-14 | v1.128 | OK | Odemeler panelinde aktif kolon metin tonu tamamlandi |

## v1.127 - Aktif Kolon Rozeti
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Secili akis kartinda aktif kolon rozeti gorunuyor | OK | `ISLEM`, `EVRAK` ve `RAPOR` kartlari secildiginde ilgili kartta `AKTIF KOLON` rozeti beliriyor |
| Odak daha toparli hissediliyor | OK | Aktif yolun kart ici okunabilirligi ek mikro katmanla desteklendi |
| 2026-06-14 | v1.127 | OK | Odemeler panelinde aktif kolon rozeti tamamlandi |

## v1.126 - Akis Baslik Baglami
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Akis basligi aktif kolonu ozetliyor | OK | Odeme Is Akisi altinda secili yone gore kisa baglam satiri guncelleniyor |
| Varsayilan metin korunuyor | OK | Aktif yol yokken alan genel aciklama halinde kaliyor |
| 2026-06-14 | v1.126 | OK | Odemeler panelinde akis baslik baglami tamamlandi |

## v1.125 - Ozet Yonu Ipuclari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Ozet kartlar asagi akis yonu veriyor | OK | Baglanti metinlerinde asagi yon ipucu kullaniliyor |
| Gorsel yogunluk sakin kaldi | OK | Ek cizgi olmadan sadece mikro yon etiketiyle iliski guclendi |
| 2026-06-14 | v1.125 | OK | Odemeler panelinde ozet yon ipuclari tamamlandi |

## v1.124 - Akis Ozet Geri Baglantisi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Akis kartlari ust ozeti aniyor | OK | Her alt akis karti iliskili ust ozet kartini kisa mikro metinle isaret ediyor |
| Panel iki yonlu okunuyor | OK | Ustten alta ve alttan uste bag kurulabiliyor |
| 2026-06-14 | v1.124 | OK | Odemeler panelinde akis-ozet geri baglantisi tamamlandi |

## v1.123 - Ozet Akis Baglanti Metni
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Ozet kartlar alt akisla iliski kuruyor | OK | Her ozet kart hangi alt akis karti ile okunacagini kisa metinle soyluyor |
| Gorsel yogunluk artmadan bag kuruluyor | OK | Cizgi veya ekstra dekor olmadan, mikro metinle yon hissi veriliyor |
| 2026-06-14 | v1.123 | OK | Odemeler panelinde ozet ve akis baglanti metinleri tamamlandi |

## v1.122 - Aktif Yol Buton Vurgusu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Aktif yol butonu hafif vurgu aliyor | OK | Son kullanilan yone ait eylem dugmesi kendi tonunda belirginlesiyor |
| Uc katmanli iz dorduncu katmanla tamamlandi | OK | Rozet, kart secimi, alt not ve buton birlikte ayni akisi isaret ediyor |
| 2026-06-14 | v1.122 | OK | Odemeler panelinde aktif yol buton vurgusu tamamlandi |

## v1.121 - Aktif Yol Alt Notu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Secili kartta aktif yol alt notu gorunuyor | OK | Son kullanilan yon ilgili ozet veya akis kartinda kucuk "Aktif yol" notu aliyor |
| Rozet, kart ve not birlikte calisiyor | OK | Ust rozet, kart secimi ve alt not ayni akis duygusunu birlestiriyor |
| 2026-06-14 | v1.121 | OK | Odemeler panelinde aktif yol alt notu tamamlandi |

## v1.120 - Son Yol Kart Secimi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Son yol ilgili kartta da gorunuyor | OK | Son kullanilan akis ilgili ozet veya akis kartinda hafif secili duruma geliyor |
| Rozet ile kart arasinda gorsel bag kuruldu | OK | Aylik, evrak, islem ve bakiye yollarinda ust rozet alttaki kartla eslesiyor |
| 2026-06-14 | v1.120 | OK | Odemeler panelinde son yol kart secimi tamamlandi |

## v1.119 - Son Yol Rozeti
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Son kullanilan akis rozeti gorunuyor | OK | Ust yardim alaninda son yol icin metinsel rozet gosteriliyor |
| Rozet akis turune gore renkleniyor | OK | Liste, islem, rapor, evrak ve bakiye yollarinda rozet tonu degisiyor |
| 2026-06-14 | v1.119 | OK | Odemeler panelinde son yol rozeti tamamlandi |

## v1.118 - Dinamik Odeme Ipucu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Ust yardim metni baglama gore degisiyor | OK | Kart hover ve ust buton hover aninda metin ilgili yone gore guncelleniyor |
| Son kullanilan akis ozeti korunuyor | OK | Kart veya buton kullanilinca yardim metni o akis ozetini gostermeye devam ediyor |
| 2026-06-14 | v1.118 | OK | Odemeler panelinde dinamik baglam ipucu tamamlandi |

## v1.117 - Odeme Kart Hover Vurgusu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Ozet kartlar hover geri bildirimi veriyor | OK | Fare ustune gelince kart arka plani ve kenarligi hafif vurgu aliyor |
| Akis kartlari hover geri bildirimi veriyor | OK | Islem, evrak ve rapor kartlari ayni sakin vurgu diliyle canlaniyor |
| 2026-06-14 | v1.117 | OK | Odemeler panelinde hover okunabilirligi guclendirildi |

## v1.116 - Ust Yonlendirme Netligi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Ust yonlendirme butonlari daha acik | OK | Fatura listesi ve rapor merkezi ifadeleri daha niyetli okunuyor |
| Yardimci durum metni gorunuyor | OK | Iki ust butonun hangi yone goturdugu kisa metinle aciklaniyor |
| 2026-06-14 | v1.116 | OK | Odemeler paneli ust genel yonlendirmesi netlestirildi |

## v1.115 - Odeme Ozet Baglami
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Ozet kartlar baglam etiketi aldi | OK | Aylik rapor, evrak kontrolu ve odeme calismasi baglamlari kartlarda gorunuyor |
| Kartlar neyi actigini acikliyor | OK | Her kart kendi rapor veya akis hedefini kisa metinle anlatiyor |
| 2026-06-14 | v1.115 | OK | Odemeler paneli ust ozet kartlarinda baglam netlestirildi |

## v1.114 - Odeme Akisi Rozetleri
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme akis yollari daha belirgin | OK | Islem, Evrak ve Rapor olarak uc ayri rozetli yonlendirme karti gorunuyor |
| Aksiyon aciklamalari ekranda okunuyor | OK | Her kisa yolun ne actigi ve ne icin kullanildigi panel icinde yaziyor |
| 2026-06-14 | v1.114 | OK | Odemeler panelinde rozetli akis aciklamalari tamamlandi |

## v1.113 - Odemeye Hedefli Gecis
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odemeler panelinden odeme alani aciliyor | OK | Odeme odakli butonlar Faturalar ekraninda odeme calisma modunu aciyor |
| Odenmemis filtre ve odak uygulanabiliyor | OK | Odenmemis kayitlar filtreleniyor, secili kayitta odeme tarihi alanina odak veriliyor |
| 2026-06-14 | v1.113 | OK | Odemeler paneli artik daha hedefli gecis sunuyor |

## v1.112 - Odemeler Gecis Paneli
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odemeler menusu artik tiklanabiliyor | OK | Sol menudeki Odemeler artik calisan bir navigasyon dugmesi |
| Odemeler paneli dogru akis noktalari sunuyor | OK | Faturalar ve Raporlar icin tek tik gecisleri ile mevcut odeme akisini acikliyor |
| 2026-06-14 | v1.112 | OK | Odemeler gecis paneli + build + self-test tamamlandi |

## v1.111 - Replay Ayirac Uyumu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi ayirac noktasi replay tonuna uyum sagliyor | OK | Replay aktifken ayirac nokta hafif yesil vurgu ile satir diline katiliyor |
| PDF yardimi ayirac noktasi replay tonuna uyum sagliyor | OK | Replay aktifken ayirac nokta hafif mavi vurgu ile satir diline katiliyor |
| 2026-06-14 | v1.111 | OK | Replay ayirac uyumu + build + self-test tamamlandi |

## v1.110 - Replay Kisa Yol Ipucu Uyumu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi kisayol ipucu replay tonuna uyum sagliyor | OK | Replay aktifken hint kapsulu hafif yesil ton ve kenarlik uyumu aliyor |
| PDF yardimi kisayol ipucu replay tonuna uyum sagliyor | OK | Replay aktifken hint kapsulu hafif mavi ton ve kenarlik uyumu aliyor |
| 2026-06-14 | v1.110 | OK | Replay kisa yol ipucu uyumu + build + self-test tamamlandi |

## v1.109 - Replay Rozeti Gecis Uyumu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi secili durum rozetinde replay gecisi var | OK | Replay aktifken tekrar rozeti replay tonunu devralip satir flashi ile uyumlu canlaniyor |
| PDF yardimi secili durum rozetinde replay gecisi var | OK | Replay aktifken tekrar rozeti replay tonunu devralip satir flashi ile uyumlu canlaniyor |
| 2026-06-14 | v1.109 | OK | Replay rozeti gecis uyumu + build + self-test tamamlandi |

## v1.108 - Yardim Durum Satiri Geri Donus Isigi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi durum satiri kisa geri donus isigi veriyor | OK | Secili aksiyon tetiklenince tum satir hafif yesil bir flash ile geri bildirim veriyor |
| PDF yardimi durum satiri kisa geri donus isigi veriyor | OK | Secili aksiyon tetiklenince tum satir hafif mavi bir flash ile geri bildirim veriyor |
| 2026-06-14 | v1.108 | OK | Yardim durum satiri geri donus isigi + build + self-test tamamlandi |

## v1.107 - Yardim Mikro Vurgu Kademesi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi mikro vurgu kademeli | OK | Prefix, tekrar ve kisayol bloklari farkli siddetlerde belirginlesiyor |
| PDF yardimi mikro vurgu kademeli | OK | Prefix, tekrar ve kisayol bloklari farkli siddetlerde belirginlesiyor |
| 2026-06-14 | v1.107 | OK | Yardim mikro vurgu kademesi + build + self-test tamamlandi |

## v1.106 - Yardim Kisayol Ipucunda Aksiyon Varyanti
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi kisayol ipucu aksiyona gore degisiyor | OK | `Enter/Space Doldur`, `Kopyala`, `Uygula` gibi varyantlar gorunuyor |
| PDF yardimi kisayol ipucu aksiyona gore degisiyor | OK | `Enter/Space Sec` ve `Ac` varyantlari gorunuyor |
| 2026-06-14 | v1.106 | OK | Yardim kisayol ipucunda aksiyon varyanti + build + self-test tamamlandi |

## v1.105 - Yardim Tekrar Rozetinde Aksiyon Etiketi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi tekrar rozeti aksiyona gore degisiyor | OK | `DOLDUR`, `KOPYALA`, `UYGULA` etiketleri gorunuyor |
| PDF yardimi tekrar rozeti aksiyona gore degisiyor | OK | `SEC` ve `AC` etiketleri gorunuyor |
| 2026-06-14 | v1.105 | OK | Yardim tekrar rozetinde aksiyon etiketi + build + self-test tamamlandi |

## v1.104 - Yardim Prefixinde Aksiyon Etiketi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi prefixi aksiyona gore degisiyor | OK | `KLN`, `SON`, `SEC` gibi kisaltmalar secili yardimi gosteriyor |
| PDF yardimi prefixi aksiyona gore degisiyor | OK | `SEC` ve `AC` kisaltmalari secili yardimi gosteriyor |
| 2026-06-14 | v1.104 | OK | Yardim prefixinde aksiyon etiketi + build + self-test tamamlandi |

## v1.103 - Yardim Metninde Aksiyon Tonu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi metni aksiyona gore ton farki aliyor | OK | Farkli yardim turleri hafif renk nuansi ile ayrisiyor |
| PDF yardimi metni aksiyona gore ton farki aliyor | OK | `PDF Sec` ve `PDF Ac` hafif renk nuansi ile ayrisiyor |
| 2026-06-14 | v1.103 | OK | Yardim metninde aksiyon tonu + build + self-test tamamlandi |

## v1.102 - Yardim Etiketlerinde Etkilesim Vurgusu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi yardimci etiketleri etkilesimde belirginlesiyor | OK | Tekrar ve kisayol etiketleri normal durumda sakin, hover/odakta daha net |
| PDF yardimi yardimci etiketleri etkilesimde belirginlesiyor | OK | Tekrar ve kisayol etiketleri normal durumda sakin, hover/odakta daha net |
| 2026-06-14 | v1.102 | OK | Yardim etiketlerinde etkilesim vurgusu + build + self-test tamamlandi |

## v1.101 - Yardim Durum Satiri Ince Ayrac
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi durum satirinda ince ayrac var | OK | Prefix ve ana metin arasinda hafif nokta ayraci gorunuyor |
| PDF yardimi durum satirinda ince ayrac var | OK | Prefix ve ana metin arasinda hafif nokta ayraci gorunuyor |
| 2026-06-14 | v1.101 | OK | Yardim durum satiri ince ayrac + build + self-test tamamlandi |

## v1.100 - Yardim Rozet Ton Dengeleme
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi rozet tonlari yumusadi | OK | Tekrar ve kisayol rozetleri ana metni bastirmiyor |
| PDF yardimi rozet tonlari yumusadi | OK | Tekrar ve kisayol rozetleri ana metni bastirmiyor |
| 2026-06-14 | v1.100 | OK | Yardim rozet ton dengeleme + build + self-test tamamlandi |

## v1.99 - Yardim Durum Satiri Kompakt Gorunum
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi durum satiri kompakt gorunuyor | OK | Rozet ve kisayol alanlari daha sik boyutlarla gosteriliyor |
| PDF yardimi durum satiri kompakt gorunuyor | OK | Rozet ve kisayol alanlari daha sik boyutlarla gosteriliyor |
| 2026-06-14 | v1.99 | OK | Yardim durum satiri kompakt gorunum + build + self-test tamamlandi |

## v1.98 - Yardim Durum Satiri Tekrar Rozeti
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi durum satirinda tekrar rozeti var | OK | `TEKRAR` rozeti prefix yaninda gorunuyor |
| PDF yardimi durum satirinda tekrar rozeti var | OK | `TEKRAR` rozeti prefix yaninda gorunuyor |
| 2026-06-14 | v1.98 | OK | Yardim durum satiri tekrar rozeti + build + self-test tamamlandi |

## v1.97 - Yardim Durum Satiri Kisayol Etiketi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi durum satirinda kisayol etiketi var | OK | `Enter/Space` etiketi gorunur halde |
| PDF yardimi durum satirinda kisayol etiketi var | OK | `Enter/Space` etiketi gorunur halde |
| 2026-06-14 | v1.97 | OK | Yardim durum satiri kisayol etiketi + build + self-test tamamlandi |

## v1.96 - Yardim Tooltip Dili Sadelestirme
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi secili durum tooltip dili sade | OK | Tooltip `Yeniden calistir: ...` biciminde kisa gosteriliyor |
| PDF yardimi secili durum tooltip dili sade | OK | Tooltip `Yeniden calistir: ...` biciminde kisa gosteriliyor |
| 2026-06-14 | v1.96 | OK | Yardim tooltip dili sadelestirme + build + self-test tamamlandi |

## v1.95 - Yardim Durum Satiri Klavye Odak Cercevesi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi durum satiri odak cercevesi gorunuyor | OK | Klavye odaginda mavi cerceve ve hafif vurgu cikiyor |
| PDF yardimi durum satiri odak cercevesi gorunuyor | OK | Klavye odaginda mavi cerceve ve hafif vurgu cikiyor |
| 2026-06-13 | v1.95 | OK | Yardim durum satiri klavye odak cercevesi + build + self-test tamamlandi |

## v1.94 - Yardim Durum Satiri Hover Focus Ipuclari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi durum satiri hover/focus ipucu veriyor | OK | Hover ve klavye odaginda metin alt cizgiyle belirginlesiyor |
| PDF yardimi durum satiri hover/focus ipucu veriyor | OK | Hover ve klavye odaginda metin alt cizgiyle belirginlesiyor |
| 2026-06-13 | v1.94 | OK | Yardim durum satiri hover/focus ipuclari + build + self-test tamamlandi |

## v1.93 - Tiklanabilir Yardim Durum Satiri
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi durum satiri tiklanabilir | OK | Durum satiri secili yardimi yeniden tetikleyen buton gibi davraniyor |
| PDF yardimi durum satiri tiklanabilir | OK | Durum satiri secili yardimi yeniden tetikleyen buton gibi davraniyor |
| 2026-06-13 | v1.93 | OK | Tiklanabilir yardim durum satiri + build + self-test tamamlandi |

## v1.92 - Yardim Durum Prefix Ipuclari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi durum satirinda YRD prefixi var | OK | Secili durum satiri `YRD` etiketiyle basliyor |
| PDF yardimi durum satirinda PDF prefixi var | OK | Secili durum satiri `PDF` etiketiyle basliyor |
| 2026-06-13 | v1.92 | OK | Yardim durum prefix ipuclari + build + self-test tamamlandi |

## v1.91 - Yardim Durum Satiri Mikro Vurgu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi durum satiri mikro vurgu aliyor | OK | Secili durum satiri kisa sureli daha belirginlesiyor |
| PDF yardimi durum satiri mikro vurgu aliyor | OK | Secili durum satiri kisa sureli daha belirginlesiyor |
| 2026-06-13 | v1.91 | OK | Yardim durum satiri mikro vurgu + build + self-test tamamlandi |

## v1.90 - Yardim Durum Satiri Kisa Yol Ipuclari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi durum satirinda klavye ipucu var | OK | `Enter/Space ile tekrar.` ifadesi gorunuyor |
| PDF yardimi durum satirinda klavye ipucu var | OK | `Enter/Space ile tekrar.` ifadesi gorunuyor |
| 2026-06-13 | v1.90 | OK | Yardim durum satiri kisa yol ipuclari + build + self-test tamamlandi |

## v1.89 - Durum Satiri Tooltip Birlesimi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi durum satiri tooltipi paylasiyor | OK | Durum satiri son aksiyon butonuyla ayni tooltip dilini kullaniyor |
| PDF yardimi durum satiri tooltipi paylasiyor | OK | Durum satiri son aksiyon butonuyla ayni tooltip dilini kullaniyor |
| 2026-06-13 | v1.89 | OK | Durum satiri tooltip birlesimi + build + self-test tamamlandi |

## v1.88 - Yardim Durum Satiri Tooltip Birlesimi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi durum satiri tooltipi var | OK | Durum satiri son aksiyon butonuyla ayni tooltip dilini kullaniyor |
| PDF yardimi durum satiri tooltipi var | OK | Durum satiri son aksiyon butonuyla ayni tooltip dilini kullaniyor |
| 2026-06-13 | v1.88 | OK | Yardim durum satiri tooltip birlesimi + build + self-test tamamlandi |

## v1.87 - Secili Yardim Durum Satiri
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi secili durum satiri var | OK | `Secili yardim: ... hazir.` metni gorunuyor |
| PDF yardimi secili durum satiri var | OK | `Secili yardim: ... hazir.` metni gorunuyor |
| 2026-06-13 | v1.87 | OK | Secili yardim durum satiri + build + self-test tamamlandi |

## v1.86 - Yardim Rozeti Mikro Vurgu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi rozeti mikro vurgu aliyor | OK | Son secilen rozet kisa sureli `YENI` etiketi ve daha guclu vurgu aliyor |
| PDF yardimi rozeti mikro vurgu aliyor | OK | Son secilen rozet kisa sureli `YENI` etiketi ve daha guclu vurgu aliyor |
| 2026-06-13 | v1.86 | OK | Yardim rozeti mikro vurgu + build + self-test tamamlandi |

## v1.85 - Secili Yardim Rozeti Ipuclari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardimi secili rozette aktif etiketi var | OK | Secili rozetlerde `AKTIF` isareti gorunuyor |
| PDF yardimi secili rozette aktif etiketi var | OK | Secili rozetlerde `AKTIF` isareti gorunuyor |
| 2026-06-13 | v1.85 | OK | Secili yardim rozeti ipuclari + build + self-test tamamlandi |

## v1.84 - Replay Ozet Tooltip Genisleme
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme replay ozet metninde tooltip var | OK | Replay aciklamasi artik summary text ustunde de okunuyor |
| PDF replay ozet metninde tooltip var | OK | Replay aciklamasi artik summary text ustunde de okunuyor |
| 2026-06-13 | v1.84 | OK | Replay ozet tooltip genisleme + build + self-test tamamlandi |

## v1.83 - Replay Indicator UI Smoke Checklist
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Replay indicator UI smoke checklist var | OK | `docs/08-replay-indicator-ui-smoke-checklist.md` eklendi |
| Replay odeme/PDF akis adimlari belgeli | OK | Odeme yardimi, PDF yardimi ve bos durum kontrol adimlari yazildi |
| 2026-06-13 | v1.83 | OK | Replay indicator UI smoke checklist + build + self-test tamamlandi |

## v1.82 - Replay Indicator Self-Test
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Replay indicator aktif pattern asserti var | OK | Aktif desen `***|||` dogrudan kontrol ediliyor |
| Replay indicator tooltip assertleri var | OK | Actionli ve bos durum tooltip metinleri self-test ile korunuyor |
| 2026-06-13 | v1.82 | OK | Replay indicator self-test + build + self-test tamamlandi |

## v1.81 - Replay Indicator Helper
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Replay indicator mantigi ortak helperda | OK | Indicator ve tooltip metin kararlari tek formatter icine tasindi |
| InvoicesView sadeleşti | OK | Tekrarlayan replay yardim mantigi view icinden cikartildi |
| 2026-06-13 | v1.81 | OK | Replay indicator helper + build + self-test tamamlandi |

## v1.80 - Replay Tooltip Ton Sadeleştirmesi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Actionli tooltip tonu daha kompakt | OK | Son cumle `Hazir` kadar kisa tutuldu |
| Actionsiz tooltip tonu daha kisa | OK | Yonlendirme cumlesi daha sikistirilmis yardim tonuna cekildi |
| 2026-06-13 | v1.80 | OK | Replay tooltip ton sadeleştirmesi + build + self-test tamamlandi |

## v1.79 - Replay Bekleme Dili Uyumu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Actionli tooltip bekleme dili yumusak | OK | Bekleme hali `Replay hazir` tonuna cekildi |
| Actionsiz tooltip bekleme dili yonlendirici | OK | Replay yardiminin ne zaman gorunecegi daha acik anlatiliyor |
| 2026-06-13 | v1.79 | OK | Replay bekleme dili uyumu + build + self-test tamamlandi |

## v1.78 - Replay Aktiflik Dili Uyumu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Actionli tooltip aktiflik dili uyumlu | OK | Replay aktifken tooltip `Yeniden tetiklendi` ifadesini kullaniyor |
| Actionsiz tooltip aktiflik dili uyumlu | OK | Replay aktifken hazir metni de yeniden tetiklenme diliyle yaklasti |
| 2026-06-13 | v1.78 | OK | Replay aktiflik dili uyumu + build + self-test tamamlandi |

## v1.77 - Replay Dili Uyumu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Actionli replay tooltip dili uyumlu | OK | Tooltip action varsa replay ayari / vurgu kalibini kullaniyor |
| Actionsiz replay tooltip dili uyumlu | OK | Tooltip replay ayari hazir kalibini koruyor |
| 2026-06-13 | v1.77 | OK | Replay dili uyumu + build + self-test tamamlandi |

## v1.76 - Replay Tooltip Metin Sikilastirmasi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Actionli replay tooltip daha kisa | OK | Action varsa tooltip dogrudan action replay ozeti veriyor |
| Actionsiz replay tooltip daha yonlendirici | OK | Action yoksa replay'in ne zaman canlanacagi anlatiliyor |
| 2026-06-13 | v1.76 | OK | Replay tooltip metin sikilastirmasi + build + self-test tamamlandi |

## v1.75 - Replay Tooltip Action Adi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme replay tooltip action adi var | OK | Tooltip kaynak action adini acikca gosteriyor |
| PDF replay tooltip action adi var | OK | Tooltip kaynak action adini acikca gosteriyor |
| 2026-06-13 | v1.75 | OK | Replay tooltip action adi + build + self-test tamamlandi |

## v1.74 - Replay Tooltip Durum Mesaji
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme replay tooltip durum mesaji var | OK | Tooltip replay aktif veya beklemede bilgisini de veriyor |
| PDF replay tooltip durum mesaji var | OK | Tooltip replay aktif veya beklemede bilgisini de veriyor |
| 2026-06-13 | v1.74 | OK | Replay tooltip durum mesaji + build + self-test tamamlandi |

## v1.73 - Replay Isaret Tooltipi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme replay tooltipi var | OK | Mini isaret ve prefix alani tooltip ile aciklama veriyor |
| PDF replay tooltipi var | OK | Mini isaret ve prefix alani tooltip ile aciklama veriyor |
| 2026-06-13 | v1.73 | OK | Replay isaret tooltipi + build + self-test tamamlandi |

## v1.72 - Replay Sekil Ayrimi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Actionli replay isareti daha canli | OK | Sure izi action varsa dik cizgilerle gosteriliyor |
| Pasif replay isareti daha sakin | OK | Sure izi action yoksa noktalı izlerle gosteriliyor |
| 2026-06-13 | v1.72 | OK | Replay sekil ayrimi + build + self-test tamamlandi |

## v1.71 - Replay Sure Izi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme replay sure isareti var | OK | Prefix icinde sure 1-4 arasi cizgi yogunluguyla ayristiriliyor |
| PDF replay sure isareti var | OK | Prefix icinde sure 1-4 arasi cizgi yogunluguyla ayristiriliyor |
| 2026-06-13 | v1.71 | OK | Replay sure izi + build + self-test tamamlandi |

## v1.70 - Replay Vurgu Seviye Isareti
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme replay seviye isareti var | OK | Prefix icinde dusuk/orta/guclu vurgu seviyesi nokta yogunluguyla ayristiriliyor |
| PDF replay seviye isareti var | OK | Prefix icinde dusuk/orta/guclu vurgu seviyesi nokta yogunluguyla ayristiriliyor |
| 2026-06-13 | v1.70 | OK | Replay seviye isareti + build + self-test tamamlandi |

## v1.69 - Replay Ozet Prefix Canlanmasi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme replay ozet prefixi canlaniyor | OK | Replay aktifken ozet prefixi de yesil replay vurgusu aliyor |
| PDF replay ozet prefixi canlaniyor | OK | Replay aktifken ozet prefixi de mavi replay vurgusu aliyor |
| 2026-06-13 | v1.69 | OK | Replay ozet prefix canlanmasi + build + self-test tamamlandi |

## v1.68 - Replay Ozet Prefix Isareti
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme replay ozet prefixi var | OK | Son aksiyona gore `KLN`, `SON`, `SEC` veya bos durumda `AYR` gorunuyor |
| PDF replay ozet prefixi var | OK | Son aksiyona gore `SEC`, `AC` veya bos durumda `AYR` gorunuyor |
| 2026-06-13 | v1.68 | OK | Replay ozet prefixi + build + self-test tamamlandi |

## v1.67 - Replay Ozet Ton Ayrimi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme replay ozeti ton ayriyor | OK | Son aksiyon varsa yesil, yoksa notr ton kullaniliyor |
| PDF replay ozeti ton ayriyor | OK | Son aksiyon varsa mavi, yoksa notr ton kullaniliyor |
| 2026-06-13 | v1.67 | OK | Replay ozet ton ayrimi + build + self-test tamamlandi |

## v1.66 - Replay Bos Durum Metni
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardim bos durum replay ozeti sakin | OK | Son aksiyon yokken `Replay ayari hazir` ifadesi kullaniliyor |
| PDF yardim bos durum replay ozeti sakin | OK | Son aksiyon yokken `Replay ayari hazir` ifadesi kullaniliyor |
| 2026-06-13 | v1.66 | OK | Replay bos durum metni + build + self-test tamamlandi |

## v1.65 - Baglamsal Replay Ozet Satiri
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardim replay ozeti baglamsal | OK | `Kalan Tutar`, `Son Aciklama`, `Secili Odeme` gibi aksiyon adlari ozet satirinda okunuyor |
| PDF yardim replay ozeti baglamsal | OK | `PDF Sec` veya `PDF Ac` aksiyon adlari ozet satirinda okunuyor |
| 2026-06-13 | v1.65 | OK | Baglamsal replay ozeti + build + self-test tamamlandi |

## v1.64 - Replay Tercih Ozeti
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardim replay ozeti var | OK | Sure ve vurgu seviyesi odeme yardim alaninda metinle gorunuyor |
| PDF yardim replay ozeti var | OK | Sure ve vurgu seviyesi PDF yardim alaninda metinle gorunuyor |
| 2026-06-13 | v1.64 | OK | Replay tercih ozeti + build + self-test tamamlandi |

## v1.63 - Kisayol Replay Tercihleri
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Replay suresi tercihi var | OK | 1-4 saniye arasi replay suresi preference olarak secilebiliyor |
| Replay vurgu tercihi var | OK | Dusuk/Orta/Guclu vurgu seviyesi preference olarak secilebiliyor |
| 2026-06-13 | v1.63 | OK | Replay tercihleri + build + self-test tamamlandi |

## v1.62 - Prefix Rozette Tekrar Canlanmasi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardim prefix replay canlanmasi var | OK | Tekrar aksiyonunda yesil rozet gecici olarak daha parlak ve beyaz yazili oluyor |
| PDF yardim prefix replay canlanmasi var | OK | Tekrar aksiyonunda mavi rozet gecici olarak daha parlak ve beyaz yazili oluyor |
| 2026-06-13 | v1.62 | OK | Prefix replay canlanmasi + build + self-test tamamlandi |

## v1.61 - Mikro Kisayol Tekrar Geri Bildirimi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardim tekrar geri bildirimi var | OK | Mikro kisayol tekrarlandiginda satir gecici olarak `yeniden tetiklendi` eki aliyor |
| PDF yardim tekrar geri bildirimi var | OK | Mikro kisayol tekrarlandiginda satir gecici olarak `yeniden tetiklendi` eki aliyor |
| 2026-06-13 | v1.61 | OK | Tekrar geri bildirimi + build + self-test tamamlandi |

## v1.60 - Aktif Prefix Rozet Vurgusu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardim prefix rozeti aktif vurgulu | OK | Hover ve klavye odaginda yesil rozet daha belirgin gorunuyor |
| PDF yardim prefix rozeti aktif vurgulu | OK | Hover ve klavye odaginda mavi rozet daha belirgin gorunuyor |
| 2026-06-13 | v1.60 | OK | Aktif prefix vurgusu + build + self-test tamamlandi |

## v1.59 - Mikro Kisayol Prefix Rozetleri
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardim prefix rozeti var | OK | Son kullanilan odeme yardimi `KLN`, `SON`, `SEC` gibi kisa rozetle okunuyor |
| PDF yardim prefix rozeti var | OK | Son kullanilan PDF yardimi `SEC` veya `AC` gibi kisa rozetle okunuyor |
| 2026-06-13 | v1.59 | OK | Prefix rozeti + build + self-test tamamlandi |

## v1.58 - Mikro Kisayol Odak Gorunurlugu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardim mikro kisayolu hover izi var | OK | Hover durumunda hafif arka plan ve alt cizgi gorunuyor |
| PDF yardim mikro kisayolu klavye odagi var | OK | Klavye odaginda daha belirgin arka plan ve alt cizgi gorunuyor |
| 2026-06-13 | v1.58 | OK | Mikro kisayol odak gorunurlugu + build + self-test tamamlandi |

## v1.57 - Son Aksiyon Mikro Kisayollari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardim mikro kisayol tooltipi var | OK | Son aksiyon satiri secili yardimi yeniden calistirmayi anlatan tooltip uretiyor |
| PDF yardim mikro kisayol tooltipi var | OK | Son aksiyon satiri secili PDF yardimini yeniden calistirmayi anlatan tooltip uretiyor |
| 2026-06-13 | v1.57 | OK | Mikro kisayol satiri + build + self-test tamamlandi |

## v1.56 - Son Aksiyon Satirinda Gecici Vurgu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardim son aksiyon vurgu aliyor | OK | Son aksiyon satiri guncellenince kisa sureli daha parlak yesil ve kalin yazi ile gosteriliyor |
| PDF yardim son aksiyon vurgu aliyor | OK | Son aksiyon satiri guncellenince kisa sureli daha parlak mavi ve kalin yazi ile gosteriliyor |
| 2026-06-13 | v1.56 | OK | Son aksiyon vurgusu + build + self-test tamamlandi |

## v1.55 - Odeme Yardiminda Son Aksiyon Satiri
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardim son aksiyon satiri var | OK | Son kullanilan `fill_remaining`, `use_last` ve `use_selected` yardimlari kisa alt metinle okunuyor |
| PDF yardim son aksiyon satiri var | OK | Son kullanilan `select_pdf` ve `open_pdf` yardimlari kisa alt metinle okunuyor |
| 2026-06-13 | v1.55 | OK | Son aksiyon satiri + build + self-test tamamlandi |

## v1.54 - Baglamsal Odeme Durum Mesajlari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Odeme yardim mesaji etiketli | OK | Rozetten gelen odeme yardim mesajlari `Odeme Yardimi` etiketiyle uretiliyor |
| PDF yardim mesaji etiketli | OK | Rozetten gelen odeme PDF mesajlari `PDF Yardimi` etiketiyle uretiliyor |
| 2026-06-13 | v1.54 | OK | Baglamsal odeme durum mesajlari + build + self-test tamamlandi |

## v1.53 - Tiklanabilir Odeme PDF Rozetleri
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| PDF rozetleri aksiyon tasiyor | OK | Uygun durumlarda rozetler `select_pdf` veya `open_pdf` aksiyon anahtarlarini tasiyor |
| Son kullanilan PDF rozeti secilebiliyor | OK | Son kullanilan PDF yardim rozeti secim vurgusuna uygun `IsSelected` durumu tasiyor |
| 2026-06-13 | v1.53 | OK | Tiklanabilir odeme PDF rozetleri + build + self-test tamamlandi |

## v1.52 - Odeme PDF Yardim Ozeti
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| PDF yardim rozetleri uretiliyor | OK | `PDF Bekleniyor`, `PDF Kayitli`, `PDF Kayip` durumlari uygun sekilde rozet uretiyor |
| Bos durum metni var | OK | Secili odeme yoksa `PDF islemleri icin secili odeme yok.` metni kullaniliyor |
| 2026-06-13 | v1.52 | OK | Odeme PDF yardim ozeti + build + self-test tamamlandi |

## v1.51 - Odeme Yardim Rozeti Klavye Gorunurlugu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Tooltip klavye ipucu tasiyor | OK | Odeme yardim rozetleri tooltip icinde `Enter/Space` ipucunu gosteriyor |
| Odak gorunurlugu eklendi | OK | Odakli rozet daha belirgin cerceveyle gorunuyor |
| 2026-06-13 | v1.51 | OK | Odeme yardim rozeti klavye gorunurlugu + build + self-test tamamlandi |

## v1.50 - Tiklanabilir Odeme Yardim Rozetleri
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Rozetler aksiyon anahtari tasiyor | OK | Yardim rozetleri `fill_remaining`, `use_last`, `use_selected` aksiyon anahtarlarini tasiyor |
| Son kullanilan rozet secilebiliyor | OK | Son kullanilan yardim rozeti secim vurgusuna uygun `IsSelected` durumu tasiyor |
| 2026-06-13 | v1.50 | OK | Tiklanabilir odeme yardim rozetleri + build + self-test tamamlandi |

## v1.49 - Odeme Yardim Ozeti
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Yardim rozetleri uretiliyor | OK | `Kalan Tutar`, `Son Aciklama`, `Secili Odeme` yardimlari uygun kosullarda ozet rozetlere donusuyor |
| Bos durum metni var | OK | Yardim yoksa `Hazir odeme yardimi yok.` metni kullaniliyor |
| 2026-06-13 | v1.49 | OK | Odeme yardim ozeti + build + self-test tamamlandi |

## v1.48 - Review Context UI Smoke Checklist
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| --health-check basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --health-check temiz gecti |
| UI smoke checklist hazir | OK | `docs/07-review-context-ui-smoke-checklist.md` review baglam cipi akislari icin tekrar kullanilabilir elle dogrulama adimlarini iceriyor |
| 2026-06-13 | v1.48 | OK | Review context UI smoke checklist + build + self-test + health-check tamamlandi |

## v1.47 - Baglam Durum Cubugu Mikro Vurgusu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kaynak etiketi ayristiriliyor | OK | `Çip`, `Klavye`, `Menü` durum mesajlari formatter seviyesinde ayristiriliyor |
| Mikro vurgu mantigi korundu | OK | Kaynakli durum mesajlari kisa sureli belirgin gorunup sonra varsayilan renge donuyor |
| 2026-06-13 | v1.47 | OK | Baglam durum cubugu mikro vurgusu + build + self-test tamamlandi |

## v1.46 - Baglam Cipi Self-Test Guvencesi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Tooltip kisayollari dogrulaniyor | OK | Baglam cipi tooltip'lerinde `Enter/Space`, `Ctrl+C`, `Shift+F10` ipuclari self-test ile kontrol ediliyor |
| Kisa mesaj bicimleri dogrulaniyor | OK | `Çip`, `Klavye`, `Menü` kisa durum mesaji formatlari self-test ile kontrol ediliyor |
| 2026-06-13 | v1.46 | OK | Baglam cipi self-test guvencesi + build + self-test tamamlandi |

## v1.45 - Baglam Cipi Kisa Durum Mesajlari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Cip mesajlari kisaldi | OK | Cip aksiyonlari durum cubugunda `Çip`, `Klavye`, `Menü` etiketiyle daha kisa gorunuyor |
| Ana dugme mesajlari korundu | OK | Baglam panelindeki ana aksiyon dugmeleri eski `Bağlam: ...` mesaj formatini koruyor |
| 2026-06-13 | v1.45 | OK | Baglam cipi kisa durum mesajlari + build + self-test tamamlandi |

## v1.44 - Baglam Cipi Escape Odak Cikisi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Esc odagi temiz alana tasiyor | OK | Odaktaki ciptte `Esc` odagi once baglam checkbox'ina, gerekirse not alanina tasiyor |
| Diger klavye kisayollari korunuyor | OK | `Enter/Space`, `Ctrl+C` ve `Shift+F10` davranislari etkilenmeden calismaya devam ediyor |
| 2026-06-13 | v1.44 | OK | Baglam cipi Escape odak cikisi + build + self-test tamamlandi |

## v1.43 - Baglam Cipi Tooltip Kisayol Ipuclari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Tooltip kisayol ipucu veriyor | OK | Cip tooltip'leri tik davranisinin yaninda `Enter/Space`, `Ctrl+C` ve `Shift+F10` bilgisini de tasiyor |
| Aksiyon ve kopya ayirimi korunuyor | OK | Aksiyonlu ve kopya ciplerinde ana davranis aciklamasi korunurken ortak kisayol ipuclari eklendi |
| 2026-06-13 | v1.43 | OK | Baglam cipi tooltip kisayol ipuclari + build + self-test tamamlandi |

## v1.42 - Baglam Cipi Hizli Klavye Kisayollari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Enter ana aksiyonu calistiriyor | OK | Odaktaki cipte `Enter` veya `Space` birincil davranisi tetikliyor |
| Ctrl+C kopyaliyor | OK | Odaktaki cipte `Ctrl+C` metni panoya kopyalarken son kullanilan cip kaydini da guncelliyor |
| 2026-06-13 | v1.42 | OK | Baglam cipi hizli klavye kisayollari + build + self-test tamamlandi |

## v1.41 - Son Baglam Cipi Vurgusu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Son kullanilan cip vurgulaniyor | OK | Tiklanan veya menuden kullanilan cip ayni baglamda belirgin cerceveyle secili kaliyor |
| Baglam degisince vurgu sifirlaniyor | OK | Yeni baglam imzasinda onceki cip secimi otomatik temizleniyor |
| 2026-06-13 | v1.41 | OK | Son baglam cipi vurgusu + build + self-test tamamlandi |

Bu dosya, her geliştirme fazından sonra mevcut çalışan davranışların bozulmadığını kontrol etmek için tutulur. Yeni Codex chatlerinde önce bu dosya okunmalı, sonra ilgili fazın testleri çalıştırılmalıdır.

## Kullanım

Her faz sonunda:

1. Genel kontroller yapılır.
2. Tamamlanmış tüm önceki fazların kritik kontrolleri tekrar edilir.
3. Yeni fazın kontrolleri eklenir ve sonuç kaydı düşülür.
4. Başarısız madde varsa bir sonraki faza geçilmez.

Sonuç etiketleri:

- `OK`: Sorun yok.
- `FAIL`: Sorun var.
- `N/A`: Bu faz için uygulanabilir değil.
- `TODO`: Henüz test edilmedi.

## Genel Kontroller

| Kontrol | Durum | Not |
|---|---|---|
| Proje klasörü doğru yerde açılıyor | OK | `C:\Users\Asus\Documents\FATURA TAKİP PROGRAMI` |
| Git durumu kontrol edildi | OK | `codex/v0.19-manuel-guvenli-yedekleme` branch'i üzerinde çalışılıyor |
| Markdown dokümantasyonu UTF-8 olarak okunuyor | OK | Plan dosyası UTF-8 korunarak kopyalandı |
| Yeni dosya adları yol sorunu azaltmak için ASCII tutuluyor | OK | Kök klasör Türkçe, proje içi doküman adları ASCII |
| `README.md` yeni chat başlangıcını açıklıyor | OK | Oluşturuldu |
| Roadmap mevcut | OK | `ROADMAP.md` |
| Regresyon dosyası mevcut | OK | `REGRESYON.md` |

## v0.1 - Proje İskeleti ve Veritabanı

| Kontrol | Durum | Not |
|---|---|---|
| WPF uygulaması derleniyor | OK | `dotnet build FaturaTakip.sln` başarılı, 0 hata |
| Uygulama açılıyor | OK | Kısa başlatma testi başarılı, uygulama erken kapanmadı |
| `database` klasörü oluşturuluyor | OK | `.gitkeep` ile takip ediliyor, runtime DB ignore ediliyor |
| `attachments` klasörü oluşturuluyor | OK | `attachments/invoices` ve `attachments/payments` hazır |
| `backups` klasörü oluşturuluyor | OK | `.gitkeep` ile takip ediliyor |
| `logs` klasörü oluşturuluyor | OK | `.gitkeep` ile takip ediliyor |
| `exports` klasörü oluşturuluyor | OK | `.gitkeep` ile takip ediliyor |
| `database/fatura_takip.db` oluşturuluyor | OK | Health-check sonrası oluştu ve git tarafından ignore ediliyor |
| Migration başlangıç mantığı tekrar çalıştırıldığında hata vermiyor | OK | Health-check iki kez çalıştırıldı |
| Bu fazda veri giriş ekranı eklenmedi | OK | Sadece boş dashboard ve sistem durumu var |
| Bu fazda rapor ekranı eklenmedi | OK | Rapor ekranı eklenmedi |

## v0.2 - Fatura Türleri Yönetimi

| Kontrol | Durum | Not |
|---|---|---|
| Fatura türü eklenebiliyor | OK | `--self-test` repository ekleme akışını doğruladı |
| Fatura türü düzenlenebiliyor | OK | `--self-test` repository güncelleme akışını doğruladı |
| Fatura türü aktif/pasif yapılabiliyor | OK | `--self-test` pasife alma akışını doğruladı |
| Kullanım birimi saklanıyor | OK | `--self-test` güncel kullanım birimini doğruladı |
| Silme yerine pasif yapma davranışı korunuyor | OK | UI ve repository fiziksel silme sunmuyor |
| Başlangıç türleri seed ediliyor | OK | Self-test en az 6 başlangıç türünü doğruladı |
| Aynı isimli fatura türü engelleniyor | OK | Repository isim benzersizliği kontrolü içeriyor |

## v0.3 - Abonelik Yönetimi

| Kontrol | Durum | Not |
|---|---|---|
| Abonelik eklenebiliyor | OK | `--self-test` repository ekleme akışını doğruladı |
| Abonelik düzenlenebiliyor | OK | `--self-test` repository güncelleme akışını doğruladı |
| Abonelik aktif/pasif yapılabiliyor | OK | `--self-test` pasife alma akışını doğruladı |
| Abonelik fatura türüne bağlanıyor | OK | `--self-test` `invoice_type_id` bağını doğruladı |
| Pasif abonelik geçmiş kayıt mantığını bozmayacak şekilde saklanıyor | OK | UI ve repository fiziksel silme sunmuyor |
| Abonelik listesi filtrelenebiliyor | OK | Tür, durum ve metin araması UI içinde destekleniyor |

## v0.4 - Fatura Kayıt Altyapısı

| Kontrol | Durum | Not |
|---|---|---|
| Fatura aboneliğe bağlı kaydediliyor | OK | `--self-test` fatura-abonelik bağını doğruladı |
| Negatif tutar reddediliyor | OK | `--self-test` negatif tutar senaryosunu doğruladı |
| Negatif kullanım reddediliyor | OK | `--self-test` negatif kullanım senaryosunu doğruladı |
| Aynı abonelikte aynı fatura no tekrarına izin verilmiyor | OK | `--self-test` aynı abonelikte tekrar fatura no senaryosunu doğruladı |
| Son ödeme tarihi fatura tarihinden önceyse uyarı veriliyor | OK | `--self-test` tarih uyarısı üretimini doğruladı |
| PDF olmadan temel fatura kaydı yapılabiliyor | OK | Fatura formunda PDF alanı yok, temel kayıt self-test ile doğrulandı |

## v0.5 - Fatura PDF Evrakı Ekleme

| Kontrol | Durum | Not |
|---|---|---|
| Faturaya PDF eklenebiliyor | OK | `--self-test` PDF ekleme akışını doğruladı |
| PDF uygulama klasörüne kopyalanıyor | OK | Hedef klasör `attachments/invoices/yyyy/MM` altında oluşturuluyor |
| PDF orijinal adı saklanıyor | OK | `--self-test` orijinal dosya adını doğruladı |
| PDF SHA-256 hash bilgisi saklanıyor | OK | `--self-test` hash alanının dolduğunu doğruladı |
| PDF olmayan dosya reddediliyor | OK | `--self-test` PDF olmayan dosya ekleme girişimini doğruladı |
| Kayıp PDF dosyası algılanıyor | OK | `--self-test` kopyalanan dosya silinince eksik durumunu doğruladı |
| Kayıtlı PDF açma akışı mevcut | OK | UI kayıtlı PDF'i varsayılan sistem uygulamasıyla açıyor |
| PDF eksikliği görülebiliyor | OK | Fatura ekranında PDF eksik sayısı ve satır PDF durumu gösteriliyor |

## v0.6 - Fatura Listesi ve Filtreleme

| Kontrol | Durum | Not |
|---|---|---|
| Fatura listesi yıla göre filtrelenebiliyor | OK | `--self-test` yıl filtresini doğruladı |
| Fatura listesi aya göre filtrelenebiliyor | OK | `--self-test` ay filtresini doğruladı |
| Fatura listesi fatura türüne göre filtrelenebiliyor | OK | `--self-test` tür filtresini doğruladı |
| Fatura listesi aboneliğe göre filtrelenebiliyor | OK | `--self-test` abonelik filtresini doğruladı |
| Fatura listesi ödeme durumuna göre filtrelenebiliyor | OK | `--self-test` ödenmiş ve gecikmiş filtrelerini doğruladı |
| Fatura listesi PDF durumuna göre filtrelenebiliyor | OK | `--self-test` PDF var ve PDF eksik filtrelerini doğruladı |
| Fatura listesi metin/fatura no ile aranabiliyor | OK | `--self-test` çok terimli aramayı doğruladı |
| Filtreler temizlenebiliyor | OK | UI üzerinde filtreleri temizleme düğmesi eklendi |

## v0.7 - Ödeme Kayıt Altyapısı

| Kontrol | Durum | Not |
|---|---|---|
| Faturaya ödeme kaydı eklenebiliyor | OK | `--self-test` ödeme ekleme akışını doğruladı |
| Ödeme tarihi, tutarı ve açıklaması saklanıyor | OK | `--self-test` ödeme açıklamasını ve ödeme listesini doğruladı |
| Kısmi ödeme faturayı erken ödendi yapmıyor | OK | `--self-test` kısmi ödeme sonrası `unpaid` ve `Kısmi` durumunu doğruladı |
| Tam ödeme faturayı ödendi yapıyor | OK | `--self-test` tam ödeme sonrası `paid` durumunu doğruladı |
| Ödenen ve kalan tutar gösteriliyor | OK | Fatura listesi ve ödeme formu ödenen/kalan tutarı gösteriyor |
| Kalan tutarı aşan ödeme engelleniyor | OK | `--self-test` kalan aşımı senaryosunu doğruladı |
| Negatif ödeme tutarı reddediliyor | OK | `--self-test` negatif ödeme senaryosunu doğruladı |
| Olmayan faturaya ödeme eklenemiyor | OK | `--self-test` geçersiz fatura senaryosunu doğruladı |
| Fatura tutarı değişince ödeme durumu yeniden hesaplanıyor | OK | `--self-test` tutar artırma sonrası kalan ödeme durumunu doğruladı |
| Bu fazda ödeme PDF evrakı eklenmedi | OK | Kapsam v0.8'e bırakıldı |

## v0.8 - Ödeme Evrakı PDF Ekleme

| Kontrol | Durum | Not |
|---|---|---|
| Ödeme kaydına PDF eklenebiliyor | OK | `--self-test` ödeme PDF ekleme akışını doğruladı |
| Ödeme PDF'i uygulama klasörüne kopyalanıyor | OK | Hedef klasör `attachments/payments/yyyy/MM` altında oluşturuluyor |
| Ödeme PDF orijinal adı saklanıyor | OK | `--self-test` orijinal dosya adını doğruladı |
| Ödeme PDF SHA-256 hash bilgisi saklanıyor | OK | `--self-test` hash alanının dolduğunu doğruladı |
| Ödeme PDF olmayan dosya reddediliyor | OK | `--self-test` PDF olmayan ödeme dosyası ekleme girişimini doğruladı |
| Kayıp ödeme PDF dosyası algılanıyor | OK | `--self-test` kopyalanan ödeme PDF'i silinince eksik durumunu doğruladı |
| Kayıtlı ödeme PDF açma akışı mevcut | OK | UI seçili ödeme PDF'ini varsayılan sistem uygulamasıyla açıyor |
| Ödeme PDF eksikliği görülebiliyor | OK | Ödeme geçmişi satırında PDF durumu ve seçili ödeme PDF bilgi alanı gösteriliyor |
| Bu fazda rapor, dışa aktarım ve yedekleme eklenmedi | OK | Kapsam v0.9+ fazlara bırakıldı |

## v0.9 - Ana Gösterge Paneli

| Kontrol | Durum | Not |
|---|---|---|
| Bu ay fatura toplamı gösteriliyor | OK | `DashboardSummaryCalculator` ve `--self-test` doğruladı |
| Bu ay ödeme toplamı gösteriliyor | OK | `DashboardSummaryCalculator` ve `--self-test` doğruladı |
| Ödenmemiş fatura sayısı ve kalan toplam gösteriliyor | OK | `DashboardSummaryCalculator` ve `--self-test` doğruladı |
| Gecikmiş fatura sayısı ve kalan toplam gösteriliyor | OK | `DashboardSummaryCalculator` ve `--self-test` doğruladı |
| Fatura PDF eksikleri gösteriliyor | OK | `DashboardSummaryCalculator` ve `--self-test` doğruladı |
| Ödeme PDF eksikleri gösteriliyor | OK | `DashboardSummaryCalculator` ve `--self-test` doğruladı |
| Temel kayıt sayıları korunuyor | OK | Fatura türü, aktif tür, aktif abonelik ve toplam fatura dashboard içinde gösteriliyor |
| Bu fazda rapor ekranı eklenmedi | OK | Rapor ekranları v0.10+ fazlara bırakıldı |

## v0.10 - Ödenmemiş ve Gecikmiş Faturalar Raporu

| Kontrol | Durum | Not |
|---|---|---|
| Rapor ekranı açılıyor | OK | Sol menüde `Raporlar` sekmesi |
| Ödenmemiş listesi gösteriliyor | OK | `ActionableInvoiceReportCalculator` ile hesaplanır |
| Gecikmiş listesi gösteriliyor | OK | `unpaid` ve son ödeme tarihi bugünden önce |
| Yaklaşan listesi gösteriliyor | OK | `unpaid` ve son ödeme tarihi bugün–7 gün |
| Üst özetler (sayı/kalan) gösteriliyor | OK | Ödenmemiş, gecikmiş, yaklaşan için ayrı tile |
| Liste kolonları beklenen alanları içeriyor | OK | Tür, abonelik, kurum, dönem, no, son ödeme, tutar, ödenen, kalan, PDF |
| `--self-test` rapor hesaplarını doğruluyor | OK | `ActionableInvoiceReportCalculator` senaryosu eklendi |

## v0.11 - Aylık Fatura Listesi

| Kontrol | Durum | Not |
|---|---|---|
| Aylık liste sekmesi açılıyor | OK | Raporlar ekranında `Aylık Liste` sekmesi |
| Yıl/ay seçimi ile liste güncelleniyor | OK | Dönem seçimi UI üzerinden değişebilir |
| Üst özetler (toplam/ödenen/kalan) gösteriliyor | OK | Aylık rapor özetleri tile olarak gösterilir |
| Ödenmemiş ve gecikmiş adetleri hesaplanıyor | OK | Aylık raporda ödenmemiş/gecikmiş adetleri detayda gösterilir |
| PDF eksik sayısı hesaplanıyor | OK | Aylık raporda PDF eksik sayısı gösterilir |
| `--self-test` aylık rapor hesaplarını doğruluyor | OK | `MonthlyInvoiceReportCalculator` senaryosu eklendi |

## v0.12 - Türe Özgü Aylık Fatura Listesi

| Kontrol | Durum | Not |
|---|---|---|
| Aylık liste için tür filtresi var | OK | Dönem filtresine `Fatura Türü` seçimi eklendi |
| Tür seçimiyle liste daralıyor | OK | Seçilen tür + yıl + ay için faturalar listelenir |
| `--self-test` tür filtresini doğruluyor | OK | `invoiceTypeId` filtresi için senaryo eklendi |

## v0.13 - Aboneliğe Özgü Aylık Fatura Bilgisi

| Kontrol | Durum | Not |
|---|---|---|
| Abonelik sekmesi açılıyor | OK | Raporlar ekranında `Abonelik` sekmesi |
| Abonelik + dönem seçimi var | OK | Abonelik, yıl, ay seçimi ile liste güncellenir |
| Önceki ay karşılaştırması görünüyor | OK | Tile detaylarında delta gösterilir |
| `--self-test` abonelik karşılaştırmasını doğruluyor | OK | `SubscriptionMonthlyComparisonCalculator` senaryosu eklendi |

## v0.14 - Aboneliğe Özgü Yıllık Fatura Listesi

| Kontrol | Durum | Not |
|---|---|---|
| Abonelik yıllık sekmesi açılıyor | OK | Raporlar ekranında `Abonelik Yıllık` sekmesi |
| 12 ay özet tablosu görünüyor | OK | Aylara göre fatura/özet kolonları |
| En yüksek/en düşük ay gösteriliyor | OK | Tile’larda ay adı ve toplam |
| `--self-test` yıllık raporu doğruluyor | OK | `SubscriptionYearlyReportCalculator` senaryosu eklendi |

## v0.15 - Türe Özgü Yıllık Fatura Listesi

| Kontrol | Durum | Not |
|---|---|---|
| Tür yıllık sekmesi açılıyor | OK | Raporlar ekranında `Tür Yıllık` sekmesi |
| Tür + yıl seçimi var | OK | Fatura türü ve yıl seçimiyle rapor güncellenir |
| 12 ay toplamları görünüyor | OK | Ay bazlı toplamlar hesaplanır |
| Abonelik dağılımı listesi görünüyor | OK | Seçilen tür+ yıl için abonelik bazlı toplamlar sıralanır |
| `--self-test` tür yıllık raporunu doğruluyor | OK | `InvoiceTypeYearlyReportCalculator` senaryosu eklendi |

## v0.16 - Evrak Eksikliği ve Dosya Kontrol Raporu

| Kontrol | Durum | Not |
|---|---|---|
| Evrak kontrol sekmesi açılıyor | OK | Raporlar ekranında `Evrak Kontrol` sekmesi |
| Eksik PDF listesi görünüyor | OK | Fatura/Ödeme için `PDF Yok` ve `PDF Kayıp` uyarıları listeleniyor |
| Aynı hash uyarıları görünüyor | OK | Fatura/Ödeme PDF hash grupları uyarı listesine ekleniyor |
| `--self-test` evrak kontrolünü doğruluyor | OK | Eksik dosya + aynı-hash senaryosu eklendi |

## v0.17 - Excel Dışa Aktarım

| Kontrol | Durum | Not |
|---|---|---|
| Excel dışa aktarım butonu görünüyor | OK | Faturalar ve Raporlar ekranında |
| XLSX dosyası exports/ altına yazılıyor | OK | Dosya adı tarih-saat içerir |
| Kolonlar doğru ve okunabilir | OK | Başlıklar + temel formatlar |
| `--self-test` temel excel exportu doğruluyor | OK | En az bir dosya oluşumu |

## v0.18 - Yazdırılabilir PDF Raporlar

| Kontrol | Durum | Not |
|---|---|---|
| PDF rapor butonu görünüyor | OK | Faturalar ve Raporlar ekranında |
| PDF A4 sayfa düzeni doğru | OK | Başlık + özet + tablo + footer + imza alanı |
| Filtre bilgileri PDF üstünde görünüyor | OK | Dönem/oluşturan/filtre alanları |
| `--self-test` temel PDF üretimini doğruluyor | OK | En az bir dosya oluşumu |

## v0.19 - Manuel Güvenli Yedekleme

| Kontrol | Durum | Not |
|---|---|---|
| Yedekleme butonu görünüyor | OK | `MainWindow.xaml` içinde `BackupNavButton` mevcut ve `BackupNavButton_Click` bağlı |
| ZIP yedek backups/ altına yazılıyor | OK | `--create-backup --backup-no-attachments --backup-no-exports` ile `backups/backup_YYYYMMDD_HHMMSS.zip` oluştu |
| Veritabanı ZIP içinde | OK | ZIP içinde `database/fatura_takip.db` var (SQLite backup ile) |
| Evraklar ZIP içinde | OK | `--create-backup` ile `database/` + `attachments/` + `exports/` + `backup.json` doğrulandı |
| CLI smoke test mevcut | OK | `dotnet run --no-build --project src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --create-backup --backup-no-attachments --backup-no-exports` |

## v0.10 ve Sonrası İçin Regresyon Başlıkları

Bu başlıklar ilgili fazlar başladığında ayrıntılandırılacak:

- Ödenmemiş ve gecikmiş fatura raporları
- Aylık ve yıllık raporlar
- Excel dışa aktarım
- PDF rapor üretimi; başlamadan önce kullanıcıdan Excel örneği istenecek ve çıktı bu örneğe göre doğrulanacak
- Manuel yedekleme
- Tutarlılık denetimi

## v0.20 - Tutarlılık Denetimi

| Kontrol | Durum | Not |
|---|---|---|
| Raporlar ekranında Tutarlılık sekmesi görünüyor | OK | `ReportsView.xaml` içinde `ConsistencyTabButton` mevcut ve `ConsistencyTabButton_Click` bağlı |
| Tutarlılık denetimi liste üretiyor | OK | Boş veri setinde 0 issue; veri varsa WARN/ERROR listelenir |
| CLI tutarlılık denetimi çalışıyor | OK | `dotnet run -c Release --no-build --project src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --consistency-check` |

## v0.21 - Güvenli Yedek Geri Yükleme

| Kontrol | Durum | Not |
|---|---|---|
| CLI geri yükleme çalışıyor | OK | `--restore-backup <zip> --restore-target <emptyFolder>` |
| Boş olmayan hedefe restore engelleniyor | OK | `--self-test` içinde negatif restore senaryosu ile doğrulandı |
| Restore sonrası DB dosyası var | OK | `database/fatura_takip.db` hedefte mevcut |

## v0.22 - UI Backup Restore

| Kontrol | Durum | Not |
|---|---|---|
| Backup ekraninda restore bolumu var | OK | BackupView icinde restore alanlari mevcut |
| Zip secme butonu calisiyor | OK | OpenFileDialog |
| Hedef klasor yolu girilebiliyor | OK | TextBox editable |
| Restore sadece bos klasore izin veriyor | OK | RestoreToEmptyRoot kontrol ediyor |

## v0.23 - Rapor Export Sablon Hizalama (Excel/PDF)

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` (0 hata) |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Self-test artifact saklama calisiyor | OK | `FATURATAKIP_SELFTEST_KEEP=1` iken test klasoru silinmez |
| Raporlar Excel export'ta Aciklama satiri var | OK | Excel raporlarinda 6. satirda `Aciklama : ...` |
| Excel export ana sayfa sablon kolonlari + Detay sayfasi | OK | Aylik, Abonelik Aylik/Yillik, Tur Yillik, Yillik Liste: ana sayfa sablon kolonlari + `Detay` |
| Raporlar ekraninda `Yillik Liste` sekmesi var | OK | Filtre degisiminde liste yenileniyor; self-test yillik liste excel export smoke eklendi |
| PDF export halen calisiyor | OK | Build + self-test OK |

## v0.24 - Build Uyari Temizligi (CS8123)

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` (0 hata) |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| CS8123 tuple isim uyari temizlendi | OK | `ReportsView.BuildPdfContent()` return tuple eleman isimleri hizalandi |

## v0.25 - PDF Rapor Matbu Stil + Toplam Satiri

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` (0 hata) |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| PDF imza blogu yok | OK | PDF raporlar bilgilendirme amacli; imza alanlari kaldirildi |
| PDF tablo matbu stilde | OK | Siyah border, bos satir yok, template raporlarda `GENEL TOPLAM` footer satiri var |

## v0.26 - PDF Footer Kapali

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` (0 hata) |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| PDF footer varsayilan kapali | OK | Ornek PDF'lerde footer yok; `includeFooter=false` default |

## v0.27 - Bagimlilik Uyari Temizligi (QuestPDF)

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` (0 hata, 0 uyari) |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| NU1603 yok | OK | QuestPDF paketi 2025.4.0'a sabitlendi |

## v0.28 - PDF Aciklama Cumlesi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| PDF Aciklama cumlesi | OK | Template raporlarda `Açıklama :` satiri filtre yerine cumle olarak uretiliyor |

## v0.29 - PDF Baslik Sadeligi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| PDF basligi sade | OK | `secondaryTitle` basliga eklenmiyor; detay `Açıklama :` satirinda kalıyor |

## Test Geçmişi

| Tarih | Faz | Sonuç | Not |
|---|---|---|---|
| 2026-05-30 | v0.0 | OK | Plan proje içine alındı, roadmap ve regresyon dosyaları oluşturuldu |
| 2026-05-30 | v0.1 | OK | WPF iskeleti, SQLite başlangıcı, klasör altyapısı ve boş dashboard doğrulandı |
| 2026-05-30 | v0.2 | OK | Fatura türleri migration, seed, listeleme, ekleme, düzenleme ve aktif/pasif akışı doğrulandı |
| 2026-05-30 | v0.3 | OK | Abonelik migration, listeleme, filtreleme, ekleme, düzenleme ve aktif/pasif akışı doğrulandı |
| 2026-05-31 | v0.4 | OK | Fatura migration, listeleme, ekleme, düzenleme ve temel doğrulamalar tamamlandı |
| 2026-05-31 | v0.5 başlangıç | OK | v0.4 master merge sonrası build, health-check, self-test ve kısa uygulama başlatma doğrulandı |
| 2026-05-31 | v0.5 | OK | Fatura PDF metadata, kopyalama, hash, açma akışı ve eksik dosya kontrolü tamamlandı |
| 2026-05-31 | v0.6 başlangıç | OK | v0.5 master merge sonrası build, health-check, self-test ve kısa uygulama başlatma doğrulandı |
| 2026-05-31 | v0.6 | OK | Fatura listesi yıl, ay, tür, abonelik, ödeme durumu, PDF durumu ve metin aramasıyla filtrelenebilir hale geldi |
| 2026-05-31 | v0.7 başlangıç | OK | v0.6 master merge sonrası build, health-check, self-test ve kısa uygulama başlatma doğrulandı |
| 2026-05-31 | v0.7 | OK | Ödeme kayıt altyapısı, kısmi/tam ödeme durumu, kalan tutar kontrolü ve self-test kapsamı tamamlandı |
| 2026-05-31 | v0.8 başlangıç | OK | v0.7 master merge sonrası build, health-check ve self-test doğrulandı |
| 2026-05-31 | v0.8 | OK | Ödeme PDF metadata, kopyalama, hash, açma akışı ve eksik dosya kontrolü tamamlandı |
| 2026-05-31 | v0.9 başlangıç | OK | v0.8 master merge sonrası build, health-check ve self-test doğrulandı |
| 2026-05-31 | v0.9 | OK | Dashboard aylık toplamlar, ödenmemiş/gecikmiş özetler ve evrak eksikleriyle geliştirildi |
| 2026-05-31 | v0.10 başlangıç | OK | v0.9 master merge sonrası build, health-check ve self-test doğrulandı |
| 2026-05-31 | v0.11 başlangıç | OK | v0.10 master merge sonrası build, health-check ve self-test doğrulandı |
| 2026-05-31 | v0.15 | OK | Tür yıllık raporu eklendi; build, self-test ve health-check doğrulandı |
| 2026-05-31 | v0.16 | OK | Evrak kontrol raporu eklendi; build, self-test ve health-check doğrulandı |
| 2026-06-01 | v0.17 | OK | Excel dışa aktarım eklendi; build, self-test ve health-check doğrulandı |
| 2026-06-01 | v0.18 | OK | PDF rapor export eklendi; build, self-test ve health-check doğrulandı |
| 2026-06-01 | v0.19 | OK | Yedekleme (UI + `--create-backup`) eklendi; build, self-test, health-check ve CLI smoke test doğrulandı |
| 2026-06-01 | v0.20 | OK | Tutarlılık denetimi eklendi; build, self-test, health-check ve `--consistency-check` smoke test doğrulandı |
| 2026-06-01 | v0.21 | OK | Güvenli geri yükleme eklendi; `--restore-backup` smoke test doğrulandı |
| 2026-06-01 | v0.22 | OK | Backup UI restore eklendi; build + self-test OK; restore hata durumda MessageBox yok |
| 2026-06-01 | v0.23 | OK | Rapor export sablon hizalama + yillik liste sekmesi; build + self-test OK |
| 2026-06-01 | v0.24 | OK | CS8123 uyarilari temizlendi; build + self-test OK |
| 2026-06-01 | v0.25 | OK | PDF rapor matbu stil + toplam satiri; build + self-test OK |
| 2026-06-02 | v0.26 | OK | PDF footer varsayilan kapali; build + self-test OK |
| 2026-06-02 | v0.27 | OK | QuestPDF NU1603 kaldirildi; build + self-test OK |
| 2026-06-02 | v0.28 | OK | PDF aciklama satiri cumle olarak; build + self-test OK |
| 2026-06-02 | v0.29 | OK | PDF başlığı sade bırakıldı; build + self-test OK |

## v0.30 - PDF Tablo Baslik Sikiligi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| PDF tablo baslik satiri daha kompakt | OK | Baslik hucrelerinin dikey padding degeri dusuruldu; tablo ust satiri daha sik gorunuyor |

| 2026-06-02 | v0.30 | OK | PDF tablo baslik satiri sikilastirildi; build + self-test OK |

## v0.31 - PDF Tablo Govde Sikiligi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| PDF govde satirlari daha kompakt | OK | Govde hucrelerinin dikey padding degeri dusuruldu; tablo satirlari daha sik gorunuyor |

| 2026-06-02 | v0.31 | OK | PDF tablo govde satirlari sikilastirildi; build + self-test OK |

## v0.32 - PDF Tablo Yazi Boyutu Dengesi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| PDF tablo yazi boyutlari dengeli | OK | Baslik, govde ve footer hucreleri 9 puntoya cekildi; gorunum daha birornek oldu |

| 2026-06-02 | v0.32 | OK | PDF tablo yazi boyutlari dengelendi; build + self-test OK |

## v0.33 - PDF Aciklama Tablo Araligi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| `Aciklama :` ile tablo arasi daha sik | OK | Ust icerik kolon spacing'i ve aciklama satiri ust boslugu azaltildi |

| 2026-06-02 | v0.33 | OK | PDF `Aciklama :` satiri ile tablo arasi sikilastirildi; build + self-test OK |

## v0.34 - PDF Ust Baslik Dengesi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| PDF ust baslik bosluklari daha dengeli | OK | Baslik blogu spacing'i ve tarih kolonunun ust padding degeri azaltildi |

| 2026-06-02 | v0.34 | OK | PDF ust baslik bosluklari dengelendi; build + self-test OK |

## v0.35 - PDF Gorsel QA

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| PDF gorsel ornekleri kontrol edildi | OK | Self-test PDF + ek aylik/ornek odenmemis PDF goruntulendi; yerlesim temiz bulundu |

| 2026-06-02 | v0.35 | OK | PDF gorsel QA tamamlandi; build + self-test OK |

## v0.36 - Restore Negatif Smoke

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Bos olmayan hedefe restore engelleniyor | OK | Self-test icinde ornek restore zip + dolu hedef klasor senaryosu eklendi |

| 2026-06-02 | v0.36 | OK | Restore negatif smoke self-test kapsamina alindi; build + self-test OK |

## v0.37 - Audit Log Temeli

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Audit log tablosu olusuyor | OK | `0008 - Create audit logs` migration'i eklendi |
| Abonelik islemleri loglaniyor | OK | Olusturma, guncelleme ve aktif/pasif degisimi kayda dusuluyor |
| Fatura islemleri loglaniyor | OK | Olusturma, guncelleme ve PDF ekleme/degistirme kayda dusuluyor |
| Odeme islemleri loglaniyor | OK | Olusturma ve PDF ekleme/degistirme kayda dusuluyor |

| 2026-06-02 | v0.37 | OK | Audit log temeli eklendi; build + self-test OK |

## v0.38 - Islem Gecmisi Raporu

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| `Raporlar` ekraninda `Islem Gecmisi` sekmesi var | OK | `ReportsView` icine yeni sekme ve grid eklendi |
| Audit log kayitlari listeleniyor | OK | `AuditLogRepository` verisi tarih, islem, varlik ve aciklama kolonlariyla gosteriliyor |
| Audit log export'u destekleniyor | OK | Excel export generic tabloyu, PDF export `ISLEM GECMISI RAPORU` icerigini uretiyor |

| 2026-06-02 | v0.38 | OK | Islem Gecmisi sekmesi eklendi; build + self-test OK |

## v0.39 - Islem Gecmisi Filtreleri

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Islem turu filtresi var | OK | `AuditLogActionInput` ile secili aksiyon turune gore daraltma yapiliyor |
| Tarih araligi filtresi var | OK | Baslangic / bitis tarihleri secilebiliyor; ters aralikta bitis tarihi duzeltiliyor |
| Audit log export'u filtreyi yansitiyor | OK | Grid ile ayni filtrelenmis satirlar export akisina gidiyor; not/filter metni guncellendi |

| 2026-06-02 | v0.39 | OK | Islem Gecmisi filtreleri eklendi; build + self-test OK |

## v0.40 - Islem Gecmisi Arama

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Varlik filtresi var | OK | `AuditLogEntityInput` ile tablo/varlik bazinda daraltma yapiliyor |
| Kullanici filtresi var | OK | `AuditLogUserInput` ile kaydi yazan kullanici bazinda daraltma yapiliyor |
| Metin aramasi var | OK | Aciklama, islem, varlik, kayit id ve kullanici alanlarinda arama yapiliyor |

| 2026-06-02 | v0.40 | OK | Islem Gecmisi arama filtreleri eklendi; build + self-test OK |

## v0.41 - Islem Gecmisi Detay Paneli

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Audit log satiri secilebiliyor | OK | `AuditLogGrid` secimiyle detay paneli guncelleniyor |
| Eski/yeni deger paneli var | OK | Grid altinda `Eski Deger` ve `Yeni Deger` alanlari gosteriliyor |
| JSON degerleri okunur gorunuyor | OK | Kayitli JSON varsa indent edilerek yazdiriliyor; bossa `Kayit yok` gosteriliyor |

| 2026-06-02 | v0.41 | OK | Islem Gecmisi detay paneli eklendi; build + self-test OK |

## v0.42 - Islem Gecmisi Alan Farki

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Alan bazli fark tablosu var | OK | `AuditLogDiffGrid` alan, eski, yeni ve durum kolonlariyla gosteriliyor |
| Eski/yeni JSON parse edilip karsilastiriliyor | OK | JSON alanlari flatten edilerek birlestirilmis key listesi uzerinden karsilastiriliyor |
| JSON olmayan payload bozulmadan gosteriliyor | OK | Parse edilemeyen icerikler `value` alaninda korunuyor |

| 2026-06-02 | v0.42 | OK | Audit log alan farki eklendi; build + self-test OK |

## v0.43 - Islem Gecmisi Diff Filtresi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Sadece degisen alanlar filtresi var | OK | `AuditLogDiffChangedOnlyCheckBox` ile `Ayni` satirlari gizlenebiliyor |
| Detay secimi korunuyor | OK | Kayit seciliyken filtre degisince ayni secili detay yeniden uygulanuyor |

| 2026-06-02 | v0.43 | OK | Audit log diff filtresi eklendi; build + self-test OK |

## v0.44 - Islem Gecmisi Diff Rozetleri

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Diff durumlari renkli rozet olarak gorunuyor | OK | `Degisti`, `Eklendi`, `Silindi`, `Ayni` icin ayri renk stili tanimlandi |
| Diff okunabilirligi artti | OK | `Durum` kolonu template column ile daha belirgin hale geldi |

| 2026-06-04 | v0.44 | OK | Audit log diff rozetleri eklendi; build + self-test OK |

## v0.45 - Islem Gecmisi Kopyalama

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Eski deger kopyalanabiliyor | OK | `CopyOldAuditLogButton` secili detaydaki metni panoya yazar |
| Yeni deger kopyalanabiliyor | OK | `CopyNewAuditLogButton` secili detaydaki metni panoya yazar |
| Kopyalama sonucu ipucu veriliyor | OK | Basari veya hata mesaji `AuditLogHintText` uzerinden gosteriliyor |

| 2026-06-04 | v0.45 | OK | Audit log detay paneline kopyalama aksiyonlari eklendi; build + self-test OK |

## v0.46 - Islem Gecmisi Diff Kopyalama

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Tum diff kopyalanabiliyor | OK | `CopyAuditLogDiffButton` gorunen diff satirlarini topluca panoya yazar |
| Filtrelenmis gorunum korunuyor | OK | `Sadece degisen alanlari goster` secimi acikken kopyalama ayni listeyi kullanir |
| Diff meta bilgisi ekleniyor | OK | Islem, varlik, kayit id, kullanici ve tarih satirlari ustte yazdiriliyor |

| 2026-06-04 | v0.46 | OK | Audit log diff toplu kopyalama aksiyonu eklendi; build + self-test OK |

## v0.47 - Islem Gecmisi Disa Aktarma

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| TXT disa aktarma var | OK | `ExportAuditLogTxtButton` secili diff ozetini `exports` altina yazar |
| JSON disa aktarma var | OK | `ExportAuditLogJsonButton` secili kaydi ve diff satirlarini yapisal olarak kaydeder |
| Durum mesaji gosteriliyor | OK | Basari veya hata sonucu `AuditLogHintText` uzerinden iletiliyor |

| 2026-06-04 | v0.47 | OK | Audit log txt/json disa aktarma aksiyonlari eklendi; build + self-test OK |

## v0.48 - Islem Gecmisi Exports Kolayligi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Exports klasoru acilabiliyor | OK | `OpenAuditLogExportsButton` klasoru olusturup Windows Gezgini ile acar |
| Klasor yoksa olusturuluyor | OK | `Directory.CreateDirectory(exportsDir)` ile guvence altina aliniyor |
| Durum mesaji gosteriliyor | OK | Basari veya hata sonucu `AuditLogHintText` uzerinden iletiliyor |

| 2026-06-04 | v0.48 | OK | Audit log exports klasoru acma aksiyonu eklendi; build + self-test OK |

## v0.49 - Islem Gecmisi Filtre Tercihleri

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Filtre tercihleri saklaniyor | OK | `AuditLogFilterPreferences` action, varlik, kullanici, arama ve tarih araligini `config` altina yazar |
| Tercihler geri yukleniyor | OK | `ReportsView.Initialize` acilisinda kayitli filtreler tekrar UI'ya uygulanir |
| Degisen alan filtresi de korunuyor | OK | `ChangedOnly` secimi de ayni preference kaydinda tutuluyor |

| 2026-06-05 | v0.49 | OK | Audit log filtre tercihleri kalici hale getirildi; build + self-test OK |

## v0.50 - Islem Gecmisi Filtre Sifirlama

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Filtreleri sifirla dugmesi var | OK | `ResetAuditLogFiltersButton` audit log filtre panelinde gosteriliyor |
| Tum filtre alanlari temizleniyor | OK | Islem, varlik, kullanici, tarih, arama ve `ChangedOnly` secimi varsayilana donuyor |
| Preference kaydi da guncelleniyor | OK | Sifirlama sonrasi config dosyasi `Default` filtrelerle yeniden yaziliyor |

| 2026-06-05 | v0.50 | OK | Audit log filtre sifirlama aksiyonu eklendi; build + self-test OK |

## v0.51 - Islem Gecmisi Son Dosya Acma

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | `dotnet build FaturaTakip.sln -c Release` |
| `--self-test` basarili | OK | `dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test` |
| Son dosyayi ac dugmesi var | OK | `OpenLastAuditLogExportButton` audit log araclari icinde gosteriliyor |
| Son export yolu hatirlaniyor | OK | `ExportAuditLogDetail` son yazilan dosya yolunu `_lastAuditLogExportPath` icinde tutuyor |
| Geriye donuk bulma var | OK | Oturumda yol yoksa `exports` altindaki en yeni `audit-log-*` dosyasi aciliyor |

| 2026-06-05 | v0.51 | OK | Audit log son disa aktarilan dosyayi acma aksiyonu eklendi; build + self-test OK |

## v0.52 - Islem Gecmisi Hizli Odak

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Secili kayda git dugmesi var | OK | FocusSelectedAuditLogButton audit log filtre panelinde gosteriliyor |
| Secili kayit gorunur alana getiriliyor | OK | ScrollIntoView ile secili row tekrar ekranda odaklaniyor |
| Filtre degisince secim korununca da odak veriliyor | OK | ApplyTab(ReportTab.AuditLog) icinde eslesen secili satir bulunursa gorunur alana aliniyor |

| 2026-06-05 | v0.52 | OK | Audit log secili kayda hizli odak aksiyonu eklendi; build + self-test OK |


## v0.53 - Islem Gecmisi Export Gecmisi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Son export listesi var | OK | AuditLogRecentExportsInput son 5 audit export dosyasini gosteriyor |
| Listeden ac aksiyonu var | OK | OpenSelectedAuditLogExportButton secilen export dosyasini aciyor |
| Son dosya fallback korunuyor | OK | Son dosya yoksa en yeni export yine listeden/fallback ile bulunuyor |

| 2026-06-05 | v0.53 | OK | Audit log export gecmisi rahatligi eklendi; build + self-test OK |


## v0.54 - Islem Gecmisi Export Listesi Yenileme

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Listeyi yenile dugmesi var | OK | RefreshAuditLogExportsButton export araclari icinde gosteriliyor |
| Audit log sekmesinde liste tazeleniyor | OK | ApplyTab(ReportTab.AuditLog) icinde export listesi yeniden yukleniyor |
| Bos liste durumu acik gosteriliyor | OK | Dosya yoksa ipucu metni kullaniciya bilgi veriyor |

| 2026-06-05 | v0.54 | OK | Audit log export listesi yenileme rahatligi eklendi; build + self-test OK |


## v0.55 - Islem Gecmisi Export Listesi Temizleme

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Listeyi temizle dugmesi var | OK | ClearAuditLogExportsButton export araclari icinde gosteriliyor |
| Yalniz audit-log dosyalari siliniyor | OK | Directory.GetFiles(exportsDir, "audit-log-*.*") kullaniliyor |
| Temizleme sonrasi liste tazeleniyor | OK | RefreshRecentAuditLogExports tekrar cagiriliyor |

| 2026-06-05 | v0.55 | OK | Audit log export listesi temizleme rahatligi eklendi; build + self-test OK |


## v0.56 - Islem Gecmisi Export Secileni Sil

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Secileni sil dugmesi var | OK | DeleteSelectedAuditLogExportButton export araclari icinde gosteriliyor |
| Secimsiz durumda acik mesaj veriyor | OK | AuditLogRecentExportsInput secili degilse ipucu metni kullaniciya bilgi veriyor |
| Silme sonrasi liste tazeleniyor | OK | Dosya silindikten sonra RefreshRecentAuditLogExports tekrar cagiriliyor |

| 2026-06-05 | v0.56 | OK | Audit log export gecmisinde secili dosyayi silme rahatligi eklendi; build + self-test OK |


## v0.57 - Islem Gecmisi Export Eskileri Temizle

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Eskileri temizle dugmesi var | OK | TrimAuditLogExportsButton export araclari icinde gosteriliyor |
| Son 5 kayit korunuyor | OK | OrderByDescending(...).Skip(5) ile sadece daha eski dosyalar siliniyor |
| Temizlik sonrasi liste tazeleniyor | OK | RefreshRecentAuditLogExports tekrar cagiriliyor |

| 2026-06-05 | v0.57 | OK | Audit log export gecmisinde son 5 kaydi koruyup eskileri temizleme rahatligi eklendi; build + self-test OK |


## v0.58 - Islem Gecmisi Export Etiketli Liste Gorunumu

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Export listesinde tip etiketi var | OK | ComboBox item template icinde JSON/TXT tipi gosteriliyor |
| Export listesinde zaman bilgisi var | OK | TimestampLabel dd.MM HH:mm biciminde gosteriliyor |
| Dosya yolu tooltip olarak korunuyor | OK | ItemTemplate ToolTip ile FilePath gosteriyor |

| 2026-06-05 | v0.58 | OK | Audit log export gecmisi etiketli liste gorunumu eklendi; build + self-test OK |


## v0.59 - Islem Gecmisi Export Tip Filtresi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Tip filtresi var | OK | AuditLogExportTypeFilterInput Tum / JSON / TXT secenekleriyle gosteriliyor |
| JSON/TXT filtresi listeyi daraltiyor | OK | RefreshRecentAuditLogExports icinde secili tipe gore Where filtresi uygulaniyor |
| Bos sonuc acik mesaj veriyor | OK | Secili export tipi icin dosya bulunmadi ipucu metni kullaniliyor |

| 2026-06-05 | v0.59 | OK | Audit log export gecmisine hizli tip filtresi eklendi; build + self-test OK |


## v0.60 - Islem Gecmisi Son Kullanilan Export Isareti

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Son kullanilan etiketi var | OK | ItemTemplate icinde IsLastUsed bagli SON rozeti gosteriliyor |
| Secili export acilinca isaret guncelleniyor | OK | OpenSelectedAuditLogExportButton_Click icinde _lastAuditLogExportPath yenileniyor |
| Son dosya acilinca isaret guncelleniyor | OK | OpenLastAuditLogExportButton_Click icinde _lastAuditLogExportPath yenileniyor |

| 2026-06-05 | v0.60 | OK | Audit log export gecmisinde son kullanilan dosya isareti eklendi; build + self-test OK |


## v0.61 - Islem Gecmisi Son Kullanilani Secili Tutma

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Son kullanilan once seciliyor | OK | RefreshRecentAuditLogExports icinde IsLastUsed olan oge seciliyor |
| Onceki secim fallback olarak korunuyor | OK | Son kullanilan gorunmuyorsa selectedPath ile eslesen oge seciliyor |
| Bos listede secim temizleniyor | OK | Dosya yoksa SelectedItem null yapilip erken donuluyor |

| 2026-06-05 | v0.61 | OK | Audit log export gecmisinde son kullanilan dosya secili tutuluyor; build + self-test OK |


## v0.62 - Islem Gecmisi Export Yolunu Kopyala

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Yolu kopyala dugmesi var | OK | CopySelectedAuditLogExportPathButton export araclari icinde gosteriliyor |
| Secimsiz durumda acik mesaj veriyor | OK | Export secili degilse ipucu metni kullaniciya bilgi veriyor |
| Secili yol panoya alinabiliyor | OK | CopyAuditLogText ile FilePath dogrudan panoya yaziliyor |

| 2026-06-05 | v0.62 | OK | Audit log export gecmisinde secili dosya yolunu kopyalama rahatligi eklendi; build + self-test OK |


## v0.63 - Islem Gecmisi Export Klasorde Goster

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Klasorde goster dugmesi var | OK | RevealSelectedAuditLogExportButton export araclari icinde gosteriliyor |
| Secimsiz durumda acik mesaj veriyor | OK | Export secili degilse ipucu metni kullaniciya bilgi veriyor |
| Explorer secili dosyayla aciliyor | OK | explorer.exe /select kullaniliyor |

| 2026-06-05 | v0.63 | OK | Audit log export gecmisinde secili dosyayi klasorde gosterme rahatligi eklendi; build + self-test OK |


## v0.64 - Islem Gecmisi Export Secim Araclari Durumlari

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Secim degisince durumlar guncelleniyor | OK | AuditLogRecentExportsInput_SelectionChanged icinde UpdateAuditLogExportActionStates cagiriliyor |
| Yenileme sonrasi durumlar guncelleniyor | OK | RefreshRecentAuditLogExports sonunda UpdateAuditLogExportActionStates cagiriliyor |
| Secimsiz durumda kritik dugmeler pasif | OK | Listeden ac, Yolu kopyala, Klasorde goster, Secileni sil butonlari devre disi kalabiliyor |

| 2026-06-05 | v0.64 | OK | Audit log export secim araclari durumlari daha anlatir hale getirildi; build + self-test OK |


## v0.65 - Islem Gecmisi Son Kullanilani Sec

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Son kullanilani sec dugmesi var | OK | SelectLastUsedAuditLogExportButton export araclari icinde gosteriliyor |
| Gorunur son kullanilan varsa seciliyor | OK | SelectLastUsedAuditLogExportButton_Click IsLastUsed ogeyi secili yapiyor |
| Gorunur son kullanilan yoksa acik mesaj veriyor | OK | Uygun item yoksa ipucu metni kullaniciya bilgi veriyor |

| 2026-06-05 | v0.65 | OK | Audit log export gecmisinde son kullanilan ogeyi hizli secme yardimi eklendi; build + self-test OK |



## v0.66 - Fatura Girisinde Sonraki Ay Taslagi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Sonrakini Hazirla dugmesi var | OK | Faturalar ekraninda secili kayittan yeni taslak ureten aksiyon eklendi |
| Taslak sonraki aya tasiniyor | OK | InvoiceDraftTemplateBuilder fatura ve son odeme tarihini AddMonths(1) ile tasiyor |
| Fatura no bilincli olarak bos basliyor | OK | Yeni taslakta tekrar eden fatura no riski azaltildi |
| Yil donusu korunuyor | OK | Aralik -> Ocak gecis senaryosu self-test ile dogrulandi |

| 2026-06-06 | v0.66 | OK | Fatura girisinde secili kayittan sonraki ay taslagi hazirlama rahatligi eklendi; build + self-test OK |


## v0.67 - Odeme Girisi Doldurma Yardimcilari

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Kalani Doldur dugmesi var | OK | Odeme formunda kalan tutari yeniden yazan yardim eklendi |
| Son Odemeden Doldur dugmesi var | OK | Son odeme aciklamasini ve kalan tutari yeni taslaga tasiyor |
| Son aciklama seciliyor | OK | PaymentEntrySuggestionBuilder en guncel bos olmayan aciklamayi kullaniyor |
| Bos aciklama tasinmiyor | OK | Yalnizca bos olmayan son aciklama yeni taslaga oneriliyor |

| 2026-06-06 | v0.67 | OK | Odeme formuna hizli doldurma yardimcilari eklendi; build + self-test OK |


## v0.68 - Secili Odemeden Taslak Hazirlama

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Secili Odemeden Doldur dugmesi var | OK | Odeme formunda secili kayda gore yeni taslak ureten yardim eklendi |
| Tutar kalanla sinirlaniyor | OK | PaymentEntrySuggestionBuilder secili odeme tutarini kalan tutari asmadan kullaniyor |
| Aciklama trim edilerek tasiniyor | OK | Secili odemenin aciklamasi bosluklardan arindirilip taslaga yaziliyor |
| Secimsiz durumda acik mesaj veriliyor | OK | Odeme listesinden kayit secilmemisse kullanici yonlendiriliyor |

| 2026-06-06 | v0.68 | OK | Secili odemeden yeni odeme taslagi hazirlama rahatligi eklendi; build + self-test OK |


## v0.69 - Backup Son Yedekler Listesi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Son 5 yedek listeleniyor | OK | BackupFileCatalog en guncel `backup_*.zip` dosyalarini ters tarihle getiriyor |
| Secileni Kullan dugmesi var | OK | Secili yedek restore zip alanina tek tikla tasiniyor |
| Zipi Ac dugmesi var | OK | Secili yedek dogrudan shell ile acilabiliyor |
| Backup katalog self-test'i var | OK | Son N yedek siniri ve siralamasi self-test ile dogrulandi |

| 2026-06-06 | v0.69 | OK | Backup ekranina son yedekler listesi ve secili yedegi kullan/ac rahatliklari eklendi; build + self-test OK |


## v0.70 - Backup Secili Zipi Klasorde Gosterme

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Klasorde Goster dugmesi var | OK | Backup listesindeki secili zip icin ayri aksiyon eklendi |
| Secim yoksa devre disi | OK | Secili oge yokken yanlis tiklama engelleniyor |
| Explorer select akisi kullaniliyor | OK | `explorer.exe /select` ile dogrudan hedef dosya odaklaniyor |

| 2026-06-06 | v0.70 | OK | Backup listesindeki secili zipi klasorde gosterme rahatligi eklendi; build + self-test OK |


## v0.71 - Restore Hedef Klasoru Secici

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Hedef Sec dugmesi var | OK | Restore hedef kutusuna klasor secici baglandi |
| Mevcut yola gore aciliyor | OK | Var olan klasor ya da parent klasor initial directory olarak kullaniliyor |
| Manuel yol yazma mecburiyeti azaldi | OK | Kullanici hedef klasoru secici ile doldurabiliyor |

| 2026-06-06 | v0.71 | OK | Restore hedef klasoru secici eklendi; build + self-test OK |


## v0.72 - Restore Hedefi Canli Uygunluk Kontrolu

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Canli hedef durum mesaji var | OK | Restore hedefi icin ayri durum metni eklendi |
| Yeni klasor uygun sayiliyor | OK | Henuz olmayan hedef "olusturulacak" mesaji ile kabul ediliyor |
| Bos mevcut klasor uygun sayiliyor | OK | Bos klasor "uygun" mesaji ile gosteriliyor |
| Dolu klasor aninda uyari veriyor | OK | Bos olmayan hedef "kullanilamaz" olarak isaretleniyor |
| Geri Yukle dugmesi uygunlukla senkron | OK | Uygun olmayan hedefte dugme pasif kaliyor |

| 2026-06-06 | v0.72 | OK | Restore hedefi icin canli uygunluk kontrolu eklendi; build + self-test OK |


## v0.73 - Bos Restore Klasoru Olusturma Yardimi

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Bos Klasor Olustur dugmesi var | OK | Restore alanina yardimci aksiyon eklendi |
| Uygun olmayan durumda aktif | OK | Klasor dolu/gecersiz/bos secilmemis durumlarda yardim sunuluyor |
| Benzersiz klasor adi uretiliyor | OK | Ayni zaman damgasinda ikinci klasor icin suffix ekleniyor |
| Alan otomatik dolduruluyor | OK | Olusturulan klasor restore hedef kutusuna yaziliyor |

| 2026-06-06 | v0.73 | OK | Uygun olmayan restore hedefinde tek tikla bos klasor olusturma yardimi eklendi; build + self-test OK |


## v0.74 - Restore Onizleme Kutusu

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Onizleme kutusu var | OK | Zip, hedef ve hazirlik ozeti ayri satirlarda gosteriliyor |
| Zip ozeti gosteriliyor | OK | Secilmedi / hazir / bulunamadi durumu yaziliyor |
| Hedef ozeti gosteriliyor | OK | EvaluateTargetRoot sonucu hedef satirina tasiniyor |
| Hazirlik ozeti gosteriliyor | OK | Geri yukleme icin hazir / ek adim gerekiyor mesaji var |
| Preview self-test'i var | OK | BuildPreviewSummary davranisi self-test ile dogrulandi |

| 2026-06-06 | v0.74 | OK | Restore bolumune onizleme kutusu eklendi; build + self-test OK |


## v0.75 - Dashboard Rapor Kisa Yollari

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Odenmemis kartinda kisa yol var | OK | Dashboard kartina `Rapora Git` dugmesi eklendi |
| Gecikmis kartinda kisa yol var | OK | Dashboard kartina `Rapora Git` dugmesi eklendi |
| Rapor paneli ilgili sekmeyi acabiliyor | OK | `ReportsView.ShowUnpaidReport()` ve `ShowOverdueReport()` yardimlari eklendi |
| Gunluk aksiyon akisi hizlandi | OK | Gosterge panelinden ilgili rapor sekmesine tek tikla gecis saglandi |

| 2026-06-06 | v0.75 | OK | Dashboard odenmemis/gecikmis kartlarina rapor kisayollari eklendi; build + self-test OK |


## v0.76 - Dashboard Ek Hizli Gecisler

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Bu ay fatura kartinda kisa yol var | OK | Dashboard kartina `Listeye Git` dugmesi eklendi |
| Bu ay odeme kartinda kisa yol var | OK | Dashboard kartina `Listeye Git` dugmesi eklendi |
| Fatura PDF eksik kartinda kisa yol var | OK | Dashboard kartina `Kontrole Git` dugmesi eklendi |
| Odeme PDF eksik kartinda kisa yol var | OK | Dashboard kartina `Kontrole Git` dugmesi eklendi |
| Rapor paneli aylik ve evrak sekmelerini acabiliyor | OK | `ReportsView.ShowMonthlyReport()` ve `ShowDocumentHealthReport()` yardimlari eklendi |

| 2026-06-06 | v0.76 | OK | Dashboard aylik hareket ve evrak eksigi kartlarina hizli gecisler eklendi; build + self-test OK |


## v0.77 - Dashboard Liste Kisa Yollari

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Fatura turu kartinda kisa yol var | OK | Dashboard kartina `Turlere Git` dugmesi eklendi |
| Aktif tur kartinda kisa yol var | OK | Dashboard kartina `Turlere Git` dugmesi eklendi |
| Aktif abonelik kartinda kisa yol var | OK | Dashboard kartina `Aboneliklere Git` dugmesi eklendi |
| Toplam fatura kartinda kisa yol var | OK | Dashboard kartina `Faturalara Git` dugmesi eklendi |
| Kisayollar ilgili ekranlari aciyor | OK | `MainWindow` dashboard event'leri dogrudan hedef panellere baglandi |

| 2026-06-07 | v0.77 | OK | Dashboard genel ozet kartlarina liste kisayollari eklendi; build + self-test OK |


## v0.78 - Fatura Hizli Filtreler

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Bu Ay hizli filtresi var | OK | Yil ve ay filtrelerini bugunun donemine kuruyor |
| Odenmemis hizli filtresi var | OK | Odeme durumu filtresini tek tikla `Unpaid` yapiyor |
| Gecikmis hizli filtresi var | OK | Odeme durumu filtresini tek tikla `Overdue` yapiyor |
| PDF Eksik hizli filtresi var | OK | PDF durumu filtresini tek tikla `MissingPdf` yapiyor |
| Sonuc bilgisi gosteriliyor | OK | Filtre sonucundaki kayit sayisi ayri ipucu metninde yaziliyor |
| Ilk kayda otomatik odaklanma var | OK | Hizli filtre sonrasi varsa ilk kayit secilip gorunur alana getiriliyor |

| 2026-06-07 | v0.78 | OK | Faturalar ekranina hazir filtre kisayollari eklendi; build + self-test OK |


## v0.79 - Fatura PDF Klasor Kisa Yolu

| Kontrol | Durum | Not |
|---|---|---|
| Derleme basarili | OK | dotnet build FaturaTakip.sln -c Release |
| --self-test basarili | OK | dotnet run -c Release --project src/FaturaTakip.App/FaturaTakip.App.csproj -- --self-test |
| Klasorde Goster dugmesi var | OK | Fatura PDF bolumune yeni yardimci dugme eklendi |
| PDF varsa dosyayi secili aciyor | OK | `explorer.exe /select` ile hedef dosya odaklaniyor |
| PDF yoksa beklenen klasoru aciyor | OK | Fatura yil/ay ek klasoru bulunup gerekirse olusturuluyor |
| Klasor yolu repository'de hesaplanıyor | OK | `InvoiceRepository.GetPdfDirectoryAbsolutePath()` eklendi |

| 2026-06-07 | v0.79 | OK | Fatura PDF klasor kisayolu eklendi; build + self-test OK |

## v0.80 - Filtreli Fatura Export Baglami
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | Self-test icinde export baglami slug dogrulamasi eklendi |
| PDF Eksik export slug | OK | pdf-eksik dosya adi baglami uretiliyor |
| Bu Ay export slug | OK | u-ay baglami dosya adina ekleniyor |
| Filtreli export mesajlari | OK | Kullaniciya gorunur liste ve aktif filtre etiketiyle bilgi veriliyor |
| 2026-06-07 | v0.80 | OK | Filtreli export baglami + build + self-test tamamlandi |

## v0.81 - Fatura Kontrol Turu Gezinmesi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | Self-test icinde kontrol turu ileri/geri sinir dogrulamasi eklendi |
| Onceki/sonraki gezinme | OK | Filtreli liste icinde secili kayit ileri geri tasinabiliyor |
| Sinir davranisi | OK | Ilk kayittan geri, son kayittan ileri gecis engelleniyor |
| Kontrol sirasi ipucu | OK | Secili kaydin gorunur listedeki konumu gosteriliyor |
| 2026-06-07 | v0.81 | OK | Fatura kontrol turu gezinmesi + build + self-test tamamlandi |

## v0.82 - Fatura Inceleme Modu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | Self-test icinde inceleme modu ipucu ve gezinme dogrulamasi eklendi |
| PDF Eksik Turu | OK | Tek tikla PDF eksik alt kumesi icin kontrol modu baslatiliyor |
| Gecikmis Turu | OK | Tek tikla gecikmis alt kumesi icin kontrol modu baslatiliyor |
| Modlu kontrol ipucu | OK | Kontrol modu: ... (x/y) metni aktif alt kume bilgisini gosteriyor |
| 2026-06-07 | v0.82 | OK | Fatura inceleme modu + build + self-test tamamlandi |

## v0.83 - Inceleme Turu Birlesik Aksiyonlari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | Mevcut inceleme modu ve gezinme self-test kapsamiyla birlikte temiz gecti |
| PDF Ac + Sonraki | OK | PDF acma aksiyonu sonrasinda bir sonraki kayda otomatik geciliyor |
| Klasor + Sonraki | OK | Klasor gosterme aksiyonu sonrasinda bir sonraki kayda otomatik geciliyor |
| Tur sonu davranisi | OK | Son kayitta aksiyon korunuyor, ileri gecis yerine tur sonu mesaji veriliyor |
| 2026-06-07 | v0.83 | OK | Inceleme turu birlesik aksiyonlari + build + self-test tamamlandi |

## v0.84 - Inceleme Turu Klavye Kisayollari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | Self-test icinde kisayol ipucu metni dogrulamasi eklendi |
| Klavye ile onceki/sonraki | OK | Ctrl+Shift+Sol/Sag ile filtreli liste icinde gezinme destekleniyor |
| Klavye ile birlesik aksiyon | OK | Ctrl+Shift+O ve Ctrl+Shift+K ile inceleme aksiyonlari tetikleniyor |
| Kisayol ipucu | OK | Kontrol ipucunda aktif kisayollar gosteriliyor |
| 2026-06-07 | v0.84 | OK | Inceleme turu kisayollari + build + self-test tamamlandi |

## v0.85 - Fatura Inceleme Notu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | Fatura inceleme notu kaydet/temizle senaryosu self-test icine eklendi |
| Inceleme notu saklaniyor | OK | `review_note` ve `reviewed_at` alanlari migration ile veritabanina eklendi |
| UI kaydet/temizle akisi var | OK | Fatura PDF/Inceleme bolumunde not kaydetme ve isaret temizleme dugmeleri eklendi |
| Audit log kaydi var | OK | `invoice_review_updated` kaydi olusuyor |
| 2026-06-07 | v0.85 | OK | Fatura inceleme notu + build + self-test tamamlandi |

## v0.86 - Bakildi Akisi ve Turkce Duzeltmeler
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Bakildi + Sonraki akisi var | OK | Secili fatura isaretlenip filtreli listedeki sonraki kayda geciliyor |
| Inceleme notu korunuyor | OK | `Bakıldı + Sonraki` mevcut not metnini de review kaydina tasiyor |
| Gorunur Turkce metinler duzeltildi | OK | Faturalar/Raporlar ekranlarindaki bozuk Turkce karakterler temizlendi |
| PDF Kayıp metni duzeltildi | OK | Yillik liste raporu dahil PDF durum metinleri dogru Turkce karakterlerle gosteriliyor |
| 2026-06-07 | v0.86 | OK | Bakildi akisi + Turkce metin duzeltmeleri + build + self-test tamamlandi |

## v0.87 - Inceleme Durumu Filtresi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Inceleme durumu filtresi var | OK | `Tum Inceleme / Incelendi / Incelenmedi` secenekleri eklendi |
| Incelenmedi hizli filtresi var | OK | Hazir filtre dugmeleri arasina tek tiklik `Incelenmedi` akisi eklendi |
| Filtre reset akisi guncellendi | OK | Temizle ve hizli filtre resetleri review filtresini de sifirliyor |
| Filter self-test dogrulamasi var | OK | `InvoiceFilter` icin reviewed/unreviewed senaryolari eklendi |
| Fatura ekraninda gorunur Turkce etiketler duzeltildi | OK | Dugme ve filtre etiketlerindeki kalan karakter sorunlari toparlandi |
| 2026-06-07 | v0.87 | OK | Inceleme durumu filtresi + Turkce etiket duzeltmeleri + build + self-test tamamlandi |

## v0.88 - Dashboard Incelenmedi Kisayolu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Dashboard karti var | OK | `Inceleme Bekleyen` karti incelenmedi kayit sayisini gosteriyor |
| Dashboard kisayolu filtreli aciyor | OK | `Faturalara Git` dugmesi Faturalar ekranini `Incelenmedi` filtresiyle aciyor |
| Dashboard summary sayimi var | OK | `DashboardSummary` icine `UnreviewedInvoiceCount` alani eklendi |
| Dashboard self-test dogrulamasi var | OK | Self-test dashboard ozetinde incelenmedi sayisini kontrol ediyor |
| 2026-06-11 | v0.88 | OK | Dashboard incelenmedi kisayolu + build + self-test tamamlandi |

## v0.89 - Raporlarda Incelenmedi Sekmesi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Rapor sekmesi var | OK | Actionable raporlara `Incelenmedi` sekmesi eklendi |
| Export basligi var | OK | Excel/PDF export aktif sekmede `İNCELENMEDİ FATURALAR RAPORU` basligini kullaniyor |
| Rapor sayimi genislendi | OK | `ActionableInvoiceReport` icine `Unreviewed` liste ve kalan toplam alani eklendi |
| Self-test rapor dogrulamasi var | OK | Actionable rapor self-test'i incelenmedi sayisi ve kalan toplamini kontrol ediyor |
| 2026-06-11 | v0.89 | OK | Raporlarda incelenmedi sekmesi + build + self-test tamamlandi |

## v0.90 - Rapordan Inceleme Akisina Gecis
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Raporda gecis dugmesi var | OK | `Incelenmedi` sekmesinde `Faturalarda İncele` dugmesi gosteriliyor |
| Ana pencere yonlendirmesi var | OK | `ReportsView` istegi `MainWindow` uzerinden Faturalar ekranina tasiniyor |
| Inceleme modu aciliyor | OK | `InvoicesView.StartUnreviewedReviewMode()` review mod etiketini kurup ilk kayda odaklaniyor |
| 2026-06-11 | v0.90 | OK | Rapordan inceleme akisina gecis + build + self-test tamamlandi |

## v0.91 - Diger Inceleme Turlarina Hizli Gecis
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Gecikmis rapor gecisi var | OK | `Gecikmisleri İncele` dugmesi Faturalar ekranindaki gecikmis turunu aciyor |
| Evrak kontrol gecisi var | OK | `PDF Eksikleri İncele` dugmesi Faturalar ekranindaki PDF eksik turunu aciyor |
| Ana pencere yonlendirmeleri var | OK | `MainWindow` rapordan gelen iki yeni istegi ilgili inceleme modlarina bagliyor |
| 2026-06-11 | v0.91 | OK | Gecikmis + PDF eksik rapor gecisleri + build + self-test tamamlandi |

## v0.92 - Secili Kayit Baglamini Tasima
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Actionable secim baglami tasiniyor | OK | `ReportRow` artik `SourceInvoiceId` tasiyor ve review gecisine ekliyor |
| Evrak kontrol baglami tasiniyor | OK | Fatura issue satirlari `DocumentHealthRow.SourceInvoiceId` ile odakli gecis sagliyor |
| Faturalar ekrani tercih edilen kaydi seciyor | OK | Review modu filtreyi kurduktan sonra varsa ayni kayda odaklaniyor |
| 2026-06-11 | v0.92 | OK | Secili kayit baglami + build + self-test tamamlandi |

## v0.93 - Gecis Baglami Ipuclari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Gecis baglami tasiniyor | OK | Navigation event args artik `ContextLabel` da tasiyor |
| Kontrol ipucu baglam gosteriyor | OK | `InvoiceReviewNavigator.BuildHint()` ciktiya `Baglam: ...` ekleyebiliyor |
| Self-test ipucu dogrulamasi var | OK | Yeni baglamli kontrol ipucu string'i self-test ile dogrulaniyor |
| 2026-06-11 | v0.93 | OK | Gecis baglami ipuclari + build + self-test tamamlandi |

## v0.94 - Kayit Ozetli Gecis Baglami
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Actionable baglam ozeti var | OK | `Rapor: ... > Tur / FaturaNo` ozeti secili satirdan uretiliyor |
| Evrak kontrol baglam ozeti var | OK | `IssueType / EntityType / PeriodOrDate` ozeti secili satirdan uretiliyor |
| 2026-06-11 | v0.94 | OK | Kayit ozetli gecis baglami + build + self-test tamamlandi |

## v0.95 - Baglami Kopyala ve Gizle
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Baglam gorunurluk kontrolu var | OK | `Bağlamı Göster` kutusu ipucudaki baglam alanini acip kapatabiliyor |
| Baglam kopyalama aksiyonu var | OK | `Bağlamı Kopyala` dugmesi mevcut context label'i panoya yaziyor |
| 2026-06-12 | v0.95 | OK | Baglami kopyala/gizle + build + self-test tamamlandi |

## v0.96 - Baglam Tercihini Hatirla
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Tercih dosyasi olusuyor | OK | `config/invoice-review-preferences.json` uzerinden saklama yapiliyor |
| Son secim geri yukleniyor | OK | `Bağlamı Göster` kutusu acilista kaydedilen degere gore kuruluyor |
| Self-test tercih saklamasini dogruluyor | OK | `InvoiceReviewPreferences` load/save roundtrip eklendi |
| 2026-06-12 | v0.96 | OK | Baglam tercihi kaliciligi + build + self-test tamamlandi |

## v0.97 - Baglam Kisayolu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release tamamlandi; calisan exe nedeniyle tek seferlik kopyalama uyarisi goruldu |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Klavye ile baglam gorunurlugu degisiyor | OK | `Ctrl+Shift+B` `Bağlamı Göster` kutusunu acip kapatiyor |
| Kontrol ipucu yeni kisayolu gosteriyor | OK | `InvoiceReviewNavigator.BuildHint()` ciktiya `Ctrl+Shift+B` eklendi |
| 2026-06-12 | v0.97 | OK | Baglam kisayolu + build + self-test tamamlandi |

## v0.98 - Baglami Kopyalama Kisayolu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release tamamlandi; calisan exe nedeniyle tek seferlik kopyalama uyarisi goruldu |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Klavye ile baglam kopyalanabiliyor | OK | `Ctrl+Shift+C` mevcut baglami panoya kopyaliyor |
| Buton ve klavye ayni akisi kullaniyor | OK | Kopyalama davranisi tek yardimci metoda tasindi |
| Kontrol ipucu yeni kisayolu gosteriyor | OK | `InvoiceReviewNavigator.BuildHint()` ciktiya `Ctrl+Shift+C` eklendi |
| 2026-06-12 | v0.98 | OK | Baglam kopyalama kisayolu + build + self-test tamamlandi |

## v0.99 - Baglam Paneli
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release tamamlandi; calisan exe nedeniyle tek seferlik kopyalama uyarisi goruldu |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Baglam ayri panelde gorunuyor | OK | `Bağlamı Göster` acikken rapor gecis baglami ayri blokta gosteriliyor |
| Kontrol ipucu sade kaldi | OK | Inceleme ipucu gezinme ve kisayol odagina dondu |
| Baglam yoksa kopyalama pasif | OK | `Bağlamı Kopyala` dugmesi baglam olmadiginda devre disi |
| 2026-06-12 | v0.99 | OK | Baglam paneli + build + self-test tamamlandi |

## v1.00 - Baglam Rozetleri
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release tamamlandi; calisan exe nedeniyle tek seferlik kopyalama uyarisi goruldu |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Baglam rozetleri gorunuyor | OK | Baglam paneli `>` ve `/` parcalarini ayri chip olarak gosteriyor |
| Rozet parcalama dogru | OK | `InvoiceReviewContextFormatter.BuildChips()` self-test ile dogrulandi |
| Tam metin korunuyor | OK | Rozetlere ek olarak tam baglam metni de panelde gorunmeye devam ediyor |
| 2026-06-12 | v1.00 | OK | Baglam rozetleri + build + self-test tamamlandi |

## v1.01 - Baglam Rozet Tipleri
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Rozet renkleri tipe gore ayrisiyor | OK | Report / issue / entity / period tipleri farkli renk ailesi kullaniyor |
| Tip atamasi dogru | OK | `InvoiceReviewContextFormatter.BuildChips()` self-test ile tipleri de dogruluyor |
| 2026-06-12 | v1.01 | OK | Baglam rozet tipleri + build + self-test tamamlandi |

## v1.02 - Baglam Rozet On Ekleri
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| On ekler gorunuyor | OK | `RPR`, `ISS`, `VAR`, `DNM` tip bazli rozet on ekleri eklendi |
| On ek atamasi dogru | OK | `InvoiceReviewContextFormatter.BuildChips()` self-test ile Prefix alanlarini da dogruluyor |
| 2026-06-12 | v1.02 | OK | Baglam rozet on ekleri + build + self-test tamamlandi |

## v1.03 - Baglam Rozet Sirasi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Rozetler sabit oncelikle siralaniyor | OK | `report -> issue -> entity -> detail -> period` sirasiyla gosteriliyor |
| Tekrar eden rozetler tekillesiyor | OK | Ayni kind+text parcasi panelde bir kez tutuluyor |
| 2026-06-12 | v1.03 | OK | Baglam rozet sirasi + build + self-test tamamlandi |

## v1.04 - Baglam Rozet Tekillestirme Duzeltmesi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release tamamlandi; calisan exe nedeniyle tek seferlik kopyalama uyarisi gorulse de basarili bitti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Tekillestirme stabil | OK | `GroupBy(...).First()` ile tekrar eden kind+text parcalari sabit sekilde tekillestiriliyor |
| v1.03 regresyonu kapandi | OK | Self-test tekrar eden baglam parcasi senaryosunda yesil |
| 2026-06-12 | v1.04 | OK | Baglam rozet tekillestirme duzeltmesi + build + self-test tamamlandi |

## v1.05 - Baglam Filtresi Aksiyonu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Baglamdan filtre cikariliyor | OK | `TryResolveSuggestedFilter()` unreviewed / overdue / missing-pdf baglamlarini ayristiriyor |
| Panel aksiyonu var | OK | `Bağlam Filtresi` dugmesi onerilen filtreyi tek tikla uyguluyor |
| Baglam yoksa aksiyon pasif | OK | Uygulanabilir onerisi olmayan baglamlarda dugme devre disi |
| 2026-06-12 | v1.05 | OK | Baglam filtresi aksiyonu + build + self-test tamamlandi |

## v1.06 - Baglam Kaydına Git
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Baglam kaydina donus aksiyonu var | OK | `Bağlam Kaydına Git` dugmesi rapordan gelen tercihli kayda odaklaniyor |
| Review filtresiyle birlikte calisiyor | OK | Uygunsa once baglam filtresi kurulup sonra tercihli kayda gidiliyor |
| Tercihli kayit yoksa aksiyon pasif | OK | `preferred invoice id` yoksa dugme devre disi |
| 2026-06-12 | v1.06 | OK | Baglam kaydina git + build + self-test tamamlandi |

## v1.07 - Baglam Donemi Filtresi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Donem cikarimi var | OK | `TryResolvePeriod()` baglamdan `yyyy-MM` bilgisini ayristiriyor |
| Panel aksiyonu var | OK | `Bağlam Dönemi` dugmesi yil/ay filtresini tek tikla kuruyor |
| Donem yoksa aksiyon pasif | OK | Baglamda donem bilgisi yoksa dugme devre disi |
| 2026-06-12 | v1.07 | OK | Baglam donemi filtresi + build + self-test tamamlandi |

## v1.08 - Baglam Turu Filtresi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Tur cikarimi var | OK | `TryResolveInvoiceTypeName()` actionable rapor baglamindan fatura turunu ayristiriyor |
| Panel aksiyonu var | OK | `Bağlam Türü` dugmesi mevcut fatura turu filtresini tek tikla kuruyor |
| Evrak kontrol baglami disarida | OK | `Rapor: Evrak Kontrol` baglamlari bu aksiyonu aktiflestirmiyor |
| 2026-06-12 | v1.08 | OK | Baglam turu filtresi + build + self-test tamamlandi |

## v1.09 - Baglam No Aramasi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Fatura no cikarimi var | OK | `TryResolveInvoiceNumber()` actionable rapor baglamindan fatura no ayristiriyor |
| Panel aksiyonu var | OK | `Bağlam No` dugmesi arama kutusunu baglamdaki fatura no ile dolduruyor |
| Evrak kontrol baglami disarida | OK | `Rapor: Evrak Kontrol` baglamlari bu aksiyonu aktiflestirmiyor |
| 2026-06-12 | v1.09 | OK | Baglam no aramasi + build + self-test tamamlandi |

## v1.10 - Baglam Paneli Turkce Metin Temizligi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Baglam dugmeleri dogru Turkce | OK | `Bağlamı Kopyala`, `Bağlam Dönemi`, `Bağlam Türü`, `Bağlam No` metinleri duzeltildi |
| Baglam durum mesajlari dogru Turkce | OK | Kopyalama, filtre, donem, tur ve no aksiyon durum mesajlari duzeltildi |
| Donem algisi daraltildi | OK | `INV-001` gibi numaralar artik yanlislikla donem gibi yorumlanmiyor |
| 2026-06-12 | v1.10 | OK | Baglam paneli Turkce temizlik + parser duzeltmesi + build + self-test tamamlandi |

## v1.11 - ReportsView Turkce Metin Temizligi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| ReportsView sekme/metinleri duzgun | OK | `Ödenmemiş`, `Gecikmiş`, `Yaklaşan`, `İncelenmedi` metinleri duzeltildi |
| PDF rapor basliklari duzgun | OK | `ÖDENMEMİŞ`, `GECİKMİŞ`, `YAKLAŞAN`, `İŞLEM GEÇMİŞİ` basliklari duzeltildi |
| Ortak hata basligi duzgun | OK | `Uygulama başlatılamadı` metni guncellendi |
| 2026-06-12 | v1.11 | OK | ReportsView Turkce temizlik + build + self-test tamamlandi |

## v1.12 - Temiz Handoff Ozet Dosyasi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Temiz ozet dosyasi var | OK | `docs/06-guncel-durum-ozeti.md` olusturuldu |
| Temiz devam kilavuzu var | OK | `docs/00-codex-devam-kilavuzu.md` yeniden yazildi |
| Handoff okuma sirasi guncel | OK | Yeni chatlerde once temiz ozet dosyasi okunacak |
| 2026-06-12 | v1.12 | OK | Temiz handoff ozet dosyasi + build + self-test tamamlandi |

## v1.13 - Baglami Tek Tikla Daraltma
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Birlesik daraltma aksiyonu var | OK | `Bağlamı Daralt` dugmesi filtre + donem + tur + no ipuclarini tek tikta uyguluyor |
| Parser Turkce eslesmeleri temiz | OK | `İncelenmedi`, `Gecikmiş`, `PDF Kayıp` baglamlari dogru ayristiriliyor |
| 2026-06-12 | v1.13 | OK | Baglami tek tikla daraltma + build + self-test tamamlandi |

## v1.14 - Daraltma Sonrasi Otomatik Odak
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Tercihli kayda odak var | OK | Baglam kaydi gorunur listedeyse `Bağlamı Daralt` dogrudan o kaydi seciyor |
| Fallback secim var | OK | Tercihli kayit bulunamazsa ilk uygun kayda dusulup kullaniciya acik mesaj veriliyor |
| 2026-06-12 | v1.14 | OK | Daraltma sonrasi otomatik odak + build + self-test tamamlandi |

## v1.15 - Baglamdan Inceleme Akisi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Guclu baglam aksiyonu var | OK | `Bağlamdan İncele` uygun oldugunda ilgili review modunu dogrudan kuruyor |
| Ikincil baglam ipuclari uygulaniyor | OK | Donem, tur ve fatura no ayni akista uygulanip secim odagi korunuyor |
| 2026-06-12 | v1.15 | OK | Baglamdan inceleme akisi + build + self-test tamamlandi |

## v1.16 - Baglam Inceleme Kisayolu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kisayol aktif | OK | `Ctrl+Shift+I` ile `Bağlamdan İncele` akisi tetikleniyor |
| Inceleme ipucu guncel | OK | Kisa yol listesi yeni aksiyonu da gosteriyor |
| 2026-06-12 | v1.16 | OK | Baglam inceleme kisayolu + build + self-test tamamlandi |

## v1.17 - Baglam Aksiyon Tooltipleri
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Tooltip'ler eklendi | OK | Baglam panelindeki butonlar kisa aciklamalar gosteriyor |
| Kisayol gorunur | OK | `Bağlamdan İncele` tooltip'i `Ctrl+Shift+I` bilgisini de tasiyor |
| 2026-06-12 | v1.17 | OK | Baglam aksiyon tooltipleri + build + self-test tamamlandi |

## v1.18 - Baglam Aksiyon Satir Duzeni
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| WrapPanel duzeni var | OK | Baglam aksiyon satiri dar alanlarda alt satira akiyor |
| Buton bosluklari tutarli | OK | Aksiyonlar arasinda daha dengeli yatay/dikey bosluk verildi |
| 2026-06-12 | v1.18 | OK | Baglam aksiyon satir duzeni + build + self-test tamamlandi |

## v1.19 - Baglam Aksiyon Hiyerarsisi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Ana/ikincil grup var | OK | Baglam aksiyonlari iki ayri baslik altinda toplandi |
| Birincil aksiyon vurgusu var | OK | `Bağlamdan İncele` artik `PrimaryButton` ile one cikiyor |
| 2026-06-12 | v1.19 | OK | Baglam aksiyon hiyerarsisi + build + self-test tamamlandi |

## v1.19.1 - Acilis NullReference Hotfix
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| --health-check basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --health-check temiz gecti |
| Acilis korumasi var | OK | Inceleme navigasyon kontrolleri daha olusmadan gelen event akisi guvenli sekilde atlanıyor |
| 2026-06-12 | v1.19.1 | OK | Acilis NullReference hotfix + build + self-test + health-check tamamlandi |

## v1.20 - Baglam Aksiyon Durum Vurgulari
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Renkli durum vurgusu var | OK | Uygulanabilir baglam aksiyonlari baglam tipine gore renkleniyor |
| Pasif aksiyonlar soluk | OK | Islenemeyen butonlar daha dusuk vurgu ile gosteriliyor |
| 2026-06-13 | v1.20 | OK | Baglam aksiyon durum vurgulari + build + self-test tamamlandi |

## v1.21 - Hazir Aksiyon Ozeti
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Ozet satiri var | OK | Hazir aksiyon sayisi ve aksiyon adlari panelde metin olarak gosteriliyor |
| Baglam yoksa gizleniyor | OK | Ozet satiri yalnizca baglam gorunuyorsa aktif |
| 2026-06-13 | v1.21 | OK | Hazir aksiyon ozeti + build + self-test tamamlandi |

## v1.22 - Rozetli Hazir Aksiyon Ozeti
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Rozetli ozet var | OK | Hazir aksiyonlar mini rozetler halinde gosteriliyor |
| Renk uyumu var | OK | Rozet renkleri baglam aksiyon turleriyle tutarli |
| 2026-06-13 | v1.22 | OK | Rozetli hazir aksiyon ozeti + build + self-test tamamlandi |

## v1.23 - Tiklanabilir Aksiyon Rozetleri
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Rozetler tiklanabilir | OK | Hazir aksiyon rozetleri ilgili baglam aksiyonunu dogrudan tetikliyor |
| Tooltip destegi var | OK | Rozetler de kendi aksiyon aciklamasini gosteriyor |
| 2026-06-13 | v1.23 | OK | Tiklanabilir aksiyon rozetleri + build + self-test tamamlandi |

## v1.24 - Rozet Secim Geri Bildirimi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Secili rozet vurgusu var | OK | Son tiklanan rozet daha belirgin kenarlik ve tonla gosteriliyor |
| Rozet secimi korunuyor | OK | Son calistirilan aksiyon anahtarina gore rozet secimi guncelleniyor |
| 2026-06-13 | v1.24 | OK | Rozet secim geri bildirimi + build + self-test tamamlandi |

## v1.25 - Rozet Secim Temizleme
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Baglam degisiminde sifirlaniyor | OK | Yeni baglam imzasi gelince eski secili rozet vurgusu temizleniyor |
| Ayni baglamda korunuyor | OK | Ayni baglam icinde son tiklanan aksiyon rozet vurgusu korunuyor |
| 2026-06-13 | v1.25 | OK | Rozet secim temizleme + build + self-test tamamlandi |

## v1.26 - Baglami Temizle Aksiyonu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Temizleme dugmesi var | OK | `Bağlamı Temizle` review baglamini ve izlerini sifirliyor |
| Normal moda donus var | OK | Filtreler varsayilan akisina geri donuyor |
| 2026-06-13 | v1.26 | OK | Baglami temizle aksiyonu + build + self-test tamamlandi |

## v1.40 - Baglam Cipi Klavye Erisimi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Klavye odagi gorunur | OK | Odaklanan cip daha belirgin cerceveyle gosteriliyor |
| Klavyeden menu aciliyor | OK | `Shift+F10` veya menu tusu ile cip menusu aciliyor |
| 2026-06-13 | v1.40 | OK | Baglam cipi klavye erisimi + build + self-test tamamlandi |

## v1.39 - Baglam Cipi Sag Tik Menusu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Sag tik menusu var | OK | Baglam ciplerinde sag tikla `Uygula` ve/veya `Kopyala` secenekleri aciliyor |
| Sol tik davranisi korundu | OK | Hizli tik akisinda onceki davranis bozulmadi |
| 2026-06-13 | v1.39 | OK | Baglam cipi sag tik menusu + build + self-test tamamlandi |

## v1.38 - Cip Davranis Isareti
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Davranis isareti var | OK | Aksiyonlu ciplere `UYG`, kopyalama ciplere `KPY` etiketi eklendi |
| Tooltip uyumu var | OK | Mikro isaret ile tooltip davranis bilgisi birbiriyle uyumlu |
| 2026-06-13 | v1.38 | OK | Cip davranis isareti + build + self-test tamamlandi |

## v1.37 - Aksiyonlu Baglam Cipleri
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Aksiyonlu ciplere tiklama var | OK | Uygun ciplere tiklandiginda baglam filtresi, donem, tur veya no akisi tetikleniyor |
| Kopyalama davranisi korundu | OK | Aksiyon tanimsiz ciplere tiklama panoya kopyalama olarak devam ediyor |
| 2026-06-13 | v1.37 | OK | Aksiyonlu baglam cipleri + build + self-test tamamlandi |

## v1.36 - Baglam Cipi Hizli Kopyalama
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Ciplere tiklama var | OK | Baglam cipleri tiklandiginda ilgili metin panoya kopyalaniyor |
| Durum geri bildirimi var | OK | Kopyalama sonrasi alt durum cubugunda kisa mesaj gosteriliyor |
| 2026-06-13 | v1.36 | OK | Baglam cipi hizli kopyalama + build + self-test tamamlandi |

## v1.35 - Baglam Ozet/Detay Gorunumu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Ozet/detay gecisi var | OK | `Detay Metin` secimi ile tam baglam ve ozet metin arasinda gecis yapiliyor |
| Tercih saklaniyor | OK | Review baglam gorunumu tercihi config altina kaydediliyor |
| 2026-06-13 | v1.35 | OK | Baglam ozet/detay gorunumu + build + self-test tamamlandi |

## v1.34 - Pasif Baglam Aksiyon Nedenleri
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Pasif tooltip acik | OK | Pasif aksiyonlarda neden kullanilamadigi tooltip ile gosteriliyor |
| Aktif tooltip korundu | OK | Aktif aksiyonlarda kisa gorev aciklamasi korunuyor |
| 2026-06-13 | v1.34 | OK | Pasif baglam aksiyon nedenleri + build + self-test tamamlandi |

## v1.33 - Son Aksiyon Dugme Vurgusu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Dugme vurgusu var | OK | Son kullanilan ana baglam aksiyon dugmesi kenarlik ve yazi agirligi ile ayristiriliyor |
| Kaynak birligi var | OK | Dugme vurgusu `Son aksiyon` metniyle ayni aksiyon anahtarini kullaniyor |
| 2026-06-13 | v1.33 | OK | Son aksiyon dugme vurgusu + build + self-test tamamlandi |

## v1.32 - Son Baglam Aksiyonu Gosterimi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Son aksiyon satiri var | OK | Baglam panelinde `Son aksiyon: ...` satiri gosteriliyor |
| Kaynak birligi var | OK | Rozet, ana dugme ve kisayol akislarinda son aksiyon ayni kaynaktan izleniyor |
| 2026-06-13 | v1.32 | OK | Son baglam aksiyonu gosterimi + build + self-test tamamlandi |

## v1.31 - Filtre Ozetinde Gecici Baglam Vurgusu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Gecici vurgu var | OK | Filtre ozet satiri baglam aksiyonu sonrasi `Bağlam: ...` etiketiyle belirginlesiyor |
| Otomatik geri donus var | OK | Kisa sure sonra filtre ozeti normal metne geri donuyor |
| 2026-06-13 | v1.31 | OK | Filtre ozetinde gecici baglam vurgusu + build + self-test tamamlandi |

## v1.30 - Form Basliginda Baglam Odagi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Yardimci satir gorunurlugu var | OK | Baglam aksiyonuyla secilen kayitta form basligi altinda odak aciklamasi gosteriliyor |
| Yardimci satir otomatik gizleniyor | OK | Normal secim veya baglam temizleme sonrasi metin kapanıyor |
| 2026-06-13 | v1.30 | OK | Form basliginda baglam odagi + build + self-test tamamlandi |

## v1.29 - Baglam Odak Rozeti
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Rozet gorunurlugu var | OK | Baglam aksiyonuyla hedeflenen secili satirda `ODAK` rozeti gosteriliyor |
| Rozet otomatik temizleniyor | OK | Farkli satira gecis veya baglam temizleme sonrasi odak izi kaldiriliyor |
| 2026-06-13 | v1.29 | OK | Baglam odak rozeti + build + self-test tamamlandi |

## v1.28 - Baglam Durum Mesaji Temizligi
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Mesaj formati ortak | OK | Baglam aksiyonlari `Bağlam: ...` biciminde kisa durum mesaji uretir hale geldi |
| Kapsam yeterli | OK | Filtre, daraltma, inceleme, donem, tur, no ve temizleme akislarinda yeni format kullaniliyor |
| 2026-06-13 | v1.28 | OK | Baglam durum mesaji temizligi + build + self-test tamamlandi |

## v1.27 - Baglami Temizle Kisayolu
| Kontrol | Durum | Not |
| --- | --- | --- |
| Derleme basarili | OK | dotnet build .\\FaturaTakip.sln -c Release temiz gecti |
| --self-test basarili | OK | dotnet run -c Release --no-build --project .\\src\\FaturaTakip.App\\FaturaTakip.App.csproj -- --self-test temiz gecti |
| Kisayol aktif | OK | `Ctrl+Shift+X` ile `Bağlamı Temizle` akisi tetikleniyor |
| Inceleme ipucu guncel | OK | Kisayol listesi yeni temizleme aksiyonunu da gosteriyor |
| 2026-06-13 | v1.27 | OK | Baglami temizle kisayolu + build + self-test tamamlandi |
