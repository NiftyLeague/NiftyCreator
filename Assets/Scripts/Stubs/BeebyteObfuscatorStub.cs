// The Beebyte Obfuscator Unity Asset Store plugin is a commercial product and
// is not redistributed in this repository. The game source marks build-safe
// members with [SkipRename]; this no-op attribute keeps those sites compiling
// without the plugin. Re-add the real plugin to restore obfuscation.
using System;

namespace Beebyte.Obfuscator
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	public sealed class SkipRenameAttribute : Attribute
	{
	}
}
