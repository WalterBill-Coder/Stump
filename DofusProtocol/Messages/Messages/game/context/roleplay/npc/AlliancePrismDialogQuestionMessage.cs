

// Generated on 01/04/2015 11:54:18
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class AlliancePrismDialogQuestionMessage : Message
    {
        public const uint Id = 6448;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public AlliancePrismDialogQuestionMessage()
        {
        }
        
        
        public override void Serialize(IDataWriter writer)
        {
        }
        
        public override void Deserialize(IDataReader reader)
        {
        }
        
    }
    
}