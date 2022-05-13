namespace GraphDemo.Entities
{
    public class Friend : Edge
    {
        public Guid SourceFriendId { get; set; }
        public Guid TargetFriendId { get; set; }

        public override Guid FromId => SourceFriendId;

        public override Guid ToId => TargetFriendId;
    }
}