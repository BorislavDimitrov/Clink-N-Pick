namespace ClickNPick.Domain.Models.Common;

public class BaseModel<TKey> : IAuditInfo, ISoftDeletableModel
{
    public virtual TKey Id { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
