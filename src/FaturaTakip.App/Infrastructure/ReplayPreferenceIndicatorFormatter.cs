namespace FaturaTakip.App.Infrastructure;

public static class ReplayPreferenceIndicatorFormatter
{
    public static string BuildIndicator(string emphasis, int seconds, bool hasAction)
    {
        var emphasisIndicator = emphasis switch
        {
            "low" => ".",
            "high" => "***",
            _ => "**"
        };

        var secondsGlyph = hasAction ? "|" : ":";
        var secondsIndicator = string.Concat(Enumerable.Repeat(secondsGlyph, Math.Clamp(seconds, 1, 4)));
        return $"{emphasisIndicator}{secondsIndicator}";
    }

    public static string BuildToolTip(string emphasis, int seconds, bool hasAction, bool isReplayActive, string? actionKey)
    {
        var emphasisLabel = emphasis switch
        {
            "low" => "dusuk vurgu",
            "high" => "guclu vurgu",
            _ => "orta vurgu"
        };

        var actionName = BuildActionDisplayName(actionKey);
        if (hasAction && !string.IsNullOrWhiteSpace(actionName))
        {
            var replayLabel = isReplayActive
                ? "Yeniden tetiklendi."
                : "Hazir.";
            return $"{actionName} replay ayari: {seconds} sn, {emphasisLabel} vurgu. {replayLabel}";
        }

        var guidanceLabel = isReplayActive
            ? "Replay yeniden tetiklendi."
            : "Bir action sec ve replay yardimini hazirla.";
        return $"Replay ayari hazir: {seconds} sn, {emphasisLabel} vurgu. {guidanceLabel}";
    }

    public static string BuildActionDisplayName(string? actionKey)
    {
        return actionKey switch
        {
            "fill_remaining" => "Kalan Tutar",
            "use_last" => "Son Aciklama",
            "use_selected" => "Secili Odeme",
            "select_pdf" => "PDF Sec",
            "open_pdf" => "PDF Ac",
            _ => string.Empty
        };
    }
}
