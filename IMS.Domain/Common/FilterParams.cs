using System.Linq.Expressions;
using IMS.Domain.Interfaces;

namespace IMS.Domain.Common;

public class FilterParams<T> where T : class, IBaseEntity
{
    public PaginationParams Pagination { get; set; } = new();
    public bool TrackChanges { get; set; } = false;
    public Expression<Func<T, bool>> Where { get; set; } = x => x.DeletedAt == null;
    public Expression<Func<T, object>> OrderBy { get; set; } = x => x.CreatedAt;
    public bool OrderByDescending { get; set; } = true;

    public void Deconstruct(
        out PaginationParams pagination,
        out bool trackChanges,
        out Expression<Func<T, bool>> where,
        out Expression<Func<T, object>> orderBy,
        out bool orderByDescending)
    {
        pagination = Pagination;
        trackChanges = TrackChanges;
        where = Where;
        orderBy = OrderBy;
        orderByDescending = OrderByDescending;
    }
}