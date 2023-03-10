namespace UniversityRegistrar.Models
{
  public class Course 
  {
    public int CourseId { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public List<Enrollment> JoinEntities { get; }
    public List<MajorCourse> MajorCourses { get; }
  }
}