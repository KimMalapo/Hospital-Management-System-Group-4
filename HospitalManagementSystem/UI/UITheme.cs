namespace UI;

/// <summary>Shared colors and fonts.</summary>
internal static class UITheme
{
    private const string FontName = "Segoe UI";

    private static readonly Color Canvas = Color.FromArgb(245, 247, 251);
    private static readonly Color Surface = Color.White;
    private static readonly Color Primary = Color.FromArgb(37, 99, 235);
    private static readonly Color Text = Color.FromArgb(51, 65, 85);
    private static readonly Color Border = Color.FromArgb(203, 213, 225);
    private static readonly Color Heading = Color.FromArgb(30, 41, 59);

    private static readonly string[] PrimaryButtonKeywords = { "Add", "Save", "Update", "Search", "Login" };

    public static void Apply(Control root)
    {
        root.BackColor = Canvas;
        root.Font = new Font(FontName, 9.5F);

        foreach (Control control in root.Controls)
            ApplyToControl(control);
    }

    private static void ApplyToControl(Control control)
    {
        switch (control)
        {
            case DataGridView grid:
                StyleGrid(grid);
                break;
            case Button button:
                StyleButton(button);
                break;
            case TextBox textBox:
                textBox.Font = new Font(FontName, 10F);
                textBox.ForeColor = Text;
                break;
            case ComboBox comboBox:
                comboBox.Font = new Font(FontName, 9.5F);
                comboBox.ForeColor = Text;
                break;
            case GroupBox groupBox:
                groupBox.BackColor = Surface;
                groupBox.ForeColor = Heading;
                groupBox.Font = new Font(FontName, 10F, FontStyle.Bold);
                break;
            case Label label:
                label.ForeColor = Text;
                if (label.Font.Size >= 18F)
                {
                    label.Font = new Font(FontName, 20F, FontStyle.Bold);
                    label.ForeColor = Heading;
                }
                break;
            case Panel panel:
                panel.BackColor = panel.BorderStyle == BorderStyle.FixedSingle ? Surface : Canvas;
                break;
        }

        foreach (Control child in control.Controls)
            ApplyToControl(child);
    }

    private static void StyleButton(Button button)
    {
        var isPrimary = PrimaryButtonKeywords.Any(keyword => button.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));

        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = isPrimary ? 0 : 1;
        button.FlatAppearance.BorderColor = Border;
        button.FlatAppearance.MouseOverBackColor = isPrimary
            ? Color.FromArgb(29, 78, 216)
            : Color.FromArgb(239, 246, 255);
        button.BackColor = isPrimary ? Primary : Surface;
        button.ForeColor = isPrimary ? Color.White : Text;
        button.Font = new Font(FontName, 9.5F, isPrimary ? FontStyle.Bold : FontStyle.Regular);
        button.Cursor = Cursors.Hand;
        button.UseVisualStyleBackColor = false;
    }

    private static void StyleGrid(DataGridView grid)
    {
        grid.BackgroundColor = Surface;
        grid.BorderStyle = BorderStyle.None;
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        grid.GridColor = Color.FromArgb(226, 232, 240);
        grid.EnableHeadersVisualStyles = false;
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105);
        grid.ColumnHeadersDefaultCellStyle.Font = new Font(FontName, 9F, FontStyle.Bold);
        grid.ColumnHeadersHeight = Math.Max(grid.ColumnHeadersHeight, 38);
        grid.DefaultCellStyle.BackColor = Surface;
        grid.DefaultCellStyle.ForeColor = Text;
        grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
        grid.DefaultCellStyle.SelectionForeColor = Heading;
        grid.DefaultCellStyle.Font = new Font(FontName, 9.5F);
        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 251, 253);
        grid.RowTemplate.Height = Math.Max(grid.RowTemplate.Height, 36);
    }
}
