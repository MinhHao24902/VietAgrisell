namespace VietAgrisell.ViewModels
{
    public class CheckoutViewModel
    {
        public bool Receiver {  get; set; }
        public int UserId {  get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? Mobile {  get; set; }
        public string? Note {  get; set; }
    }
}
