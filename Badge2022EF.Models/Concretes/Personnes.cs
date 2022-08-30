
namespace Badge2022EF.Models.Concretes
{
    public class Personnes
    {
        public Personnes(string uNom, string uPrenom, string uEmail, DateTime uDate, string uRue, string uCodep, string uVille, string uPays)
        {
            unom = uNom;
            uprenom = uPrenom;
            Email = uEmail;
            udate = uDate;
            urue = uRue;
            ucodep = uCodep;
            uville = uVille;
            upays = uPays;
        }

        public string? unom { get; set; }
        public string? uprenom { get; set; }
        public string? Email { get; set; }
        public DateTime? udate { get; set; }
        public string? urue { get; set; }
        public string? ucodep { get; set; }
        public string? uville { get; set; }
        public string? upays { get; set; }

        public ICollection<Roles>? urole { get; set; }
        public Formations? uformation { get; set; }
        public ICollection<NotesEleves>? uNotesEleve { get; set; }


    }
}
