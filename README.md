# SharpFont

## Cross-platform FreeType bindings for .NET

SharpFont is a library that provides FreeType bindings for .NET. It's MIT licensed to make sure licensing doesn't get
in the way of using the library in your own projects. Unlike
[Tao.FreeType](http://taoframework.svn.sourceforge.net/viewvc/taoframework/trunk/src/Tao.FreeType/), SharpFont provides
the full public API and not just the basic methods needed to render simple text. Everything from format-specific APIs
to the caching subsystem are included.

SharpFont simplifies the FreeType API in a few ways:

 - The error codes that most FreeType methods return are converted to exceptions.
 - Since the return values are no longer error codes, an `out` parameter can be returned instead.
 - Both static methods and instance methods are available. This provides both an easy way to port C/C++ code directly
but still offer an API with a .NET look-and-feel.

For example, a regular FreeType method like this:

    Face face;
    int err = FT_New_Face(library, "./myfont.ttf", 0, &face);
    
With SharpFont, it looks like this:

    Face face = library.NewFace("./myfont.ttf", 0);