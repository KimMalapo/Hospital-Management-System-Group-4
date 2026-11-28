using System;
using System.Drawing;
using System.Drawing.Printing;
using BusinessLogic;
using Model;

namespace UI.Printing;

public sealed class ReceiptPrintDocument : PrintDocument
{
    private readonly int _paymentId;
    private readonly PaymentService _paymentService;
    private readonly Config.HospitalInfo _hospital;
    private ReceiptData? _data;

    public ReceiptPrintDocument(int paymentId, string connectionString)
    {
        _paymentId = paymentId;
        _paymentService = new PaymentService(connectionString);
        _hospital = Config.GetHospitalInfo();
        DocumentName = $"Receipt-{paymentId}";
    }

    protected override void OnBeginPrint(PrintEventArgs e)
    {
        base.OnBeginPrint(e);
        _data = _paymentService.GetReceipt(_paymentId)
            ?? throw new InvalidOperationException("Receipt record could not be loaded from the database.");
        DocumentName = $"Receipt-{_data.ReceiptNo ?? _paymentId.ToString()}";
    }

    protected override void OnPrintPage(PrintPageEventArgs e)
    {
        base.OnPrintPage(e);
        if (e.Graphics == null || _data == null) return;

        var g = e.Graphics;
        float left = e.MarginBounds.Left;
        float width = e.MarginBounds.Width;
        float y = e.MarginBounds.Top;

        using var titleFont = new Font("Segoe UI", 14, FontStyle.Bold);
        using var subFont = new Font("Segoe UI", 9, FontStyle.Regular);
        using var sectionFont = new Font("Segoe UI", 11, FontStyle.Bold);
        using var labelFont = new Font("Segoe UI", 9.5f, FontStyle.Bold);
        using var valueFont = new Font("Segoe UI", 9.5f, FontStyle.Regular);
        using var totalFont = new Font("Segoe UI", 11, FontStyle.Bold);
        using var pen = new Pen(Color.FromArgb(203, 213, 225), 1f);

        var centerFmt = new StringFormat { Alignment = StringAlignment.Center };
        var rightFmt = new StringFormat { Alignment = StringAlignment.Far };

        // Hospital Header
        g.DrawString(_hospital.Name, titleFont, Brushes.Black, new RectangleF(left, y, width, 26), centerFmt);
        y += 26;
        g.DrawString(_hospital.Address, subFont, Brushes.DimGray, new RectangleF(left, y, width, 18), centerFmt);
        y += 18;
        g.DrawString(_hospital.Contact, subFont, Brushes.DimGray, new RectangleF(left, y, width, 18), centerFmt);
        y += 26;

        g.DrawLine(pen, left, y, left + width, y);
        y += 12;

        g.DrawString("OFFICIAL PAYMENT RECEIPT", sectionFont, Brushes.Black, new RectangleF(left, y, width, 24), centerFmt);
        y += 32;

        void DrawRow(string label, string value, Font? valFont = null)
        {
            g.DrawString(label, labelFont, Brushes.DimGray, left, y);
            g.DrawString(value, valFont ?? valueFont, Brushes.Black, new RectangleF(left, y, width, 22), rightFmt);
            y += 24;
        }

        var localDate = _data.PaymentDate.HasValue
            ? TimeConversion.ToLocal(_data.PaymentDate.Value).ToString("yyyy-MM-dd hh:mm tt")
            : "N/A";

        DrawRow("Receipt No.:", _data.ReceiptNo ?? "—");
        DrawRow("Date & Time:", localDate);
        DrawRow("Patient:", _data.PatientName ?? "—");
        DrawRow("Admission No.:", _data.AdmissionNo ?? "—");
        DrawRow("Bill No.:", _data.BillNo ?? "—");
        DrawRow("Payment Method:", string.IsNullOrWhiteSpace(_data.ReferenceNo)
            ? (_data.Method ?? "—")
            : $"{_data.Method} (Ref: {_data.ReferenceNo})");

        y += 6;
        g.DrawLine(pen, left, y, left + width, y);
        y += 12;

        DrawRow("Bill Total:", $"₱{_data.TotalAmount:N2}");
        DrawRow("Amount Paid:", $"₱{_data.Amount:N2}", totalFont);
        DrawRow("Balance After Payment:", $"₱{_data.BalanceAfter:N2}", totalFont);

        y += 6;
        g.DrawLine(pen, left, y, left + width, y);
        y += 14;

        DrawRow("Cashier:", _data.ReceivedBy ?? "—");

        e.HasMorePages = false;
    }
}