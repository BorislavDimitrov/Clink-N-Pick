using ClickNPick.Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ClickNPick.Application.DtoModels.Comments.Response;

public class CreateCommentResponseDto
{
    public string Id { get; set; }

    public string CreatorId { get; set; }

    public string Content { get; set; }

    public string ParentId { get; set; }

    public string CreatorUsername { get; set; }

    public string CreatorImageUrl { get; set; }

    public string CreatedOn { get; set; }

    public static CreateCommentResponseDto FromComment(Comment comment)
    {
        var dto = new CreateCommentResponseDto();

        dto.Id = comment.Id;
        dto.CreatorId = comment.CreatorId;
        dto.ParentId = comment.ParentId;
        dto.Content = comment.Content;
        dto.CreatorUsername = comment.Creator.UserName;
        dto.CreatorImageUrl = comment.Creator.Image.Url;
        dto.CreatedOn = comment.CreatedOn.ToString("yyyy-MM-ddTHH:mm:ssZ");
        
        return dto;
    }
}
