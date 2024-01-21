namespace QdaoCaseManager.Entites
{
    public class Note:AuditRoot
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int CaseId { get; set; }
        public Case Case { get; set; }
    }
}
