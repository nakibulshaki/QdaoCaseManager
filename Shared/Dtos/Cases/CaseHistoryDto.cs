using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QdaoCaseManager.Shared.Dtos.Cases
{
    public class CaseHistoryDto
    {
        public int Id { get; set; }
        public int CaseId { get; set; }
        public string ActionType { get; set; }
        public DateTime ActionTime { get; set; }
    }
}
