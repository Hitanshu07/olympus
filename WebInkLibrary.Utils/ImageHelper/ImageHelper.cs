using System;
using System.Drawing;
using System.IO;
using System.Web;

namespace WebInkLibrary.Utils.ImageHelper
{
    public class ImageHelper
    {
        public String Add(HttpPostedFileBase image, string fileName, string path)
        {
            const string res = "";
            if (image == null || image.ContentLength < 1)
                return res;
            var uploadPath = HttpContext.Current.Server.MapPath(path);
            //string UninqueId = DateTime.Now.ToFileTimeUtc() + image.FileName;
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            //using (var fs = File.Create(Path.Combine(uploadPath, fileName)))
            //{
            //    image.InputStream.CopyTo(fs);
            //}
            var imagename = HtmlHelper.HtmlHelper.GetFileNameWebSafe(fileName, fileName.Length);
            image.SaveAs(Path.Combine(uploadPath, imagename));
            return imagename;
        }

        public void Delete(string imageName, string path)
        {
            var fixedPath = HttpContext.Current.Server.MapPath(path + "/" + imageName);

            if (File.Exists(fixedPath))
            {
                File.Delete(fixedPath);
            }
        }

        public static void ResizeAndSaveImage(HttpPostedFile fu, int height, int width, string filePath)
        {
            var image = Image.FromStream(fu.InputStream);

            var imageHeight = (int)image.PhysicalDimension.Height;
            var imageWidth = (int)image.PhysicalDimension.Width;
            if (imageHeight <= height && imageWidth <= width)
            {
                image.Save(filePath, image.RawFormat);
                image.Dispose();
            }
            else
            {
                var newSize = Image_Resize(height, width, imageHeight, imageWidth);

                var img = (Bitmap)image;
                var imageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;

                var imgOutput = new Bitmap(img, newSize.Width, newSize.Height);
                var myresizer = Graphics.FromImage(imgOutput);
                myresizer.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                myresizer.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                myresizer.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                myresizer.DrawImage(img, 0, 0, newSize.Width, newSize.Height);
                try
                {
                    imgOutput.Save(filePath, imageFormat);
                }
                catch
                {
                    myresizer.Dispose();
                    imgOutput.Dispose();
                    img.Dispose();
                }
                myresizer.Dispose();
                imgOutput.Dispose();
                img.Dispose();
            }
        }
        public static Size Image_Resize(int containerHeight, int containerWidth, int srcHeight, int srcWidth)
        {
            int resultHeight, resultWidth;
            var resultantSize = new Size();

            var srcRatio = (float)srcWidth / srcHeight;
            var srcRatioPos = (float)srcHeight / srcWidth;
            var trgRatio = (float)containerWidth / containerHeight;

            if (containerHeight > srcHeight && containerWidth > srcWidth)
            {
                resultHeight = srcHeight;
                resultWidth = srcWidth;
                //No resize required as the Source image is smaller then the target container
            }
            else if (containerHeight == 0 || containerWidth == 0)      //If the src height or width is '0' then only resize the non-zero dimension of the src_image. 
            {
                if (containerHeight == 0)
                {
                    resultHeight = srcHeight;
                    resultWidth = containerWidth;
                }
                else
                {
                    resultHeight = containerHeight;
                    resultWidth = srcWidth;
                }
            }
            else
            {
                if (srcRatio < trgRatio)
                {
                    resultHeight = containerHeight;
                    resultWidth = (int)(resultHeight * srcRatio);
                }
                else
                {
                    resultWidth = containerWidth;
                    resultHeight = (int)(resultWidth * srcRatioPos);
                }
            }
            resultantSize.Height = resultHeight;
            resultantSize.Width = resultWidth;
            return resultantSize;
        }
    }
}
