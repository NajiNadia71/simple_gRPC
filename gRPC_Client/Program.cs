using Grpc.Net.Client;
using Google.Protobuf.WellKnownTypes;
using Ad.V1;  // This should match the csharp_namespace in .proto file
using Grpc.Core;

var channel = GrpcChannel.ForAddress("http://localhost:5000",
    new GrpcChannelOptions
    {
        Credentials = ChannelCredentials.Insecure
    });

var client = new AdService.AdServiceClient(channel);
// Create a new ad
//Console.WriteLine("Creating new ad...");
//var createResponse = await client.CreateAdAsync(new CreateAdRequest
//{
//    Title = "New GRPC",
//    ProductionId = 2,
//    Text = "text GRPC"
//});
//Console.WriteLine($"Created ad with ID: {createResponse.Id}");

//// Get the created ad
//Console.WriteLine("\nFetching ad...");
//var getResponse = await client.GetAdAsync(new GetAdRequest { Id = createResponse.Id });
//Console.WriteLine($"Retrieved ad: {getResponse.Title}");

//// Update the ad
//Console.WriteLine("\nUpdating ad...");
//var updateResponse = await client.UpdateAdAsync(new UpdateAdRequest
//{
//    Id = createResponse.Id,
//    Title = "Updated Advertisement",
//    ProductionId = 1,
//    Text = "Updated advertisement text"
//});
//Console.WriteLine($"Updated ad title: {updateResponse.Title}");

// List all ads
Console.WriteLine("\nListing all ads...");
var listResponse = await client.ListAdsAsync(new Empty());
foreach (var ad in listResponse.Ads)
{
    Console.WriteLine($"- {ad.Id}: {ad.Title}");
}

// Delete the ad
//Console.WriteLine("\nDeleting ad...");
//await client.DeleteAdAsync(new DeleteAdRequest { Id = createResponse.Id });
//Console.WriteLine("Ad deleted successfully");
       