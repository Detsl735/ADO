namespace Lab3_EF.Models
{
    public class Lecture
    {
        public int Id { get; set; }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => _date = value.ToUniversalTime(); 
        }

        public string Topic { get; set; }
    }

}
