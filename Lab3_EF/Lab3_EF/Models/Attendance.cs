namespace Lab3_EF.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public string StudentName { get; set; }

        private DateTime _lectureDate;
        public DateTime LectureDate
        {
            get => _lectureDate;
            set => _lectureDate = value.ToUniversalTime();
        }

        public int Mark { get; set; }
    }

}
