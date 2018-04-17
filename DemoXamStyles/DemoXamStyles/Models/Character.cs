using System;
using System.Collections.Generic;
using System.Text;

namespace DemoXamStyles.Models
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public string AvatarSource { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
