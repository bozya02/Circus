using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Circus.DB;
using IronBarCode;

namespace Circus.Services
{
    public class BarcodeService
    {
        public static Bitmap GenerateQRCode(Ticket ticket)
        {
            var content = $"{ticket.Id} " +
                          $"{ticket.Perfomance.Name} " +
                          $"{ticket.Perfomance.Date.ToShortDateString()} " +
                          $"{ticket.Perfomance.StartTime}-" +
                          $"{ticket.Perfomance.EndTime} " +
                          $"{ticket.User.FullName} " +
                          $"{ticket.Perfomance.TicketPrice} руб.";

            var qrCode = BarcodeWriter.CreateBarcode(content, BarcodeWriterEncoding.QRCode);
            qrCode.TextEncoding = Encoding.UTF8;
            qrCode.ChangeBarCodeColor(Color.OrangeRed);
            return qrCode.ToBitmap();
        }
    }
}
