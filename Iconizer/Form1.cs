using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TsudaKageyu;

namespace Iconizer
{
    public enum DropOperation
    {
        NormalExport = 0,
        DesktopINI = 1,
        BitmapExport = 2,
    }
    public partial class Form1 : Form
    {
        public List<BitmapData> bitmaps = new List<BitmapData>();

        static bool CheckIfImage(string path)
        {
            var ext = Path.GetExtension(path);
            return ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".bmp";
        }

        static bool CheckIfBinary(string path)
        {
            var ext = Path.GetExtension(path);
            return ext == ".exe" || ext == ".dll";
        }

        static bool CheckIfIcon(string path)
        {
            var ext = Path.GetExtension(path);
            return ext == ".ico";
        }

        bool CheckIfExist(string path)
        {
            return bitmaps.Any((x) => x.path == path);
        }

        static bool CheckIfCompatible(string[] paths)
        {
            return paths.Any((x) => CheckIfImage(x) || CheckIfBinary(x) || CheckIfIcon(x));
        }

        void AddImage(Image bitmap, string path)
        {
            var data = new BitmapData() { bitmap = bitmap, path = path, name = Path.GetFileNameWithoutExtension(path) };
            bitmaps.Add(data);
            _view.LargeImageList.Images.Add(data.GetHashCode().ToString(), data.bitmap);
            _view.Items.Add(new ListViewItem() { ImageKey = data.GetHashCode().ToString(), Text = data.name, ToolTipText = data.path, Tag = data, Selected = true });
        }

        void AddBinary(string path)
        {
            var icons = new IconExtractor(path);
            var iter = 0;
            foreach (var icon in icons.GetAllIcons())
            {
                var iconss = IconUtil.Split(icon);
                var data = new BitmapData() { bitmap = IconUtil.ToBitmap(Utility.GetNicestIcon(iconss)), path = path, name = Path.GetFileNameWithoutExtension(path), iteration = iter++ };
                bitmaps.Add(data);
                _view.LargeImageList.Images.Add(data.GetHashCode().ToString(), data.bitmap);
                _view.Items.Add(new ListViewItem() { ImageKey = data.GetHashCode().ToString(), Text = data.name, ToolTipText = data.path, Tag = data, Selected = true });

                foreach (var ic in iconss)
                {
                    ic.Dispose();
                }
                icon.Dispose();
            }
        }

        void AddIcon(string path)
        {
            Icon c = new Icon(path);
            var cc = (IconUtil.Split(c));
            var data = new BitmapData() { bitmap = IconUtil.ToBitmap(Utility.GetNicestIcon(cc)), path = path, name = Path.GetFileNameWithoutExtension(path) };
            bitmaps.Add(data);
            _view.LargeImageList.Images.Add(data.GetHashCode().ToString(), data.bitmap);
            _view.Items.Add(new ListViewItem() { ImageKey = data.GetHashCode().ToString(), Text = data.name, ToolTipText = data.path, Tag = data, Selected = true });

            foreach (var ic in cc)
            {
                ic.Dispose();
            }
            c.Dispose();
        }

        void RemoveSelected()
        {
            _view.BeginUpdate();
            for (int i = _view.SelectedItems.Count - 1; i >= 0; i--)
            {
                var data = (BitmapData)_view.SelectedItems[i].Tag;
                bitmaps.Remove(data);
                _view.SelectedItems[i].Remove();
                _view.LargeImageList.Images.RemoveByKey(data.GetHashCode().ToString());
                data.bitmap.Dispose();
            }
            _view.EndUpdate();
        }

        void RemoveAll()
        {
            _view.BeginUpdate();
            for (int i = _view.Items.Count - 1; i >= 0; i--)
            {
                var data = (BitmapData)_view.Items[i].Tag;
                bitmaps.Remove(data);
                _view.Items[i].Remove();
                _view.LargeImageList.Images.RemoveByKey(data.GetHashCode().ToString());
                data.bitmap.Dispose();
            }
            _view.EndUpdate();
        }


        Bitmap[] GetImageSeries(Image master, OptionsData options)
        {
            var v = new Bitmap[options.sizes.Count];
            for (int i = 0; i < options.sizes.Count; i++)
            {
                if (!options.keepCrisp || (master.Width >= options.sizes[i].Width && master.Height >= options.sizes[i].Height))
                    v[i] = Utility.ResizeImage(master, options.sizes[i], options.keepAspect);
            }
            return v;
        }

        void SaveQuick()
        {
            var options = OptionsData.FromCheckBoxes(_options);
            foreach (var data in bitmaps)
            {
                var images = GetImageSeries(data.bitmap, options);
                var file = new FileStream(Utility.NextAvailableFilename(data.GetIcoPath()), FileMode.CreateNew);
                Utility.SavePngsAsIcon(images, file);

                file.Dispose();
                foreach (var img in images)
                {
                    if (img != null)
                        img.Dispose();
                }
            }
        }

