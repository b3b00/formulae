namespace formulae.build.dependencygraph
{
    public class Vertice
    {
        public Vertex Target { get; set; }
        
        public bool Visited { get; set; }

        public Vertice(Vertex target)
        {
            Target = target;
        }
    }
}