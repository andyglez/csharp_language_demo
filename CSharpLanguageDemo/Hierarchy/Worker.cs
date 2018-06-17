namespace CSharpLanguageDemo.Hierarchy
{
    public class Worker : Person, IWorker
    {
        public Worker(string name) => Name = name;
        public void Cash() { }
    }
}
