namespace Application.Share.Dto
{
    public class MoveInput
    {
        public long Id { get; set; }
        public long? InsertAfterId { get; set; }

        public long ParentId { get; set; }


    }
}
