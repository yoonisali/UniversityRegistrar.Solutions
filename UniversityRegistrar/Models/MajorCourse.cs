namespace UniversityRegistrar.Models 
{
  public class MajorCourse 
  {
    public int MajorCourseId { get; set; }
    public Course Course { get; set; }
    public int CourseId { get; set; }
    public Major Major { get; set; }
    public int MajorId { get; set; }
  }
}