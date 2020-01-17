# MkvDefaultTrackSwitcher
Windows application to change the default subitle and/or audio track in MKV video files, it can handle multiple files if the track names/numbers are the same.

### Demo
![Demo gif](https://raw.githubusercontent.com/MikeYaye/MkvDefaultTrackSwitcher/master/demo.gif?token=AIUWK3WCPN6MY772B3IGBGC6EIA7G)  
Demo where English audio and English(Signs and songs) subtitles by default are changed to Japanese audio and English(Dialogue) subtitles.

## Credits
- [ffprobe](https://ffmpeg.org/ffprobe.html) Reads the metadata from the MKv file (like, what audio/subitlte tracks are there)
- [mkvpropedit](https://mkvtoolnix.download/doc/mkvpropedit.html) Does the actual change to the default tracks.

This application is just a interface to change the default track, the actual changing of the MKV files is done by mkvpropedit. The above 2 executables are included alongside this application.

## Download 
See the [Releases](https://github.com/MikeYaye/MkvDefaultTrackSwitcher/releases) page to download the latest version, download the ZIP file, open it and execute `MkvDefaultTrackSwitcher.exe`. The 2 other executables need to be in the same folder as `MkvDefaultTrackSwitcher.exe`.

## Help
Feel free to make a [new issue](https://github.com/MikeYaye/MkvDefaultTrackSwitcher/issues/new) if you encounter any problems.
