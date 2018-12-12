namespace Cashier.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Surname { get; set; }
        public string Name { get; set; }
        public string Fathername { get; set; }

        public string Login { get; set; }

        public UserRole UserRole { get; set; }
    }
}
