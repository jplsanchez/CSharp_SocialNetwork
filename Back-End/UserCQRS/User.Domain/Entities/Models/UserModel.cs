using User.Domain.Models.Base;

namespace User.Domain.Models
{
    public class UserModel : BaseModel
    {
        public string? Name { get; set; }
        public char Gender { get; set; }
        public DateTime Birth { get; set; }
        public string? Email { get; set; }
        public IEnumerable<ContactModel> Following { get; set; } = new List<ContactModel>();
        public IEnumerable<ContactModel> Follower { get; set; } = new List<ContactModel>();
    }
}