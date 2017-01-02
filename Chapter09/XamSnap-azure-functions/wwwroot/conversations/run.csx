#r "Microsoft.WindowsAzure.Storage"

using System.Net;
using Microsoft.WindowsAzure.Storage.Table;

public async static Task<HttpResponseMessage> Run(HttpRequestMessage req, IQueryable<Conversation> inputTable, TraceWriter log)
{
    dynamic data = await req.Content.ReadAsAsync<object>();
    string userName = data?.userName;
    if (string.IsNullOrEmpty(userName))
    {
        return new HttpResponseMessage(HttpStatusCode.BadRequest);
    }

    var results = inputTable
        .Where(r => r.PartitionKey == userName)
        .Select(r => new { Id = r.RowKey, UserName = r.UserName })
        .ToList();
    return req.CreateResponse(HttpStatusCode.OK, results);
}

public class Conversation : TableEntity
{
    public string UserName { get; set; }
}