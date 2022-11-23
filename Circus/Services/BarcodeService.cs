using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Circus.DB;
using QRCoder;

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

            Bitmap qrCodeImage = null; 
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q))
            using (QRCode qrCode = new QRCode(qrCodeData))
            {
                qrCodeImage = qrCode.GetGraphic(25, Color.OrangeRed, Color.White, true);
            }

            return qrCodeImage;
        }
    }
}
