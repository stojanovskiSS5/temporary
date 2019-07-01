using AForge.Imaging.Filters;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;

namespace FINKI_Application_ocr.Datamanipulation
{
    public class ImagePreprocessing
    {
        private Bitmap _image;

        public Bitmap Image { get => _image; set => _image = value; }

        public ImagePreprocessing(Bitmap _image)
        {
            Image = _image ?? throw new ArgumentNullException(nameof(_image));
        }


        public void StartImagePreprocessing()
        {

            //worked najdobro
            RescaleResizeImage();
            NoiseRemoval();
            ConvertToGrayscale();
            TransformToBlackAndWhite();
            SharpenningImage();
        }

        private void ConvertToGrayscale()
        {
            Image<Gray, Byte> result = new Image<Gray, byte>(_image);
            Image = result.ToBitmap();
        }

        private void SharpenningImage()
        {

            int[,] kernel = {
            { 0, -1,  0 },
            { -1,  40,  -1 },
            {  0,  -1,  0 } };
            // create filter
            Convolution filter = new Convolution(kernel);
            // apply the filter
            filter.ApplyInPlace(Image);
        }


        private void NoiseRemoval()
        {
            // create filter
            ConservativeSmoothing filter = new ConservativeSmoothing();
            // apply the filter
            filter.ApplyInPlace(Image);
        }

        private void TransformToBlackAndWhite()
        {
            Bitmap newBmp = new Bitmap(Image.Width, Image.Height);

            for (int x = 0; x < Image.Width; x++)
            {
                for (int y = 0; y < Image.Height; y++)
                {
                    Color pixelColor = Image.GetPixel(x, y);

                    if (pixelColor.R >= 165 && pixelColor.G >= 165 && pixelColor.B >= 165 && pixelColor.A >= 165)
                    {
                        Color newColor = Color.FromArgb(255, 255, 255, 255);
                        newBmp.SetPixel(x, y, newColor);
                    }
                    else
                    {
                        Color newColor = Color.FromArgb(0, 0, 0, 0);
                        newBmp.SetPixel(x, y, newColor);
                    }
                }
            }
            Image = newBmp;
        }

        private void DeskewedImage()
        {
            Grayscale filterg = new Grayscale(0.2125, 0.7154, 0.0721);
            Image = filterg.Apply(Image);
        }

        private void RescaleResizeImage()
        {
            double initialNumber = 1024.0;
            double factor = Math.Min(2, initialNumber / Image.Height);
            int newHeight = (int)factor * Image.Height;
            int newWidth = (int)factor * Image.Width;

            if (newHeight == 0 || newWidth == 0)
            {
                newHeight = Image.Height / 2;
                newWidth = Image.Width / 2;
            }

            // create filter
            ResizeBilinear filter = new ResizeBilinear(newWidth, newHeight);
            // apply the filter
            Image = filter.Apply(Image);
        }


        private void AdjustContrast()
        {
            // create filter
            ContrastCorrection filter = new ContrastCorrection(30);
            // process image
            filter.ApplyInPlace(Image);
        }




    }
}
