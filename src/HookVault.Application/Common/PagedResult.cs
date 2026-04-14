using System;
using System.Collections.Generic;
using System.Text;

namespace HookVault.Application.Common
{
    public sealed class PagedResult<T>(IReadOnlyList<T> Items, int Page, int PageSize, int TotalCount)
    {

        public IReadOnlyList<T> Items { get; } = Items;
        public int Page { get; } = Page;
        public int PageSize { get; } = PageSize;
        public int TotalCount { get; } = TotalCount;
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    }
}
