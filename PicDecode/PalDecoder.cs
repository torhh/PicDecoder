/// Microprose MTG .TR palette decoder

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Text.RegularExpressions;

namespace PicDecode
{
    class PalDecoder
    {
        public Color[] Palette = new Color[256];

        private void ResetPalette()
        {
            for(int i=0; i<256; i++)
            {
                Palette[i] = Color.Black;
            }
        }

        public PalDecoder (byte [] data)
        {
            int index = 0;
            byte rValue, gValue, bValue;

            ResetPalette();

            if (data.Length != 768) {
                throw new Exception("Invalid palette entry!");
            }

            for (index = 0; index < 256; index++) {

                rValue = data[(index * 3)] <<= 2;
                gValue = data[(index * 3) + 1] <<= 2;
                bValue = data[(index * 3) + 2] <<= 2;

                Palette[index] = Color.FromArgb(rValue, gValue, bValue);
            }

        }
        


    }

}
