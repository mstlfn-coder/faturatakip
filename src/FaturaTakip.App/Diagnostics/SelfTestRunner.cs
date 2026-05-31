using System.IO;
using FaturaTakip.App.Data;
using FaturaTakip.App.Data.Dashboard;
using FaturaTakip.App.Data.Invoices;
using FaturaTakip.App.Data.InvoiceTypes;
using FaturaTakip.App.Data.Payments;
using FaturaTakip.App.Data.Reports;
using FaturaTakip.App.Data.Subscriptions;

namespace FaturaTakip.App.Diagnostics;

public sealed class SelfTestRunner
{
    public void Run()
    {
        var testRoot = Path.Combine(Path.GetTempPath(), "FaturaTakip.SelfTest", Guid.NewGuid().ToString("N"));
        var databasePath = Path.Combine(testRoot, "database", "fatura_takip.db");

        try
        {
            new DatabaseInitializer().Initialize(databasePath);
            var repository = new InvoiceTypeRepository(databasePath);

            var seeded = repository.GetAll();
            Assert(seeded.Count >= 6, "Başlangıç fatura türleri oluşturulmadı.");

            var added = repository.Add(new InvoiceTypeInput(
                "Test Türü",
                "Self-test kaydı",
                "adet",
                IsActive: true));
            Assert(added.Id > 0, "Fatura türü ekleme başarısız.");

            var updated = repository.Update(added.Id, new InvoiceTypeInput(
                "Test Türü Güncel",
                "Self-test güncellemesi",
                "saat",
                IsActive: true));
            Assert(updated.DefaultUsageUnit == "saat", "Fatura türü güncelleme başarısız.");

            repository.SetActive(updated.Id, isActive: false);
            var passive = repository.GetAll().Single(item => item.Id == updated.Id);
            Assert(!passive.IsActive, "Fatura türü pasife alma başarısız.");

            var subscriptionRepository = new SubscriptionRepository(databasePath);
            var invoiceType = seeded.First();

            var addedSubscription = subscriptionRepository.Add(new SubscriptionInput(
                invoiceType.Id,
                "Ana Bina Test Aboneliği",
                "Test Kurumu",
                "SUB-001",
                "TES-001",
                "SAY-001",
                "Test Sağlayıcı",
                "Test Mahallesi",
                "Ana Bina",
                invoiceType.DefaultUsageUnit,
                IsActive: true,
                StartDate: new DateTime(2026, 1, 1),
                EndDate: null,
                "Self-test abonelik kaydı"));
            Assert(addedSubscription.Id > 0, "Abonelik ekleme başarısız.");
            Assert(addedSubscription.InvoiceTypeId == invoiceType.Id, "Abonelik fatura türüne bağlanmadı.");

            var updatedSubscription = subscriptionRepository.Update(addedSubscription.Id, new SubscriptionInput(
                invoiceType.Id,
                "Ana Bina Test Aboneliği Güncel",
                "Test Kurumu",
                "SUB-001",
                "TES-002",
                "SAY-001",
                "Test Sağlayıcı",
                "Test Mahallesi",
                "Ana Bina",
                invoiceType.DefaultUsageUnit,
                IsActive: true,
                StartDate: new DateTime(2026, 1, 1),
                EndDate: null,
                "Self-test abonelik güncellemesi"));
            Assert(updatedSubscription.InstallationNo == "TES-002", "Abonelik düzenleme başarısız.");

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
                "Self-test fatura kaydı"));
            Assert(invoice.SubscriptionId == updatedSubscription.Id, "Fatura aboneliğe bağlanmadı.");
            Assert(invoice.InvoiceTypeId == invoiceType.Id, "Fatura türü abonelikten alınmadı.");

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
                "Self-test fatura güncellemesi"));
            Assert(updatedInvoice.Amount == 1300m, "Fatura düzenleme başarısız.");

            var paymentRepository = new PaymentRepository(databasePath);
            var partialPayment = paymentRepository.Add(new PaymentInput(
                updatedInvoice.Id,
                new DateTime(2026, 1, 25),
                300m,
                "Self-test kısmi ödeme"));
            Assert(partialPayment.Id > 0, "Ödeme kaydı ekleme başarısız.");
            Assert(partialPayment.Description == "Self-test kısmi ödeme", "Ödeme açıklaması saklanmadı.");

            var partialInvoice = invoiceRepository.GetAll().Single(item => item.Id == updatedInvoice.Id);
            Assert(partialInvoice.PaidAmount == 300m, "Kısmi ödeme toplamı faturaya yansımadı.");
            Assert(partialInvoice.RemainingAmount == 1000m, "Kalan ödeme tutarı hatalı hesaplandı.");
            Assert(partialInvoice.Status == "unpaid", "Kısmi ödeme faturayı erken ödendi yapmamalı.");
            Assert(partialInvoice.State == "Kısmi", "Kısmi ödeme durumu gösterilmedi.");
            Assert(paymentRepository.GetForInvoice(updatedInvoice.Id).Count == 1, "Fatura ödeme listesi okunamadı.");

            AssertThrows(
                () => paymentRepository.Add(new PaymentInput(
                    updatedInvoice.Id,
                    new DateTime(2026, 1, 26),
                    1000.01m,
                    "Kalanı aşan ödeme")),
                "Kalan tutarı aşan ödeme engellenmedi.");

            paymentRepository.Add(new PaymentInput(
                updatedInvoice.Id,
                new DateTime(2026, 1, 26),
                1000m,
                "Self-test tamamlama ödemesi"));
            var paidInvoice = invoiceRepository.GetAll().Single(item => item.Id == updatedInvoice.Id);
            Assert(paidInvoice.PaidAmount == 1300m, "Tam ödeme toplamı faturaya yansımadı.");
            Assert(paidInvoice.RemainingAmount == 0m, "Tam ödeme sonrası kalan tutar sıfırlanmadı.");
            Assert(paidInvoice.Status == "paid", "Tam ödeme faturayı ödendi yapmadı.");

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
                "Self-test fatura tutarı artırıldı"));
            Assert(increasedInvoice.Status == "unpaid", "Tutar artınca ödeme durumu yeniden hesaplanmadı.");
            Assert(increasedInvoice.RemainingAmount == 200m, "Tutar artışı sonrası kalan ödeme hatalı.");

            paymentRepository.Add(new PaymentInput(
                updatedInvoice.Id,
                new DateTime(2026, 1, 27),
                200m,
                "Self-test son ödeme"));
            var fullyPaidInvoice = invoiceRepository.GetAll().Single(item => item.Id == updatedInvoice.Id);
            Assert(fullyPaidInvoice.Status == "paid", "Ek ödeme sonrası fatura yeniden ödendi olmadı.");
            Assert(fullyPaidInvoice.PaidAmount == 1500m, "Ek ödeme toplamı hatalı.");

            AssertThrows(
                () => paymentRepository.Add(new PaymentInput(
                    updatedInvoice.Id,
                    new DateTime(2026, 1, 28),
                    -1m,
                    "Negatif ödeme")),
                "Negatif ödeme tutarı engellenmedi.");
            AssertThrows(
                () => paymentRepository.Add(new PaymentInput(
                    999999,
                    new DateTime(2026, 1, 28),
                    1m,
                    "Olmayan fatura")),
                "Olmayan faturaya ödeme eklenebildi.");

            var samplePaymentPdfPath = Path.Combine(testRoot, "sample-payment.pdf");
            File.WriteAllText(samplePaymentPdfPath, "%PDF-1.4\n1 0 obj\n<<>>\nendobj\ntrailer\n<<>>\n%%EOF");

            var paymentWithPdf = paymentRepository.AttachPdf(partialPayment.Id, samplePaymentPdfPath);
            Assert(paymentWithPdf.HasPdf, "Ödeme PDF metadata kaydı oluşturulmadı.");
            Assert(paymentWithPdf.PdfOriginalFileName == "sample-payment.pdf", "Ödeme PDF orijinal dosya adı saklanmadı.");
            Assert(!string.IsNullOrWhiteSpace(paymentWithPdf.PdfSha256Hash), "Ödeme PDF hash bilgisi saklanmadı.");
            Assert(paymentWithPdf.PdfFilePath.StartsWith(Path.Combine("attachments", "payments", "2026", "01"), StringComparison.Ordinal), "Ödeme PDF hedef klasörü ödeme tarihi altında değil.");
            Assert(paymentRepository.PdfFileExists(paymentWithPdf), "Kopyalanan ödeme PDF dosyası bulunamadı.");

            var attachedPaymentPdfPath = paymentRepository.GetPdfAbsolutePath(paymentWithPdf);
            File.Delete(attachedPaymentPdfPath);
            Assert(paymentRepository.IsPdfMissing(paymentWithPdf), "Kayıp ödeme PDF dosyası algılanmadı.");

            var samplePdfPath = Path.Combine(testRoot, "sample-invoice.pdf");
            File.WriteAllText(samplePdfPath, "%PDF-1.4\n1 0 obj\n<<>>\nendobj\ntrailer\n<<>>\n%%EOF");

            var invoiceWithPdf = invoiceRepository.AttachPdf(updatedInvoice.Id, samplePdfPath);
            Assert(invoiceWithPdf.HasPdf, "Fatura PDF metadata kaydı oluşturulmadı.");
            Assert(invoiceWithPdf.PdfOriginalFileName == "sample-invoice.pdf", "PDF orijinal dosya adı saklanmadı.");
            Assert(!string.IsNullOrWhiteSpace(invoiceWithPdf.PdfSha256Hash), "PDF hash bilgisi saklanmadı.");
            Assert(invoiceWithPdf.PdfFilePath.StartsWith(Path.Combine("attachments", "invoices", "2026", "01"), StringComparison.Ordinal), "PDF hedef klasörü dönem altında değil.");
            Assert(invoiceRepository.PdfFileExists(invoiceWithPdf), "Kopyalanan PDF dosyası bulunamadı.");

            var attachedPdfPath = invoiceRepository.GetPdfAbsolutePath(invoiceWithPdf);
            File.Delete(attachedPdfPath);
            Assert(invoiceRepository.IsPdfMissing(invoiceWithPdf), "Kayıp PDF dosyası algılanmadı.");

            var invalidAttachmentPath = Path.Combine(testRoot, "not-pdf.txt");
            File.WriteAllText(invalidAttachmentPath, "PDF değil");
            AssertThrows(
                () => invoiceRepository.AttachPdf(updatedInvoice.Id, invalidAttachmentPath),
                "PDF olmayan dosya eklenebildi.");
            AssertThrows(
                () => paymentRepository.AttachPdf(partialPayment.Id, invalidAttachmentPath),
                "PDF olmayan ödeme dosyası eklenebildi.");

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
                    SubscriptionName = "Ek Hizmet Binası",
                    InstitutionName = "Test Kurumu",
                    InvoiceYear = 2026,
                    InvoiceMonth = 2,
                    DueDate = new DateTime(2026, 3, 1),
                    InvoiceNo = "SU-001",
                    Status = "paid",
                    Description = "Şubat su",
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
                    Description = "Eski kayıt",
                },
            };
            var filterToday = new DateTime(2026, 2, 1);

            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(Year: 2026), filterToday).Count == 2, "Yıl filtresi çalışmadı.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(Month: 1), filterToday).Single().InvoiceNo == "ELK-001", "Ay filtresi çalışmadı.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(InvoiceTypeId: 100), filterToday).Count == 2, "Fatura türü filtresi çalışmadı.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(SubscriptionId: 11), filterToday).Single().InvoiceNo == "SU-001", "Abonelik filtresi çalışmadı.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(PaymentStatus: InvoicePaymentStatusFilter.Paid), filterToday).Single().InvoiceNo == "SU-001", "Ödeme durumu filtresi çalışmadı.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(PaymentStatus: InvoicePaymentStatusFilter.Overdue), filterToday).Single().InvoiceNo == "ELK-001", "Gecikmiş fatura filtresi çalışmadı.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(PdfStatus: InvoicePdfStatusFilter.HasPdf), filterToday).Single().InvoiceNo == "ELK-001", "PDF var filtresi çalışmadı.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(PdfStatus: InvoicePdfStatusFilter.MissingPdf), filterToday).Count == 2, "PDF eksik filtresi çalışmadı.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(SearchText: "Ana ELK"), filterToday).Count == 2, "Metin arama filtresi çalışmadı.");

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
            Assert(dashboardSummary.MonthlyInvoiceCount == 2, "Dashboard aylık fatura sayısı hatalı.");
            Assert(dashboardSummary.MonthlyInvoiceTotal == 300m, "Dashboard aylık fatura toplamı hatalı.");
            Assert(dashboardSummary.MonthlyPaymentCount == 2, "Dashboard aylık ödeme sayısı hatalı.");
            Assert(dashboardSummary.MonthlyPaymentTotal == 225m, "Dashboard aylık ödeme toplamı hatalı.");
            Assert(dashboardSummary.UnpaidInvoiceCount == 2, "Dashboard ödenmemiş fatura sayısı hatalı.");
            Assert(dashboardSummary.UnpaidRemainingTotal == 125m, "Dashboard ödenmemiş kalan toplamı hatalı.");
            Assert(dashboardSummary.OverdueInvoiceCount == 1, "Dashboard gecikmiş fatura sayısı hatalı.");
            Assert(dashboardSummary.OverdueRemainingTotal == 75m, "Dashboard gecikmiş kalan toplamı hatalı.");
            Assert(dashboardSummary.MissingInvoicePdfCount == 1, "Dashboard fatura PDF eksik sayısı hatalı.");
            Assert(dashboardSummary.MissingPaymentPdfCount == 1, "Dashboard ödeme PDF eksik sayısı hatalı.");

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
            Assert(report.Unpaid.Count == 3, "Rapor ödenmemiş sayısı hatalı.");
            Assert(report.UnpaidRemainingTotal == 335m, "Rapor ödenmemiş kalan toplamı hatalı.");
            Assert(report.Overdue.Count == 1, "Rapor gecikmiş sayısı hatalı.");
            Assert(report.OverdueRemainingTotal == 75m, "Rapor gecikmiş kalan toplamı hatalı.");
            Assert(report.Upcoming.Count == 1, "Rapor yaklaşan sayısı hatalı.");
            Assert(report.UpcomingRemainingTotal == 200m, "Rapor yaklaşan kalan toplamı hatalı.");

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
            Assert(monthly.TotalInvoiceCount == 2, "Aylık rapor toplam fatura sayısı hatalı.");
            Assert(monthly.TotalAmount == 300m, "Aylık rapor toplam tutar hatalı.");
            Assert(monthly.PaidTotal == 225m, "Aylık rapor ödenen toplam hatalı.");
            Assert(monthly.RemainingTotal == 75m, "Aylık rapor kalan toplam hatalı.");
            Assert(monthly.UnpaidInvoiceCount == 1, "Aylık rapor ödenmemiş sayısı hatalı.");
            Assert(monthly.OverdueInvoiceCount == 1, "Aylık rapor gecikmiş sayısı hatalı.");
            Assert(monthly.MissingPdfCount == 1, "Aylık rapor PDF eksik sayısı hatalı.");

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
            Assert(typedMonthly.TotalInvoiceCount == 1, "Tür filtresi aylık raporda çalışmadı.");
            Assert(typedMonthly.TotalAmount == 100m, "Tür filtresi aylık toplam tutarı yanlış.");

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
                "Aynı abonelikte aynı fatura numarası engellenmedi.");

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
                "Negatif fatura tutarı engellenmedi.");

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
                    "Negatif kullanım")),
                "Negatif kullanım miktarı engellenmedi.");

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
                "Tarih uyarısı"));
            Assert(!string.IsNullOrWhiteSpace(dueDateWarning), "Son ödeme tarihi uyarısı üretilmedi.");

            subscriptionRepository.SetActive(updatedSubscription.Id, isActive: false);
            var passiveSubscription = subscriptionRepository.GetAll().Single(item => item.Id == updatedSubscription.Id);
            Assert(!passiveSubscription.IsActive, "Abonelik pasife alma başarısız.");
        }
        finally
        {
            if (Directory.Exists(testRoot))
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
