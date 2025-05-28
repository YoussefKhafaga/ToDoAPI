using System;

namespace TodoApi.DTOs.Responses;

public class PagedResult<T>
{
    public int PageNumber { get; set; }
    public int Limit { get; set; }
    public int TotalCount { get; set; }
    public IEnumerable<T> Items { get; set; } = new List<T>();
}
