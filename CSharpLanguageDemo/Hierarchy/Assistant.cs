namespace CSharpLanguageDemo.Hierarchy
{
    public class Assistant : Student, ITeacher
    {
        public Assistant(string name) : base(name) { }

        public void Teach() { }
    }
}
