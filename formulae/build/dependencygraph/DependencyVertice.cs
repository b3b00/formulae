namespace formulae.build.dependencygraph
{
    public class DependencyVertice
    {
        public DependencyVertice(DependencyVertex target)
        {
            Target = target;
        }

        public DependencyVertex Target { get; set; }

        public bool Visited { get; set; }
    }
}