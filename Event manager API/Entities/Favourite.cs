namespace Event_manager_API.Entities
{
    public class Favourite
    {
        public int Id { get; set; }
        public DateTime createdAt { get; set; }


        //RELATIONSHIPS

        ///------User
        public int UserId { get; set; }
        public User User { get; set; }

        //------Event
        public int EventId { get; set; }
        public Event Event { get; set; }


        //LISTS

        //------X
    }
}