        StringCollection SendToCopyDump(DropOperation op)
        {
            var options = OptionsData.FromCheckBoxes(_options);
            StringCollection paths = new StringCollection();
            if (op == DropOperation.BitmapExport)
                foreach (ListViewItem item in _view.SelectedItems)
                {
                    var data = (BitmapData)item.Tag;
                    var path = data.GetPngPath(Path.GetTempPath());
                    if (File.Exists(path)) File.Delete(path);
                    var file = new FileStream(path, FileMode.Create);
                    paths.Add(path);
                    data.bitmap.Save(file, ImageFormat.Png);
                    file.Close();
                }
            else
                foreach (ListViewItem item in _view.SelectedItems)
                {
                    var data = (BitmapData)item.Tag;
                    var images = GetImageSeries(data.bitmap, options);
                    var path = data.GetIcoPath(Path.GetTempPath());
                    if (File.Exists(path)) File.Delete(path);
                    var file = new FileStream(path, FileMode.Create);
                    paths.Add(path);
                    Utility.SavePngsAsIcon(images, file);

                    file.Dispose();
                    foreach (var img in images)
                    {
                        if (img != null)
                            img.Dispose();
                    }
                }

            if (op == DropOperation.DesktopINI && _view.SelectedItems.Count == 1)
            {
                var path = Path.Combine(Path.GetTempPath(), "desktop.ini");
                if (File.Exists(path)) File.Delete(path);
                var ico = (BitmapData)_view.SelectedItems[0].Tag;
                var icon = Path.GetFileName(ico.GetIcoPath());
                File.WriteAllText(path, string.Format("[.ShellClassInfo]\r\nIconResource={0}", icon));
                new FileInfo(ico.GetIcoPath(Path.GetTempPath())).Attributes = new FileInfo(path).Attributes = FileAttributes.Hidden | FileAttributes.System;

                paths.Add(path);
            }

            return paths;
        }

        void SaveFull(string folder)
        {
            var options = OptionsData.FromCheckBoxes(_options);
            foreach (var data in bitmaps)
            {
                var images = GetImageSeries(data.bitmap, options);
                var file = new FileStream(Utility.NextAvailableFilename(data.GetIcoPath(folder)), FileMode.CreateNew);
                Utility.SavePngsAsIcon(images, file);

                file.Dispose();
                foreach (var img in images)
                {
                    if (img != null)
                        img.Dispose();
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            _view.LargeImageList = new ImageList() { ImageSize = new Size(128, 128), ColorDepth = ColorDepth.Depth32Bit };
            for (int i = 0; i < _options.Items.Count; i++)
            {
                _options.SetItemChecked(i, true);
            }
        }

        private void _view_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                _view.BeginUpdate();
                foreach (var path in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    if (CheckIfExist(path)) { }
                    else if (CheckIfImage(path))
                        AddImage(Image.FromFile(path), path);
                    else if (CheckIfBinary(path))
                        AddBinary(path);
                    else if (CheckIfIcon(path))
                        AddIcon(path);
                }
                _view.EndUpdate();
            }

        }

        private void _view_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && CheckIfCompatible((string[])e.Data.GetData(DataFormats.FileDrop)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void _clear_Click(object sender, EventArgs e)
        {
            if (_view.SelectedItems.Count == 0)
                RemoveAll();
            else
                RemoveSelected();
        }

        private void _saveQuick_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            Application.DoEvents();
            SaveQuick();
            UseWaitCursor = false;
            MessageBox.Show("Icon(s) saved successfully!", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void _saveFull_Click(object sender, EventArgs e)
        {
            if (_saveDiag.ShowDialog() == DialogResult.OK)
            {
                UseWaitCursor = true;
                Application.DoEvents();
                SaveFull(_saveDiag.SelectedPath);
                UseWaitCursor = false;
                MessageBox.Show("Icon(s) saved successfully!", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void _add_Click(object sender, EventArgs e)
        {
            if (_openDiag.ShowDialog() == DialogResult.OK)
            {
                foreach (var path in _openDiag.FileNames)
                {
                    if (CheckIfImage(path))
                        AddImage(Image.FromFile(path), path);
                    if (CheckIfBinary(path))
                        AddBinary(path);
                    else if (CheckIfIcon(path))
                        AddIcon(path);
                }
            }
        }

        private void _view_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                RemoveSelected();
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                _view.BeginUpdate();
                foreach (ListViewItem item in _view.Items)
                {
                    item.Selected = true;
                }
                _view.EndUpdate();
            }
        }

        private void _view_ItemDrag(object sender, ItemDragEventArgs e)
        {
            bool withPNG = ModifierKeys == Keys.Shift;
            bool withINI = !withPNG && ModifierKeys == Keys.Control;

            UseWaitCursor = true;
            if (withINI)
                this.Text = "Iconizer - Drop to Change Folder icon";
            else
                this.Text = withPNG ? "Iconizer - Release SHIFT and Drop to Export PNG" : "Iconizer - Drop to Export ICO";

            Application.DoEvents();
            var lists = SendToCopyDump((triggerInvalidating = withINI) ? DropOperation.DesktopINI : (withPNG ? DropOperation.BitmapExport : DropOperation.NormalExport));
            var data = new DataObject();
            data.SetFileDropList(lists);
            data.SetData("Preferred DropEffect", DragDropEffects.Move); // Cut

            DoDragDrop(data, DragDropEffects.Copy);
            if (withINI)
                this.Text = "Iconizer - Click here to Invalidate the explorer cache";
            else
                this.Text = "Iconizer";
            UseWaitCursor = false;
        }


        // This trick is used for invalidating folder caches..
        bool triggerInvalidating = false;

        private void Form1_Activated(object sender, EventArgs e)
        {

            if (triggerInvalidating)
            {
                triggerInvalidating = false;
                Utility.InvalidateExplorerCache();
                this.Text = "Iconizer";
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Iconizer is made with <3 by Wello Soft\n\n" +
                "http://github.com/willnode/Iconizer" + "\n" +
                "http://wellosoft.wordpress.com", "Iconizer " + AssemblyName.GetAssemblyName(Assembly.GetExecutingAssembly().Location).Version.ToSt‌​ring(), MessageBoxButtons.OK, MessageBoxIcon.Information
                );
        }
    }
}
