namespace CSharpLanguageDemo.Hierarchy
{
    public class Teacher : Person, ITeacher, IWorker
    {
        public Teacher(string name) => Name = name;

        public void Cash() { }

        public void Teach() { }
    }
}
