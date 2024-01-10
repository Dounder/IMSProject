namespace IMS.Domain.Common;

public class PaginationParams
{
    public int Limit { get; set; } = 10;
    public int Offset { get; set; } = 0;

    public void Deconstruct(out int limit, out int offset)
    {
        limit = Limit;
        offset = Offset;
    }
}