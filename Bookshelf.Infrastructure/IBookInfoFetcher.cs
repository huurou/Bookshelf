using System.Collections.ObjectModel;

namespace Bookshelf.Infrastructure;

/// <summary>
/// 本情報取得インターフェース
/// </summary>
public interface IBookInfoFetcher
{
    Task<ReadOnlyCollection<BookInfoDataModel>> FetchBookInfoAsync(string series);
}