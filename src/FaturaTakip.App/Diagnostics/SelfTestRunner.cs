using System.IO;
using FaturaTakip.App.Data;
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
}
