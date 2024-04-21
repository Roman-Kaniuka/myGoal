using myGoal.Domain.Interfaces;

namespace myGoal.Domain.Entity;

 public class User : IEbtityId<long>, IAuditable
{
 public long Id { get; set; }

 public string Login { get; set; }
 
 public string Password { get; set; }
 

 public  List<Report> Reports { get; set; }
 
 
 public DateTime CreateAt { get; set; }
 
 public long CreateBy { get; set; }
 
 public DateTime UpdateAt { get; set; }
 
 public long UpdateBy { get; set; }
}