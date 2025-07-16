# 📋 Clipboard Manager (WinForms Edition)

A lightweight clipboard history manager built with .NET and WinForms. It tracks copied text, allows pinning items, and preserves pinned entries between sessions.

Perfect for everyday use—and a quick demo of UI design, Windows API usage, and local persistence in C#.

---

## 🚀 Features

- 🧠 Clipboard history tracker (via `AddClipboardFormatListener`)
- 📌 Pin/unpin clipboard entries for long-term use
- 💾 Automatic save/load of pinned items using JSON
- ⚡ Built with rapid prototyping in mind (WinForms UI)
- 🔧 Easy to extend with search, tray icon, hotkeys, or rich formatting

---

## 🧰 Tech Stack

| Layer         | Tech                      |
|---------------|---------------------------|
| Framework     | .NET Framework / .NET Core |
| UI            | WinForms                  |
| Language      | C#                        |
| Persistence   | JSON via `System.Text.Json` |
| OS Support    | Windows only              |

