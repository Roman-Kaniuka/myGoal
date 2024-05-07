namespace myGoal.Domain.Interfaces;


public interface IAuditable {
    public DateTime CreateAt { get; set; }

    public long CreateBy { get; set; }
    public DateTime? UpdateAt { get; set; }
    public long? UpdateBy { get; set; }
}
