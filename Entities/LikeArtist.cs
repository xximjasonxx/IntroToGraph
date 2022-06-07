namespace GraphDemo.Entities
{
    public class LikeArtist : Edge
    {
        public string ArtistId { get; set; }

        public string UserId { get; set; }

        public override string FromId => UserId;

        public override string ToId => ArtistId;
    }
}