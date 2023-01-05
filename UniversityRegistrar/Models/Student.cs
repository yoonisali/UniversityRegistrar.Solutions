using System.ComponentModel.DataAnnotations;

namespace UniversityRegistrar.Models 
{
  public class Student 
  {
    public int StudentId { get; set; }
    [Required(ErrorMessage = "Student name cannot be empty!")]
    public string Name { get; set; }
    public DateTime? EnrollmentDate { get; set; }
    public List<Enrollment> JoinEntities { get; }
    public List<MajorStudent> MajorStudents { get; }
  }
}