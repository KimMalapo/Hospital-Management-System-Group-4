using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using BusinessLogic;
using Model;

namespace UI.Printing;

public sealed class StatementPrintDocument : PrintDocument
{
    private readonly int _billId;
    private readonly BillingService _billingService;
    private readonly AdmissionService _admissionService;
    private readonly PatientService _patientService;
    private readonly PaymentService _paymentService;
    private readonly Config.HospitalInfo _hospital;

    private StatementData? _data;
    private int _currentItemIndex;
    private int _currentPaymentIndex;
    private bool _itemsCompleted;
    private bool _totalsCompleted;

    public StatementPrintDocument(int billId, string connectionString)
    {
        _billId = billId;
        _billingService = new BillingService(connectionString);
        _admissionService = new AdmissionService(connectionString);
        _patientService = new PatientService(connectionString);
        _paymentService = new PaymentService(connectionString);
        _hospital = Config.GetHospitalInfo();
        DocumentName = $"Statement-{billId}";
    }

    protected override void OnBeginPrint(PrintEventArgs e)
    {
        base.OnBeginPrint(e);

        var bill = _billingService.GetById(_billId)
            ?? throw new InvalidOperationException("Bill could not be found in the database.");
        var admission = _admissionService.GetById(bill.AdmissionID)
            ?? throw new InvalidOperationException("Admission record could not be found.");

        _data = new StatementData
        {
            Bill = bill,
            Admission = admission,
            Patient = _patientService.GetById(admission.PatientID),
            Items = _billingService.GetItems(_billId),
            Payments = _paymentService.GetByBill(_billId)
        };

        DocumentName = $"Statement-{bill.BillNo ?? _billId.ToString()}";
        _currentItemIndex = 0;
        _currentPaymentIndex = 0;
        _itemsCompleted = false;
        _totalsCompleted = false;
    }

    protected override void OnPrintPage(PrintPageEventArgs e)
    {
        base.OnPrintPage(e);
        if (e.Graphics == null || _data == null) return;

        var g = e.Graphics;
        float left = e.MarginBounds.Left;
        float width = e.MarginBounds.Width;
        float bottom = e.MarginBounds.Bottom;
        float y = e.MarginBounds.Top;

        using var titleFont = new Font("Segoe UI", 14, FontStyle.Bold);
        using var subFont = new Font("Segoe UI", 9);
        using var headerFont = new Font("Segoe UI", 9.5f, FontStyle.Bold);
        using var bodyFont = new Font("Segoe UI", 9);
        using var boldFont = new Font("Segoe UI", 9.5f, FontStyle.Bold);
        using var pen = new Pen(Color.FromArgb(203, 213, 225), 1f);
        using var headerBrush = new SolidBrush(Color.FromArgb(241, 245, 249));

        var rightFmt = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
        var leftFmt = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter };

        // Page 1 Header: Hospital + Patient & Admission Info
        if (_currentItemIndex == 0 && !_itemsCompleted)
        {
            g.DrawString(_hospital.Name, titleFont, Brushes.Black, left, y);
            g.DrawString("STATEMENT OF ACCOUNT", titleFont, Brushes.Black, new RectangleF(left, y, width, 26), rightFmt);
            y += 24;
            g.DrawString($"{_hospital.Address} | {_hospital.Contact}", subFont, Brushes.DimGray, left, y);
            y += 22;
            g.DrawLine(pen, left, y, left + width, y);
            y += 10;

            var patientName = _data.Patient != null
                ? $"{_data.Patient.FirstName} {_data.Patient.LastName}".Trim()
                : _data.Admission.PatientName ?? "—";
            var admitDate = _data.Admission.AdmitDate.HasValue
                ? TimeConversion.ToLocal(_data.Admission.AdmitDate.Value).ToString("yyyy-MM-dd")
                : "—";
            var dischargeDate = _data.Admission.DischargeDate.HasValue
                ? TimeConversion.ToLocal(_data.Admission.DischargeDate.Value).ToString("yyyy-MM-dd")
                : "Active";

            g.DrawString($"Patient: {patientName}", boldFont, Brushes.Black, left, y);
            g.DrawString($"Bill No.: {_data.Bill.BillNo}", boldFont, Brushes.Black, new RectangleF(left, y, width, 20), rightFmt);
            y += 20;
            g.DrawString($"Admission No.: {_data.Admission.AdmissionNo} | Room: {_data.Admission.RoomNo ?? "—"}", bodyFont, Brushes.Black, left, y);
            g.DrawString($"Bill Status: {_data.Bill.Status}", bodyFont, Brushes.Black, new RectangleF(left, y, width, 20), rightFmt);
            y += 20;
            g.DrawString($"Admitted: {admitDate} | Discharged: {dischargeDate}", bodyFont, Brushes.Black, left, y);
            if (!string.IsNullOrWhiteSpace(_data.Patient?.ContactNumber))
                g.DrawString($"Contact: {_data.Patient.ContactNumber}", bodyFont, Brushes.Black, new RectangleF(left, y, width, 20), rightFmt);
            y += 26;
        }
        else
        {
            g.DrawString($"{_hospital.Name} — Statement ({_data.Bill.BillNo}) (Continued)", subFont, Brushes.DimGray, left, y);
            y += 22;
        }

        // Itemized Charges Table
        float colType = width * 0.16f;
        float colDesc = width * 0.44f;
        float colQty = width * 0.10f;
        float colUnit = width * 0.15f;
        float colTotal = width * 0.15f;
        float rowHeight = 24f;

