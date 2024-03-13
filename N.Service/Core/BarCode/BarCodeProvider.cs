using Microsoft.Extensions.Hosting.Internal;
using System.Drawing.Imaging;
using System.Drawing;
using IronBarCode;

namespace N.Core.BarCodeProvider
{
    public class BarCodeProvider
    {

        public static string Generate(string? text)
        {
            var myBarcode = BarcodeWriter.CreateBarcode(text, BarcodeWriterEncoding.EAN8);

            // Reading a barcode is easy with IronBarcode:
            //var resultFromFile = BarcodeReader.Read(@"file/barcode.png"); // From a file
            //var resultFromPdf = BarcodeReader.ReadPdf(@"file/mydocument.pdf"); // From PDF use ReadPdf

            // After creating a barcode, we may choose to resize and save which is easily done with:
            myBarcode.ResizeTo(400, 150);
            var guid = Guid.NewGuid().ToString();
            guid = text ?? "";
            myBarcode.SaveAsImage("BarCode/" + guid + ".png");
            return guid;
        }

    }
}
