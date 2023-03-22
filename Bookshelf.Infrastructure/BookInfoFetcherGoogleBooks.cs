using System.Collections.ObjectModel;
using System.Text.Json.Nodes;

namespace Bookshelf.Infrastructure;

/// <summary>
/// GoogleBooksAPIを利用した本情報取得クラス
/// </summary>
public class BookInfoFetcherGoogleBooks: IBookInfoFetcher
{
    private readonly HttpClient _client;

    public BookInfoFetcherGoogleBooks(HttpClient client)
    {
        _client = client;
    }

    public async Task<ReadOnlyCollection<BookInfoDataModel>> FetchBookInfoAsync(string series)
    {
        var startIndex = 0;
        var responseJsonObj = await GetResponseObjct(startIndex);
        var totalItems = responseJsonObj?["totalItems"]?.GetValue<int>();
        var items = responseJsonObj?["items"]?.AsArray();
        if (totalItems is null || totalItems == 0 || items is null) return new List<BookInfoDataModel>().AsReadOnly();
        var bookInfoTasks = items.Select(x => DeserializeBookInfoAsync(x?.AsObject())).ToList();
        series = string.Join(' ', series.Split().Select(x => $"\"{x}\""));
        // totalItemsが41以上なら全て取得できるまでstartIndexをずらして取得する
        while (totalItems >= 41)
        {
            startIndex += 40;
            responseJsonObj = await GetResponseObjct(startIndex);
            items = responseJsonObj?["items"]?.AsArray();
            if (items is not null)
            {
                bookInfoTasks.AddRange(items.Select(x => DeserializeBookInfoAsync(x?.AsObject())));
            }
            totalItems -= 40;
        }
        return (await Task.WhenAll(bookInfoTasks)).ToList().AsReadOnly();

        async Task<JsonObject?> GetResponseObjct(int startIndex)
        {
            var requestUri = $"https://www.googleapis.com/books/v1/volumes?q=intitle:{series}&langRestrict=ja&startIndex={startIndex}&maxResults=40";
            var responseJson = await _client.GetStringAsync(requestUri);
            return responseJsonObj = JsonNode.Parse(responseJson)?.AsObject();
        }
    }

    private async Task<BookInfoDataModel> DeserializeBookInfoAsync(JsonObject? item)
    {
        var id = item?["id"]?.GetValue<string>();
        var volumeInfo = item?["volumeInfo"]?.AsObject();
        var title = volumeInfo?["title"]?.GetValue<string>() ?? "";
        var author = volumeInfo?["authors"]?.AsArray().FirstOrDefault()?.GetValue<string>() ?? "";
        var publisher = volumeInfo?["publisher"]?.GetValue<string>() ?? "";
        var publishDate = volumeInfo?["publishDate"]?.GetValue<string>() ?? "";
        Stream? imageStream = null;
        Stream? thumbnailStream = null;
        if (!string.IsNullOrEmpty(id))
        {
            var imageUrl = $"https://books.google.com/books/content?id={id}&printsec=frontcover&img=1&zoom=0&source=gbs_api";
            var imageResponse = await _client.GetAsync(imageUrl);
            if (imageResponse.IsSuccessStatusCode)
            {
                imageStream = await imageResponse.Content.ReadAsStreamAsync();
            }
            var thumbnailUrl = $"https://books.google.com/books/content?id={id ?? ""}&printsec=frontcover&img=1&zoom=1&source=gbs_api";
            var thumbnailResponse = await _client.GetAsync(thumbnailUrl);
            if (thumbnailResponse.IsSuccessStatusCode)
            {
                thumbnailStream = await thumbnailResponse.Content.ReadAsStreamAsync();
            }
        }
        return new BookInfoDataModel(id ?? "", title, author, imageStream, thumbnailStream, publishDate, publisher);
    }
}