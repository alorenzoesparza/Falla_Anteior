﻿namespace Falla.Backend.Helpers
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web;

    public class FilesHelper
    {
        public static string UploadPhotoBackEnd(HttpPostedFileBase file, 
            string folder, 
            string name, 
            int iWidth, 
            int iHeight)
        {

            if (file == null || string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(name))
            {
                return null;
            }

            try
            {
                string directory = string.Empty;
                string path = string.Empty;
                string pic = string.Empty;

                if (file != null)
                {
                    pic = Path.GetFileName(file.FileName);
                    directory = Path.Combine(HttpContext.Current.Server.MapPath(folder));
                    path = Path.Combine(HttpContext.Current.Server.MapPath(folder), pic);

                    var existDirectory = Existirectory(directory);

                    if (!existDirectory)
                    {
                        return null;
                    }

                    file.SaveAs(path);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                    var newName = Path.Combine(HttpContext.Current.Server.MapPath(folder), name);
                    // Redimensionar la imagen 
                    var newImagenName = ResizeImage(path, newName, iWidth, iHeight);
                    return newImagenName;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }

        public static string ResizeImage(string strImgPath, 
            string strImgOutputPath, 
            int iWidth, 
            int iHeight)
        {
            try
            {
                bool mismaImagen = strImgPath.Equals(strImgOutputPath);
                if (mismaImagen)
                {
                    strImgOutputPath = strImgPath + "___.jpg";
                }

                mismaImagen = true;

                string[] extensiones = {
                                   ".jpg",
                                   ".png",
                                   ".bmp",
                                   ".gif"
                               };

                if (!extensiones.Contains(Path.GetExtension(strImgPath.ToLower())))
                    throw new Exception("Extensión no soportada");

                var extension = Path.GetExtension(strImgPath.ToLower());
                strImgOutputPath = string.Format("{0}{1}", strImgOutputPath, extension);
                //Lee el fichero en un stream
                Stream mystream = null;

                if (strImgPath.StartsWith("http"))
                {
                    HttpWebRequest wreq = (HttpWebRequest)WebRequest.Create(strImgPath);
                    HttpWebResponse wresp = (HttpWebResponse)wreq.GetResponse();
                    mystream = wresp.GetResponseStream();
                }
                else
                {
                    mystream = File.OpenRead(strImgPath);
                }

                // Cargo la imágen
                Bitmap imgToResize = new Bitmap(mystream);

                Size size = new Size(iWidth, iHeight);

                int sourceWidth = imgToResize.Width;
                int sourceHeight = imgToResize.Height;

                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;

                nPercentW = ((float)size.Width / (float)sourceWidth);
                nPercentH = ((float)size.Height / (float)sourceHeight);

                if (nPercentH < nPercentW)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;

                int destWidth = (int)(sourceWidth * nPercent);
                int destHeight = (int)(sourceHeight * nPercent);

                Bitmap b = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage((Image)b);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
                g.Dispose();

                // We will store the correct image codec in this object
                ImageCodecInfo ici = GetEncoderInfo("image/jpeg");
                // This will specify the image quality to the encoder
                EncoderParameter epQuality = new EncoderParameter(Encoder.Quality, 99L);
                // Store the quality parameter in the list of encoder parameters
                EncoderParameters eps = new EncoderParameters(1);
                eps.Param[0] = epQuality;
                //strImgOutputPath = string.Format("{0}{1}", strImgOutputPath, extension);
                b.Save(strImgOutputPath, ici, eps);

                imgToResize.Dispose();
                mystream.Close();
                mystream.Dispose();
                b.Dispose();
                g.Dispose();

                //if (mismaImagen)
                //{
                //    File.Delete(strImgPath);
                //    File.Move(strImgOutputPath, strImgPath);
                //}
                File.Delete(strImgPath);
                return strImgOutputPath;
            }
            catch
            {
                throw;
            }
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        public static bool Existirectory(string path)
        {
            try
            {
                // Determine whether the directory exists.
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string UploadPhoto(HttpPostedFileBase file, string folder)
        {
            string path = string.Empty;
            string pic = string.Empty;

            if (file != null)
            {
                pic = Path.GetFileName(file.FileName);
                path = Path.Combine(HttpContext.Current.Server.MapPath(folder), pic);
                file.SaveAs(path);
            }

            return pic;
        }

        public static bool UploadPhoto(MemoryStream stream, string folder, string name)
        {
            try
            {
                stream.Position = 0;
                var path = Path.Combine(HttpContext.Current.Server.MapPath(folder), name);
                File.WriteAllBytes(path, stream.ToArray());
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
