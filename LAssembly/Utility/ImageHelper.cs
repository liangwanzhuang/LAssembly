using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace LTools.Utility
{
    public class ImageHelper
    {
        /// <summary>
        /// 无损压缩图片(比例压缩)
        /// </summary>
        /// <param name="sFile">原图片地址</param>
        /// <param name="dFile">压缩后保存图片地址</param>
        /// <param name="flag">压缩质量（数字越小压缩率越高）1-100</param>
        /// <param name="size">压缩后图片的最大大小</param>
        /// <param name="sfsc">是否是第一次调用</param>
        /// <returns></returns>
        public static bool CompressImage(string sFile, string dFile, int flag = 8, int size = 80, bool sfsc = true)
        {
            Image iSource = Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;
            //如果是第一次调用，原始图像的大小小于要压缩的大小，则直接复制文件，并且返回true
            FileInfo firstFileInfo = new FileInfo(sFile);
            if (sfsc == true && firstFileInfo.Length < size * 1024)
            {
                firstFileInfo.CopyTo(dFile);
                return true;
            }

            int dHeight = iSource.Height / 2;
            int dWidth = iSource.Width / 2;
            int sW = 0, sH = 0;
            //按比例缩放
            Size tem_size = new Size(iSource.Width, iSource.Height);
            if (tem_size.Width > dHeight || tem_size.Width > dWidth)
            {
                if ((tem_size.Width * dHeight) > (tem_size.Width * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * tem_size.Height) / tem_size.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (tem_size.Width * dHeight) / tem_size.Height;
                }
            }
            else
            {
                sW = tem_size.Width;
                sH = tem_size.Height;
            }

            Bitmap ob = new Bitmap(dWidth, dHeight);
            Graphics g = Graphics.FromImage(ob);

            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);

            g.Dispose();

            //以下代码为保存图片时，设置压缩质量
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;

            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径    
                                                    //ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径
                                                    //FileInfo fi = new FileInfo(dFile);
                                                    //if (fi.Length > 1024 * size)
                                                    //{
                                                    //    //flag = flag - 10;
                                                    //    CompressImage(sFile, dFile, flag, size, false);
                                                    //}
                }
                else
                {
                    ob.Save(dFile, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();
            }
        }

        /// <summary>
        /// 生成缩略图（固定 size）
        /// </summary>
        /// <param name="serverImagePath">图片地址</param>
        /// <param name="thumbnailImagePath">缩略图地址</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <param name="p"></param>
        public static void GetThumbnail(string serverImagePath, string thumbnailImagePath, int width, int height)
        {
            System.Drawing.Image serverImage = System.Drawing.Image.FromFile(serverImagePath);
            serverImage = KiRotate(serverImage, RotateFlipType.Rotate270FlipNone);
            //画板大小
            int towidth = width;
            int toheight = height;
            //缩略图矩形框的像素点
            int x = 0;
            int y = 0;
            int ow = serverImage.Width;
            int oh = serverImage.Height;

            if (ow > oh)
            {
                toheight = serverImage.Height * width / serverImage.Width;
            }
            else
            {
                towidth = serverImage.Width * height / serverImage.Height;
            }
            //新建一个bmp图片
            System.Drawing.Image bm = new System.Drawing.Bitmap(width, height);
            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bm);
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.White);
            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(serverImage, new System.Drawing.Rectangle((width - towidth) / 2, (height - toheight) / 2, towidth, toheight),
                0, 0, ow, oh,
                System.Drawing.GraphicsUnit.Pixel);
            try
            {
                bm.Save(thumbnailImagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                serverImage.Dispose();
                bm.Dispose();
                g.Dispose();
            }
        }
        //图片旋转方向

        /// <summary>
        /// 顺时针旋转90度 RotateFlipType.Rotate90FlipNone
        /// 逆时针旋转90度 RotateFlipType.Rotate270FlipNone
        ///水平翻转 RotateFlipType.Rotate180FlipY
        ///垂直翻转 RotateFlipType.Rotate180FlipX
        /// </summary>
        /// <param name="Image"></param>
        /// <param RotateFlipType="flipType"></param>
        /// <returns></returns>
        public static Image KiRotate(Image img, RotateFlipType flipType)
        {
            try
            {
                //顺时针旋转90度 RotateFlipType.Rotate90FlipNone
                //逆时针旋转90度 RotateFlipType.Rotate270FlipNone
                //水平翻转 RotateFlipType.Rotate180FlipY
                //垂直翻转 RotateFlipType.Rotate180FlipX
                //
                img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                return img;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 将文本绘制为Image。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        public static Image CreateImage4Text(string text, Font font, out Size size, int? width = null, int? height = null, StringAlignment vAlign = StringAlignment.Center, Color? textColor = null)
        {
            var sf = new StringFormat()
            {
                LineAlignment = vAlign
            };

            //大小
            if (width.HasValue && height.HasValue)
            {
                size = new Size(width.Value, height.Value);
            }
            else
            {
                using (var bmp = new System.Drawing.Bitmap(1, 1))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        size = g.MeasureString(text, font, int.MaxValue, sf).ToSize();

                        g.Clear(Color.White);
                    }
                }

                if (width.HasValue)
                {
                    size.Width = width.Value;
                }
                else
                {
                    //因为sizeF.ToSize可能会舍去，导致宽度不够
                    size.Width += 1;
                }

                if (height.HasValue)
                {
                    size.Height = height.Value;
                }
            }

            //绘制
            var image = new System.Drawing.Bitmap(size.Width, size.Height);

            using (Graphics g = Graphics.FromImage(image))
            {
                g.Clear(Color.White);

                using (var brush = new SolidBrush(textColor.HasValue ? textColor.Value : Color.Black))
                {
                    g.DrawString(text, font, brush, new Rectangle(0, 0, size.Width, size.Height), sf);
                }

                return image;
            }
        }
    }
}
