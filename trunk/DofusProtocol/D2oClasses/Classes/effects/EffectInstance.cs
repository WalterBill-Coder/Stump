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
	
	public class EffectInstance : Object
	{
		internal const String UNKNOWN_NAME = "???";
		internal const int UNDEFINED_CATEGORY = -2;
		internal const int UNDEFINED_SHOW = -1;
		internal const String UNDEFINED_DESCRIPTION = "undefined";
		public uint effectId;
		public int targetId;
		public int duration;
		public int random;
		public Boolean trigger;
		public uint zoneSize;
		public uint zoneShape;
		public int modificator;
		
	}
}
