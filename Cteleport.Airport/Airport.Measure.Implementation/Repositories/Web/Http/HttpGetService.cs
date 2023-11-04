namespace Airport.Measure.Implementation.Repositories.Web.Http;

/// <summary>
/// HTTP Get service
/// </summary>
public class HttpGetService: IHttpGet
{
    #region .ctor

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="baseUrl">Base URL</param>
    public HttpGetService(string baseUrl)
    {
        // validate
        if (string.IsNullOrWhiteSpace(baseUrl))
            throw new AggregateException("Base URL for IATA repository cannot be empty");
        
        // init
        if (!baseUrl.EndsWith("/"))
            baseUrl += "/";
        
        baseUri = new Uri(baseUrl);
    }
    
    #endregion
    
    #region Private

    private readonly HttpClient _http = new HttpClient();
    private readonly Uri baseUri;

    #endregion
    
    #region IHttpGet implementation

    /// <inheritdoc />
    public async Task<string?> GetAsync(string code)
    {
        // build url 
        var uri = new Uri(baseUri, code);
        
        // make a request 
        var response = await _http.GetAsync(uri);

        //
        return await response.Content.ReadAsStringAsync();
    }
    
    #endregion
}