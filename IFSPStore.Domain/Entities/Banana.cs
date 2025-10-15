

using IFSPStore.Domain.Base;

namespace IFSPStore.Domain.Entities
{
    public class Banana : BaseEntity<int>
    {
        public Banana() : base(0)
        {
        }
        public Banana(int id, string name) : base(id)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
