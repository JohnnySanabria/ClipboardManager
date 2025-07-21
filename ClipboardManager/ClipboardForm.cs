using System.Data;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace ClipboardManager
{
    public partial class ClipboardForm : Form
    {

        [DllImport("user32.dll")]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);
        [DllImport("user32.dll")]
        public static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        private readonly List<ClipboardItem> history = [];
        private const string PERSISTENCE_PATH = "clipboard_history.json";

        public ClipboardForm()
        {
            InitializeComponent();
            AddClipboardFormatListener(this.Handle);
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_CLIPBOARDUPDATE = 0x031D;
            if (m.Msg == WM_CLIPBOARDUPDATE)
            {
                string clipText = Clipboard.GetText();
                if (!string.IsNullOrWhiteSpace(clipText))
                {
                    if (!history.Exists(i => i.Text == clipText))
                    {
                        history.Add(new ClipboardItem { Text = clipText });
                        RefreshList();
                    }
                }
            }
            base.WndProc(ref m);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadPinnedHistory();
            RefreshList();
        }

        private void RefreshList()
        {
            listBox1.Items.Clear();
            foreach (var i in history)
            {
                listBox1.Items.Add(i.IsPinned ? $"📌 {i.Text}" : i.Text);
            }
        }


        private void SavePinnedHistory()
        {
            var pinnedItems = history.Where(h => h.IsPinned).ToList();
            string json = JsonSerializer.Serialize(pinnedItems);
            File.WriteAllText(PERSISTENCE_PATH, json);
        }

        private void LoadPinnedHistory()
        {
            if (File.Exists(PERSISTENCE_PATH))
            {
                string json = File.ReadAllText(PERSISTENCE_PATH);
                var pinnedItems = JsonSerializer.Deserialize<List<ClipboardItem>>(json);
                if (pinnedItems != null)
                {
                    history.AddRange(pinnedItems);
                    RefreshList();
                }
            }
        }


        private void btnPin_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < history.Count)
            {
                history[selectedIndex].IsPinned = !history[selectedIndex].IsPinned;
                RefreshList();
            }

        }

        private void ClipboardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SavePinnedHistory();
            RemoveClipboardFormatListener(this.Handle);
        }
    }
}
