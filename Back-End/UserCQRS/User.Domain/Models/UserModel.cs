using User.Domain.Models.Base;

namespace User.Domain.Models
{
    public class UserModel : BaseModel
    {
        public string? Name { get; set; }
        public ushort Age { get; set; }
        public char Gender { get; set; }
    }
}