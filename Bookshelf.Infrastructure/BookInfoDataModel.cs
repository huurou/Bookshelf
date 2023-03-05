namespace Bookshelf.Infrastructure;

/// <summary>
/// 本情報のデータモデル
/// </summary>
/// <param name="Id">ID</param>
/// <param name="Title">タイトル</param>
/// <param name="Author">著者</param>
/// <param name="ImageStream">表紙画像ストリーム 大きい画像</param>
/// <param name="ThumbnailStream">表紙サムネイルストリーム 小さい画像</param>
/// <param name="PublishDate">発売日</param>
/// <param name="Publisher">出版社</param>
public record BookInfoDataModel(
    string Id,
    string Title,
    string Author,
    Stream? ImageStream,
    Stream? ThumbnailStream,
    string PublishDate,
    string Publisher);