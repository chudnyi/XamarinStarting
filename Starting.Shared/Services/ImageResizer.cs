
#if __IOS__
using System;
using System.Drawing;
using UIKit;
using CoreGraphics;
using StartingPCL.ListView;

#endif

#if __ANDROID__
using System.IO;
using Android.Graphics;
using StartingPCL.ListView;

#endif

#if WINDOWS_PHONE
using Microsoft.Phone;
using System.Windows.Media.Imaging;
#endif

namespace StartingPCL.Helpers
{
	public class ImageResizer : IImageResizer
    {
		static ImageResizer ()
		{
		}

		public byte[] ResizeImage (byte[] imageData, float width, float height)
		{
			#if __IOS__
			return ResizeImageIOS (imageData, width, height);
			#endif
			#if __ANDROID__
			return ResizeImageAndroid ( imageData, width, height );
			#endif 
			#if WINDOWS_PHONE
			return ResizeImageWinPhone ( imageData, width, height );
			#endif
		}


		#if __IOS__
		public byte[] ResizeImageIOS (byte[] imageData, float width, float height)
		{
			UIImage originalImage = ImageFromByteArray (imageData);
			UIImageOrientation orientation = originalImage.Orientation;

			//create a 24bit RGB image
			using (CGBitmapContext context = new CGBitmapContext (IntPtr.Zero,
				                                 (int)width, (int)height, 8,
				                                 (int)(4 * width), CGColorSpace.CreateDeviceRGB (),
				                                 CGImageAlphaInfo.PremultipliedFirst)) {

				RectangleF imageRect = new RectangleF (0, 0, width, height);

				// draw the image
				context.DrawImage (imageRect, originalImage.CGImage);

				UIKit.UIImage resizedImage = UIKit.UIImage.FromImage (context.ToImage (), 0, orientation);

				// save the image as a jpeg
				return resizedImage.AsJPEG ().ToArray ();
			}
		}

		public UIKit.UIImage ImageFromByteArray (byte[] data)
		{
			if (data == null) {
				return null;
			}

			UIKit.UIImage image;
			try {
				image = new UIKit.UIImage (Foundation.NSData.FromArray (data));
			} catch (Exception e) {
				Console.WriteLine ("Image load failed: " + e.Message);
				return null;
			}
			return image;
		}
		#endif

		#if __ANDROID__
		
		public byte[] ResizeImageAndroid (byte[] imageData, float width, float height)
		{
			// Load the bitmap
			Bitmap originalImage = BitmapFactory.DecodeByteArray (imageData, 0, imageData.Length);
			Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);

			using (MemoryStream ms = new MemoryStream())
			{
				resizedImage.Compress (Bitmap.CompressFormat.Jpeg, 100, ms);
				return ms.ToArray ();
			}
		}

		#endif

		#if WINDOWS_PHONE
		
        public byte[] ResizeImageWinPhone (byte[] imageData, float width, float height)
        {
            byte[] resizedData;

            using (MemoryStream streamIn = new MemoryStream (imageData))
            {
                WriteableBitmap bitmap = PictureDecoder.DecodeJpeg (streamIn, (int)width, (int)height);

                using (MemoryStream streamOut = new MemoryStream ())
                {
                    bitmap.SaveJpeg(streamOut, (int)width, (int)height, 0, 100);
                    resizedData = streamOut.ToArray();
                }
            }
            return resizedData;
        }
        
        #endif

	}
}

