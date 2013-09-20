FixSpaz
=======

Tool for modifying SPAZ game saves

What does it do?
---------------
Looks for a decompiled SPAZ save game. If none is found, add DENY delete permission to save folder for spaz, and wait for input. (This is where you manually have to save your spaz game, and edit the decompiled file to your liking.)
When done, or if you start with an already edited decompiled file. The app will move the decompiled file to the 'default settings' location and wait for input. (This is where you start spaz, so the file becomes compiled.)
When done, it restores the files to their original locations, restores save directory permissions, and finally moves the modified game save to its proper location.

Parameters?
----------
If you give it a parameter, it will assume it to be the intended save game label, and
use that instead of prompting for one.
