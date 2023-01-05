namespace UniversityRegistrar.Models 
{
  public class MajorStudent 
  {
    public int MajorStudentId { get; set; }
    public int StudentId { get; set; }
    public Student Student { get; set; }
    public int MajorId { get; set; }
    public Major Major { get; set; }
  }
}