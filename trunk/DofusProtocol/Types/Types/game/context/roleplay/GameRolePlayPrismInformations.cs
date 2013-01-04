
// Generated on 01/04/2013 14:36:05
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class GameRolePlayPrismInformations : GameRolePlayActorInformations
    {
        public const short Id = 161;
        public override short TypeId
        {
            get { return Id; }
        }
        
        public Types.ActorAlignmentInformations alignInfos;
        
        public GameRolePlayPrismInformations()
        {
        }
        
        public GameRolePlayPrismInformations(int contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, Types.ActorAlignmentInformations alignInfos)
         : base(contextualId, look, disposition)
        {
            this.alignInfos = alignInfos;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            alignInfos.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            alignInfos = new Types.ActorAlignmentInformations();
            alignInfos.Deserialize(reader);
        }
        
    }
    
}