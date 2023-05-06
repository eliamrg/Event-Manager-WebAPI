namespace Event_manager_API.Entities
{
    public class Follow
    {
        public int Id { get; set; }
        public DateTime createdAt { get; set; }


        //RELATIONSHIPS

        //------User
        public int? UserId { get; set; }
        public User User { get; set; }
        
        //------User Admin
        public int? AdminId { get; set; }
        public User Admin { get; set; }




        //LISTS

        //------X
    }
}
