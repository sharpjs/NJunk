using System.Reflection;
using System.Security;

// Description
[assembly: AssemblyTitle       ("NJunk")]
[assembly: AssemblyDescription (".NET Junk Data Generator")]

// Security
//
// All code is transparent; the entire assembly will not do anything privileged or unsafe.
// http://msdn.microsoft.com/en-us/library/dd233102.aspx
//
[assembly: SecurityTransparent]
