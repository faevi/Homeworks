namespace WebApi2.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int Customer_Id { get; set; }
        public string DateTime { get; set; }
        public List<Stuff> Stuffs { get; set; }
    }
}

