﻿﻿﻿using System.IO;
using FaturaTakip.App.Data;
using FaturaTakip.App.Data.AuditLogs;
using FaturaTakip.App.Data.Dashboard;
using FaturaTakip.App.Data.Invoices;
using FaturaTakip.App.Data.InvoiceTypes;
using FaturaTakip.App.Data.Payments;
using FaturaTakip.App.Data.Reports;
using FaturaTakip.App.Data.Subscriptions;
using FaturaTakip.App.Infrastructure;

namespace FaturaTakip.App.Diagnostics;

public sealed class SelfTestRunner
{
    public void Run()
    {
        var testRoot = Path.Combine(Path.GetTempPath(), "FaturaTakip.SelfTest", Guid.NewGuid().ToString("N"));
        var databasePath = Path.Combine(testRoot, "database", "fatura_takip.db");
        var keepArtifacts = string.Equals(
            Environment.GetEnvironmentVariable("FATURATAKIP_SELFTEST_KEEP"),
            "1",
            StringComparison.OrdinalIgnoreCase);

        try
        {
            new DatabaseInitializer().Initialize(databasePath);
            var repository = new InvoiceTypeRepository(databasePath);

            var seeded = repository.GetAll();
            Assert(seeded.Count >= 6, "Baslangic fatura turleri olusturulmadi.");

            var added = repository.Add(new InvoiceTypeInput(
                "Test Turu",
                "Self-test kaydi",
                "adet",
                IsActive: true));
            Assert(added.Id > 0, "Fatura turu ekleme basarisiz.");

            var updated = repository.Update(added.Id, new InvoiceTypeInput(
                "Test Turu Guncel",
                "Self-test guncellemesi",
                "saat",
                IsActive: true));
            Assert(updated.DefaultUsageUnit == "saat", "Fatura turu guncelleme basarisiz.");

            repository.SetActive(updated.Id, isActive: false);
            var passive = repository.GetAll().Single(item => item.Id == updated.Id);
            Assert(!passive.IsActive, "Fatura turu pasife alma basarisiz.");

            var subscriptionRepository = new SubscriptionRepository(databasePath);
            var invoiceType = seeded.First();

            var addedSubscription = subscriptionRepository.Add(new SubscriptionInput(
                invoiceType.Id,
                "Ana Bina Test Aboneligi",
                "Test Kurumu",
                "SUB-001",
                "TES-001",
                "SAY-001",
                "Test Saglayici",
                "Test Mahallesi",
                "Ana Bina",
                invoiceType.DefaultUsageUnit,
                IsActive: true,
                StartDate: new DateTime(2026, 1, 1),
                EndDate: null,
                "Self-test abonelik kaydi"));
            Assert(addedSubscription.Id > 0, "Abonelik ekleme basarisiz.");
            Assert(addedSubscription.InvoiceTypeId == invoiceType.Id, "Abonelik fatura turune baglanmadi.");

            var updatedSubscription = subscriptionRepository.Update(addedSubscription.Id, new SubscriptionInput(
                invoiceType.Id,
                "Ana Bina Test Aboneligi Guncel",
                "Test Kurumu",
                "SUB-001",
                "TES-002",
                "SAY-001",
                "Test Saglayici",
                "Test Mahallesi",
                "Ana Bina",
                invoiceType.DefaultUsageUnit,
                IsActive: true,
                StartDate: new DateTime(2026, 1, 1),
                EndDate: null,
                "Self-test abonelik guncellemesi"));
            Assert(updatedSubscription.InstallationNo == "TES-002", "Abonelik duzenleme basarisiz.");

            var invoiceRepository = new InvoiceRepository(databasePath);
            var invoice = invoiceRepository.Add(new InvoiceInput(
                updatedSubscription.Id,
                2026,
                1,
                new DateTime(2026, 1, 10),
                new DateTime(2026, 1, 20),
                "INV-001",
                1250.75m,
                345.5m,
                invoiceType.DefaultUsageUnit,
                "Self-test fatura kaydi"));
            Assert(invoice.SubscriptionId == updatedSubscription.Id, "Fatura abonelige baglanmadi.");
            Assert(invoice.InvoiceTypeId == invoiceType.Id, "Fatura turu abonelikten alinmadi.");

            var updatedInvoice = invoiceRepository.Update(invoice.Id, new InvoiceInput(
                updatedSubscription.Id,
                2026,
                1,
                new DateTime(2026, 1, 10),
                new DateTime(2026, 1, 20),
                "INV-001-G",
                1300m,
                350m,
                invoiceType.DefaultUsageUnit,
                "Self-test fatura guncellemesi"));
            Assert(updatedInvoice.Amount == 1300m, "Fatura duzenleme basarisiz.");

            var reviewedInvoice = invoiceRepository.UpdateReviewStatus(
                updatedInvoice.Id,
                "Kontrol edildi",
                new DateTimeOffset(2026, 2, 1, 10, 30, 0, TimeSpan.FromHours(3)));
            Assert(reviewedInvoice.ReviewNote == "Kontrol edildi", "Inceleme notu kaydedilmedi.");
            Assert(reviewedInvoice.ReviewedAt is not null, "Inceleme zamani kaydedilmedi.");

            var clearedReviewInvoice = invoiceRepository.ClearReviewStatus(updatedInvoice.Id);
            Assert(string.IsNullOrWhiteSpace(clearedReviewInvoice.ReviewNote), "Inceleme notu temizlenmedi.");
            Assert(clearedReviewInvoice.ReviewedAt is null, "Inceleme zamani temizlenmedi.");

            var nextDraft = InvoiceDraftTemplateBuilder.FromInvoice(updatedInvoice);
            Assert(nextDraft.SubscriptionId == updatedInvoice.SubscriptionId, "Sonraki ay taslagi aboneligi korumadi.");
            Assert(nextDraft.InvoiceYear == 2026 && nextDraft.InvoiceMonth == 2, "Sonraki ay taslagi donemi bir ileri tasimadi.");
            Assert(nextDraft.InvoiceDate == new DateTime(2026, 2, 10), "Sonraki ay taslagi fatura tarihini bir ay ileri tasimadi.");
            Assert(nextDraft.DueDate == new DateTime(2026, 2, 20), "Sonraki ay taslagi son odeme tarihini bir ay ileri tasimadi.");
            Assert(string.IsNullOrEmpty(nextDraft.InvoiceNo), "Sonraki ay taslagi fatura numarasini bos birakmadi.");
            Assert(nextDraft.Amount == updatedInvoice.Amount, "Sonraki ay taslagi tutari korumadi.");
            Assert(nextDraft.UsageAmount == updatedInvoice.UsageAmount, "Sonraki ay taslagi kullanim miktarini korumadi.");
            Assert(nextDraft.UsageUnit == updatedInvoice.UsageUnit, "Sonraki ay taslagi kullanim birimini korumadi.");

            var yearRolloverInvoice = new Invoice
            {
                Id = updatedInvoice.Id,
                SubscriptionId = updatedInvoice.SubscriptionId,
                InvoiceTypeId = updatedInvoice.InvoiceTypeId,
                InvoiceTypeName = updatedInvoice.InvoiceTypeName,
                SubscriptionName = updatedInvoice.SubscriptionName,
                InstitutionName = updatedInvoice.InstitutionName,
                InvoiceYear = 2026,
                InvoiceMonth = 12,
                InvoiceDate = new DateTime(2026, 12, 31),
                DueDate = new DateTime(2027, 1, 5),
                InvoiceNo = updatedInvoice.InvoiceNo,
                Amount = updatedInvoice.Amount,
                PaidAmount = updatedInvoice.PaidAmount,
                UsageAmount = updatedInvoice.UsageAmount,
                UsageUnit = updatedInvoice.UsageUnit,
                Status = updatedInvoice.Status,
                Description = updatedInvoice.Description,
                PdfFilePath = updatedInvoice.PdfFilePath,
                PdfOriginalFileName = updatedInvoice.PdfOriginalFileName,
                PdfSha256Hash = updatedInvoice.PdfSha256Hash,
                PdfAttachedAt = updatedInvoice.PdfAttachedAt,
                CreatedAt = updatedInvoice.CreatedAt,
                UpdatedAt = updatedInvoice.UpdatedAt,
            };
            var yearRolloverDraft = InvoiceDraftTemplateBuilder.FromInvoice(yearRolloverInvoice);
            Assert(yearRolloverDraft.InvoiceYear == 2027 && yearRolloverDraft.InvoiceMonth == 1, "Sonraki ay taslagi yil donusunu dogru tasimadi.");
            Assert(yearRolloverDraft.InvoiceDate == new DateTime(2027, 1, 31), "Sonraki ay taslagi ay sonu tarihini koruyamadi.");

            var paymentSuggestion = PaymentEntrySuggestionBuilder.CreateDefault(updatedInvoice, new DateTime(2026, 2, 1));
            Assert(paymentSuggestion.PaymentDate == new DateTime(2026, 2, 1), "Odeme varsayilan taslagi bugun tarihini kullanmadi.");
            Assert(paymentSuggestion.Amount == updatedInvoice.RemainingAmount, "Odeme varsayilan taslagi kalan tutari kullanmadi.");
            Assert(string.IsNullOrEmpty(paymentSuggestion.Description), "Odeme varsayilan taslagi aciklamayi bos baslatmadi.");

            var selectedPaymentSuggestion = PaymentEntrySuggestionBuilder.CreateFromSelectedPayment(
                updatedInvoice,
                new Payment
                {
                    Id = 4,
                    InvoiceId = updatedInvoice.Id,
                    PaymentDate = new DateTime(2026, 1, 30),
                    Amount = 350m,
                    Description = "  Secili odeme  ",
                },
                new DateTime(2026, 2, 4));
            Assert(selectedPaymentSuggestion.PaymentDate == new DateTime(2026, 2, 4), "Secili odemeden taslak bugun tarihini kullanmadi.");
            Assert(selectedPaymentSuggestion.Amount == 350m, "Secili odemeden taslak mevcut odeme tutarini korumadi.");
            Assert(selectedPaymentSuggestion.Description == "Secili odeme", "Secili odemeden taslak aciklamayi trim etmedi.");

            var lowRemainingInvoice = new Invoice
            {
                Id = updatedInvoice.Id,
                SubscriptionId = updatedInvoice.SubscriptionId,
                InvoiceTypeId = updatedInvoice.InvoiceTypeId,
                InvoiceTypeName = updatedInvoice.InvoiceTypeName,
                SubscriptionName = updatedInvoice.SubscriptionName,
                InstitutionName = updatedInvoice.InstitutionName,
                InvoiceYear = updatedInvoice.InvoiceYear,
                InvoiceMonth = updatedInvoice.InvoiceMonth,
                InvoiceDate = updatedInvoice.InvoiceDate,
                DueDate = updatedInvoice.DueDate,
                InvoiceNo = updatedInvoice.InvoiceNo,
                Amount = 1500m,
                PaidAmount = 1490m,
                UsageAmount = updatedInvoice.UsageAmount,
                UsageUnit = updatedInvoice.UsageUnit,
                Status = updatedInvoice.Status,
                Description = updatedInvoice.Description,
                PdfFilePath = updatedInvoice.PdfFilePath,
                PdfOriginalFileName = updatedInvoice.PdfOriginalFileName,
                PdfSha256Hash = updatedInvoice.PdfSha256Hash,
                PdfAttachedAt = updatedInvoice.PdfAttachedAt,
                CreatedAt = updatedInvoice.CreatedAt,
                UpdatedAt = updatedInvoice.UpdatedAt,
            };
            var clippedSelectedPaymentSuggestion = PaymentEntrySuggestionBuilder.CreateFromSelectedPayment(
                lowRemainingInvoice,
                new Payment
                {
                    Id = 5,
                    InvoiceId = updatedInvoice.Id,
                    PaymentDate = new DateTime(2026, 1, 31),
                    Amount = 80m,
                    Description = "Kucuk kalan",
                },
                new DateTime(2026, 2, 5));
            Assert(clippedSelectedPaymentSuggestion.Amount == 10m, "Secili odemeden taslak kalan tutari asmayacak sekilde kirpmadi.");

            var recentPaymentSuggestion = PaymentEntrySuggestionBuilder.CreateFromRecentPayment(
                updatedInvoice,
                new[]
                {
                    new Payment { Id = 1, InvoiceId = updatedInvoice.Id, PaymentDate = new DateTime(2026, 1, 15), Amount = 100m, Description = "Ilk odeme" },
                    new Payment { Id = 2, InvoiceId = updatedInvoice.Id, PaymentDate = new DateTime(2026, 1, 20), Amount = 150m, Description = "  Son aciklama  " },
                },
                new DateTime(2026, 2, 2));
            Assert(recentPaymentSuggestion.PaymentDate == new DateTime(2026, 2, 2), "Son odemeden taslak bugun tarihini kullanmadi.");
            Assert(recentPaymentSuggestion.Amount == updatedInvoice.RemainingAmount, "Son odemeden taslak kalan tutari kullanmadi.");
            Assert(recentPaymentSuggestion.Description == "Son aciklama", "Son odemeden taslak en guncel aciklamayi getirmedi.");

            var emptyDescriptionSuggestion = PaymentEntrySuggestionBuilder.CreateFromRecentPayment(
                updatedInvoice,
                new[]
                {
                    new Payment { Id = 3, InvoiceId = updatedInvoice.Id, PaymentDate = new DateTime(2026, 1, 25), Amount = 50m, Description = "   " },
                },
                new DateTime(2026, 2, 3));
            Assert(string.IsNullOrEmpty(emptyDescriptionSuggestion.Description), "Bos son odeme aciklamasi bos donmeliydi.");

            var paymentHelperBadges = PaymentEntryHelperSummaryBuilder.BuildBadges(
                updatedInvoice,
                new[]
                {
                    new Payment { Id = 7, InvoiceId = updatedInvoice.Id, PaymentDate = new DateTime(2026, 1, 18), Amount = 120m, Description = "Son odeme aciklamasi" },
                },
                new Payment { Id = 8, InvoiceId = updatedInvoice.Id, PaymentDate = new DateTime(2026, 1, 19), Amount = 95m, Description = "Secili odeme" });
            Assert(paymentHelperBadges.Count == 3, "Odeme yardim rozetleri beklenen yardimlari uretmedi.");
            Assert(paymentHelperBadges.Any(item => item.Prefix == "KLN" && item.Text == "Kalan Tutar"), "Kalan tutar yardim rozeti uretilmedi.");
            Assert(paymentHelperBadges.Any(item => item.Prefix == "SON" && item.Text == "Son Aciklama"), "Son aciklama yardim rozeti uretilmedi.");
            Assert(paymentHelperBadges.Any(item => item.Prefix == "SEC" && item.Text == "Secili Odeme"), "Secili odeme yardim rozeti uretilmedi.");
            Assert(paymentHelperBadges.Any(item => item.ActionKey == "fill_remaining"), "Kalan tutar yardim rozeti aksiyon anahtari tasimiyor.");
            Assert(paymentHelperBadges.Any(item => item.ActionKey == "use_last"), "Son aciklama yardim rozeti aksiyon anahtari tasimiyor.");
            Assert(paymentHelperBadges.Any(item => item.ActionKey == "use_selected"), "Secili odeme yardim rozeti aksiyon anahtari tasimiyor.");
            Assert(paymentHelperBadges.Any(item => item.ToolTip.Contains("Enter/Space", StringComparison.Ordinal)), "Odeme yardim rozetlerinde klavye tooltip ipucu eksik.");
            Assert(
                PaymentStatusMessageFormatter.BuildActionSuccess("Odeme taslagi kalan tutarla guncellendi.", "Odeme Yardimi") == "Odeme Yardimi: Odeme taslagi kalan tutarla guncellendi.",
                "Odeme yardim status basari mesaji beklenen formati uretmedi.");
            Assert(
                PaymentStatusMessageFormatter.BuildActionError("Bu fatura icin daha once kaydedilmis odeme yok.", "Odeme Yardimi") == "Odeme Yardimi: Bu fatura icin daha once kaydedilmis odeme yok.",
                "Odeme yardim status hata mesaji beklenen formati uretmedi.");
            Assert(
                PaymentEntryHelperSummaryBuilder.BuildLastActionText("use_selected") == "Son hizli yardim: Secili Odeme uygulandi.",
                "Odeme yardim son aksiyon metni beklenen formati uretmedi.");
            Assert(
                PaymentEntryHelperSummaryBuilder.BuildLastActionToolTip("use_selected") == "Tikla ve Secili Odeme yardimini yeniden calistir.",
                "Odeme yardim son aksiyon tooltip metni beklenen formati uretmedi.");
            Assert(
                PaymentEntryHelperSummaryBuilder.BuildLastActionPrefix("use_selected") == "SEC",
                "Odeme yardim son aksiyon prefix metni beklenen formati uretmedi.");
            Assert(
                PaymentEntryHelperSummaryBuilder.BuildReplayFeedbackText("Son hizli yardim: Secili Odeme uygulandi.", true) == "Son hizli yardim: Secili Odeme uygulandi. (yeniden tetiklendi)",
                "Odeme yardim tekrar geri bildirimi beklenen formati uretmedi.");
            Assert(
                PaymentEntryHelperSummaryBuilder.BuildReplayPreferenceSummaryText("use_selected", 3, "high") == "Secili Odeme replay ayari: 3 sn, guclu vurgu.",
                "Odeme yardim replay tercih ozeti beklenen formati uretmedi.");
            var selectedPaymentHelperBadges = PaymentEntryHelperSummaryBuilder.BuildBadges(
                updatedInvoice,
                new[] { new Payment { Id = 10, InvoiceId = updatedInvoice.Id, PaymentDate = new DateTime(2026, 1, 21), Amount = 40m, Description = "Aciklama" } },
                new Payment { Id = 11, InvoiceId = updatedInvoice.Id, PaymentDate = new DateTime(2026, 1, 22), Amount = 30m, Description = "Secim" },
                selectedActionKey: "use_selected");
            Assert(selectedPaymentHelperBadges.Any(item => item.ActionKey == "use_selected" && item.IsSelected), "Secili odeme yardim rozeti son kullanilan secim vurgusunu tasimiyor.");
            Assert(
                PaymentEntryHelperSummaryBuilder.BuildSummaryText(
                    updatedInvoice,
                    new[] { new Payment { Id = 9, InvoiceId = updatedInvoice.Id, PaymentDate = new DateTime(2026, 1, 20), Amount = 50m, Description = "Aciklama" } },
                    null) == "Hazir yardimlar: Kalan Tutar, Son Aciklama.",
                "Odeme yardim ozet metni beklenen sirayi uretmedi.");
            Assert(
                PaymentEntryHelperSummaryBuilder.BuildSummaryText(null, Array.Empty<Payment>(), null) == "Hazir odeme yardimi yok.",
                "Bos odeme yardim ozeti beklenen metni uretmedi.");
            Assert(
                PaymentPdfHelperSummaryBuilder.BuildSummaryText(null, paymentPdfExists: false) == "PDF islemleri icin secili odeme yok.",
                "Bos odeme PDF yardim ozeti beklenen metni uretmedi.");
            var paymentPdfMissingBadges = PaymentPdfHelperSummaryBuilder.BuildBadges(
                new Payment { Id = 12, InvoiceId = updatedInvoice.Id, PaymentDate = new DateTime(2026, 1, 23), Amount = 80m, Description = "Eksik", PdfFilePath = string.Empty },
                paymentPdfExists: false);
            Assert(paymentPdfMissingBadges.Any(item => item.Text == "PDF Bekleniyor"), "PDF eklenmemis odeme icin beklenen yardim rozeti uretilmedi.");
            Assert(paymentPdfMissingBadges.Any(item => item.ActionKey == "select_pdf"), "PDF bekleyen odeme rozetleri secme aksiyonu tasimiyor.");
            var paymentPdfReadyBadges = PaymentPdfHelperSummaryBuilder.BuildBadges(
                new Payment { Id = 13, InvoiceId = updatedInvoice.Id, PaymentDate = new DateTime(2026, 1, 24), Amount = 60m, Description = "Var", PdfFilePath = "attachments/payments/2026/01/a.pdf", PdfOriginalFileName = "a.pdf" },
                paymentPdfExists: true);
            Assert(paymentPdfReadyBadges.Any(item => item.Text == "PDF Kayitli"), "PDF kayitli odeme icin yardim rozeti uretilmedi.");
            Assert(paymentPdfReadyBadges.Any(item => item.ActionKey == "open_pdf"), "PDF kayitli odeme rozetleri acma aksiyonu tasimiyor.");
            var paymentPdfLostBadges = PaymentPdfHelperSummaryBuilder.BuildBadges(
                new Payment { Id = 14, InvoiceId = updatedInvoice.Id, PaymentDate = new DateTime(2026, 1, 25), Amount = 55m, Description = "Kayip", PdfFilePath = "attachments/payments/2026/01/missing.pdf", PdfOriginalFileName = "missing.pdf" },
                paymentPdfExists: false);
            Assert(paymentPdfLostBadges.Any(item => item.Text == "PDF Kayip"), "PDF kayip odeme icin yardim rozeti uretilmedi.");
            Assert(paymentPdfLostBadges.Any(item => item.ToolTip.Contains("Enter/Space", StringComparison.Ordinal)), "Odeme PDF yardim rozetlerinde klavye tooltip ipucu eksik.");
            var selectedPaymentPdfBadges = PaymentPdfHelperSummaryBuilder.BuildBadges(
                new Payment { Id = 16, InvoiceId = updatedInvoice.Id, PaymentDate = new DateTime(2026, 1, 27), Amount = 44m, Description = "Secim", PdfFilePath = "attachments/payments/2026/01/exists.pdf", PdfOriginalFileName = "exists.pdf" },
                paymentPdfExists: true,
                selectedActionKey: "open_pdf");
            Assert(selectedPaymentPdfBadges.Any(item => item.ActionKey == "open_pdf" && item.IsSelected), "Odeme PDF yardim rozeti son kullanilan secim vurgusunu tasimiyor.");
            Assert(
                PaymentStatusMessageFormatter.BuildActionSuccess("Ödeme PDF dosyası açıldı.", "PDF Yardimi") == "PDF Yardimi: Ödeme PDF dosyası açıldı.",
                "Odeme PDF yardim status basari mesaji beklenen formati uretmedi.");
            Assert(
                PaymentStatusMessageFormatter.BuildActionError("Ödeme PDF dosyası bulunamadı.", "PDF Yardimi") == "PDF Yardimi: Ödeme PDF dosyası bulunamadı.",
                "Odeme PDF yardim status hata mesaji beklenen formati uretmedi.");
            Assert(
                PaymentPdfHelperSummaryBuilder.BuildLastActionText("open_pdf") == "Son hizli yardim: PDF Ac uygulandi.",
                "Odeme PDF yardim son aksiyon metni beklenen formati uretmedi.");
            Assert(
                PaymentPdfHelperSummaryBuilder.BuildLastActionToolTip("open_pdf") == "Tikla ve PDF Ac yardimini yeniden calistir.",
                "Odeme PDF yardim son aksiyon tooltip metni beklenen formati uretmedi.");
            Assert(
                PaymentPdfHelperSummaryBuilder.BuildLastActionPrefix("open_pdf") == "AC",
                "Odeme PDF yardim son aksiyon prefix metni beklenen formati uretmedi.");
            Assert(
                PaymentPdfHelperSummaryBuilder.BuildReplayFeedbackText("Son hizli yardim: PDF Ac uygulandi.", true) == "Son hizli yardim: PDF Ac uygulandi. (yeniden tetiklendi)",
                "Odeme PDF yardim tekrar geri bildirimi beklenen formati uretmedi.");
            Assert(
                PaymentPdfHelperSummaryBuilder.BuildReplayPreferenceSummaryText("open_pdf", 2, "medium") == "PDF Ac replay ayari: 2 sn, orta vurgu.",
                "Odeme PDF replay tercih ozeti beklenen formati uretmedi.");
            var replayPreferences = new InvoiceReviewPreferences
            {
                ShowContext = true,
                ShowContextDetails = true,
                PaymentShortcutReplaySeconds = 9,
                PaymentShortcutReplayEmphasis = "unknown"
            }.Sanitize();
            Assert(replayPreferences.PaymentShortcutReplaySeconds == 4, "Replay tercih suresi ust sinirda kirpilamadi.");
            Assert(replayPreferences.PaymentShortcutReplayEmphasis == "medium", "Replay tercih vurgu seviyesi varsayilana donmedi.");
            Assert(
                PaymentPdfHelperSummaryBuilder.BuildSummaryText(
                    new Payment { Id = 15, InvoiceId = updatedInvoice.Id, PaymentDate = new DateTime(2026, 1, 26), Amount = 45m, Description = "Hazir", PdfFilePath = "attachments/payments/2026/01/ready.pdf" },
                    paymentPdfExists: true) == "Secili odeme icin PDF kaydi hazir.",
                "PDF hazir durum ozeti beklenen metni uretmedi.");

            var paymentRepository = new PaymentRepository(databasePath);
            var partialPayment = paymentRepository.Add(new PaymentInput(
                updatedInvoice.Id,
                new DateTime(2026, 1, 25),
                300m,
                "Self-test kismi odeme"));
            Assert(partialPayment.Id > 0, "Odeme kaydi ekleme basarisiz.");
            Assert(partialPayment.Description == "Self-test kismi odeme", "Odeme aciklamasi saklanmadi.");

            var partialInvoice = invoiceRepository.GetAll().Single(item => item.Id == updatedInvoice.Id);
            Assert(partialInvoice.PaidAmount == 300m, "Kismi odeme toplami faturaya yansimadi.");
            Assert(partialInvoice.RemainingAmount == 1000m, "Kalan odeme tutari hatali hesaplandi.");
            Assert(partialInvoice.Status == "unpaid", "Kismi odeme faturayi erken odendi yapmamali.");
            Assert(partialInvoice.State == "Kısmi", "Kismi odeme durumu gosterilmedi.");
            Assert(paymentRepository.GetForInvoice(updatedInvoice.Id).Count == 1, "Fatura odeme listesi okunamadi.");

            AssertThrows(
                () => paymentRepository.Add(new PaymentInput(
                    updatedInvoice.Id,
                    new DateTime(2026, 1, 26),
                    1000.01m,
                    "Kalani asan odeme")),
                "Kalan tutari asan odeme engellenmedi.");

            paymentRepository.Add(new PaymentInput(
                updatedInvoice.Id,
                new DateTime(2026, 1, 26),
                1000m,
                "Self-test tamamlama odemesi"));
            var paidInvoice = invoiceRepository.GetAll().Single(item => item.Id == updatedInvoice.Id);
            Assert(paidInvoice.PaidAmount == 1300m, "Tam odeme toplami faturaya yansimadi.");
            Assert(paidInvoice.RemainingAmount == 0m, "Tam odeme sonrasi kalan tutar sifirlanmadi.");
            Assert(paidInvoice.Status == "paid", "Tam odeme faturayi odendi yapmadi.");

            var increasedInvoice = invoiceRepository.Update(updatedInvoice.Id, new InvoiceInput(
                updatedSubscription.Id,
                2026,
                1,
                new DateTime(2026, 1, 10),
                new DateTime(2026, 1, 20),
                "INV-001-G",
                1500m,
                350m,
                invoiceType.DefaultUsageUnit,
                "Self-test fatura tutari artirildi"));
            Assert(increasedInvoice.Status == "unpaid", "Tutar artinca odeme durumu yeniden hesaplanmadi.");
            Assert(increasedInvoice.RemainingAmount == 200m, "Tutar artisi sonrasi kalan odeme hatali.");

            paymentRepository.Add(new PaymentInput(
                updatedInvoice.Id,
                new DateTime(2026, 1, 27),
                200m,
                "Self-test son odeme"));
            var fullyPaidInvoice = invoiceRepository.GetAll().Single(item => item.Id == updatedInvoice.Id);
            Assert(fullyPaidInvoice.Status == "paid", "Ek odeme sonrasi fatura yeniden odendi olmadi.");
            Assert(fullyPaidInvoice.PaidAmount == 1500m, "Ek odeme toplami hatali.");

            AssertThrows(
                () => paymentRepository.Add(new PaymentInput(
                    updatedInvoice.Id,
                    new DateTime(2026, 1, 28),
                    -1m,
                    "Negatif odeme")),
                "Negatif odeme tutari engellenmedi.");
            AssertThrows(
                () => paymentRepository.Add(new PaymentInput(
                    999999,
                    new DateTime(2026, 1, 28),
                    1m,
                    "Olmayan fatura")),
                "Olmayan faturaya odeme eklenebildi.");

            var samplePaymentPdfPath = Path.Combine(testRoot, "sample-payment.pdf");
            File.WriteAllText(samplePaymentPdfPath, "%PDF-1.4\n1 0 obj\n<<>>\nendobj\ntrailer\n<<>>\n%%EOF");

            var paymentWithPdf = paymentRepository.AttachPdf(partialPayment.Id, samplePaymentPdfPath);
            Assert(paymentWithPdf.HasPdf, "Odeme PDF metadata kaydi olusturulmadi.");
            Assert(paymentWithPdf.PdfOriginalFileName == "sample-payment.pdf", "Odeme PDF orijinal dosya adi saklanmadi.");
            Assert(!string.IsNullOrWhiteSpace(paymentWithPdf.PdfSha256Hash), "Odeme PDF hash bilgisi saklanmadi.");
            Assert(paymentWithPdf.PdfFilePath.StartsWith(Path.Combine("attachments", "payments", "2026", "01"), StringComparison.Ordinal), "Odeme PDF hedef klasoru odeme tarihi altinda degil.");
            Assert(paymentRepository.PdfFileExists(paymentWithPdf), "Kopyalanan odeme PDF dosyasi bulunamadi.");

            var attachedPaymentPdfPath = paymentRepository.GetPdfAbsolutePath(paymentWithPdf);
            File.Delete(attachedPaymentPdfPath);
            Assert(paymentRepository.IsPdfMissing(paymentWithPdf), "Kayip odeme PDF dosyasi algilanmadi.");

            var samplePdfPath = Path.Combine(testRoot, "sample-invoice.pdf");
            File.WriteAllText(samplePdfPath, "%PDF-1.4\n1 0 obj\n<<>>\nendobj\ntrailer\n<<>>\n%%EOF");

            var invoiceWithPdf = invoiceRepository.AttachPdf(updatedInvoice.Id, samplePdfPath);
            Assert(invoiceWithPdf.HasPdf, "Fatura PDF metadata kaydi olusturulmadi.");
            Assert(invoiceWithPdf.PdfOriginalFileName == "sample-invoice.pdf", "PDF orijinal dosya adi saklanmadi.");
            Assert(!string.IsNullOrWhiteSpace(invoiceWithPdf.PdfSha256Hash), "PDF hash bilgisi saklanmadi.");
            Assert(invoiceWithPdf.PdfFilePath.StartsWith(Path.Combine("attachments", "invoices", "2026", "01"), StringComparison.Ordinal), "PDF hedef klasoru donem altinda degil.");
            Assert(invoiceRepository.PdfFileExists(invoiceWithPdf), "Kopyalanan PDF dosyasi bulunamadi.");

            var attachedPdfPath = invoiceRepository.GetPdfAbsolutePath(invoiceWithPdf);
            File.Delete(attachedPdfPath);
            Assert(invoiceRepository.IsPdfMissing(invoiceWithPdf), "Kayip PDF dosyasi algilanmadi.");

            var invalidAttachmentPath = Path.Combine(testRoot, "not-pdf.txt");
            File.WriteAllText(invalidAttachmentPath, "PDF degil");
            AssertThrows(
                () => invoiceRepository.AttachPdf(updatedInvoice.Id, invalidAttachmentPath),
                "PDF olmayan dosya eklenebildi.");
            AssertThrows(
                () => paymentRepository.AttachPdf(partialPayment.Id, invalidAttachmentPath),
                "PDF olmayan odeme dosyasi eklenebildi.");

            var filterSamples = new[]
            {
                new Invoice
                {
                    Id = 1,
                    SubscriptionId = 10,
                    InvoiceTypeId = 100,
                    InvoiceTypeName = "Elektrik",
                    SubscriptionName = "Ana Bina",
                    InstitutionName = "Test Kurumu",
                    InvoiceYear = 2026,
                    InvoiceMonth = 1,
                    DueDate = new DateTime(2026, 1, 15),
                    InvoiceNo = "ELK-001",
                    Status = "unpaid",
                    Description = "Ocak elektrik",
                    ReviewNote = "Kontrol edildi",
                    ReviewedAt = new DateTimeOffset(2026, 2, 1, 10, 0, 0, TimeSpan.FromHours(3)),
                    PdfFilePath = "attachments/invoices/2026/01/elk.pdf",
                    PdfOriginalFileName = "elk.pdf",
                },
                new Invoice
                {
                    Id = 2,
                    SubscriptionId = 11,
                    InvoiceTypeId = 101,
                    InvoiceTypeName = "Su",
                    SubscriptionName = "Ek Hizmet Binasi",
                    InstitutionName = "Test Kurumu",
                    InvoiceYear = 2026,
                    InvoiceMonth = 2,
                    DueDate = new DateTime(2026, 3, 1),
                    InvoiceNo = "SU-001",
                    Status = "paid",
                    Description = "Subat su",
                },
                new Invoice
                {
                    Id = 3,
                    SubscriptionId = 10,
                    InvoiceTypeId = 100,
                    InvoiceTypeName = "Elektrik",
                    SubscriptionName = "Ana Bina",
                    InstitutionName = "Test Kurumu",
                    InvoiceYear = 2025,
                    InvoiceMonth = 12,
                    DueDate = new DateTime(2026, 1, 1),
                    InvoiceNo = "ELK-OLD",
                    Status = "canceled",
                    Description = "Eski kayit",
                },
            };
            var filterToday = new DateTime(2026, 2, 1);

            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(Year: 2026), filterToday).Count == 2, "Yil filtresi calismadi.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(Month: 1), filterToday).Single().InvoiceNo == "ELK-001", "Ay filtresi calismadi.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(InvoiceTypeId: 100), filterToday).Count == 2, "Fatura turu filtresi calismadi.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(SubscriptionId: 11), filterToday).Single().InvoiceNo == "SU-001", "Abonelik filtresi calismadi.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(PaymentStatus: InvoicePaymentStatusFilter.Paid), filterToday).Single().InvoiceNo == "SU-001", "Odeme durumu filtresi calismadi.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(PaymentStatus: InvoicePaymentStatusFilter.Overdue), filterToday).Single().InvoiceNo == "ELK-001", "Gecikmis fatura filtresi calismadi.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(PdfStatus: InvoicePdfStatusFilter.HasPdf), filterToday).Single().InvoiceNo == "ELK-001", "PDF var filtresi calismadi.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(PdfStatus: InvoicePdfStatusFilter.MissingPdf), filterToday).Count == 2, "PDF eksik filtresi calismadi.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(ReviewStatus: InvoiceReviewStatusFilter.Reviewed), filterToday).Single().InvoiceNo == "ELK-001", "Incelendi filtresi calismadi.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(ReviewStatus: InvoiceReviewStatusFilter.Unreviewed), filterToday).Count == 2, "Incelenmedi filtresi calismadi.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(SearchText: "Ana ELK"), filterToday).Count == 2, "Metin arama filtresi calismadi.");

            var missingPdfExportContext = InvoiceExportContextBuilder.Build(
                new InvoiceFilterCriteria(PdfStatus: InvoicePdfStatusFilter.MissingPdf),
                filterToday);
            Assert(missingPdfExportContext.FileSlug == "pdf-eksik", "Filtreli export baglami PDF eksik icin dogru slug uretmedi.");

            var currentMonthExportContext = InvoiceExportContextBuilder.Build(
                new InvoiceFilterCriteria(Year: filterToday.Year, Month: filterToday.Month),
                filterToday);
            Assert(currentMonthExportContext.FileSlug.Contains("bu-ay", StringComparison.Ordinal), "Filtreli export baglami bu ay secimi icin beklenen slug uretmedi.");

            Assert(InvoiceReviewNavigator.TryMove(1, 3, 1, out var nextIndex) && nextIndex == 2, "Kontrol turu sonraki kayda gecis hatali.");
            Assert(InvoiceReviewNavigator.TryMove(1, 3, -1, out var previousIndex) && previousIndex == 0, "Kontrol turu onceki kayda gecis hatali.");
            Assert(!InvoiceReviewNavigator.TryMove(0, 3, -1, out _), "Kontrol turu ilk kayittan geriye gecmemeli.");
            Assert(!InvoiceReviewNavigator.TryMove(2, 3, 1, out _), "Kontrol turu son kayittan ileri gecmemeli.");
            Assert(InvoiceReviewNavigator.BuildHint("PDF Eksik", 1, 4) == "Kontrol modu: PDF Eksik (2/4)", "Kontrol modu ipucu beklenen metni uretmedi.");
            Assert(InvoiceReviewNavigator.BuildHint("Gecikmis", null, 0) == "Kontrol modu: Gecikmis - gorunur liste bos.", "Bos kontrol modu ipucu beklenen metni uretmedi.");
            Assert(
                InvoiceReviewNavigator.BuildHint("PDF Eksik", 0, 3, includeShortcuts: true)
                    == "Kontrol modu: PDF Eksik (1/3) | Kisayollar: Ctrl+Shift+Sol/Sag, Ctrl+Shift+O, Ctrl+Shift+K, Ctrl+Shift+B, Ctrl+Shift+C, Ctrl+Shift+I, Ctrl+Shift+X",
                "Kontrol modu kisayol ipucu beklenen metni uretmedi.");
            Assert(
                InvoiceReviewNavigator.BuildHint("PDF Eksik", 0, 3, includeShortcuts: false, contextLabel: "Rapor: Evrak Kontrol > PDF Kayip")
                    == "Kontrol modu: PDF Eksik (1/3) | Baglam: Rapor: Evrak Kontrol > PDF Kayip",
                "Kontrol modu baglam ipucu beklenen metni uretmedi.");
            var reviewContextChips = InvoiceReviewContextFormatter.BuildChips("Rapor: Evrak Kontrol > PDF Kayip / Fatura / 2026-01");
            Assert(reviewContextChips.Count == 4, "Inceleme baglam rozet sayisi beklenen parcayi uretmedi.");
            Assert(reviewContextChips[0].Text == "Rapor: Evrak Kontrol", "Inceleme baglam rozetinde rapor basligi korunmadi.");
            Assert(reviewContextChips[0].Kind == "report", "Inceleme baglam rozetinde rapor tipi isaretlenmedi.");
            Assert(reviewContextChips[0].Prefix == "RPR", "Inceleme baglam rozetinde rapor on eki atanamadi.");
            Assert(reviewContextChips[0].ActionBadge == "UYG", "Aksiyonlu baglam cipinde beklenen isaret bulunamadi.");
            Assert(reviewContextChips[1].Text == "PDF Kayip", "Inceleme baglam rozetinde issue tipi ayristirilamadi.");
            Assert(reviewContextChips[1].Kind == "issue", "Inceleme baglam rozetinde issue tipi atanamadi.");
            Assert(reviewContextChips[1].Prefix == "ISS", "Inceleme baglam rozetinde issue on eki atanamadi.");
            Assert(reviewContextChips[2].Text == "Fatura", "Inceleme baglam rozetinde entity tipi ayristirilamadi.");
            Assert(reviewContextChips[2].Kind == "entity", "Inceleme baglam rozetinde entity tipi atanamadi.");
            Assert(reviewContextChips[2].Prefix == "VAR", "Inceleme baglam rozetinde entity on eki atanamadi.");
            Assert(reviewContextChips[3].Text == "2026-01", "Inceleme baglam rozetinde donem ozeti ayristirilamadi.");
            Assert(reviewContextChips[3].Kind == "period", "Inceleme baglam rozetinde donem tipi atanamadi.");
            Assert(reviewContextChips[3].Prefix == "DNM", "Inceleme baglam rozetinde donem on eki atanamadi.");
            Assert(reviewContextChips[2].ActionBadge == "KPY", "Kopyalama cipinde beklenen isaret bulunamadi.");
            Assert(reviewContextChips[0].ActionKey == "apply_filter", "Rapor cipi beklenen filtre aksiyonunu uretmedi.");
            Assert(reviewContextChips[3].ActionKey == "apply_period", "Donem cipi beklenen donem aksiyonunu uretmedi.");
            Assert(
                reviewContextChips[0].ToolTip.Contains("Enter/Space", StringComparison.Ordinal) &&
                reviewContextChips[0].ToolTip.Contains("Ctrl+C", StringComparison.Ordinal) &&
                reviewContextChips[0].ToolTip.Contains("Shift+F10", StringComparison.Ordinal),
                "Aksiyonlu baglam cipi tooltip'inde beklenen kisayol ipuclari bulunamadi.");
            Assert(
                reviewContextChips[2].ToolTip.Contains("Enter/Space", StringComparison.Ordinal) &&
                reviewContextChips[2].ToolTip.Contains("Ctrl+C", StringComparison.Ordinal) &&
                reviewContextChips[2].ToolTip.Contains("Shift+F10", StringComparison.Ordinal),
                "Kopyalama baglam cipi tooltip'inde beklenen kisayol ipuclari bulunamadi.");
            Assert(
                ReviewContextStatusMessageFormatter.BuildCopySuccess("INV-001", "Klavye") == "Klavye: INV-001 kopyalandı.",
                "Klavye baglam cipi kopyalama mesaji beklenen kisa formati uretmedi.");
            Assert(
                ReviewContextStatusMessageFormatter.BuildCopySuccess("INV-001", null) == "Bağlam çipi panoya kopyalandı: INV-001",
                "Varsayilan baglam cipi kopyalama mesaji bozuldu.");
            Assert(
                ReviewContextStatusMessageFormatter.BuildCopyError("Pano kilitli", "Menü") == "Menü: Kopyalama başarısız - Pano kilitli",
                "Menu baglam cipi kopyalama hata mesaji beklenen formati uretmedi.");
            Assert(
                ReviewContextStatusMessageFormatter.BuildActionSuccess("Filtre uygulandı", "PDF Eksik", "Çip") == "Çip: Filtre uygulandı - PDF Eksik.",
                "Cip baglam aksiyon basari mesaji beklenen kisa formati uretmedi.");
            Assert(
                ReviewContextStatusMessageFormatter.BuildActionSuccess("Filtre uygulandı", "PDF Eksik", null) == "Bağlam: Filtre uygulandı - PDF Eksik.",
                "Varsayilan baglam aksiyon basari mesaji bozuldu.");
            Assert(
                ReviewContextStatusMessageFormatter.BuildActionError("Bağlamdan uygulanabilir bir filtre çıkarılamadı.", "Klavye") == "Klavye: Bağlamdan uygulanabilir bir filtre çıkarılamadı.",
                "Klavye baglam aksiyon hata mesaji beklenen kisa formati uretmedi.");
            Assert(
                ReviewContextStatusMessageFormatter.TryResolveLead("Çip: Filtre uygulandı - PDF Eksik.", out var clickLead) &&
                clickLead == "Çip",
                "Cip durum mesajinda kaynak etiketi ayristirilamadi.");
            Assert(
                ReviewContextStatusMessageFormatter.TryResolveLead("Klavye: INV-001 kopyalandı.", out var keyboardLead) &&
                keyboardLead == "Klavye",
                "Klavye durum mesajinda kaynak etiketi ayristirilamadi.");
            Assert(
                ReviewContextStatusMessageFormatter.TryResolveLead("Menü: Fatura no uygulandı - INV-001.", out var menuLead) &&
                menuLead == "Menü",
                "Menu durum mesajinda kaynak etiketi ayristirilamadi.");
            Assert(
                !ReviewContextStatusMessageFormatter.TryResolveLead("Bağlam: Filtre uygulandı - PDF Eksik.", out _),
                "Normal baglam durum mesaji yanlislikla cip kaynagi gibi yorumlandi.");
            Assert(
                InvoiceReviewContextFormatter.TryResolveSuggestedFilter("Rapor: İncelenmedi > Elektrik / INV-001", out var unreviewedFilter) &&
                unreviewedFilter == InvoiceReviewContextFormatter.SuggestedFilter.Unreviewed,
                "Inceleme baglamindan 'Incelenmedi' filtresi cikartilamadi.");
            Assert(
                InvoiceReviewContextFormatter.TryResolveSuggestedFilter("Rapor: Gecikmiş > Su / INV-002", out var overdueFilter) &&
                overdueFilter == InvoiceReviewContextFormatter.SuggestedFilter.Overdue,
                "Inceleme baglamindan 'Gecikmis' filtresi cikartilamadi.");
            Assert(
                InvoiceReviewContextFormatter.TryResolveSuggestedFilter("Rapor: Evrak Kontrol > PDF Kayip / Fatura / 2026-01", out var missingPdfFilter) &&
                missingPdfFilter == InvoiceReviewContextFormatter.SuggestedFilter.MissingPdf,
                "Inceleme baglamindan 'PDF Eksik' filtresi cikartilamadi.");
            Assert(
                InvoiceReviewContextFormatter.TryResolvePeriod("Rapor: Evrak Kontrol > PDF Kayip / Fatura / 2026-01", out var periodYear, out var periodMonth) &&
                periodYear == 2026 &&
                periodMonth == 1,
                "Inceleme baglamindan donem filtresi cikartilamadi.");
            Assert(
                InvoiceReviewContextFormatter.TryResolveInvoiceTypeName("Rapor: İncelenmedi > Elektrik / INV-001", out var invoiceTypeName) &&
                invoiceTypeName == "Elektrik",
                "Inceleme baglamindan fatura turu cikartilamadi.");
            Assert(
                InvoiceReviewContextFormatter.TryResolveInvoiceNumber("Rapor: Gecikmiş > Su / INV-002", out var invoiceNumber) &&
                invoiceNumber == "INV-002",
                "Inceleme baglamindan fatura no cikartilamadi.");
            Assert(
                !InvoiceReviewContextFormatter.TryResolveInvoiceTypeName("Rapor: Evrak Kontrol > PDF Kayip / Fatura / 2026-01", out _),
                "Evrak kontrol baglami yanlislikla fatura turu gibi yorumlandi.");
            Assert(
                !InvoiceReviewContextFormatter.TryResolveInvoiceNumber("Rapor: Evrak Kontrol > PDF Kayip / Fatura / 2026-01", out _),
                "Evrak kontrol baglami yanlislikla fatura no gibi yorumlandi.");
            var actionableReviewContextChips = InvoiceReviewContextFormatter.BuildChips("Rapor: İncelenmedi > Elektrik / INV-001");
            Assert(actionableReviewContextChips.Any(chip => chip.Text == "Elektrik" && chip.ActionKey == "apply_type"), "Fatura turu cipi beklenen tur aksiyonunu uretmedi.");
            Assert(actionableReviewContextChips.Any(chip => chip.Text == "INV-001" && chip.ActionKey == "apply_invoice_no"), "Fatura no cipi beklenen arama aksiyonunu uretmedi.");
            var reorderedReviewContextChips = InvoiceReviewContextFormatter.BuildChips("Rapor: Evrak Kontrol > PDF Kayip / Fatura / 2026-01 > PDF Kayip / Fatura / 2026-01");
            Assert(reorderedReviewContextChips.Count == 4, "Inceleme baglam rozetleri tekrar eden parcayi tekillestiremedi.");
            Assert(reorderedReviewContextChips[0].Kind == "report", "Inceleme baglam rozetleri rapor basligini basa tasimadi.");
            Assert(reorderedReviewContextChips[1].Kind == "issue", "Inceleme baglam rozetleri issue tipini ikinci siraya getirmedi.");
            Assert(reorderedReviewContextChips[2].Kind == "entity", "Inceleme baglam rozetleri entity tipini issue sonrasina getirmedi.");
            Assert(reorderedReviewContextChips[3].Kind == "period", "Inceleme baglam rozetleri donem etiketini sona tasimadi.");

            var dashboardInvoices = new[]
            {
                new Invoice
                {
                    Id = 10,
                    InvoiceYear = 2026,
                    InvoiceMonth = 2,
                    Amount = 100m,
                    PaidAmount = 25m,
                    DueDate = new DateTime(2026, 2, 10),
                    Status = "unpaid",
                },
                new Invoice
                {
                    Id = 11,
                    InvoiceYear = 2026,
                    InvoiceMonth = 2,
                    Amount = 200m,
                    PaidAmount = 200m,
                    DueDate = new DateTime(2026, 2, 20),
                    Status = "paid",
                    ReviewNote = "Kontrol edildi",
                    ReviewedAt = new DateTimeOffset(2026, 2, 14, 9, 0, 0, TimeSpan.FromHours(3)),
                    PdfFilePath = "attachments/invoices/2026/02/paid.pdf",
                },
                new Invoice
                {
                    Id = 12,
                    InvoiceYear = 2026,
                    InvoiceMonth = 1,
                    Amount = 50m,
                    DueDate = new DateTime(2026, 3, 1),
                    Status = "unpaid",
                    PdfFilePath = "attachments/invoices/2026/01/old.pdf",
                },
            };
            var dashboardPayments = new[]
            {
                new Payment
                {
                    Id = 20,
                    InvoiceId = 10,
                    PaymentDate = new DateTime(2026, 2, 12),
                    Amount = 25m,
                },
                new Payment
                {
                    Id = 21,
                    InvoiceId = 11,
                    PaymentDate = new DateTime(2026, 2, 13),
                    Amount = 200m,
                    PdfFilePath = "attachments/payments/2026/02/paid.pdf",
                },
            };
            var dashboardSummary = DashboardSummaryCalculator.Calculate(
                dashboardInvoices,
                dashboardPayments,
                new DateTime(2026, 2, 15),
                invoice => !invoice.HasPdf,
                payment => !payment.HasPdf);
            Assert(dashboardSummary.MonthlyInvoiceCount == 2, "Dashboard aylik fatura sayisi hatali.");
            Assert(dashboardSummary.MonthlyInvoiceTotal == 300m, "Dashboard aylik fatura toplami hatali.");
            Assert(dashboardSummary.MonthlyPaymentCount == 2, "Dashboard aylik odeme sayisi hatali.");
            Assert(dashboardSummary.MonthlyPaymentTotal == 225m, "Dashboard aylik odeme toplami hatali.");
            Assert(dashboardSummary.UnreviewedInvoiceCount == 2, "Dashboard incelenmedi fatura sayisi hatali.");
            Assert(dashboardSummary.UnpaidInvoiceCount == 2, "Dashboard odenmemis fatura sayisi hatali.");
            Assert(dashboardSummary.UnpaidRemainingTotal == 125m, "Dashboard odenmemis kalan toplami hatali.");
            Assert(dashboardSummary.OverdueInvoiceCount == 1, "Dashboard gecikmis fatura sayisi hatali.");
            Assert(dashboardSummary.OverdueRemainingTotal == 75m, "Dashboard gecikmis kalan toplami hatali.");
            Assert(dashboardSummary.MissingInvoicePdfCount == 1, "Dashboard fatura PDF eksik sayisi hatali.");
            Assert(dashboardSummary.MissingPaymentPdfCount == 1, "Dashboard odeme PDF eksik sayisi hatali.");

            var reportSamples = new[]
            {
                new Invoice
                {
                    Id = 30,
                    InvoiceYear = 2026,
                    InvoiceMonth = 2,
                    Amount = 100m,
                    PaidAmount = 25m,
                    DueDate = new DateTime(2026, 2, 10),
                    Status = "unpaid",
                },
                new Invoice
                {
                    Id = 31,
                    InvoiceYear = 2026,
                    InvoiceMonth = 2,
                    Amount = 200m,
                    PaidAmount = 0m,
                    DueDate = new DateTime(2026, 2, 20),
                    Status = "unpaid",
                    PdfFilePath = "attachments/invoices/2026/02/unpaid.pdf",
                },
                new Invoice
                {
                    Id = 32,
                    InvoiceYear = 2026,
                    InvoiceMonth = 2,
                    Amount = 50m,
                    PaidAmount = 50m,
                    DueDate = new DateTime(2026, 2, 9),
                    Status = "paid",
                },
                new Invoice
                {
                    Id = 33,
                    InvoiceYear = 2026,
                    InvoiceMonth = 2,
                    Amount = 60m,
                    PaidAmount = 0m,
                    DueDate = new DateTime(2026, 2, 23),
                    Status = "unpaid",
                },
            };
            var report = ActionableInvoiceReportCalculator.Calculate(
                reportSamples,
                new DateTime(2026, 2, 15),
                upcomingDays: 7,
                invoice => false);
            Assert(report.Unpaid.Count == 3, "Rapor odenmemis sayisi hatali.");
            Assert(report.UnpaidRemainingTotal == 335m, "Rapor odenmemis kalan toplami hatali.");
            Assert(report.Overdue.Count == 1, "Rapor gecikmis sayisi hatali.");
            Assert(report.OverdueRemainingTotal == 75m, "Rapor gecikmis kalan toplami hatali.");
            Assert(report.Unreviewed.Count == 4, "Rapor incelenmedi sayisi hatali.");
            Assert(report.UnreviewedRemainingTotal == 335m, "Rapor incelenmedi kalan toplami hatali.");
            Assert(report.Upcoming.Count == 1, "Rapor yaklasan sayisi hatali.");
            Assert(report.UpcomingRemainingTotal == 200m, "Rapor yaklasan kalan toplami hatali.");

            var monthlySamples = new[]
            {
                new Invoice
                {
                    Id = 40,
                    InvoiceYear = 2026,
                    InvoiceMonth = 2,
                    Amount = 100m,
                    PaidAmount = 25m,
                    DueDate = new DateTime(2026, 2, 10),
                    Status = "unpaid",
                },
                new Invoice
                {
                    Id = 41,
                    InvoiceYear = 2026,
                    InvoiceMonth = 2,
                    Amount = 200m,
                    PaidAmount = 200m,
                    DueDate = new DateTime(2026, 2, 5),
                    Status = "paid",
                    PdfFilePath = "attachments/invoices/2026/02/paid.pdf",
                },
                new Invoice
                {
                    Id = 42,
                    InvoiceYear = 2026,
                    InvoiceMonth = 1,
                    Amount = 50m,
                    PaidAmount = 0m,
                    DueDate = new DateTime(2026, 1, 25),
                    Status = "unpaid",
                },
            };
            var monthly = MonthlyInvoiceReportCalculator.Calculate(
                monthlySamples,
                2026,
                2,
                invoiceTypeId: null,
                new DateTime(2026, 2, 15),
                invoice => !invoice.HasPdf);
            Assert(monthly.TotalInvoiceCount == 2, "Aylik rapor toplam fatura sayisi hatali.");
            Assert(monthly.TotalAmount == 300m, "Aylik rapor toplam tutar hatali.");
            Assert(monthly.PaidTotal == 225m, "Aylik rapor odenen toplam hatali.");
            Assert(monthly.RemainingTotal == 75m, "Aylik rapor kalan toplam hatali.");
            Assert(monthly.UnpaidInvoiceCount == 1, "Aylik rapor odenmemis sayisi hatali.");
            Assert(monthly.OverdueInvoiceCount == 1, "Aylik rapor gecikmis sayisi hatali.");
            Assert(monthly.MissingPdfCount == 1, "Aylik rapor PDF eksik sayisi hatali.");

            var typedMonthlySamples = new[]
            {
                new Invoice
                {
                    Id = 50,
                    InvoiceTypeId = 100,
                    InvoiceTypeName = "Elektrik",
                    InvoiceYear = 2026,
                    InvoiceMonth = 2,
                    Amount = 100m,
                    PaidAmount = 0m,
                    DueDate = new DateTime(2026, 2, 10),
                    Status = "unpaid",
                },
                new Invoice
                {
                    Id = 51,
                    InvoiceTypeId = 101,
                    InvoiceTypeName = "Su",
                    InvoiceYear = 2026,
                    InvoiceMonth = 2,
                    Amount = 200m,
                    PaidAmount = 0m,
                    DueDate = new DateTime(2026, 2, 11),
                    Status = "unpaid",
                },
            };
            var typedMonthly = MonthlyInvoiceReportCalculator.Calculate(
                typedMonthlySamples,
                2026,
                2,
                invoiceTypeId: 100,
                new DateTime(2026, 2, 15),
                invoice => false);
            Assert(typedMonthly.TotalInvoiceCount == 1, "Tur filtresi aylik raporda calismadi.");
            Assert(typedMonthly.TotalAmount == 100m, "Tur filtresi aylik toplam tutari yanlis.");

            var subscriptionSamples = new[]
            {
                new Invoice
                {
                    Id = 60,
                    SubscriptionId = 10,
                    InvoiceYear = 2026,
                    InvoiceMonth = 2,
                    Amount = 120m,
                    PaidAmount = 20m,
                    DueDate = new DateTime(2026, 2, 10),
                    Status = "unpaid",
                },
                new Invoice
                {
                    Id = 61,
                    SubscriptionId = 10,
                    InvoiceYear = 2026,
                    InvoiceMonth = 1,
                    Amount = 80m,
                    PaidAmount = 80m,
                    DueDate = new DateTime(2026, 1, 5),
                    Status = "paid",
                    PdfFilePath = "attachments/invoices/2026/01/paid.pdf",
                },
                new Invoice
                {
                    Id = 62,
                    SubscriptionId = 11,
                    InvoiceYear = 2026,
                    InvoiceMonth = 2,
                    Amount = 999m,
                    PaidAmount = 0m,
                    DueDate = new DateTime(2026, 2, 12),
                    Status = "unpaid",
                },
            };
            var comparison = SubscriptionMonthlyComparisonCalculator.Calculate(
                subscriptionSamples,
                subscriptionId: 10,
                year: 2026,
                month: 2,
                today: new DateTime(2026, 2, 15),
                isPdfMissing: invoice => false);
            Assert(comparison.Current.TotalInvoiceCount == 1, "Abonelik raporu (current) toplam fatura sayisi hatali.");
            Assert(comparison.Current.TotalAmount == 120m, "Abonelik raporu (current) toplam tutar hatali.");
            Assert(comparison.Current.PaidTotal == 20m, "Abonelik raporu (current) odenen toplam hatali.");
            Assert(comparison.Current.RemainingTotal == 100m, "Abonelik raporu (current) kalan toplam hatali.");
            Assert(comparison.Previous.TotalAmount == 80m, "Abonelik raporu (previous) toplam tutar hatali.");
            Assert(comparison.TotalAmountDelta == 40m, "Abonelik raporu toplam delta hatali.");

            var yearlySamples = new[]
            {
                new Invoice
                {
                    Id = 70,
                    SubscriptionId = 10,
                    InvoiceYear = 2026,
                    InvoiceMonth = 1,
                    Amount = 100m,
                    PaidAmount = 100m,
                    DueDate = new DateTime(2026, 1, 5),
                    Status = "paid",
                    PdfFilePath = "attachments/invoices/2026/01/paid.pdf",
                },
                new Invoice
                {
                    Id = 71,
                    SubscriptionId = 10,
                    InvoiceYear = 2026,
                    InvoiceMonth = 2,
                    Amount = 250m,
                    PaidAmount = 0m,
                    DueDate = new DateTime(2026, 2, 10),
                    Status = "unpaid",
                },
                new Invoice
                {
                    Id = 72,
                    SubscriptionId = 10,
                    InvoiceYear = 2026,
                    InvoiceMonth = 2,
                    Amount = 50m,
                    PaidAmount = 50m,
                    DueDate = new DateTime(2026, 2, 20),
                    Status = "paid",
                },
                new Invoice
                {
                    Id = 73,
                    SubscriptionId = 11,
                    InvoiceYear = 2026,
                    InvoiceMonth = 2,
                    Amount = 999m,
                    PaidAmount = 0m,
                    DueDate = new DateTime(2026, 2, 12),
                    Status = "unpaid",
                },
            };
            var yearly = SubscriptionYearlyReportCalculator.Calculate(
                yearlySamples,
                subscriptionId: 10,
                year: 2026,
                today: new DateTime(2026, 2, 15),
                isPdfMissing: invoice => !invoice.HasPdf);
            Assert(yearly.TotalInvoiceCount == 3, "Yillik rapor toplam fatura sayisi hatali.");
            Assert(yearly.TotalAmount == 400m, "Yillik rapor toplam tutar hatali.");
            Assert(yearly.PaidTotal == 150m, "Yillik rapor odenen toplam hatali.");
            Assert(yearly.RemainingTotal == 250m, "Yillik rapor kalan toplam hatali.");
            Assert(yearly.MissingPdfCount == 2, "Yillik rapor PDF eksik sayisi hatali.");
            Assert(yearly.HighestMonth == 2, "Yillik rapor en yuksek ay hatali.");
            Assert(yearly.HighestMonthTotal == 300m, "Yillik rapor en yuksek ay toplami hatali.");
            Assert(yearly.LowestMonth == 1, "Yillik rapor en dusuk ay hatali.");
            Assert(yearly.LowestMonthTotal == 100m, "Yillik rapor en dusuk ay toplami hatali.");

            var typeYearlySamples = new[]
            {
                new Invoice
                {
                    Id = 80,
                    InvoiceTypeId = 100,
                    InvoiceTypeName = "Elektrik",
                    SubscriptionId = 10,
                    SubscriptionName = "Ana Bina",
                    InstitutionName = "Kurum A",
                    InvoiceYear = 2026,
                    InvoiceMonth = 1,
                    Amount = 100m,
                    PaidAmount = 0m,
                    DueDate = new DateTime(2026, 1, 10),
                    Status = "unpaid",
                },
                new Invoice
                {
                    Id = 81,
                    InvoiceTypeId = 100,
                    InvoiceTypeName = "Elektrik",
                    SubscriptionId = 11,
                    SubscriptionName = "Sube",
                    InstitutionName = "Kurum B",
                    InvoiceYear = 2026,
                    InvoiceMonth = 2,
                    Amount = 250m,
                    PaidAmount = 250m,
                    DueDate = new DateTime(2026, 2, 5),
                    Status = "paid",
                    PdfFilePath = "attachments/invoices/2026/02/paid.pdf",
                },
                new Invoice
                {
                    Id = 82,
                    InvoiceTypeId = 101,
                    InvoiceTypeName = "Su",
                    SubscriptionId = 10,
                    SubscriptionName = "Ana Bina",
                    InstitutionName = "Kurum A",
                    InvoiceYear = 2026,
                    InvoiceMonth = 2,
                    Amount = 999m,
                    PaidAmount = 0m,
                    DueDate = new DateTime(2026, 2, 12),
                    Status = "unpaid",
                },
            };
            var typeYearly = InvoiceTypeYearlyReportCalculator.Calculate(
                typeYearlySamples,
                invoiceTypeId: 100,
                invoiceTypeName: "Elektrik",
                year: 2026,
                today: new DateTime(2026, 2, 15),
                isPdfMissing: invoice => !invoice.HasPdf);
            Assert(typeYearly.TotalInvoiceCount == 2, "Tur yillik rapor toplam fatura sayisi hatali.");
            Assert(typeYearly.TotalAmount == 350m, "Tur yillik rapor toplam tutar hatali.");
            Assert(typeYearly.Distribution.Count == 2, "Tur yillik rapor dagilim satir sayisi hatali.");
            Assert(typeYearly.Distribution[0].TotalAmount == 250m, "Tur yillik rapor dagilim siralamasi hatali.");

            var documentHealthInvoices = new List<Invoice>
            {
                new()
                {
                    Id = 1001,
                    InvoiceTypeId = 100,
                    InvoiceTypeName = "Elektrik",
                    SubscriptionId = 10,
                    SubscriptionName = "Ana Bina",
                    InstitutionName = "Kurum A",
                    InvoiceYear = 2026,
                    InvoiceMonth = 1,
                    Amount = 10m,
                    PaidAmount = 0m,
                    DueDate = new DateTime(2026, 1, 10),
                    Status = "unpaid",
                },
                new()
                {
                    Id = 1002,
                    InvoiceTypeId = 100,
                    InvoiceTypeName = "Elektrik",
                    SubscriptionId = 10,
                    SubscriptionName = "Ana Bina",
                    InstitutionName = "Kurum A",
                    InvoiceYear = 2026,
                    InvoiceMonth = 2,
                    Amount = 20m,
                    PaidAmount = 0m,
                    DueDate = new DateTime(2026, 2, 10),
                    Status = "unpaid",
                    PdfFilePath = "attachments/invoices/2026/02/missing.pdf",
                    PdfSha256Hash = "H-MISSING",
                },
                new()
                {
                    Id = 1003,
                    InvoiceTypeId = 101,
                    InvoiceTypeName = "Su",
                    SubscriptionId = 11,
                    SubscriptionName = "Sube",
                    InstitutionName = "Kurum B",
                    InvoiceYear = 2026,
                    InvoiceMonth = 3,
                    Amount = 30m,
                    PaidAmount = 0m,
                    DueDate = new DateTime(2026, 3, 10),
                    Status = "unpaid",
                    PdfFilePath = "attachments/invoices/2026/03/dup-1.pdf",
                    PdfSha256Hash = "H-DUP",
                },
                new()
                {
                    Id = 1004,
                    InvoiceTypeId = 101,
                    InvoiceTypeName = "Su",
                    SubscriptionId = 11,
                    SubscriptionName = "Sube",
                    InstitutionName = "Kurum B",
                    InvoiceYear = 2026,
                    InvoiceMonth = 4,
                    Amount = 40m,
                    PaidAmount = 0m,
                    DueDate = new DateTime(2026, 4, 10),
                    Status = "unpaid",
                    PdfFilePath = "attachments/invoices/2026/04/dup-2.pdf",
                    PdfSha256Hash = "H-DUP",
                },
            };

            var documentHealthPayments = new List<Payment>
            {
                new()
                {
                    Id = 2001,
                    InvoiceId = 1001,
                    PaymentDate = new DateTime(2026, 1, 5),
                    Amount = 1m,
                },
                new()
                {
                    Id = 2002,
                    InvoiceId = 1002,
                    PaymentDate = new DateTime(2026, 2, 5),
                    Amount = 2m,
                    PdfFilePath = "attachments/payments/2026/02/missing.pdf",
                    PdfSha256Hash = "P-MISSING",
                },
                new()
                {
                    Id = 2003,
                    InvoiceId = 1003,
                    PaymentDate = new DateTime(2026, 3, 5),
                    Amount = 3m,
                    PdfFilePath = "attachments/payments/2026/03/dup-1.pdf",
                    PdfSha256Hash = "P-DUP",
                },
                new()
                {
                    Id = 2004,
                    InvoiceId = 1004,
                    PaymentDate = new DateTime(2026, 4, 5),
                    Amount = 4m,
                    PdfFilePath = "attachments/payments/2026/04/dup-2.pdf",
                    PdfSha256Hash = "P-DUP",
                },
            };

            var documentHealth = DocumentHealthReportCalculator.Calculate(
                documentHealthInvoices,
                documentHealthPayments,
                invoice => invoice.Id != 1002,
                payment => payment.Id != 2002);

            Assert(documentHealth.InvoiceNoPdfCount == 1, "Evrak kontrol: fatura PDF yok sayisi hatali.");
            Assert(documentHealth.InvoiceMissingFileCount == 1, "Evrak kontrol: fatura PDF kayip sayisi hatali.");
            Assert(documentHealth.PaymentNoPdfCount == 1, "Evrak kontrol: odeme PDF yok sayisi hatali.");
            Assert(documentHealth.PaymentMissingFileCount == 1, "Evrak kontrol: odeme PDF kayip sayisi hatali.");
            Assert(documentHealth.DuplicateInvoiceHashItemCount == 2, "Evrak kontrol: fatura ayni-hash madde sayisi hatali.");
            Assert(documentHealth.DuplicatePaymentHashItemCount == 2, "Evrak kontrol: odeme ayni-hash madde sayisi hatali.");
            Assert(documentHealth.Issues.Count >= 8, "Evrak kontrol: uyari listesi beklenenden kisa.");

            var consistency = ConsistencyReportCalculator.Calculate(
                documentHealthInvoices,
                documentHealthPayments,
                invoice => invoice.Id != 1002,
                payment => payment.Id != 2002);
            Assert(consistency.ErrorCount == 0, "Tutarlilik denetimi ERROR uretti.");

            var exportXlsxPath = Path.Combine(testRoot, "exports", $"faturalar-{DateTime.Now:yyyyMMdd-HHmmss}.xlsx");
            Directory.CreateDirectory(Path.GetDirectoryName(exportXlsxPath)!);
            ExcelExportWriter.WriteInvoices(
                exportXlsxPath,
                documentHealthInvoices,
                isPdfMissing: invoice => !invoice.HasPdf);
            Assert(File.Exists(exportXlsxPath), "Excel export dosyasi olusmadi.");
            var exportFileInfo = new FileInfo(exportXlsxPath);
            Assert(exportFileInfo.Length > 1024, "Excel export dosyasi beklenenden kucuk.");

            var reportXlsxPath = Path.Combine(testRoot, "exports", $"raporlar-selftest-{DateTime.Now:yyyyMMdd-HHmmss}.xlsx");
            ExcelExportWriter.WriteReportWithHeader(
                reportXlsxPath,
                sheetName: "Rapor",
                meta: new ExcelExportWriter.ReportMeta(
                    AppTitle: "KURUM FATURA TAKIP PROGRAMI",
                    InstitutionName: "Test Kurum",
                    ReportTitle: "SELF-TEST EXCEL",
                    ReportPeriod: "2026/01",
                    ReportDate: new DateTime(2026, 6, 1),
                    CreatedBy: "codex",
                    FilterText: "Test"),
                summary: new[]
                {
                    new ExcelExportWriter.SummaryItem("Toplam Fatura", "1", "Toplam 1.00"),
                    new ExcelExportWriter.SummaryItem("Ödenen", "0.00", "PDF eksik 0"),
                    new ExcelExportWriter.SummaryItem("Kalan", "1.00", "Ödenmemiş 1, Gecikmiş 0"),
                },
                headers: new[]
                {
                    "Dönem",
                    "Tür",
                    "Abonelik",
                    "Kurum",
                    "Durum",
                    "Fatura Tarihi",
                    "Son Ödeme",
                    "Fatura No",
                    "Tutar",
                    "Kullanım",
                    "Birim",
                    "Ödenen",
                    "Kalan",
                    "Fatura PDF",
                    "Ödeme Tarihi",
                    "Ödeme PDF",
                    "Açıklama",
                },
                rows: new[]
                {
                    new object?[]
                    {
                        "2026/01",
                        "Elektrik",
                        "Ana Bina",
                        "Test Kurumu",
                        "Ödenmedi",
                        new DateTime(2026, 1, 10),
                        new DateTime(2026, 1, 20),
                        "INV-001",
                        1.00m,
                        10.5m,
                        "kWh",
                        0.00m,
                        1.00m,
                        "PDF Yok",
                        "",
                        "PDF Yok",
                        "Self-test satırı",
                    },
                },
                notes: "Self-test rapor örneği: kolon/başlık düzeni doğrulama amacıyla üretilmiştir.");
            Assert(File.Exists(reportXlsxPath), "Rapor excel export dosyasi olusmadi.");
            Assert(new FileInfo(reportXlsxPath).Length > 1024, "Rapor excel export dosyasi beklenenden kucuk.");

            // Yearly list report smoke: ensure YearlyAll-style template + detail sheets are produced.
            invoiceRepository.Add(new InvoiceInput(
                updatedSubscription.Id,
                2026,
                2,
                new DateTime(2026, 2, 10),
                new DateTime(2026, 2, 20),
                "INV-002",
                200m,
                20m,
                invoiceType.DefaultUsageUnit,
                "Self-test yearly list invoice (Feb)"));
            invoiceRepository.Add(new InvoiceInput(
                updatedSubscription.Id,
                2026,
                3,
                new DateTime(2026, 3, 10),
                new DateTime(2026, 3, 20),
                "INV-003",
                300m,
                30m,
                invoiceType.DefaultUsageUnit,
                "Self-test yearly list invoice (Mar)"));

            // Add a second invoice type to verify optional type filtering.
            var secondType = seeded.Skip(1).First();
            var secondSub = subscriptionRepository.Add(new SubscriptionInput(
                secondType.Id,
                "Ek Abonelik",
                "Test Kurumu",
                "SUB-002",
                "TES-010",
                "SAY-010",
                "Test Saglayici",
                "Test Mahallesi",
                "Ek Bina",
                secondType.DefaultUsageUnit,
                IsActive: true,
                StartDate: new DateTime(2026, 1, 1),
                EndDate: null,
                "Self-test abonelik (2)"));
            invoiceRepository.Add(new InvoiceInput(
                secondSub.Id,
                2026,
                1,
                new DateTime(2026, 1, 12),
                new DateTime(2026, 1, 25),
                "INV-X-001",
                50m,
                5m,
                secondType.DefaultUsageUnit,
                "Self-test yearly list invoice (other type)"));

            var yearlyInvoices = invoiceRepository.GetAll();
            var yearlyList = YearlyInvoiceListReportCalculator.Calculate(
                yearlyInvoices,
                2026,
                invoiceTypeId: null,
                today: new DateTime(2026, 6, 1),
                isPdfMissing: _ => false);
            Assert(yearlyList.Rows.Count >= 3, "Yillik liste raporu beklenen sayida satir uretmedi.");

            var yearlyExcelPath = Path.Combine(testRoot, "exports", $"raporlar-yillik-liste-selftest-{DateTime.Now:yyyyMMdd-HHmmss}.xlsx");
            var metaYearly = new ExcelExportWriter.ReportMeta(
                AppTitle: "KURUM FATURA TAKIP PROGRAMI",
                InstitutionName: "Test Kurum",
                ReportTitle: "YILLIK FATURA RAPORU",
                ReportPeriod: "2026",
                ReportDate: new DateTime(2026, 6, 1),
                CreatedBy: "codex",
                FilterText: "Self-test");

            var mainHeaders = (IReadOnlyList<string>)new[]
            {
                "Yıl",
                "Ay",
                "Abone Bilgisi",
                "F. Tarihi",
                "Fatura Sayısı",
                "Kullanım M.",
                "Fatura Tutar",
            };
            var mainRows = yearlyList.Rows.Select(r => (IReadOnlyList<object?>)new object?[]
            {
                r.Invoice.InvoiceYear,
                global::System.Globalization.CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat.GetMonthName(r.Invoice.InvoiceMonth),
                r.Invoice.SubscriptionName,
                r.Invoice.InvoiceDate,
                r.Invoice.InvoiceNo,
                r.Invoice.UsageAmount,
                r.Invoice.Amount,
            }).ToList();

            var detailHeaders = (IReadOnlyList<string>)new[]
            {
                "Dönem",
                "Tür",
                "Abonelik",
                "Kurum",
                "Durum",
                "Fatura Tarihi",
                "Son Ödeme",
                "Fatura No",
                "Tutar",
                "Kullanım",
                "Birim",
                "Ödenen",
                "Kalan",
                "Fatura PDF",
                "Açıklama",
            };
            var detailRows = yearlyList.Rows.Select(r => (IReadOnlyList<object?>)new object?[]
            {
                r.Invoice.Period,
                r.Invoice.InvoiceTypeName,
                r.Invoice.SubscriptionName,
                r.Invoice.InstitutionName,
                r.Invoice.State,
                r.Invoice.InvoiceDate,
                r.Invoice.DueDate,
                r.Invoice.InvoiceNo,
                r.Invoice.Amount,
                r.Invoice.UsageAmount,
                r.Invoice.UsageUnit,
                r.Invoice.PaidAmount,
                r.Invoice.RemainingAmount,
                r.PdfState,
                r.Invoice.Description,
            }).ToList();

            ExcelExportWriter.WriteReportWithTwoSheets(
                yearlyExcelPath,
                mainSheetName: "Rapor",
                meta: metaYearly,
                summary: new[]
                {
                    new ExcelExportWriter.SummaryItem("Toplam (Yıl)", yearlyList.TotalInvoiceCount.ToString(global::System.Globalization.CultureInfo.InvariantCulture), $"Toplam {yearlyList.TotalAmount:N2}"),
                },
                headers: mainHeaders,
                rows: mainRows,
                secondSheetName: "Detay",
                secondHeaders: detailHeaders,
                secondRows: detailRows,
                notes: "Self-test yillik liste: sablon + detay sayfasi smoke");
            Assert(File.Exists(yearlyExcelPath), "Yillik liste excel export dosyasi olusmadi.");

            using (var wb = new global::ClosedXML.Excel.XLWorkbook(yearlyExcelPath))
            {
                var rapor = wb.Worksheet("Rapor");
                Assert(rapor.Cell(6, 1).GetString() == "Açıklama", "Yillik liste excel: Aciklama satiri yazilmadi.");
                Assert(rapor.Cell(7, 1).GetString() == "Yıl", "Yillik liste excel: sablon basliklari yazilmadi.");
                Assert(wb.Worksheets.Any(s => s.Name == "Detay"), "Yillik liste excel: 'Detay' sayfasi bulunamadi.");
            }

            var reportPdfPath = Path.Combine(testRoot, "exports", $"raporlar-selftest-{DateTime.Now:yyyyMMdd-HHmmss}.pdf");
            PdfReportWriter.WriteSimpleTableReport(
                reportPdfPath,
                new PdfReportWriter.ReportMeta(
                    AppTitle: "KURUM FATURA TAKIP PROGRAMI",
                    InstitutionName: "Test Kurum",
                    ReportTitle: "SELF-TEST PDF",
                    ReportPeriod: "2026",
                    ReportDate: new DateTime(2026, 6, 1),
                    CreatedBy: "codex",
                    FilterText: string.Empty),
                summary: Array.Empty<PdfReportWriter.SummaryItem>(),
                headers: new[] { "Yıl", "Ay", "Abone Bilgisi", "F. Tarihi", "Fatura Sayısı", "Kullanım M.", "Fatura Tutar" },
                rows: new[]
                {
                    new[] { "2026", "Ocak", "Ana Bina", "31.01.2026", "INV-001", "10,500", "1,00" },
                    new[] { "2026", "Şubat", "Ana Bina", "29.02.2026", "INV-002", "20,000", "2,00" },
                },
                notes: "Self-test açıklama satırı",
                footerCells: new[]
                {
                    new PdfReportWriter.TableFooterCell("GENEL TOPLAM", ColumnSpan: 5, Bold: true, AlignRight: true),
                    new PdfReportWriter.TableFooterCell("30,500", Bold: true, AlignRight: true),
                    new PdfReportWriter.TableFooterCell("3,00", Bold: true, AlignRight: true),
                },
                columnWeights: new float[] { 1.0f, 1.4f, 2.4f, 1.6f, 3.2f, 1.6f, 1.8f });
            Assert(File.Exists(reportPdfPath), "PDF export dosyasi olusmadi.");
            Assert(new FileInfo(reportPdfPath).Length > 1024, "PDF export dosyasi beklenenden kucuk.");

            var filterPreferences = new AuditLogFilterPreferences(
                ActionType: "invoice_updated",
                EntityName: "invoices",
                UserName: "mstlfn",
                SearchText: "gecikmis",
                StartDate: new DateTime(2026, 6, 1),
                EndDate: new DateTime(2026, 6, 5),
                ChangedOnly: true);
            filterPreferences.Save(testRoot);
            var loadedPreferences = AuditLogFilterPreferences.LoadOrDefault(testRoot);
            Assert(loadedPreferences.ActionType == "invoice_updated", "Audit log filtre tercihi action saklanmadi.");
            Assert(loadedPreferences.EntityName == "invoices", "Audit log filtre tercihi varlik saklanmadi.");
            Assert(loadedPreferences.UserName == "mstlfn", "Audit log filtre tercihi kullanici saklanmadi.");
            Assert(loadedPreferences.SearchText == "gecikmis", "Audit log filtre tercihi arama metni saklanmadi.");
            Assert(loadedPreferences.StartDate == new DateTime(2026, 6, 1), "Audit log filtre tercihi baslangic tarihi saklanmadi.");
            Assert(loadedPreferences.EndDate == new DateTime(2026, 6, 5), "Audit log filtre tercihi bitis tarihi saklanmadi.");
            Assert(loadedPreferences.ChangedOnly, "Audit log filtre tercihi degisen alan filtresi saklanmadi.");

            var invoiceReviewPreferences = new InvoiceReviewPreferences
            {
                ShowContext = false,
                ShowContextDetails = false
            };
            invoiceReviewPreferences.Save(testRoot);
            var loadedInvoiceReviewPreferences = InvoiceReviewPreferences.LoadOrDefault(testRoot);
            Assert(!loadedInvoiceReviewPreferences.ShowContext, "Inceleme baglami gorunurluk tercihi saklanmadi.");
            Assert(!loadedInvoiceReviewPreferences.ShowContextDetails, "Inceleme baglami detay tercihi saklanmadi.");

            var backupsDir = Path.Combine(testRoot, "backups");
            Directory.CreateDirectory(backupsDir);
            var olderBackup = Path.Combine(backupsDir, "backup_20260601_090000.zip");
            var newerBackup = Path.Combine(backupsDir, "backup_20260602_090000.zip");
            var newestBackup = Path.Combine(backupsDir, "backup_20260603_090000.zip");
            File.WriteAllText(olderBackup, "old");
            File.WriteAllText(newerBackup, "newer");
            File.WriteAllText(newestBackup, "newest");
            File.SetLastWriteTimeUtc(olderBackup, new DateTime(2026, 6, 1, 9, 0, 0, DateTimeKind.Utc));
            File.SetLastWriteTimeUtc(newerBackup, new DateTime(2026, 6, 2, 9, 0, 0, DateTimeKind.Utc));
            File.SetLastWriteTimeUtc(newestBackup, new DateTime(2026, 6, 3, 9, 0, 0, DateTimeKind.Utc));

            var recentBackups = BackupFileCatalog.GetRecentBackups(testRoot, take: 2);
            Assert(recentBackups.Count == 2, "Backup katalogu son N yedegi sinirlayamadi.");
            Assert(recentBackups[0].FileName == "backup_20260603_090000.zip", "Backup katalogu en guncel yedegi basa getirmedi.");
            Assert(recentBackups[1].FileName == "backup_20260602_090000.zip", "Backup katalogu siralamasi hatali.");

            var newRestoreTargetAssessment = BackupRestoreService.EvaluateTargetRoot(Path.Combine(testRoot, "restore-empty"));
            Assert(newRestoreTargetAssessment.CanRestore, "Henuz olusmamis restore hedefi uygun sayilmadi.");
            Assert(newRestoreTargetAssessment.Message.Contains("olusturulacak", StringComparison.OrdinalIgnoreCase), "Yeni restore hedefi icin bilgilendirme mesaji bekleniyordu.");

            var existingEmptyRestoreRoot = Path.Combine(testRoot, "restore-existing-empty");
            Directory.CreateDirectory(existingEmptyRestoreRoot);
            var existingEmptyAssessment = BackupRestoreService.EvaluateTargetRoot(existingEmptyRestoreRoot);
            Assert(existingEmptyAssessment.CanRestore, "Mevcut bos restore hedefi uygun sayilmadi.");
            Assert(existingEmptyAssessment.Message.Contains("uygun", StringComparison.OrdinalIgnoreCase), "Bos restore hedefi uygunluk mesaji vermedi.");

            var suggestedRestoreBase = Path.Combine(testRoot, "restore-suggested");
            var suggestedRestoreTarget = BackupRestoreService.CreateSuggestedEmptyTarget(suggestedRestoreBase, new DateTime(2026, 6, 6, 12, 0, 0));
            Assert(Directory.Exists(suggestedRestoreTarget), "Onerilen bos restore klasoru olusturulamadi.");
            var secondSuggestedRestoreTarget = BackupRestoreService.CreateSuggestedEmptyTarget(suggestedRestoreBase, new DateTime(2026, 6, 6, 12, 0, 0));
            Assert(suggestedRestoreTarget != secondSuggestedRestoreTarget, "Ayni anda uretilen restore klasor adlari cakisti.");

            var previewWithoutZip = BackupRestoreService.BuildPreviewSummary(null, existingEmptyRestoreRoot);
            Assert(!previewWithoutZip.CanRestore, "Zip yokken restore preview hazir gorunmemeliydi.");
            Assert(previewWithoutZip.ZipSummary.Contains("secilmedi", StringComparison.OrdinalIgnoreCase), "Zip secilmedi mesaji bekleniyordu.");

            var restoreSourceRoot = Path.Combine(testRoot, "restore-source");
            var restoreDatabaseDir = Path.Combine(restoreSourceRoot, "database");
            Directory.CreateDirectory(restoreDatabaseDir);
            File.Copy(databasePath, Path.Combine(restoreDatabaseDir, "fatura_takip.db"), overwrite: true);

            var restoreZipPath = Path.Combine(testRoot, "restore-sample.zip");
            System.IO.Compression.ZipFile.CreateFromDirectory(
                restoreSourceRoot,
                restoreZipPath,
                System.IO.Compression.CompressionLevel.Optimal,
                includeBaseDirectory: true);
            Assert(File.Exists(restoreZipPath), "Restore self-test zip dosyasi olusturulamadi.");

            var nonEmptyRestoreTarget = Path.Combine(testRoot, "restore-target-non-empty");
            Directory.CreateDirectory(nonEmptyRestoreTarget);
            File.WriteAllText(Path.Combine(nonEmptyRestoreTarget, "occupied.txt"), "not empty");
            var nonEmptyAssessment = BackupRestoreService.EvaluateTargetRoot(nonEmptyRestoreTarget);
            Assert(!nonEmptyAssessment.CanRestore, "Dolu restore hedefi yanlislikla uygun sayildi.");
            Assert(nonEmptyAssessment.Message.Contains("bos degil", StringComparison.OrdinalIgnoreCase), "Dolu restore hedefi icin acik uyari mesaji uretilmedi.");
            var previewReady = BackupRestoreService.BuildPreviewSummary(restoreZipPath, existingEmptyRestoreRoot);
            Assert(previewReady.CanRestore, "Gecerli zip + bos hedef icin restore preview hazir gorunmeliydi.");
            Assert(previewReady.ReadinessSummary.Contains("hazir", StringComparison.OrdinalIgnoreCase), "Restore preview hazirlik mesaji bekleniyordu.");
            AssertThrows(
                () => BackupRestoreService.RestoreToEmptyRoot(restoreZipPath, nonEmptyRestoreTarget),
                "Bos olmayan hedefe restore engellenmedi.");

            AssertThrows(
                () => invoiceRepository.Add(new InvoiceInput(
                    updatedSubscription.Id,
                    2026,
                    1,
                    new DateTime(2026, 1, 11),
                    new DateTime(2026, 1, 21),
                    "INV-001-G",
                    1m,
                    1m,
                    invoiceType.DefaultUsageUnit,
                    "Tekrar fatura no")),
                "Ayni abonelikte ayni fatura numarasi engellenmedi.");

            AssertThrows(
                () => invoiceRepository.Add(new InvoiceInput(
                    updatedSubscription.Id,
                    2026,
                    2,
                    new DateTime(2026, 2, 10),
                    new DateTime(2026, 2, 20),
                    "INV-NEG",
                    -1m,
                    1m,
                    invoiceType.DefaultUsageUnit,
                    "Negatif tutar")),
                "Negatif fatura tutari engellenmedi.");

            AssertThrows(
                () => invoiceRepository.Add(new InvoiceInput(
                    updatedSubscription.Id,
                    2026,
                    2,
                    new DateTime(2026, 2, 10),
                    new DateTime(2026, 2, 20),
                    "INV-USAGE-NEG",
                    1m,
                    -1m,
                    invoiceType.DefaultUsageUnit,
                    "Negatif kullanim")),
                "Negatif kullanim miktari engellenmedi.");

            var dueDateWarning = InvoiceRepository.GetDueDateWarning(new InvoiceInput(
                updatedSubscription.Id,
                2026,
                3,
                new DateTime(2026, 3, 10),
                new DateTime(2026, 3, 1),
                "INV-WARN",
                1m,
                1m,
                invoiceType.DefaultUsageUnit,
                "Tarih uyarisi"));
            Assert(!string.IsNullOrWhiteSpace(dueDateWarning), "Son odeme tarihi uyarisi uretilmedi.");

            subscriptionRepository.SetActive(updatedSubscription.Id, isActive: false);
            var passiveSubscription = subscriptionRepository.GetAll().Single(item => item.Id == updatedSubscription.Id);
            Assert(!passiveSubscription.IsActive, "Abonelik pasife alma basarisiz.");

            var auditLogRepository = new AuditLogRepository(databasePath);
            var auditLogs = auditLogRepository.GetAll();
            Assert(auditLogs.Count >= 8, "Audit log kayitlari beklenenden az.");
            Assert(auditLogs.Any(x => x.ActionType == "subscription_created"), "Audit log: subscription_created kaydi yok.");
            Assert(auditLogs.Any(x => x.ActionType == "subscription_updated"), "Audit log: subscription_updated kaydi yok.");
            Assert(auditLogs.Any(x => x.ActionType == "subscription_deactivated"), "Audit log: subscription_deactivated kaydi yok.");
            Assert(auditLogs.Any(x => x.ActionType == "invoice_created"), "Audit log: invoice_created kaydi yok.");
            Assert(auditLogs.Any(x => x.ActionType == "invoice_updated"), "Audit log: invoice_updated kaydi yok.");
            Assert(auditLogs.Any(x => x.ActionType == "invoice_review_updated"), "Audit log: invoice_review_updated kaydi yok.");
            Assert(auditLogs.Any(x => x.ActionType == "invoice_pdf_attached"), "Audit log: invoice_pdf_attached kaydi yok.");
            Assert(auditLogs.Any(x => x.ActionType == "payment_created"), "Audit log: payment_created kaydi yok.");
            Assert(auditLogs.Any(x => x.ActionType == "payment_pdf_attached"), "Audit log: payment_pdf_attached kaydi yok.");
        }
        finally
        {
            if (keepArtifacts)
            {
                try
                {
                    Console.WriteLine($"Self-test artifacts kept at: {testRoot}");
                }
                catch
                {
                    // Ignore console issues in WPF.
                }
            }
            else if (Directory.Exists(testRoot))
            {
                Directory.Delete(testRoot, recursive: true);
            }
        }
    }

    private static void Assert(bool condition, string message)
    {
        if (!condition)
        {
            throw new InvalidOperationException(message);
        }
    }

    private static void AssertThrows(Action action, string message)
    {
        try
        {
            action();
        }
        catch (InvalidOperationException)
        {
            return;
        }

        throw new InvalidOperationException(message);
    }
}
