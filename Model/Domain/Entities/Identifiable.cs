namespace Model.Domain.Entities;

public interface Identifiable<Tid>
{
    public Tid id { get; set; }
}