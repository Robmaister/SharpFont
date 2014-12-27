SharpFont Dependencies
======================

SharpFont provides builds for all the native libraries it depends on for 
platforms that don't ship with the libraries and that don't have packages for
them in some kind of package manager.

## FreeType

For FreeType, we will support and store a large number of versions in the
following structure:

 - Dependencies
    - freetype2
       - FREETYPE MAJOR.MINOR.PATCH
          - COMPILER (e.g. msvc12, mingw)
             - deps
                - DEP1.dll
                - DEP2.dll
                - DEP3.dll
             - DEP1
                - freetype6.dll
             - DEP2
                - freetype6.dll
             - DEP1-DEP3
                - freetype6.dll
             - freetype6.dll

In this example I only use .dll's, but any other platform is fine, so long as
it is different than the build distributed with the OS/package manager
and doesn't include anything proprietary. **Feel free to submit a pull request
if you can compile a different version of FreeType than what is currently
available**. I will eventually try and get this worked into a testing
framework.

## Managed Dependencies

Any dependency that's managed may sit as a single .dll in this directory.

## Other Dependencies

In the case that there are native dependencies outside of FreeType, they
should be placed in their own folder and include subfolders for each compiled
platform, similar to FreeType. If versions are also included it should
probably become it's own wrapper library. :stuck_out_tongue:

## Licensing

Libraries that require a copy of their license be distributed with binaries
should all be included in [LICENSE.md][1].


[1]: https://github.com/Robmaister/SharpFont/blob/master/Dependencies/LICENSE.md
