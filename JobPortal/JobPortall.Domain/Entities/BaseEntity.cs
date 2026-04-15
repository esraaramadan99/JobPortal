using Microsoft.AspNetCore.Mvc;

namespace JobPortal.JobPortall.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }           // كل entity عندها ID
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }   // Soft delete — مش بنمسح من DB
    }
}
