//using Grpc.Core;
//using gRPC_Server;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using gRPC_Server.Entities;

//namespace gRPC_Server.Services
//{
//    public class GreeterService : Greeter.GreeterBase
//    {
//        private readonly ILogger<GreeterService> _logger;
//        public GreeterService(ILogger<GreeterService> logger)
//        {
//            _logger = logger;
//        }

//        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
//        {
//            return Task.FromResult(new HelloReply
//            {
//                Message = "Hello " + request.Name
//            });
//        }
//    }
//}
