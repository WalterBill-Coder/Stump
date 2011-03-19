using System;
using System.Collections.Generic;
namespace Stump.DofusProtocol.D2oClasses
{
	
	[D2OClass("NpcMessages")]
	public class NpcMessage : Object
	{
		internal const String MODULE = "NpcMessages";
		public int id;
		public uint messageId;
		public String messageParams;
		
	}
}
