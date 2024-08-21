namespace LibraryMVC.Models
{
    public class CustomerFilterViewModel
    {
        public string SearchName { get; set; }
        public string SearchEmail { get; set; }
        public string SearchPhone { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
    }
}
