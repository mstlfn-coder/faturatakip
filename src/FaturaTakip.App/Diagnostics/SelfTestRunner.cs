using System.IO;
using FaturaTakip.App.Data;
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

        try
        {
            new DatabaseInitializer().Initialize(databasePath);
            var repository = new InvoiceTypeRepository(databasePath);

            var seeded = repository.GetAll();
            Assert(seeded.Count >= 6, "BaÅŸlangÄ±Ã§ fatura tÃ¼rleri oluÅŸturulmadÄ±.");

            var added = repository.Add(new InvoiceTypeInput(
                "Test TÃ¼rÃ¼",
                "Self-test kaydÄ±",
                "adet",
                IsActive: true));
            Assert(added.Id > 0, "Fatura tÃ¼rÃ¼ ekleme baÅŸarÄ±sÄ±z.");

            var updated = repository.Update(added.Id, new InvoiceTypeInput(
                "Test TÃ¼rÃ¼ GÃ¼ncel",
                "Self-test gÃ¼ncellemesi",
                "saat",
                IsActive: true));
            Assert(updated.DefaultUsageUnit == "saat", "Fatura tÃ¼rÃ¼ gÃ¼ncelleme baÅŸarÄ±sÄ±z.");

            repository.SetActive(updated.Id, isActive: false);
            var passive = repository.GetAll().Single(item => item.Id == updated.Id);
            Assert(!passive.IsActive, "Fatura tÃ¼rÃ¼ pasife alma baÅŸarÄ±sÄ±z.");

            var subscriptionRepository = new SubscriptionRepository(databasePath);
            var invoiceType = seeded.First();

            var addedSubscription = subscriptionRepository.Add(new SubscriptionInput(
                invoiceType.Id,
                "Ana Bina Test AboneliÄŸi",
                "Test Kurumu",
                "SUB-001",
                "TES-001",
                "SAY-001",
                "Test SaÄŸlayÄ±cÄ±",
                "Test Mahallesi",
                "Ana Bina",
                invoiceType.DefaultUsageUnit,
                IsActive: true,
                StartDate: new DateTime(2026, 1, 1),
                EndDate: null,
                "Self-test abonelik kaydÄ±"));
            Assert(addedSubscription.Id > 0, "Abonelik ekleme baÅŸarÄ±sÄ±z.");
            Assert(addedSubscription.InvoiceTypeId == invoiceType.Id, "Abonelik fatura tÃ¼rÃ¼ne baÄŸlanmadÄ±.");

            var updatedSubscription = subscriptionRepository.Update(addedSubscription.Id, new SubscriptionInput(
                invoiceType.Id,
                "Ana Bina Test AboneliÄŸi GÃ¼ncel",
                "Test Kurumu",
                "SUB-001",
                "TES-002",
                "SAY-001",
                "Test SaÄŸlayÄ±cÄ±",
                "Test Mahallesi",
                "Ana Bina",
                invoiceType.DefaultUsageUnit,
                IsActive: true,
                StartDate: new DateTime(2026, 1, 1),
                EndDate: null,
                "Self-test abonelik gÃ¼ncellemesi"));
            Assert(updatedSubscription.InstallationNo == "TES-002", "Abonelik dÃ¼zenleme baÅŸarÄ±sÄ±z.");

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
                "Self-test fatura kaydÄ±"));
            Assert(invoice.SubscriptionId == updatedSubscription.Id, "Fatura aboneliÄŸe baÄŸlanmadÄ±.");
            Assert(invoice.InvoiceTypeId == invoiceType.Id, "Fatura tÃ¼rÃ¼ abonelikten alÄ±nmadÄ±.");

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
                "Self-test fatura gÃ¼ncellemesi"));
            Assert(updatedInvoice.Amount == 1300m, "Fatura dÃ¼zenleme baÅŸarÄ±sÄ±z.");

            var paymentRepository = new PaymentRepository(databasePath);
            var partialPayment = paymentRepository.Add(new PaymentInput(
                updatedInvoice.Id,
                new DateTime(2026, 1, 25),
                300m,
                "Self-test kÄ±smi Ã¶deme"));
            Assert(partialPayment.Id > 0, "Ã–deme kaydÄ± ekleme baÅŸarÄ±sÄ±z.");
            Assert(partialPayment.Description == "Self-test kÄ±smi Ã¶deme", "Ã–deme aÃ§Ä±klamasÄ± saklanmadÄ±.");

            var partialInvoice = invoiceRepository.GetAll().Single(item => item.Id == updatedInvoice.Id);
            Assert(partialInvoice.PaidAmount == 300m, "KÄ±smi Ã¶deme toplamÄ± faturaya yansÄ±madÄ±.");
            Assert(partialInvoice.RemainingAmount == 1000m, "Kalan Ã¶deme tutarÄ± hatalÄ± hesaplandÄ±.");
            Assert(partialInvoice.Status == "unpaid", "KÄ±smi Ã¶deme faturayÄ± erken Ã¶dendi yapmamalÄ±.");
            Assert(partialInvoice.State == "Kısmi", "KÄ±smi Ã¶deme durumu gÃ¶sterilmedi.");
            Assert(paymentRepository.GetForInvoice(updatedInvoice.Id).Count == 1, "Fatura Ã¶deme listesi okunamadÄ±.");

            AssertThrows(
                () => paymentRepository.Add(new PaymentInput(
                    updatedInvoice.Id,
                    new DateTime(2026, 1, 26),
                    1000.01m,
                    "KalanÄ± aÅŸan Ã¶deme")),
                "Kalan tutarÄ± aÅŸan Ã¶deme engellenmedi.");

            paymentRepository.Add(new PaymentInput(
                updatedInvoice.Id,
                new DateTime(2026, 1, 26),
                1000m,
                "Self-test tamamlama Ã¶demesi"));
            var paidInvoice = invoiceRepository.GetAll().Single(item => item.Id == updatedInvoice.Id);
            Assert(paidInvoice.PaidAmount == 1300m, "Tam Ã¶deme toplamÄ± faturaya yansÄ±madÄ±.");
            Assert(paidInvoice.RemainingAmount == 0m, "Tam Ã¶deme sonrasÄ± kalan tutar sÄ±fÄ±rlanmadÄ±.");
            Assert(paidInvoice.Status == "paid", "Tam Ã¶deme faturayÄ± Ã¶dendi yapmadÄ±.");

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
                "Self-test fatura tutarÄ± artÄ±rÄ±ldÄ±"));
            Assert(increasedInvoice.Status == "unpaid", "Tutar artÄ±nca Ã¶deme durumu yeniden hesaplanmadÄ±.");
            Assert(increasedInvoice.RemainingAmount == 200m, "Tutar artÄ±ÅŸÄ± sonrasÄ± kalan Ã¶deme hatalÄ±.");

            paymentRepository.Add(new PaymentInput(
                updatedInvoice.Id,
                new DateTime(2026, 1, 27),
                200m,
                "Self-test son Ã¶deme"));
            var fullyPaidInvoice = invoiceRepository.GetAll().Single(item => item.Id == updatedInvoice.Id);
            Assert(fullyPaidInvoice.Status == "paid", "Ek Ã¶deme sonrasÄ± fatura yeniden Ã¶dendi olmadÄ±.");
            Assert(fullyPaidInvoice.PaidAmount == 1500m, "Ek Ã¶deme toplamÄ± hatalÄ±.");

            AssertThrows(
                () => paymentRepository.Add(new PaymentInput(
                    updatedInvoice.Id,
                    new DateTime(2026, 1, 28),
                    -1m,
                    "Negatif Ã¶deme")),
                "Negatif Ã¶deme tutarÄ± engellenmedi.");
            AssertThrows(
                () => paymentRepository.Add(new PaymentInput(
                    999999,
                    new DateTime(2026, 1, 28),
                    1m,
                    "Olmayan fatura")),
                "Olmayan faturaya Ã¶deme eklenebildi.");

            var samplePaymentPdfPath = Path.Combine(testRoot, "sample-payment.pdf");
            File.WriteAllText(samplePaymentPdfPath, "%PDF-1.4\n1 0 obj\n<<>>\nendobj\ntrailer\n<<>>\n%%EOF");

            var paymentWithPdf = paymentRepository.AttachPdf(partialPayment.Id, samplePaymentPdfPath);
            Assert(paymentWithPdf.HasPdf, "Ã–deme PDF metadata kaydÄ± oluÅŸturulmadÄ±.");
            Assert(paymentWithPdf.PdfOriginalFileName == "sample-payment.pdf", "Ã–deme PDF orijinal dosya adÄ± saklanmadÄ±.");
            Assert(!string.IsNullOrWhiteSpace(paymentWithPdf.PdfSha256Hash), "Ã–deme PDF hash bilgisi saklanmadÄ±.");
            Assert(paymentWithPdf.PdfFilePath.StartsWith(Path.Combine("attachments", "payments", "2026", "01"), StringComparison.Ordinal), "Ã–deme PDF hedef klasÃ¶rÃ¼ Ã¶deme tarihi altÄ±nda deÄŸil.");
            Assert(paymentRepository.PdfFileExists(paymentWithPdf), "Kopyalanan Ã¶deme PDF dosyasÄ± bulunamadÄ±.");

            var attachedPaymentPdfPath = paymentRepository.GetPdfAbsolutePath(paymentWithPdf);
            File.Delete(attachedPaymentPdfPath);
            Assert(paymentRepository.IsPdfMissing(paymentWithPdf), "KayÄ±p Ã¶deme PDF dosyasÄ± algÄ±lanmadÄ±.");

            var samplePdfPath = Path.Combine(testRoot, "sample-invoice.pdf");
            File.WriteAllText(samplePdfPath, "%PDF-1.4\n1 0 obj\n<<>>\nendobj\ntrailer\n<<>>\n%%EOF");

            var invoiceWithPdf = invoiceRepository.AttachPdf(updatedInvoice.Id, samplePdfPath);
            Assert(invoiceWithPdf.HasPdf, "Fatura PDF metadata kaydÄ± oluÅŸturulmadÄ±.");
            Assert(invoiceWithPdf.PdfOriginalFileName == "sample-invoice.pdf", "PDF orijinal dosya adÄ± saklanmadÄ±.");
            Assert(!string.IsNullOrWhiteSpace(invoiceWithPdf.PdfSha256Hash), "PDF hash bilgisi saklanmadÄ±.");
            Assert(invoiceWithPdf.PdfFilePath.StartsWith(Path.Combine("attachments", "invoices", "2026", "01"), StringComparison.Ordinal), "PDF hedef klasÃ¶rÃ¼ dÃ¶nem altÄ±nda deÄŸil.");
            Assert(invoiceRepository.PdfFileExists(invoiceWithPdf), "Kopyalanan PDF dosyasÄ± bulunamadÄ±.");

            var attachedPdfPath = invoiceRepository.GetPdfAbsolutePath(invoiceWithPdf);
            File.Delete(attachedPdfPath);
            Assert(invoiceRepository.IsPdfMissing(invoiceWithPdf), "KayÄ±p PDF dosyasÄ± algÄ±lanmadÄ±.");

            var invalidAttachmentPath = Path.Combine(testRoot, "not-pdf.txt");
            File.WriteAllText(invalidAttachmentPath, "PDF deÄŸil");
            AssertThrows(
                () => invoiceRepository.AttachPdf(updatedInvoice.Id, invalidAttachmentPath),
                "PDF olmayan dosya eklenebildi.");
            AssertThrows(
                () => paymentRepository.AttachPdf(partialPayment.Id, invalidAttachmentPath),
                "PDF olmayan Ã¶deme dosyasÄ± eklenebildi.");

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
                    SubscriptionName = "Ek Hizmet BinasÄ±",
                    InstitutionName = "Test Kurumu",
                    InvoiceYear = 2026,
                    InvoiceMonth = 2,
                    DueDate = new DateTime(2026, 3, 1),
                    InvoiceNo = "SU-001",
                    Status = "paid",
                    Description = "Åubat su",
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
                    Description = "Eski kayÄ±t",
                },
            };
            var filterToday = new DateTime(2026, 2, 1);

            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(Year: 2026), filterToday).Count == 2, "YÄ±l filtresi Ã§alÄ±ÅŸmadÄ±.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(Month: 1), filterToday).Single().InvoiceNo == "ELK-001", "Ay filtresi Ã§alÄ±ÅŸmadÄ±.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(InvoiceTypeId: 100), filterToday).Count == 2, "Fatura tÃ¼rÃ¼ filtresi Ã§alÄ±ÅŸmadÄ±.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(SubscriptionId: 11), filterToday).Single().InvoiceNo == "SU-001", "Abonelik filtresi Ã§alÄ±ÅŸmadÄ±.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(PaymentStatus: InvoicePaymentStatusFilter.Paid), filterToday).Single().InvoiceNo == "SU-001", "Ã–deme durumu filtresi Ã§alÄ±ÅŸmadÄ±.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(PaymentStatus: InvoicePaymentStatusFilter.Overdue), filterToday).Single().InvoiceNo == "ELK-001", "GecikmiÅŸ fatura filtresi Ã§alÄ±ÅŸmadÄ±.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(PdfStatus: InvoicePdfStatusFilter.HasPdf), filterToday).Single().InvoiceNo == "ELK-001", "PDF var filtresi Ã§alÄ±ÅŸmadÄ±.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(PdfStatus: InvoicePdfStatusFilter.MissingPdf), filterToday).Count == 2, "PDF eksik filtresi Ã§alÄ±ÅŸmadÄ±.");
            Assert(InvoiceFilter.Apply(filterSamples, new InvoiceFilterCriteria(SearchText: "Ana ELK"), filterToday).Count == 2, "Metin arama filtresi Ã§alÄ±ÅŸmadÄ±.");

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
            Assert(dashboardSummary.MonthlyInvoiceCount == 2, "Dashboard aylÄ±k fatura sayÄ±sÄ± hatalÄ±.");
            Assert(dashboardSummary.MonthlyInvoiceTotal == 300m, "Dashboard aylÄ±k fatura toplamÄ± hatalÄ±.");
            Assert(dashboardSummary.MonthlyPaymentCount == 2, "Dashboard aylÄ±k Ã¶deme sayÄ±sÄ± hatalÄ±.");
            Assert(dashboardSummary.MonthlyPaymentTotal == 225m, "Dashboard aylÄ±k Ã¶deme toplamÄ± hatalÄ±.");
            Assert(dashboardSummary.UnpaidInvoiceCount == 2, "Dashboard Ã¶denmemiÅŸ fatura sayÄ±sÄ± hatalÄ±.");
            Assert(dashboardSummary.UnpaidRemainingTotal == 125m, "Dashboard Ã¶denmemiÅŸ kalan toplamÄ± hatalÄ±.");
            Assert(dashboardSummary.OverdueInvoiceCount == 1, "Dashboard gecikmiÅŸ fatura sayÄ±sÄ± hatalÄ±.");
            Assert(dashboardSummary.OverdueRemainingTotal == 75m, "Dashboard gecikmiÅŸ kalan toplamÄ± hatalÄ±.");
            Assert(dashboardSummary.MissingInvoicePdfCount == 1, "Dashboard fatura PDF eksik sayÄ±sÄ± hatalÄ±.");
            Assert(dashboardSummary.MissingPaymentPdfCount == 1, "Dashboard Ã¶deme PDF eksik sayÄ±sÄ± hatalÄ±.");

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
            Assert(report.Unpaid.Count == 3, "Rapor Ã¶denmemiÅŸ sayÄ±sÄ± hatalÄ±.");
            Assert(report.UnpaidRemainingTotal == 335m, "Rapor Ã¶denmemiÅŸ kalan toplamÄ± hatalÄ±.");
            Assert(report.Overdue.Count == 1, "Rapor gecikmiÅŸ sayÄ±sÄ± hatalÄ±.");
            Assert(report.OverdueRemainingTotal == 75m, "Rapor gecikmiÅŸ kalan toplamÄ± hatalÄ±.");
            Assert(report.Upcoming.Count == 1, "Rapor yaklaÅŸan sayÄ±sÄ± hatalÄ±.");
            Assert(report.UpcomingRemainingTotal == 200m, "Rapor yaklaÅŸan kalan toplamÄ± hatalÄ±.");

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
            Assert(monthly.TotalInvoiceCount == 2, "AylÄ±k rapor toplam fatura sayÄ±sÄ± hatalÄ±.");
            Assert(monthly.TotalAmount == 300m, "AylÄ±k rapor toplam tutar hatalÄ±.");
            Assert(monthly.PaidTotal == 225m, "AylÄ±k rapor Ã¶denen toplam hatalÄ±.");
            Assert(monthly.RemainingTotal == 75m, "AylÄ±k rapor kalan toplam hatalÄ±.");
            Assert(monthly.UnpaidInvoiceCount == 1, "AylÄ±k rapor Ã¶denmemiÅŸ sayÄ±sÄ± hatalÄ±.");
            Assert(monthly.OverdueInvoiceCount == 1, "AylÄ±k rapor gecikmiÅŸ sayÄ±sÄ± hatalÄ±.");
            Assert(monthly.MissingPdfCount == 1, "AylÄ±k rapor PDF eksik sayÄ±sÄ± hatalÄ±.");

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
            Assert(typedMonthly.TotalInvoiceCount == 1, "TÃ¼r filtresi aylÄ±k raporda Ã§alÄ±ÅŸmadÄ±.");
            Assert(typedMonthly.TotalAmount == 100m, "TÃ¼r filtresi aylÄ±k toplam tutarÄ± yanlÄ±ÅŸ.");

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
            Assert(comparison.Current.TotalInvoiceCount == 1, "Abonelik raporu (current) toplam fatura sayÄ±sÄ± hatalÄ±.");
            Assert(comparison.Current.TotalAmount == 120m, "Abonelik raporu (current) toplam tutar hatalÄ±.");
            Assert(comparison.Current.PaidTotal == 20m, "Abonelik raporu (current) Ã¶denen toplam hatalÄ±.");
            Assert(comparison.Current.RemainingTotal == 100m, "Abonelik raporu (current) kalan toplam hatalÄ±.");
            Assert(comparison.Previous.TotalAmount == 80m, "Abonelik raporu (previous) toplam tutar hatalÄ±.");
            Assert(comparison.TotalAmountDelta == 40m, "Abonelik raporu toplam delta hatalÄ±.");

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
            Assert(yearly.TotalInvoiceCount == 3, "YÄ±llÄ±k rapor toplam fatura sayÄ±sÄ± hatalÄ±.");
            Assert(yearly.TotalAmount == 400m, "YÄ±llÄ±k rapor toplam tutar hatalÄ±.");
            Assert(yearly.PaidTotal == 150m, "YÄ±llÄ±k rapor Ã¶denen toplam hatalÄ±.");
            Assert(yearly.RemainingTotal == 250m, "YÄ±llÄ±k rapor kalan toplam hatalÄ±.");
            Assert(yearly.MissingPdfCount == 2, "YÄ±llÄ±k rapor PDF eksik sayÄ±sÄ± hatalÄ±.");
            Assert(yearly.HighestMonth == 2, "YÄ±llÄ±k rapor en yÃ¼ksek ay hatalÄ±.");
            Assert(yearly.HighestMonthTotal == 300m, "YÄ±llÄ±k rapor en yÃ¼ksek ay toplamÄ± hatalÄ±.");
            Assert(yearly.LowestMonth == 1, "YÄ±llÄ±k rapor en dÃ¼ÅŸÃ¼k ay hatalÄ±.");
            Assert(yearly.LowestMonthTotal == 100m, "YÄ±llÄ±k rapor en dÃ¼ÅŸÃ¼k ay toplamÄ± hatalÄ±.");

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
                    SubscriptionName = "Åube",
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
            Assert(typeYearly.TotalInvoiceCount == 2, "TÃ¼r yÄ±llÄ±k rapor toplam fatura sayÄ±sÄ± hatalÄ±.");
            Assert(typeYearly.TotalAmount == 350m, "TÃ¼r yÄ±llÄ±k rapor toplam tutar hatalÄ±.");
            Assert(typeYearly.Distribution.Count == 2, "TÃ¼r yÄ±llÄ±k rapor daÄŸÄ±lÄ±m satÄ±r sayÄ±sÄ± hatalÄ±.");
            Assert(typeYearly.Distribution[0].TotalAmount == 250m, "TÃ¼r yÄ±llÄ±k rapor daÄŸÄ±lÄ±m sÄ±ralamasÄ± hatalÄ±.");

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
                    SubscriptionName = "Åube",
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
                    SubscriptionName = "Åube",
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

            Assert(documentHealth.InvoiceNoPdfCount == 1, "Evrak kontrol: fatura PDF yok sayÄ±sÄ± hatalÄ±.");
            Assert(documentHealth.InvoiceMissingFileCount == 1, "Evrak kontrol: fatura PDF kayÄ±p sayÄ±sÄ± hatalÄ±.");
            Assert(documentHealth.PaymentNoPdfCount == 1, "Evrak kontrol: Ã¶deme PDF yok sayÄ±sÄ± hatalÄ±.");
            Assert(documentHealth.PaymentMissingFileCount == 1, "Evrak kontrol: Ã¶deme PDF kayÄ±p sayÄ±sÄ± hatalÄ±.");
            Assert(documentHealth.DuplicateInvoiceHashItemCount == 2, "Evrak kontrol: fatura aynÄ±-hash madde sayÄ±sÄ± hatalÄ±.");
            Assert(documentHealth.DuplicatePaymentHashItemCount == 2, "Evrak kontrol: Ã¶deme aynÄ±-hash madde sayÄ±sÄ± hatalÄ±.");
            Assert(documentHealth.Issues.Count >= 8, "Evrak kontrol: uyarÄ± listesi beklenenden kÄ±sa.");

            var consistency = ConsistencyReportCalculator.Calculate(
                documentHealthInvoices,
                documentHealthPayments,
                invoice => invoice.Id != 1002,
                payment => payment.Id != 2002);
            Assert(consistency.ErrorCount == 0, "TutarlÄ±lÄ±k denetimi ERROR Ã¼retti.");

            var exportXlsxPath = Path.Combine(testRoot, "exports", $"faturalar-{DateTime.Now:yyyyMMdd-HHmmss}.xlsx");
            Directory.CreateDirectory(Path.GetDirectoryName(exportXlsxPath)!);
            ExcelExportWriter.WriteInvoices(
                exportXlsxPath,
                documentHealthInvoices,
                isPdfMissing: invoice => !invoice.HasPdf);
            Assert(File.Exists(exportXlsxPath), "Excel export dosyasÄ± oluÅŸmadÄ±.");
            var exportFileInfo = new FileInfo(exportXlsxPath);
            Assert(exportFileInfo.Length > 1024, "Excel export dosyasÄ± beklenenden kÃ¼Ã§Ã¼k.");

            var reportXlsxPath = Path.Combine(testRoot, "exports", $"raporlar-selftest-{DateTime.Now:yyyyMMdd-HHmmss}.xlsx");
            ExcelExportWriter.WriteTable(
                reportXlsxPath,
                sheetName: "Rapor",
                headers: new[] { "A", "B" },
                rows: new[] { new object?[] { "X", 1 } });
            Assert(File.Exists(reportXlsxPath), "Rapor excel export dosyasÄ± oluÅŸmadÄ±.");

            var reportPdfPath = Path.Combine(testRoot, "exports", $"raporlar-selftest-{DateTime.Now:yyyyMMdd-HHmmss}.pdf");
            PdfReportWriter.WriteSimpleTableReport(
                reportPdfPath,
                new PdfReportWriter.ReportMeta(
                    AppTitle: "KURUM FATURA TAKIP PROGRAMI",
                    InstitutionName: "Test Kurum",
                    ReportTitle: "SELF-TEST PDF",
                    ReportPeriod: "2026/01",
                    ReportDate: new DateTime(2026, 6, 1),
                    CreatedBy: "codex",
                    FilterText: "Test"),
                summary: new[] { new PdfReportWriter.SummaryItem("Toplam", "1") },
                headers: new[] { "Kolon" },
                rows: new[] { new[] { "SatÄ±r" } });
            Assert(File.Exists(reportPdfPath), "PDF export dosyasÄ± oluÅŸmadÄ±.");
            Assert(new FileInfo(reportPdfPath).Length > 1024, "PDF export dosyasÄ± beklenenden kÃ¼Ã§Ã¼k.");

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
                "AynÄ± abonelikte aynÄ± fatura numarasÄ± engellenmedi.");

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
                "Negatif fatura tutarÄ± engellenmedi.");

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
                    "Negatif kullanÄ±m")),
                "Negatif kullanÄ±m miktarÄ± engellenmedi.");

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
                "Tarih uyarÄ±sÄ±"));
            Assert(!string.IsNullOrWhiteSpace(dueDateWarning), "Son Ã¶deme tarihi uyarÄ±sÄ± Ã¼retilmedi.");

            subscriptionRepository.SetActive(updatedSubscription.Id, isActive: false);
            var passiveSubscription = subscriptionRepository.GetAll().Single(item => item.Id == updatedSubscription.Id);
            Assert(!passiveSubscription.IsActive, "Abonelik pasife alma baÅŸarÄ±sÄ±z.");
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
