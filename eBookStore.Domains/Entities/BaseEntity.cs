using System.ComponentModel.DataAnnotations;

namespace eBookStore.Domains.Entities;

    public class BaseEntity 
    {
		[Key]
        public Guid Id { get; set; } = Guid.NewGuid();

		public DateTime CreationDate { get; set; } = DateTime.Now;

		public Guid? CreatedBy { get; set; }

		public DateTime? ModificationDate { get; set; }

		public Guid? ModificationBy { get; set; }

		public DateTime? DeletionDate { get; set; }

		public Guid? DeleteBy { get; set; }

		public bool IsDeleted { get; set; } = false;
    
    }
