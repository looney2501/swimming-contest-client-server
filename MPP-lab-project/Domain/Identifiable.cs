namespace MPP_lab_project.Domain
{
    public interface Identifiable<Tid>
    {
        public Tid ID { get; set; }
    }
}