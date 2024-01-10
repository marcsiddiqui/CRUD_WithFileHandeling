namespace FirstApp.Models
{
    public class StudentListModel
    {
        public StudentListModel()
        {
            Students = new List<StudentModel>();                    // empty
        }

        public List<StudentModel> Students { get; set; }            // null
    }

    public class StudentModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public string Email { get; set; }
    }
}
