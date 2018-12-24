using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ZXing;
using ZXing.Common;

namespace LAssembly.Utility
{
    public class ZXingCommonHeple
    {
        public static Bitmap BuildBarCode(string code, int height, int width)
        {
            Bitmap img = null;
            // 1.设置条形码规格
            EncodingOptions encodeOption = new EncodingOptions();
            encodeOption.Height = height; // 必须制定高度、宽度
            encodeOption.Width = width;

            // 2.生成条形码图片并保存
            ZXing.BarcodeWriter wr = new BarcodeWriter();
            wr.Options = encodeOption;
            wr.Format = BarcodeFormat.CODE_128; //  条形码规格：EAN13规格：12（无校验位）或13位数字
            img = wr.Write(code); // 生成图片
            //string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "\\EAN_13-23455666.jpg";
            //img.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            return img;
        }
        public static string RedbarCode(Bitmap img)
        {
            string ret = string.Empty;
            // 1.设置读取条形码规格
            DecodingOptions decodeOption = new DecodingOptions();
            decodeOption.PossibleFormats = new List<BarcodeFormat>() {
               BarcodeFormat.CODE_128,
            };

            // 2.进行读取操作
            ZXing.BarcodeReader br = new BarcodeReader();
            br.Options = decodeOption;
            ZXing.Result rs = br.Decode(img);
            if (rs == null)
            {
                ret = "";
            }
            else
            {
                ret = rs.Text;
            }
            return ret;
        }
        public static Bitmap BulidQRCode(string code,int height, int width, int margin)
        {
            Bitmap img = null;
            // 1.设置QR二维码的规格
            ZXing.QrCode.QrCodeEncodingOptions qrEncodeOption = new ZXing.QrCode.QrCodeEncodingOptions();
            qrEncodeOption.CharacterSet = "UTF-8"; // 设置编码格式，否则读取'中文'乱码
            qrEncodeOption.Height = height;
            qrEncodeOption.Width = width;
            qrEncodeOption.Margin = margin; // 设置周围空白边距

            // 2.生成条形码图片并保存
            ZXing.BarcodeWriter wr = new BarcodeWriter();
            wr.Format = BarcodeFormat.QR_CODE; // 二维码
            wr.Options = qrEncodeOption;
            img = wr.Write(code);
            return img;
        }

        public static Bitmap BulidQRHaveLogo(string code ,Bitmap logoImg,int height,int width, int margin)
        {
            Bitmap img = null;
            // 1.设置QR二维码的规格
            ZXing.QrCode.QrCodeEncodingOptions qrEncodeOption = new ZXing.QrCode.QrCodeEncodingOptions();
            qrEncodeOption.CharacterSet = "UTF-8"; // 设置编码格式，否则读取'中文'乱码
            qrEncodeOption.Height = height;
            qrEncodeOption.Width = width;
            qrEncodeOption.Margin = margin; // 设置周围空白边距

            // 2.生成条形码图片
            ZXing.BarcodeWriter wr = new BarcodeWriter();
            wr.Format = BarcodeFormat.QR_CODE; // 二维码
            wr.Options = qrEncodeOption;
            img = wr.Write(code);

            // 3.在二维码的Bitmap对象上绘制logo图片
            Graphics g = Graphics.FromImage(img);
            Rectangle logoRec = new Rectangle(); // 设置logo图片的大小和绘制位置
            logoRec.Width = img.Width / 6;
            logoRec.Height = img.Height / 6;
            logoRec.X = img.Width / 2 - logoRec.Width / 2; // 中心点
            logoRec.Y = img.Height / 2 - logoRec.Height / 2;
            g.DrawImage(logoImg, logoRec);
            return img;
        }
        public static string RedQRCode(Bitmap img)
        {
            string ret = string.Empty;
            // 1.设置读取条形码规格
            DecodingOptions decodeOption = new DecodingOptions();
            decodeOption.PossibleFormats = new List<BarcodeFormat>() {
               BarcodeFormat.QR_CODE,
           };

            // 2.进行读取操作
            ZXing.BarcodeReader br = new BarcodeReader();
            br.Options = decodeOption;
            ZXing.Result rs = br.Decode(img);
            return ret;
        }

        
    }
}
