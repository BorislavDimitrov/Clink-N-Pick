namespace ClickNPick.Application.DtoModels;

public class FilterPaginationDto
{
    public string? Search { get; set; }

    public decimal MinPrice { get; set; }

    public decimal MaxPrice { get; set; }

    public List<string> CategoryIds { get; set; } = new List<string>();

    public string? OrderBy { get; set; }

    public int PageNumber { get; set; }

    public int TotalItems { get; set; }

    public int PageSize { get; private set; } = 9;
}
