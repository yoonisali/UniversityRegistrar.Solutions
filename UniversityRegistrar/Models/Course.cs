namespace UniversityRegistrar.Models
{
  public class Course 
  {
    public int CourseId { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public List<Student> Students { get; set; }
    public List<Enrollment> JoinEntities { get; }
  }
}