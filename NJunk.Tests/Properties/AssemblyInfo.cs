﻿using System.Reflection;
using System.Security;

// Description
[assembly: AssemblyTitle       ("NJunk.Tests")]
[assembly: AssemblyDescription ("Tests for .NET Junk Data Generator")]

// Security
//
// All code is transparent; the entire assembly will not do anything privileged or unsafe.
// http://msdn.microsoft.com/en-us/library/dd233102.aspx
//
[assembly: SecurityTransparent]