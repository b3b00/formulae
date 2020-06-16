namespace formulae.build.dependencygraph
{
    public class Vertice
    {
        public Vertice(Vertex target)
        {
            Target = target;
        }

        public Vertex Target { get; set; }

        public bool Visited { get; set; }
    }
}