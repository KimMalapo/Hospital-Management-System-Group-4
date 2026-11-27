
using System;
using System.Drawing;
using System.Windows.Forms;

namespace UI;

internal static class UITheme
{
    private static readonly Color Canvas = Color.FromArgb(245, 247, 251);
    private static readonly Color Surface = Color.White;
    private static readonly Color Primary = Color.FromArgb(37, 99, 235);
    private static readonly Color Text = Color.FromArgb(51, 65, 85);
    private static readonly Color Border = Color.FromArgb(203, 213, 225);

   
    private static readonly Font BodyFont =
        new Font("Segoe UI", 11F);

    private static readonly Font InputFont =
        new Font("Segoe UI", 11F);

    private static readonly Font HeadingFont =
        new Font("Segoe UI", 20F, FontStyle.Bold);

    private static readonly Font SubHeadingFont =
        new Font("Segoe UI", 14F, FontStyle.Bold);

    private static readonly Font ButtonFont =
        new Font("Segoe UI", 11F, FontStyle.Bold);

    public static void Apply(Control root)
    {
        root.BackColor = Canvas;
        root.Font = BodyFont;

        foreach (Control control in root.Controls)
            ApplyToControl(control);
    }

    private static void ApplyToControl(Control control)
    {
        
        control.Font = BodyFont;

        switch (control)
        {
            case DataGridView grid:
                StyleGrid(grid);
                break;

            case Button button:
                StyleButton(button);
                break;

            case TextBox textBox:
                textBox.Font = InputFont;
                textBox.ForeColor = Text;
                break;

            case ComboBox comboBox:
                comboBox.Font = InputFont;
                comboBox.ForeColor = Text;
                break;

            case GroupBox groupBox:
                groupBox.BackColor = Surface;
                groupBox.ForeColor = Color.FromArgb(30, 41, 59);
                groupBox.Font = SubHeadingFont;
                break;

            case Label label:
                label.ForeColor = Text;

                if (label.Font.Size >= 18F)
                {
                    label.Font = HeadingFont;
                    label.ForeColor = Color.FromArgb(30, 41, 59);
                }
                else if (label.Font.Bold)
                {
                    label.Font = SubHeadingFont;
                }
                else
                {
                    label.Font = BodyFont;
                }
                break;

            case Panel panel:
                panel.BackColor =
                    panel.BorderStyle == BorderStyle.FixedSingle
                    ? Surface : Canvas;
                break;
        }

        foreach (Control child in control.Controls)
            ApplyToControl(child);
    }

    private static void StyleButton(Button button)
    {
        var isPrimary =
            button.Name.Contains("Add", StringComparison.OrdinalIgnoreCase)
            || button.Name.Contains("Save", StringComparison.OrdinalIgnoreCase)
            || button.Name.Contains("Update", StringComparison.OrdinalIgnoreCase)
            || button.Name.Contains("Search", StringComparison.OrdinalIgnoreCase)
            || button.Name.Contains("Login", StringComparison.OrdinalIgnoreCase);

        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = isPrimary ? 0 : 1;
        button.FlatAppearance.BorderColor = Border;

        button.FlatAppearance.MouseOverBackColor = isPrimary
            ? Color.FromArgb(29, 78, 216)
            : Color.FromArgb(239, 246, 255);

        button.BackColor = isPrimary ? Primary : Surface;
        button.ForeColor = isPrimary ? Color.White : Text;
        button.Font = ButtonFont;
        button.Cursor = Cursors.Hand;
        button.UseVisualStyleBackColor = false;
    }

    private static void StyleGrid(DataGridView grid)
    {
        grid.BackgroundColor = Surface;
        grid.BorderStyle = BorderStyle.None;
        grid.CellBorderStyle =
            DataGridViewCellBorderStyle.SingleHorizontal;

        grid.GridColor = Color.FromArgb(226, 232, 240);
        grid.EnableHeadersVisualStyles = false;

        grid.ColumnHeadersBorderStyle =
            DataGridViewHeaderBorderStyle.None;

        grid.ColumnHeadersDefaultCellStyle.BackColor =
            Color.FromArgb(248, 250, 252);

        grid.ColumnHeadersDefaultCellStyle.ForeColor =
            Color.FromArgb(71, 85, 105);

        grid.ColumnHeadersDefaultCellStyle.Font =
            SubHeadingFont;

        grid.ColumnHeadersHeight = Math.Max(
            grid.ColumnHeadersHeight, 42);

        grid.DefaultCellStyle.BackColor = Surface;
        grid.DefaultCellStyle.ForeColor = Text;

        grid.DefaultCellStyle.SelectionBackColor =
            Color.FromArgb(219, 234, 254);

        grid.DefaultCellStyle.SelectionForeColor =
            Color.FromArgb(30, 41, 59);

        grid.DefaultCellStyle.Font = BodyFont;

        grid.AlternatingRowsDefaultCellStyle.BackColor =
            Color.FromArgb(250, 251, 253);

        grid.RowTemplate.Height = Math.Max(
            grid.RowTemplate.Height, 40);

        grid.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;
    }
}
