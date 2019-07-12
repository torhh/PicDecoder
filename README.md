# PicDecoder

Microprose classic games .PIC file format decoder.

This is a fork from [ogamespec](https://github.com/ogamespec/PicDecoder). Most of this is to his credit.

## TODO
* ~Create a save function~
* ~Read MVGA embedded palette if present~
* Support all embedded palettes (CGA, EGA) and switch between them
* Read .PIC files that are stored within a .CAT file

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

## .CAT files
Most images are stored in various .CAT files:
* ID.CAT (only used for copy protection during startup)
* GAME.CAT (main stuff)
* ACES.CAT (faces of the different aces in the game)

### Extract data from .CAT files

The two first bytes of the file tells the number of entries in the file. Then follows headers for each entry.

```c
struct entry {
  char filename[12];            /* DOS 8.3 format */
  unsigned int unknown;
  unsigned int size;            /* data size in bytes */
  unsigned int offset;          /* offset from start of file */
}
```

After this it is straight forward. Create the file **filename**, fseek to **offset**, read **size** bytes and write to disk. I'm planning to make PicDecoder able to read .CAT files as well at some point.
