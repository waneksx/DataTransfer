using MessagePack;

namespace Entities
{
    [MessagePackObject]
    public class Call
    {
        [Key(0)]
        public long Id { get; set; }
        [Key(1)]
        public string CallExternalId { get; set; }
        [Key(2)]
        public long CampaignId { get; set; }
        [Key(3)]
        public long AgentId { get; set; }
        [Key(4)]
        public string PhoneNumber { get; set; }
        [Key(5)]
        public string DialedPhoneNumber { get; set; }
        [Key(6)]
        public System.DateTime StartTime { get; set; }
        [Key(7)]
        public System.DateTime EndTime { get; set; }
        [Key(8)]
        public int Status { get; set; }
        [Key(9)]
        public bool WasSentToHub { get; set; }
    }
}