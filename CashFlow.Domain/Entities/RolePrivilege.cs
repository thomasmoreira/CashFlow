namespace CashFlow.Domain.Entities
{
    public class RolePrivilege
    {
        public int Id { get; set; }
        public Role Role { get; set; }
        public Guid? RoleId { get; set; }
        public string Privilege { get; set; }
    }
}
