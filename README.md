# PicDecoder

Microprose classic games .PIC file format decoder.

This is a fork from [ogamespec](https://github.com/ogamespec/PicDecoder). Most of this is to his credit.

## Knights of the Sky
The reason I forked this was to b able to open.PIC files from
Knights of the Sky. They use the same LZW compression and RLE encoding,
but KotS files have embedded palettes which didn't work with the original code.

I have also added a save function so that the viewed files can be saved
as a .PNG file.

## Usage

First, load a .PIC image. If an embedded palette is included this will be loaded as well

![PIC View](/images/picview.jpg)

You can also load an appropriate .PAL palette:

![Palette View](/images/palview.jpg)

You can now save the image to disk using File->Save as...
