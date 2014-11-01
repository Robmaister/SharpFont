SharpFont [![NuGet Version](http://img.shields.io/nuget/v/SharpNav.svg)](https://www.nuget.org/packages/SharpNav) [![NuGet Downloads](http://img.shields.io/nuget/dt/SharpNav.svg)](https://www.nuget.org/packages/SharpNav) [![Gratipay Tips](https://img.shields.io/gratipay/Robmaister.svg)](https://gratipay.com/Robmaister)
=========
### Cross-platform FreeType bindings for .NET

SharpFont is a library that provides FreeType bindings for .NET. It's MIT licensed to make sure licensing doesn't get
in the way of using the library in your own projects. Unlike
[Tao.FreeType](http://taoframework.svn.sourceforge.net/viewvc/taoframework/trunk/src/Tao.FreeType/), SharpFont provides
the full public API and not just the basic methods needed to render simple text. Everything from format-specific APIs
to the caching subsystem are included.

SharpFont simplifies the FreeType API in a few ways:

 - The error codes that most FreeType methods return are converted to exceptions.
 - Since the return values are no longer error codes, methods with a single `out` parameter are returned instead.
 - Most methods are instance methods instead of static methods. This avoids unnecessary redundancy in method calls and
creates an API with a .NET look-and-feel.

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
SharpFont is available on [NuGet](https://nuget.org/packages/SharpFont/). It can be installed by issuing the following
command in the package manager console:

```
PM> Install-Package SharpFont
```

###From Source
Clone the repository and compile the solution. Copy `SharpFont.dll` to your project and include it as a reference. On
Windows, you must include a compiled copy of FreeType2 as `freetype6.dll` in the project's output directory. It is
possible to rename the file by changing the filename constant in
[FT.Internal.cs](SharpFont/blob/master/SharpFont/FT.Internal.cs) and recompile. On Linux and OSX (and any other
Mono-supported platform), you must also copy `SharpFont.dll.config` to the project's output directory.

####Mono
If compiling on the command line, `xbuild` will choose the Debug WIN64 configuration by default, which will cause
issues. Instead, run `make` to compile with the proper configuration.

####FreeType
A 32-bit copy of `freetype6.dll` is included in the [Examples](SharpFont/blob/master/Examples) project.

Currently, Windows 64-bit systems require you to either compile SharpFont under the WIN64 configurations and include a
64-bit copy of `freetype6.dll` or to compile your project as an x86 project (instead of Any CPU). I describe this issue
in further detail in the Known Issues section.

##Compiling FreeType on Windows

The included copy of `freetype6.dll` is something I build on my machine when a new version of FreeType is released.
This means that it may not work on older versions of Windows. If this is the case, you can compile FreeType yourself
from source.

**Note**: Any copy of `freetype6.dll` can work as a drop-in replacement, including
[this copy](http://gnuwin32.sourceforge.net/packages/freetype.htm) from the GnuWin32 project. Older versions such as
that one may crash with a `EntryPointException` when using newer APIs.

Thanks to [this StackOverflow answer](http://stackoverflow.com/a/7387618/1122135) for the directions:

 1. Download the latest [FreeType source code](http://sourceforge.net/projects/freetype/files/freetype2/).
 2. Open `builds\win32\vc2010\freetype.sln` (or whatever version of Visual Studio you have) in Visual Studio.
 3. Change the compile configuration from Debug to Release.
 4. Open the project properties window through Project -> Properties.
 5. In the `General` selection, change the `Target Name` to `freetype6` and the `Configuration Type` to `Dynamic
Library (.dll)`.
 6. Open up `ftoption.h` (in the project's `Header Files` section) and add the following three lines near the `DLL
export compilation` section:

```C
#define FT_EXPORT(x) __declspec(dllexport) x
#define FT_EXPORT_DEF(x) __declspec(dllexport) x
#define FT_BASE(x) __declspec(dllexport) x
```

Finally, complile the project (`F6` or Build -> Build Solution). `freetype6.dll` will be output to `objs\win32\vc2010`.

##Known Issues

The biggest currently known issue is the Windows 64-bit incompatibility. This is a three part issue:

 - Windows uses the LLP64 data model while most other operating systems (including Linux and OSX) use the LP64 data
model. This means that on Windows, only `long long`s and pointers are 64 bits long and everything else remains 32 bits
long. On LP64 systems, `long`s are also 64 bits long. This creates a discrepancy between the length of the `long` type
on different operating systems.
 - FreeType makes heavy use of the `long` type, specifically their `FT_Long`, `FT_ULong`, `FT_Fixed`, `FT_Pos`, and
`FT_F26Dot6` typedefs. This makes the size of FreeType structs different on Windows 64-bit systems and other 64-bit
systems.
 - The C# `long` type is always 64 bits long, which doesn't always match the length of the native `long` type. For all
LP64 systems, the `IntPtr` type works because it's length matches the length of a pointer on that system. For LLP64
systems, this doesn't work because `long` is 32 bits long while a pointer is 64 bits long.

The simplest solution is to compile your project as x86 because all systems will default on the 32 bit FreeType binary.
However, this can be restrictive to some applications. The best solution is to compile a version of FreeType with
64-bit `long`s, which I will look into and test in the near future.

##License

As metioned earlier, SharpFont is licensed under the MIT License. The terms of the MIT license are included in both the
[LICENSE](SharpFont/blob/master/LICENSE) file and below:

```
Copyright (c) 2012-2013 Robert Rouhani <robert.rouhani@gmail.com>

SharpFont based on Tao.FreeType, Copyright (c) 2003-2007 Tao Framework Team

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

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

The Windows binary of FreeType that is included in the Examples project and in the NuGet package is redistributed under
the FreeType License (FTL).

```
Portions of this software are copyright (c) 2013 The FreeType Project
(www.freetype.org). All rights reserved.
```
