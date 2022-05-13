
using ExRam.Gremlinq.Core;
using ExRam.Gremlinq.Core.Models;
using GraphDemo.Entities;
using Microsoft.Extensions.Configuration;
using static ExRam.Gremlinq.Core.GremlinQuerySource;

namespace GraphDemo.Services
{
    public class CosmosGremlinQuerySource : IQuerySource
    {
        private readonly IConfiguration _configuration;

        public CosmosGremlinQuerySource(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        IGremlinQuerySource GremlinQuerySource
        {
            get
            {
                return g
                    .ConfigureEnvironment(env => env
                        .UseModel(GraphModel
                            .FromBaseTypes<Vertex, Edge>(lookup => lookup
                                .IncludeAssembliesOfBaseTypes())))
                    .UseCosmosDb(config => config
                        .At(new Uri(_configuration["CosmosDb:ServerUri"]))
                        .OnDatabase(_configuration["CosmosDb:DatabaseName"])
                        .OnGraph(_configuration["CosmosDb:GraphName"])
                        .AuthenticateBy(_configuration["CosmosDb:AccessKey"]));
            }   
        }

        public async Task AddEdgeAsync<TEdge>(TEdge edge) where TEdge : Edge
        {
            await GremlinQuerySource
                .V(edge.FromId)
                .AddE<TEdge>(edge)
                .To(_ => _.V(edge.ToId));
        }

        public async Task<TVertex> AddVertex<TVertex>(TVertex vertex) where TVertex : Vertex
        {
            return await GremlinQuerySource
                .AddV<TVertex>(vertex)
                .FirstAsync();
        }

        public async Task<IList<TVertex>> GetVertices<TVertex>() where TVertex : Vertex
        {
            return (await GremlinQuerySource
                .V<TVertex>()).ToList();
        }
    }

    public interface IQuerySource
    {
        Task AddEdgeAsync<TEdge>(TEdge edge) where TEdge : Edge;
        Task<TVertex> AddVertex<TVertex>(TVertex vertex) where TVertex : Vertex;
        Task<IList<TVertex>> GetVertices<TVertex>() where TVertex : Vertex;
    }
}