
namespace SmartBin.Api.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        private readonly MqttBuffer _buffer;

        public NotificationHub(MqttBuffer buffer)
        {
            _buffer = buffer;
        }

        public override async Task OnConnectedAsync()
        {
            var isAdmin = Context.User.IsInRole("BinAdmin");  // Kiểm tra role Admin từ JWT token

            if (isAdmin)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "BinAdmins");
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Users");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var isAdmin = Context.User.IsInRole("BinAdmin");

            if (isAdmin)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "BinAdmins");
            }
            else
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Users");
            }

            await base.OnDisconnectedAsync(exception);
        }
        public async Task GetBufferForUser()
        {
            var tag = _buffer.GetTagsForUser();
            await Clients.Group("Users").SendAsync("TagForUser",tag);
        }
        public async Task GetBufferForAdmin()
        {
            var tag = _buffer.GetTagsForAdmin();
            await Clients.Group("BinAdmins").SendAsync("TagForAdmin", tag);
        }
    }
}
