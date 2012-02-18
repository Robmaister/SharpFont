#region MIT License
/*Copyright (c) 2012 Robert Rouhani <robert.rouhani@gmail.com>

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
SOFTWARE.*/
#endregion

using System;

namespace SharpFont.Cache
{
	/// <summary>
	/// This object corresponds to one instance of the cache-subsystem. It is
	/// used to cache one or more <see cref="Face"/> objects, along with
	/// corresponding <see cref="Size"/> objects.
	/// 
	/// The manager intentionally limits the total number of opened
	/// <see cref="Face"/> and <see cref="Size"/> objects to control memory
	/// usage. See the ‘max_faces’ and ‘max_sizes’ parameters of
	/// <see cref="FTC.ManagerNew"/>.
	/// 
	/// The manager is also used to cache ‘nodes’ of various types while
	/// limiting their total memory usage.
	/// 
	/// All limitations are enforced by keeping lists of managed objects in
	/// most-recently-used order, and flushing old nodes to make room for new
	/// ones.
	/// </summary>
	public class Manager
	{
		internal IntPtr reference;

		internal Manager(IntPtr reference)
		{
			this.reference = reference;
		}
	}
}
