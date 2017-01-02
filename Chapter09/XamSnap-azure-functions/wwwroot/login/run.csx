#r "Microsoft.WindowsAzure.Storage"

using System.Net;
using System.Text;
using Microsoft.WindowsAzure.Storage.Table;

private const string PartitionKey = "XamSnap";

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, CloudTable outputTable, TraceWriter log)
{
    dynamic data = await req.Content.ReadAsAsync<object>();
    string userName = data?.userName;
    string password = data?.password;

    if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
    {
        return new HttpResponseMessage(HttpStatusCode.BadRequest);
    }

    //Let's hash all incoming passwords
    password = Hash(password);

    var operation = TableOperation.Retrieve<User>(PartitionKey, userName);
    var result = outputTable.Execute(operation);
    var existing = result.Result as User;
    if (existing == null)
    {
        operation = TableOperation.Insert(new User
        {
            RowKey = userName,
            PartitionKey = PartitionKey,
            PasswordHash = password,
        });
        result = outputTable.Execute(operation);

        if (result.HttpStatusCode == (int)HttpStatusCode.Created)
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        else
        {
            return new HttpResponseMessage((HttpStatusCode)result.HttpStatusCode);
        }
    }
    else if (existing.PasswordHash != password)
    {
        return new HttpResponseMessage(HttpStatusCode.Unauthorized);
    }
    else
    {
        return new HttpResponseMessage(HttpStatusCode.OK);
    }
}

private static string Hash(string password)
{
    var crypt = new System.Security.Cryptography.SHA256Managed();
    var hash = new StringBuilder();
    byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
    foreach (byte b in crypto)
    {
        hash.Append(b.ToString("x2"));
    }
    return hash.ToString();
}

public class User : TableEntity
{
    public string PasswordHash { get; set; }
}