
using System.Reflection.Metadata.Ecma335;

namespace SmartBin.Infrastructure.MqttClients
{
    public class MqttBuffer
    {
        public List<TagChangedNotification> TagChangedForUser { get; set; } = new();
        public List<TagChangedNotification> TagChangedForAdmin { get; set; } = new();
        public MqttBuffer()
        {
        }

        public async Task Update(TagChangedNotification tagChangedNotification)
        {
            var isExist = TagChangedForAdmin.FirstOrDefault(x => x.Id == tagChangedNotification.Id && x.BinUnitId == tagChangedNotification.BinUnitId && x.BinId == tagChangedNotification.BinId);
            if (isExist is null)
            {
                TagChangedForAdmin.Add(tagChangedNotification);
                if(tagChangedNotification.Id >= 80)
                {
                    TagChangedForUser.Add(tagChangedNotification);
                }
            }
            else
            {
                isExist.Value = tagChangedNotification.Value;
            }
            await Task.CompletedTask;
        }
        public string GetTagsForUser() => System.Text.Json.JsonSerializer.Serialize(TagChangedForUser);
        public string GetTagsForAdmin() => System.Text.Json.JsonSerializer.Serialize(TagChangedForAdmin);

    }
}
