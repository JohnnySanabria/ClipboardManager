using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipboardManager
{
    public class ClipboardItem
    {
        public string Text { get; set; } = string.Empty;
        public bool IsPinned { get; set; } = false;
    }
}
