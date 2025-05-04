using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using gRPC_Server.Entities;
using gRPC_Server.DbContexts;
using gRPC_Server.ViewModels;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ad.V1;
using Google.Protobuf.WellKnownTypes;

namespace gRPC_Server.Services
{
    public class AdServiceImplementation : Ad.V1.AdService.AdServiceBase
    {
         private readonly SqliteDbContext _context;
        // private readonly AdDbContext _context;
        private readonly ILogger<AdServiceImplementation> _logger;
        public AdServiceImplementation(SqliteDbContext dbContext, ILogger<AdServiceImplementation> logger)
        {
              _context = dbContext;
              _logger = logger;
        }

        public override async Task<AdResponse> CreateAd(CreateAdRequest request, ServerCallContext context)
        {
            _logger.LogDebug("CreateAd",request.ProductionId);
            var ad = new gRPC_Server.Entities.Ad
            {
                Title = request.Title,
                ProductionId = request.ProductionId,
                Text = request.Text,
                CreateDate = DateTimeOffset.UtcNow.ToString()

            };

            _context.Ads.Add(ad);
            await _context.SaveChangesAsync();

            return await GetAdResponseFromModel(ad);
        }

        public override async Task<AdResponse> GetAd(GetAdRequest request, ServerCallContext context)
        {
            var ad = await _context.Ads
                .Include(a => a.Production)
                .FirstOrDefaultAsync(a => a.Id == request.Id);

            if (ad == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Ad not found"));

            return await GetAdResponseFromModel(ad);
        }

        public override async Task<AdResponse> UpdateAd(UpdateAdRequest request, ServerCallContext context)
        {
            var ad = await _context.Ads.FindAsync(request.Id);
            if (ad == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Ad not found"));

            ad.Title = request.Title;
            ad.ProductionId = request.ProductionId;
            ad.Text = request.Text;

            await _context.SaveChangesAsync();
            return await GetAdResponseFromModel(ad);
        }

        public override async Task<Empty> DeleteAd(DeleteAdRequest request, ServerCallContext context)
        {
            var ad = await _context.Ads.FindAsync(request.Id);
            if (ad == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Ad not found"));

            _context.Ads.Remove(ad);
            await _context.SaveChangesAsync();

            return new Empty();
        }

        public override async Task<ListAdsResponse> ListAds(Empty request, ServerCallContext context)
        {
            try {
                var ads = await _context.Ads
                    //  .Include(a => a.Production)
                    .ToListAsync();

                var response = new ListAdsResponse();
                foreach (var ad in ads)
                {
                    response.Ads.Add(await GetAdResponseFromModel(ad));
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error listing ads");
                throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
            }
            
            }

        private async Task<AdResponse> GetAdResponseFromModel(gRPC_Server.Entities.Ad ad)
        {
            try{
            var production = await _context.Productions
                .FirstOrDefaultAsync(p => p.Id == ad.ProductionId);
          
            return new AdResponse
            {
                Id = ad.Id,
                Title = ad.Title,
                ProductionId = ad.ProductionId,
                CreateDate = Timestamp.FromDateTimeOffset(DateTimeOffset.Parse(ad.CreateDate)),
                Text = ad.Text,
                Production = production != null ? new ProductionResponse
                {
                    Id = production.Id,
                    Count = production.Count,
                    Title = production.Title,
                    ProductionTypeId = production.ProductionTypeId,
                    CreateDate = Timestamp.FromDateTimeOffset(DateTimeOffset.Parse(production.CreateDate)),
                    Comment = production.Comment
                } : null
            };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error converting ad to response model");
                throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
            }
        }
    }
}