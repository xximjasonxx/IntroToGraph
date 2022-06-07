namespace GraphDemo.Entities
{
    public class Friend : Edge
    {
        public string SourceFriendId { get; set; }
        public string TargetFriendId { get; set; }

        public override string FromId => SourceFriendId;

        public override string ToId => TargetFriendId;

        public Friend Reverse()
        {
            return new Friend
            {
                Id = Guid.NewGuid(),
                SourceFriendId = TargetFriendId,
                TargetFriendId = SourceFriendId
            };
        }
    }
}