using System;
using System.Collections.Generic;
namespace Stump.DofusProtocol.D2oClasses
{
	
	[D2OClass("Interactives")]
	public class Interactive : Object
	{
		internal const String MODULE = "Interactives";
		public int id;
		public uint nameId;
		public int actionId;
		
	}
}
