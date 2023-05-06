namespace Event_manager_API.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }


        //RELATIONSHIPS
        
        //------X
        
        
        //LISTS
        
        //------Events
        public List<Event> EventsList { get; set; }


        
    }
}
