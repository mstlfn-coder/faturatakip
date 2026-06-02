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
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(SearchText: "Ana ELK"), filterToday).Count == 2, "Metin arama filtresi calismadi.");

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
