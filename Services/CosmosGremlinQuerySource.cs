
using ExRam.Gremlinq.Core;
using ExRam.Gremlinq.Core.Models;
using GraphDemo.Entities;
using GraphDemo.Models;
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
                                .IncludeAssembliesOfBaseTypes())
                            .ConfigureProperties(model => model
                                .ConfigureElement<Vertex>(conf => conf
                                    .IgnoreOnUpdate(x => x.ItemType)))))
                    .UseCosmosDb(config => config
                        .At(new Uri(_configuration["CosmosDb:ServerUri"]))
                        .OnDatabase(_configuration["CosmosDb:DatabaseName"])
                        .OnGraph(_configuration["CosmosDb:GraphName"])
                        .AuthenticateBy(_configuration["CosmosDb:AccessKey"]));
            }   
        }

        public async Task AddEdge<TEdge>(TEdge edge) where TEdge : Edge
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

        public async Task<IList<TReturnVertex>> GetSingleEdgedVertices<TSourceVertex, TReturnVertex, TEdge>(TSourceVertex sourceVertex)
            where TSourceVertex : Vertex
            where TReturnVertex : Vertex
            where TEdge : Edge
        {
            var returnArray = await GremlinQuerySource
                .V<TSourceVertex>(sourceVertex.Id)
                .Out<TEdge>()
                .OfType<TReturnVertex>()
                .ToArrayAsync();

            return returnArray.ToList();
        }

        public async Task<IList<TEdge>> GetEdges<TVertex, TEdge>(TVertex vertex) where TVertex : Vertex
            where TEdge : Edge
        {
            return (await GremlinQuerySource
                .V<TVertex>(vertex.Id)
                .OutE<TEdge>()
                .ToArrayAsync())
                .ToList();
        }

        public async Task<TVertex> GetVertex<TVertex>(Guid id) where TVertex : Vertex
        {
            return await GremlinQuerySource
                .V<TVertex>(id)
                .FirstOrDefaultAsync();
        }

        public async Task<IList<TVertex>> GetVertices<TVertex>() where TVertex : Vertex
        {
            return (await GremlinQuerySource
                .V<TVertex>()).ToList();
        }

        public async Task<IList<TReturnVertex>> GetDoubleEdgedVertices<TSourceVertex, TReturnVertex, TEdge>(TSourceVertex sourceVertex)
            where TSourceVertex : Vertex
            where TReturnVertex : Vertex
            where TEdge : Edge
        {
            var returnArray = await GremlinQuerySource
                .V<TSourceVertex>(sourceVertex.Id)
                .Both<TEdge>()
                .OfType<TReturnVertex>()
                .ToArrayAsync();

            return returnArray.ToList();
        }

        public async Task DropAllVertices()
        {
            await GremlinQuerySource
                .V()
                .Drop();
        }

        public async Task<int> CountVertices<TVertex>() where TVertex : Vertex
        {
            var results = await GremlinQuerySource
                .V<TVertex>();

            return results.Count();
        }

        public async Task<int> CountEdges<TEdge>() where TEdge : Edge
        {
            var results = await GremlinQuerySource
                .E<TEdge>();

            return results.Count();
        }

        public async Task<int> CountVertexEdges<TVertex, TEdge>(TVertex vertex)
            where TVertex : Vertex
            where TEdge : Edge
        {
            var edges = await GremlinQuerySource
                .V<TVertex>(vertex.Id)
                .OutE<TEdge>();

            return edges.Count();
        }
    }

    public interface IQuerySource
    {
        Task<TVertex> AddVertex<TVertex>(TVertex vertex) where TVertex : Vertex;
        Task<IList<TVertex>> GetVertices<TVertex>() where TVertex : Vertex;
        Task<TVertex> GetVertex<TVertex>(Guid id) where TVertex : Vertex;
        Task DropAllVertices();
        Task<int> CountVertices<TVertex>() where TVertex : Vertex;
        Task<int> CountEdges<TEdge>() where TEdge : Edge;

        Task AddEdge<TEdge>(TEdge edge) where TEdge : Edge;
        Task<IList<TEdge>> GetEdges<TVertex, TEdge>(TVertex vertex) where TVertex : Vertex
            where TEdge : Edge;

        Task<int> CountVertexEdges<TVertex, TEdge>(TVertex vertex) where TVertex : Vertex
            where TEdge : Edge;

        Task<IList<TReturnVertex>> GetSingleEdgedVertices<TSourceVertex, TReturnVertex, TEdge>(TSourceVertex sourceVertex) where TSourceVertex : Vertex
            where TReturnVertex : Vertex
            where TEdge : Edge;

        Task<IList<TReturnVertex>> GetDoubleEdgedVertices<TSourceVertex, TReturnVertex, TEdge>(TSourceVertex sourceVertex) where TSourceVertex : Vertex
            where TReturnVertex : Vertex
            where TEdge : Edge;
    }
}