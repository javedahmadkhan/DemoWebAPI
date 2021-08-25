using Demo.Entities.DomainEntity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Entities
{
    /// <summary>
    /// 
    /// </summary>
    [Table("TodoItem", Schema = "dbo")]
    public class TodoItem : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PKId { get; set; }

        [MaxLength(200)]
        public string TaskName { get; set; }
        [MaxLength(200)]
        public string MorningTask { get; set; }

        [MaxLength(200)]
        public string AfternoonTask { get; set; }

        [MaxLength(200)]
        public string EveningTask { get; set; }

        public bool IsTaskComplete { get; set; }

        [NotMapped]
        public override int Id
        {
            get { return PKId; }
            set { PKId = value; }
        }
    }
}
