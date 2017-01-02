#r "Microsoft.WindowsAzure.Storage"
#r "Microsoft.Azure.NotificationHubs"

using System.Net;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.Azure.NotificationHubs;

public async static Task<HttpResponseMessage> Run(HttpRequestMessage req, CloudTable outputTable, TraceWriter log)
{
    dynamic data = await req.Content.ReadAsAsync<object>();
    if (data == null)
        return req.CreateResponse(HttpStatusCode.BadRequest);

    var operation = TableOperation.InsertOrReplace(new Message
    {
        PartitionKey = data.Conversation,
        RowKey = data.Id,
        UserName = data.UserName,
        Text = data.Text,
    });
    var result = outputTable.Execute(operation);

    await SendPush((string)data.UserName, (string)data.Text);

    return req.CreateResponse((HttpStatusCode)result.HttpStatusCode);
}

private async static Task SendPush(string userName, string message)
{
    var dictionary = new Dictionary<string, string>();
    dictionary["message"] = userName + ": " + message;

    var hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://xamsnap.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=UsBxev0o2YNMdY/poBijA7blfbfAGLkL1gz/YtNHzh4=", "xamsnap");
    await hub.SendTemplateNotificationAsync(dictionary, userName);
}

public class Message : TableEntity
{
    public string UserName { get; set; }
    public string Text { get; set; }
}