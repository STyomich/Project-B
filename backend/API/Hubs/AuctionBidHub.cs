using Application.Interfaces;
using AutoMapper;
using Core.Domain.Entities;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace API.Hubs
{
    public class AuctionBidHub : Hub
    {
        private readonly DataContext _context;
        private readonly IAuctionBidService _auctionBidService;
        private readonly IMapper _mapper;
        public AuctionBidHub(DataContext dataContext, IAuctionBidService auctionBidService, IMapper mapper)
        {
            _context = dataContext;
            _mapper = mapper;
            _auctionBidService = auctionBidService;

        }
        public async Task JoinAuctionGroup(Guid auctionInfoId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, auctionInfoId.ToString());

            var currentMaxBid = await _context.AuctionBids
                .Where(b => b.AuctionInfoId == auctionInfoId)
                .OrderByDescending(b => b.BidAmount)
                .FirstOrDefaultAsync();

            if (currentMaxBid != null)
            {
                var bidDto = new AuctionBidDto
                {
                    AuctionInfoId = currentMaxBid.AuctionInfoId,
                    UserId = currentMaxBid.UserId,
                    BidAmount = currentMaxBid.BidAmount,
                    BidDate = currentMaxBid.BidDate
                };

                await Clients.Group(bidDto.AuctionInfoId.ToString()).SendAsync("CurrentMaxBid", bidDto);
            }
        }

        public async Task LeaveAuctionGroup(Guid auctionInfoId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, auctionInfoId.ToString());
        }
        public async Task SubmitBid(AuctionBidDto bidDto)
        {
            var placeBidResult = await _auctionBidService.PlaceBid(_mapper.Map<AuctionBid>(bidDto));

            if (placeBidResult)
            {
                await Clients.Group(bidDto.AuctionInfoId.ToString())
                .SendAsync("ReceiveBid", bidDto);
            }
            else
            {
                await Clients.Caller.SendAsync("BidRejected", new
                {
                    Message = $"Your bid (${bidDto.BidAmount}) is incorrect."
                });
            }
        }
    }
}