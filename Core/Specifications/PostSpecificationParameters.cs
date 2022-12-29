namespace Core.Specifications;

public class PostSpecificationParameters
{
    private const int MaxPageSize = 50;
    public int PageIndex { get; set; } = 1;
    
    private int _pageSize = 50;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > _pageSize) ? _pageSize : value;
    }

    public string? Sort { get; set; }

    private string? _search;
    public string? Search
    {
        get => _search;
        set => _search = value!.ToLower();
    }

    private int? _type = 0;
    public int? Type
    {
        get => _type;
        set => _type = value ?? _type;
    }
    
    private int? _receiptField = 0;
    public int? ReceiptField
    {
        get => _receiptField;
        set => _receiptField = value ?? _receiptField;
    }
}