        void DrawItemsHeader()
        {
            g.FillRectangle(headerBrush, left, y, width, rowHeight);
            g.DrawRectangle(pen, left, y, width, rowHeight);
            float x = left + 4;
            g.DrawString("Type", headerFont, Brushes.Black, new RectangleF(x, y, colType - 4, rowHeight), leftFmt); x += colType;
            g.DrawString("Description", headerFont, Brushes.Black, new RectangleF(x, y, colDesc - 4, rowHeight), leftFmt); x += colDesc;
            g.DrawString("Qty", headerFont, Brushes.Black, new RectangleF(x, y, colQty - 4, rowHeight), rightFmt); x += colQty;
            g.DrawString("Unit Price", headerFont, Brushes.Black, new RectangleF(x, y, colUnit - 4, rowHeight), rightFmt); x += colUnit;
            g.DrawString("Line Total", headerFont, Brushes.Black, new RectangleF(x, y, colTotal - 4, rowHeight), rightFmt);
            y += rowHeight;
        }

        if (!_itemsCompleted)
        {
            DrawItemsHeader();
            while (_currentItemIndex < _data.Items.Rows.Count)
            {
                if (y + rowHeight > bottom)
                {
                    e.HasMorePages = true;
                    return; // Moves to next page before drawing row so no row is cut
                }

                DataRow row = _data.Items.Rows[_currentItemIndex];
                float x = left + 4;
                g.DrawString(Convert.ToString(row["ItemType"]) ?? "", bodyFont, Brushes.Black, new RectangleF(x, y, colType - 4, rowHeight), leftFmt); x += colType;
                g.DrawString(Convert.ToString(row["Description"]) ?? "", bodyFont, Brushes.Black, new RectangleF(x, y, colDesc - 4, rowHeight), leftFmt); x += colDesc;
                g.DrawString(Convert.ToString(row["Quantity"]) ?? "", bodyFont, Brushes.Black, new RectangleF(x, y, colQty - 4, rowHeight), rightFmt); x += colQty;
                g.DrawString($"₱{Convert.ToDecimal(row["UnitPrice"]):N2}", bodyFont, Brushes.Black, new RectangleF(x, y, colUnit - 4, rowHeight), rightFmt); x += colUnit;
                g.DrawString($"₱{Convert.ToDecimal(row["Amount"]):N2}", bodyFont, Brushes.Black, new RectangleF(x, y, colTotal - 4, rowHeight), rightFmt);
                g.DrawLine(pen, left, y + rowHeight, left + width, y + rowHeight);

                y += rowHeight;
                _currentItemIndex++;
            }
            _itemsCompleted = true;
            y += 14;
        }

        // Totals Summary (matches Billing screen: Subtotal, Discount, Total, Amount Paid, Balance)
        if (!_totalsCompleted)
        {
            float totalsBlockHeight = 135f;
            if (y + totalsBlockHeight > bottom)
            {
                e.HasMorePages = true;
                return;
            }

            float summaryLeft = left + width * 0.52f;
            float summaryWidth = width * 0.48f;

            void DrawTotalLine(string label, decimal amount, bool bold = false)
            {
                var f = bold ? boldFont : bodyFont;
                g.DrawString(label, f, Brushes.Black, summaryLeft, y);
                g.DrawString($"₱{amount:N2}", f, Brushes.Black, new RectangleF(summaryLeft, y, summaryWidth, 20), rightFmt);
                y += 22;
            }

            DrawTotalLine("Subtotal:", _data.Bill.Subtotal);
            DrawTotalLine("Discount:", _data.Bill.Discount);
            DrawTotalLine("Total:", _data.Bill.TotalAmount, bold: true);
            DrawTotalLine("Amount Paid:", _data.Bill.AmountPaid);
            g.DrawLine(pen, summaryLeft, y, left + width, y);
            y += 4;
            DrawTotalLine("Balance:", _data.Bill.Balance, bold: true);

            _totalsCompleted = true;
            y += 16;
        }

        // Payments History
        if (_data.Payments.Rows.Count > 0 && _currentPaymentIndex < _data.Payments.Rows.Count)
        {
            if (y + rowHeight * 2 > bottom)
            {
                e.HasMorePages = true;
                return;
            }

            g.DrawString("Payments", headerFont, Brushes.Black, left, y);
            y += 22;

            while (_currentPaymentIndex < _data.Payments.Rows.Count)
            {
                if (y + rowHeight > bottom)
                {
                    e.HasMorePages = true;
                    return;
                }

                DataRow p = _data.Payments.Rows[_currentPaymentIndex];
                var pDate = p["PaymentDate"] != DBNull.Value
                    ? TimeConversion.ToLocal(Convert.ToDateTime(p["PaymentDate"])).ToString("yyyy-MM-dd HH:mm")
                    : "—";
                var receiptNo = Convert.ToString(p["ReceiptNo"]) ?? "—";
                var method = Convert.ToString(p["Method"]) ?? "—";
                var status = Convert.ToString(p["Status"]) ?? "Posted";
                var amt = Convert.ToDecimal(p["Amount"]);

                g.DrawString($"{receiptNo}  |  {pDate}  |  {method} ({status})", bodyFont, Brushes.Black, left, y);
                g.DrawString($"₱{amt:N2}", bodyFont, Brushes.Black, new RectangleF(left, y, width, 20), rightFmt);
                y += rowHeight;
                _currentPaymentIndex++;
            }
        }

        e.HasMorePages = false;
    }
}