namespace UniversityRegistrar.Models 
{
  public class Major 
  {
    public string Name { get; set; }
    public int MajorId { get; set; }
    public List<MajorStudent> MajorStudents { get; }
    public List<MajorCourse> MajorCourses { get; }
  }
}