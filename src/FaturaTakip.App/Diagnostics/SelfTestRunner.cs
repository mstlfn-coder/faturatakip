using System.IO;
using FaturaTakip.App.Data;
using FaturaTakip.App.Data.Invoices;
using FaturaTakip.App.Data.InvoiceTypes;
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
