using System;
using System.Collections.Generic;
namespace Stump.DofusProtocol.D2oClasses
{
	
	[D2OClass("InfoMessages")]
	public class InfoMessage : Object
	{
		internal const String MODULE = "InfoMessages";
		public uint typeId;
		public uint messageId;
		public uint textId;
		
	}
}
