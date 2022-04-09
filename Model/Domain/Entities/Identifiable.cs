namespace Model.Domain.Entities;

public interface Identifiable<Tid>
{
    public Tid ID { get; set; }
}