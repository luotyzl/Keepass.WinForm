using System;
using System.Threading;
using System.Windows;

namespace Keepass.Wpf.Services
{
    public class ClipboardService
    {
        private Timer? _clipboardTimer;
        private string _lastClipboardContent = string.Empty;

        public void CopyToClipboardWithTimer(string content)
        {
            _clipboardTimer?.Dispose();
            
            _lastClipboardContent = content;
            Clipboard.SetText(content);
            
            _clipboardTimer = new Timer(ClearClipboard, null, 30000, Timeout.Infinite);
        }

        private void ClearClipboard(object? state)
        {
            try
            {
                if (Clipboard.ContainsText() && Clipboard.GetText() == _lastClipboardContent)
                {
                    Clipboard.Clear();
                }
            }
            catch
            {
                // Ignore clipboard access errors
            }
            finally
            {
                _clipboardTimer?.Dispose();
                _clipboardTimer = null;
            }
        }

        public void Dispose()
        {
            _clipboardTimer?.Dispose();
        }
    }
}