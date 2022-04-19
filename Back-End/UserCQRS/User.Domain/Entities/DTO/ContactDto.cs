using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Entities.DTO
{
    public record ContactDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public ushort Age { get; init; }
        public char Gender { get; init; }
        public DateOnly Birth { get; init; }
    }
}
