namespace UniversityRegistrar.Models 
{
  public class Student 
  {
    public int StudentId { get; set; }
    public string Name { get; set; }
    public string EnrollmentDate { get; set; }

    public List<Enrollment> JoinEntities { get; }
  }
}