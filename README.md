gRPC is a high-performance, open-source Remote Procedure Call (RPC) framework that enables applications to communicate efficiently, even if they use different programming languages.

In gRPC, a client application can directly call a method on a server application on a different machine as if it were a local object, making it easier for you to create distributed applications and services. As in many RPC systems, gRPC is based around the idea of defining a service, specifying the methods that can be called remotely with their parameters and return types. On the server side, the server implements this interface and runs a gRPC server to handle client calls. On the client side, the client has a stub (referred to as just a client in some languages) that provides the same methods as the server.
[https://grpc.io/docs/]



Protocol Buffers are a language-neutral, platform-neutral extensible mechanism for serializing structured data.

It’s like JSON, except it’s smaller and faster, and it generates native language bindings. You define how you want your data to be structured once, then you can use special generated source code to easily write and read your structured data to and from a variety of data streams and using a variety of languages.

Protocol buffers are a combination of the definition language (created in .proto files), the code that the proto compiler generates to interface with data, language-specific runtime libraries, the serialization format for data that is written to a file (or sent across a network connection), and the serialized data.
[https://protobuf.dev/overview/]
-------------------------------

In this Repo I used the adsCompnay as Basic project  for the Server where the DB and other general code are exixted ,then there is a Client and a Server for the gRPC that 
are created for learning how dose gRPC work and Make a connection to understand this subject better.


SO for the Server 
-------------------------------
gRPC Server, hosts services.
dotnet add package Grpc.AspNetCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
in gRPC server there is a Folder Named Protos 

The .proto file defines:
   A complete CRUD interface (CreateAd, GetAd, UpdateAd, DeleteAd) for ads.
   Protobuf messages that ensure structured, typed, and efficient communication.
   Integration with C# using option csharp_namespace.
   Timestamps and Empty types via imports for clean API design.
-----------------------------------Def for proto ----------------------------------------------------

[ad.proto](gRPC_Server/Protos/ad.proto)
This file defines the AdService, a gRPC service with methods to CRUD on ads. It uses Protocol Buffers v3 (proto3) and includes nested message types.
In this File, here are the Important Parts:


service AdService {
    rpc CreateAd (CreateAdRequest) returns (AdResponse);
    rpc GetAd (GetAdRequest) returns (AdResponse);
    rpc UpdateAd (UpdateAdRequest) returns (AdResponse);
    rpc DeleteAd (DeleteAdRequest) returns (google.protobuf.Empty);
    rpc ListAds (google.protobuf.Empty) returns (ListAdsResponse);
}
This declares a gRPC service named AdService with 5 methods for CRUD:


message CreateAdRequest {
    string title = 1;
    int32 production_id = 2;
    string text = 3;
}
Sent when creating an ad. Includes a title, associated production_id, and the ad text.



message GetAdRequest {
    int32 id = 1;
}
Sent to fetch a single ad by its ID.

Other parts are same justified for other services 
dotnet run
for the Client 
-------------------------------
gRPC Client that communicates with the server,this is a Console Project and needs

dotnet add package Grpc.Net.Client
dotnet add package Google.Protobuf
dotnet add package Grpc.Tools
Just would Call the API and see the reponse for one.
dotnet run


Protocol Buffers
-----------------------------

