using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Iconizer
{
    /// <summary>
    /// Each options data
    /// </summary>
    public class OptionsData
    {
        public List<Size> sizes = new List<Size>();
        public bool keepAspect;
        public bool keepCrisp;

        public static OptionsData FromCheckBoxes(CheckedListBox box)
        {
            var data = new OptionsData();
            if (box.GetItemChecked(0))
                data.sizes.Add(new Size(8, 8));
            if (box.GetItemChecked(1))
                data.sizes.Add(new Size(16, 16));
            if (box.GetItemChecked(2))
                data.sizes.Add(new Size(32, 32));
            if (box.GetItemChecked(3))
                data.sizes.Add(new Size(48, 48));
            if (box.GetItemChecked(4))
                data.sizes.Add(new Size(64, 64));
            if (box.GetItemChecked(5))
                data.sizes.Add(new Size(128, 128));
            if (box.GetItemChecked(6))
                data.sizes.Add(new Size(256, 256));
            if (box.GetItemChecked(7))
                data.keepCrisp = true;
            if (box.GetItemChecked(8))
                data.keepAspect = true;
            return data;
        }
    }

    /// <summary>
    /// Stored individual bitmap data
    /// </summary>
    public class BitmapData
    {
        public Image bitmap;
        public string path;
        public string name;

        public int iteration;

        public override int GetHashCode()
        {
            return path.GetHashCode() ^ iteration;
        }

        public override bool Equals(object obj)
        {
            return obj is BitmapData && path.GetHashCode() == ((BitmapData)obj).GetHashCode();
        }

        public string GetIcoPath()
        {
            if (iteration == 0)
                return Path.ChangeExtension(path, ".ico");
            else
                return Path.ChangeExtension(path, null) + iteration.ToString() + ".ico";
        }

        public string GetIcoPath(string directory)
        {
            return Path.Combine(directory, Path.GetFileName(GetIcoPath()));
        }

        public string GetPngPath()
        {
            if (iteration == 0)
                return Path.ChangeExtension(path, ".png");
            else
                return Path.ChangeExtension(path, null) + iteration.ToString() + ".png";
        }

        public string GetPngPath(string directory)
        {
            return Path.Combine(directory, Path.GetFileName(GetPngPath()));
        }

    }


}
