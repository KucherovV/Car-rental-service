namespace Entities
{
    class EngineType
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public EngineType(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
