namespace GraphDemo.Entities
{
    public class LikeArtist : Edge
    {
        public Guid ArtistId { get; set; }

        public Guid UserId { get; set; }

        public override Guid FromId => UserId;

        public override Guid ToId => ArtistId;
    }
}