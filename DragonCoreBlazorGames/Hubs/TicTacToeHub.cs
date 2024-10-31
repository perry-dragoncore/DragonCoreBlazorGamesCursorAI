/*
using Microsoft.AspNetCore.SignalR;
namespace DragonCoreBlazorGames.Hubs
{
    public class TicTacToeHub : Hub
    {
        public async Task JoinGame(string gameId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
        }

        public async Task MakeMove(string gameId, int position)
        {
            await Clients.Group(gameId).SendAsync("MoveMade", position);
        }
    }
} 
*/