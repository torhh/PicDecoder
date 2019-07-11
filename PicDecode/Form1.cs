/// Microprose MTG .PIC and .TR Viewer

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Runtime.InteropServices;

namespace PicDecode
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32")]
        static extern bool AllocConsole();

        private Color[] palette = new Color[256];
        private byte[] SavedPic;

        private string fileName;

        public Form1()
        {
            InitializeComponent();

#if DEBUG
            AllocConsole();
#endif

            SetupGrayPalette();
            saveAsToolStripMenuItem.Enabled = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        Random rnd = new Random();

        private Color RandomColor()
        {
            return Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }

        private Color GrayColor(byte value)
        {
            return Color.FromArgb(value, value, value);
        }

        private void SetupGrayPalette ()
        {
            for(int i=0; i<256; i++)
            {
                palette[i] = GrayColor((byte)i);
            }
        }

        private void CheckForPalette(byte [] data)
        {
            PicDecoder decoder = new PicDecoder(data);
            if (decoder.hasPalette) {
                PalDecoder paldecoder = new PalDecoder(data, 6);
                palette = paldecoder.Palette;
            }

        }

        /// <summary>
        /// Load and decode .PIC image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadPicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ( openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SavedPic = File.ReadAllBytes(openFileDialog1.FileName);

                CheckForPalette(SavedPic);
                ShowPic(SavedPic);
                
                
                saveAsToolStripMenuItem.Enabled = true;

                this.Text = "PIC View - " + openFileDialog1.SafeFileName;
                fileName = openFileDialog1.SafeFileName;
            }
        }

        private void ShowPic ( byte [] data)
        {
            PicDecoder decoder = new PicDecoder(data);

            /// Output image as picture box

            Bitmap bitmap = new Bitmap(decoder.width, decoder.height);

            byte[] line = new byte[decoder.width];

            for (int y = 0; y < decoder.height; y++)
            {
                decoder.DecodeNextBytes(line);

                for (int x = 0; x < decoder.width; x++)
                {
                    byte value = line[x];
                    bitmap.SetPixel(x, y, palette[value]);
                }
            }
            pictureBox1.Image = bitmap;
        }

        /// <summary>
        /// Load and setup palette
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadPaletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ( openFileDialog2.ShowDialog() == DialogResult.OK )
            {
                byte[] data = File.ReadAllBytes(openFileDialog2.FileName);

                PalDecoder decoder = new PalDecoder(data);

                palette = decoder.Palette;

                if (SavedPic != null)
                {
                    ShowPic(SavedPic);
                }
            }
        }

        private void viewPaletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPalette paletteView = new FormPalette(palette);

            paletteView.ShowDialog();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
            {
            saveFileDialog1.FileName = Path.GetFileNameWithoutExtension(fileName);
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                pictureBox1.Image.Save(saveFileDialog1.FileName);
            }
        }
    }


}
