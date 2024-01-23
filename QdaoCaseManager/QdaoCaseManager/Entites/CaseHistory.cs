namespace QdaoCaseManager.Entites;

    public class CaseHistory
    {
        public int Id { get; set; }
        public int CaseId { get; set; }
        public string ActionType { get; set; }
        public DateTime ActionTime { get; set; }
    }

