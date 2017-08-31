﻿/*
 * The MIT License (MIT)
 * 
 * Copyright (c) 2013-2017  Denis Kuzmin < entry.reg@gmail.com > :: github.com/3F
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
*/

using System.Diagnostics;

namespace net.r_eg.MvsSln.Core
{
    [DebuggerDisplay("{DbgDisplay}")]
    public class Section: ISection
    {
        /// <summary>
        /// Contains handler that processed this section.
        /// </summary>
        public object Handler
        {
            get;
            protected set;
        }

        /// <summary>
        /// Known line number to this section.
        /// </summary>
        public long Line
        {
            get;
            protected set;
        }

        /// <summary>
        /// Raw data from stream.
        /// </summary>
        public RawText Raw
        {
            get;
            protected set;
        }

        /// <summary>
        /// To ignore this from other sections.
        /// </summary>
        public bool Ignore
        {
            get;
            set;
        }

        /// <summary>
        /// User's mixed object for anything.
        /// </summary>
        public object User
        {
            get;
            set;
        }

        private string DbgDisplay
        {
            get => $"{(Ignore ? "x: " : "")}[{Handler?.GetType().Name}] #{Line}:'{Raw}'";
        }

        public Section(object h, RawText raw, long line = -1)
        {
            Handler = h;
            Raw     = raw;
            Line    = line;
        }
    }
}