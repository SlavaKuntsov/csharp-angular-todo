using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStore.Core.Models
{
    public class Group
    {
        public Guid Id { get; }
        public string? Title { get; }
        public Guid UserId { get; }
        public User? User { get; }
        public List<Item> Items { get; } = new List<Item>();

    }
}
