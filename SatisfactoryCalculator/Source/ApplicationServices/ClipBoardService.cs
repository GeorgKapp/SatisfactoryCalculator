namespace SatisfactoryCalculator.Source.ApplicationServices;

internal class ClipBoardService
{
    public void CopyToClipboard(string content)
    {
        Clipboard.SetText(content);
    }
}
