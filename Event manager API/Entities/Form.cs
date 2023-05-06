namespace Event_manager_API.Entities
{
    public class Form
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Comment { get; set; }

        //RELATIONSHIPS

        //------User
        public int UserId { get; set; }
        public User User { get; set; }

        //------Event
        public int EventId { get; set; }
        public Event Event { get; set; }


        //LISTS

        //------X
    }
}
