# GooglePublicDns
Client for the Google Public Dns over HTTPS

## Example of usage

```csharp
var client = new GooglePublicDnsClient();
var res = client.Resolve("selz.com",RecordType.ANY).Result;

foreach (var answer in res.Answer)
{
    
    Console.WriteLine("Name: " + answer.Name);
    Console.WriteLine("TTL" + answer.TTL);
    Console.WriteLine("Type:" + answer.Type);
    Console.WriteLine("Data:" + answer.Data);
}

Console.ReadKey();
```
