SharpFont [![NuGet Version](http://img.shields.io/nuget/v/SharpFont.svg)](https://www.nuget.org/packages/SharpFont) [![NuGet Downloads](http://img.shields.io/nuget/dt/SharpFont.svg)](https://www.nuget.org/packages/SharpFont) [![Gratipay Tips](https://img.shields.io/gratipay/Robmaister.svg)](https://gratipay.com/Robmaister)
=========
### Cross-platform FreeType bindings for .NET

SharpFont is a library that provides FreeType bindings for .NET. It's MIT
licensed to make sure licensing doesn't get in the way of using the library in
your own projects. Unlike [Tao.FreeType][1], SharpFont provides the full
public API and not just the basic methods needed to render simple text.
Everything from format-specific APIs to the caching subsystem are included.

SharpFont simplifies the FreeType API in a few ways:

 - The error codes that most FreeType methods return are converted to
   exceptions.
 - Since the return values are no longer error codes, methods with a single
   `out` parameter are returned instead.
 - Most methods are instance methods instead of static methods. This avoids
   unnecessary redundancy in method calls and creates an API with a .NET
   look-and-feel.

For example, a regular FreeType method looks like this:

```C
Face face;
int err = FT_New_Face(library, "./myfont.ttf", 0, &face);
```

The equivalent code in C# with SharpFont is:

```CSharp
Face face = new Face(library, "./myfont.ttf");
```

##Quick Start

###NuGet
SharpFont is available on [NuGet][2]. It can be installed by issuing the
following command in the package manager console:

```
PM> Install-Package SharpFont
```

###From Source
Clone the repository and compile the solution. Copy `SharpFont.dll` to your
project and include it as a reference. On Windows, you must include a compiled
copy of FreeType2 as `freetype6.dll` in the project's output directory. It is
possible to rename the file by changing the filename constant in
[FT.Internal.cs][3] and recompile. On Linux and OSX (and any other Mono
supported platform), you must also copy `SharpFont.dll.config` to the
project's output directory.

####Mono
With the removal of the `WIN64` configurations, the included `Makefile` is
effectively redundant. However, you can still build SharpFont by calling
`make` while in the root directory of this project.

####FreeType
A large number of FreeType builds for Windows are now available in the
[SharpFont.Dependencies][4] repository.

##Compiling FreeType on Windows

The copies of `freetype6.dll` that the Examples project uses by default are
chosen based on what works on my machine, and I will probably update it as
soon as a new version of FreeType is released. This means that it may not work
on older versions of Windows. If this is the case, you can either modify
the project file to point to another included version of freetype or you can
compile FreeType yourself from source.

**Note**: Any copy of `freetype6.dll` can work as a drop-in replacement,
including [this copy][5] from the GnuWin32 project. Older versions such as
that one may crash with a `EntryPointException` when using newer APIs. **If on
a 64-bit machine** not patching the source code will cause SharpFont to crash
in weird places.

Thanks to [this StackOverflow answer][6] for the directions:

 1. Download the latest [FreeType source code][7].
 2. Open `builds\win32\vc2010\freetype.sln` (or whatever version of Visual
 Studio you have) in Visual Studio.
 3. Change the compile configuration from Debug to Release.
 4. Open the project properties window through Project -> Properties.
 5. In the `General` selection, change the `Target Name` to `freetype6` and
 the `Configuration Type` to `Dynamic Library (.dll)`.
 6. **If compiling for 64-bit** 
   - Apply a patch to the source code (see [Known Issues](#known-issues)).
   - Open up Configuration Manager (the last option in  the dropdown menu when
   changing your compile configuration) and change `Platform` to `x64`.
 7. Open up `ftoption.h` (in the project's `Header Files` section) and add the
 following three lines near the `DLL export compilation` section:

```C
#define FT_EXPORT(x) __declspec(dllexport) x
#define FT_EXPORT_DEF(x) __declspec(dllexport) x
#define FT_BASE(x) __declspec(dllexport) x
```

Finally, complile the project (`F6` or Build -> Build Solution).
`freetype6.dll` will be output to `objs\win32\vc2010`. If this is a build that
isn't included in [Dependencies][4], consider forking and submitting a pull
request with your new build.

##Known Issues

The biggest currently known issue is the Windows 64-bit incompatibility. A
patch must be applied to FreeType before it will work properly with SharpFont.

TODO: make a .patch file

##License

As metioned earlier, SharpFont is licensed under the MIT License. The terms of
the MIT license are included in both the [LICENSE][8] file and below:

```
Copyright (c) 2012-2013 Robert Rouhani <robert.rouhani@gmail.com>

SharpFont based on Tao.FreeType, Copyright (c) 2003-2007 Tao Framework Team

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

The Windows binary of FreeType that is included in the Examples project and in
the NuGet package is redistributed under the FreeType License (FTL).

```
Portions of this software are copyright (c) 2015 The FreeType Project
(www.freetype.org). All rights reserved.
```


[1]: http://taoframework.svn.sourceforge.net/viewvc/taoframework/trunk/src/Tao.FreeType/
[2]: https://nuget.org/packages/SharpFont/
[3]: SharpFont/FT.Internal.cs
[4]: https://github.com/Robmaister/SharpFont.Dependencies
[5]: http://gnuwin32.sourceforge.net/packages/freetype.htm
[6]: http://stackoverflow.com/a/7387618/1122135
[7]: http://sourceforge.net/projects/freetype/files/freetype2/
[8]: LICENSE
