namespace Badge2022EF.Models.Concretes
{
    public class Roles
    {
        public Roles(string rolesName)
        {
            Name = rolesName;
            //personnes = rolesPersonnes;
        }
        public int Id { get; set; } = int.MinValue;
        public string Name { get; set; } = string.Empty;

        public ICollection<Personnes>? Personnes { get; set; }
    }
}
