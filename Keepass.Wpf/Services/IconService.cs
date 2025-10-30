using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using KeePassLib;

namespace Keepass.Wpf.Services
{
    public class IconService
    {
        private readonly Dictionary<string, ImageSource> _iconCache = new();

        public ImageSource GetEntryIcon(PwEntry entry, PwDatabase? database)
        {
            try
            {
                if (entry.CustomIconUuid != PwUuid.Zero && database != null)
                {
                    var customIcon = database.CustomIcons.FirstOrDefault(ci => ci.Uuid.Equals(entry.CustomIconUuid));
                    if (customIcon?.ImageDataPng != null)
                    {
                        var iconKey = entry.CustomIconUuid.ToHexString();
                        
                        if (!_iconCache.ContainsKey(iconKey))
                        {
                            using var stream = new MemoryStream(customIcon.ImageDataPng);
                            var bitmapImage = new BitmapImage();
                            bitmapImage.BeginInit();
                            bitmapImage.StreamSource = stream;
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapImage.DecodePixelWidth = 16;
                            bitmapImage.DecodePixelHeight = 16;
                            bitmapImage.EndInit();
                            bitmapImage.Freeze();
                            
                            _iconCache[iconKey] = bitmapImage;
                        }
                        return _iconCache[iconKey];
                    }
                }
                
                var standardIconKey = $"std_{(int)entry.IconId}";
                
                if (!_iconCache.ContainsKey(standardIconKey))
                {
                    _iconCache[standardIconKey] = CreateStandardIcon(entry.IconId);
                }
                return _iconCache[standardIconKey];
            }
            catch
            {
                return CreateDefaultIcon();
            }
        }

        private ImageSource CreateStandardIcon(PwIcon iconId)
        {
            using var bitmap = new Bitmap(16, 16);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(System.Drawing.Color.Transparent);
                
                var iconColor = GetStandardIconColor(iconId);
                using var brush = new SolidBrush(iconColor);
                g.FillEllipse(brush, 2, 2, 12, 12);
            }
            
            return ConvertBitmapToImageSource(bitmap);
        }

        private System.Drawing.Color GetStandardIconColor(PwIcon iconId)
        {
            return iconId switch
            {
                PwIcon.Key => System.Drawing.Color.Gray,
                PwIcon.World => System.Drawing.Color.Blue,
                PwIcon.Warning => System.Drawing.Color.Orange,
                PwIcon.NetworkServer => System.Drawing.Color.Green,
                PwIcon.MarkedDirectory => System.Drawing.Color.Yellow,
                PwIcon.UserCommunication => System.Drawing.Color.Purple,
                PwIcon.Parts => System.Drawing.Color.Brown,
                PwIcon.Notepad => System.Drawing.Color.LightBlue,
                PwIcon.WorldSocket => System.Drawing.Color.DarkBlue,
                PwIcon.Identity => System.Drawing.Color.Pink,
                _ => System.Drawing.Color.Gray
            };
        }

        private ImageSource CreateDefaultIcon()
        {
            using var bitmap = new Bitmap(16, 16);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(System.Drawing.Color.Transparent);
                
                using var brush = new SolidBrush(System.Drawing.Color.Gray);
                g.FillEllipse(brush, 2, 2, 12, 12);
            }
            
            return ConvertBitmapToImageSource(bitmap);
        }

        private static ImageSource ConvertBitmapToImageSource(Bitmap bitmap)
        {
            using var memory = new MemoryStream();
            bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
            memory.Position = 0;

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memory;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            bitmapImage.Freeze();

            return bitmapImage;
        }
    }
}