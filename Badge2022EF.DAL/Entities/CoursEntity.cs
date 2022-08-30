namespace Badge2022EF.DAL.Entities
{ 
    public partial class CoursEntity
        {
        public int cid { get; set; } = int.MinValue;
        public string cnom { get; set; } = string.Empty;
        public virtual ICollection<ExamenEntity>? cexams { get; set; }
        public FormationEntity? cform { get; set; }
        public virtual ICollection<NotesEleveEntity>? cNotesEleve { get; set; }
    }
}
