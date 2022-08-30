namespace Badge2022EF.DAL.Entities
{ 
    public partial class CoursEntity
        {
        public int cid { get; set; } = int.MinValue;
        public string cnom { get; set; } = string.Empty;
        public virtual ICollection<ExamenEntity>? cexams { get; set; }
        public virtual ICollection<FormationEntity>? cform { get; set; }
        public ICollection<NotesEleveEntity> cNotesEleve { get; set; }
    }
}
