using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace UI.Printing;

public static class PrintHelper
{
    public static void ShowPreview(IWin32Window owner, PrintDocument document)
    {
        using var preview = new PrintPreviewDialog
        {
            Document = document,
            Width = 980,
            Height = 740,
            StartPosition = FormStartPosition.CenterParent,
            UseAntiAlias = true
        };

        if (preview.Controls.OfType<ToolStrip>().FirstOrDefault() is { } strip)
        {
            var choosePrinterBtn = new ToolStripButton("Choose Printer / PDF...")
            {
                DisplayStyle = ToolStripItemDisplayStyle.Text
            };
            choosePrinterBtn.Click += (_, _) =>
            {
                using var dialog = new PrintDialog
                {
                    Document = document,
                    UseEXDialog = true
                };
                if (dialog.ShowDialog(preview) == DialogResult.OK)
                {
                    document.Print();
                }
            };
            strip.Items.Insert(1, choosePrinterBtn);
        }

        preview.ShowDialog(owner);
    }
}