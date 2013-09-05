#JsBuild
Javascript joiner &amp; minifier tool.

## Usage

1. Create config files.
2. Run application.

### Running application
jsbuild configFile1 configFile2 ... configFileN

### Example

*config.txt*
```
in:
C:\PathToSources

out:
lib\req.js

workers:
joiner

files:
js\dejavu.min.js
js\easeljs-0.6.1.min.js
js\easeljs.ext.min.js
js\colorpicker.min.js
js\base64.min.js
js\canvas2image.min.js
```

*running example in cmd*
```
jsbuild C:\PathToConfig\config.txt
```