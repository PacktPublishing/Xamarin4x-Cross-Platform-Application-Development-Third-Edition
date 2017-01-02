#r "Microsoft.WindowsAzure.Storage"

using System.Net;
using Microsoft.WindowsAzure.Storage.Table;

public async static Task<HttpResponseMessage> Run(HttpRequestMessage req, IQueryable<TableEntity> inputTable, TraceWriter log)
{
    dynamic data = await req.Content.ReadAsAsync<object>();
    string userName = data?.userName;
    if (string.IsNullOrEmpty(userName))
    {
        return new HttpResponseMessage(HttpStatusCode.BadRequest);
    }

    var results = inputTable
        .Where(r => r.PartitionKey == userName)
        .Select(r => new { Name = r.RowKey })
        .ToList();
    return req.CreateResponse(HttpStatusCode.OK, results);
}