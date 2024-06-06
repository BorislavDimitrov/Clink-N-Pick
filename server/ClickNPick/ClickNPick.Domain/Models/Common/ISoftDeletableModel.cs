namespace ClickNPick.Domain.Models.Common;

public interface ISoftDeletableModel
{
    bool IsDeleted { get; set; }

    DateTime? DeletedOn { get; set; }
}
