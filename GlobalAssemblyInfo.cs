using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Luna")]
[assembly: AssemblyCopyright("Copyright ©  2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion("0.0.0.4")]
[assembly: AssemblyFileVersion("0.0.0.0")]
[assembly: CLSCompliant(true)]
[assembly: NeutralResourcesLanguage("fr-FR")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#elif QUALITY
[assembly: AssemblyConfiguration("Quality")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif