// /*************************************************************************
//  *
//  *  Copyright (C) 2010 - 2011 Stump Team
//  *
//  *  This program is free software: you can redistribute it and/or modify
//  *  it under the terms of the GNU General Public License as published by
//  *  the Free Software Foundation, either version 3 of the License, or
//  *  (at your option) any later version.
//  *
//  *  This program is distributed in the hope that it will be useful,
//  *  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  *  GNU General Public License for more details.
//  *
//  *  You should have received a copy of the GNU General Public License
//  *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//  *
//  *************************************************************************/
using System;
using System.Collections.Generic;
namespace Stump.DofusProtocol.D2oClasses
{
	
	[AttributeAssociatedFile("Spells")]
	public class Spell : Object
	{
		internal const String MODULE = "Spells";
		internal Array _indexedParam;
		internal Array _indexedCriticalParam;
		public int id;
		public uint nameId;
		public uint descriptionId;
		public uint typeId;
		public String scriptParams;
		public String scriptParamsCritical;
		public int scriptId;
		public int scriptIdCritical;
		public uint iconId;
		public List<uint> spellLevels;
		public Boolean useParamCache = true;
		internal Array _spellLevels;
		
	}
}
