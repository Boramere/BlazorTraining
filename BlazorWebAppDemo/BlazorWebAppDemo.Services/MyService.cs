namespace BlazorWebAppDemo.Services
{

    public class MyService : IMyService
    {

        public MyService() 
        {
            AddName("One");
            AddName("Two");
            AddName("Three");
        }

        public List<string> Names { get; set; } = [];

        public void AddName(string name)
        {
            Names.Add(name);
        }

    }

    public interface IMyService
    {
        List<string> Names { get; set; }
        void AddName(string name);
    }
}
