using User.Domain.Models.Base;

namespace User.Domain.Models
{
    public class ContactModel : BaseModel
    {
        public string? Name { get; set; }
        public char Gender { get; set; }
    }
}