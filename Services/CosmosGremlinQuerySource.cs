
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

        public async Task<TType> AddVertex<TType>(TType vertex) where TType : class
        {
            return await GremlinQuerySource
                .AddV<TType>(vertex)
                .FirstAsync();
        }

        public async Task<IList<TType>> GetVertices<TType>() where TType : class
        {
            return (await GremlinQuerySource
                .V<TType>()).ToList();
        }
    }

    public interface IQuerySource
    {
        Task<TType> AddVertex<TType>(TType vertex) where TType : class;
        Task<IList<TType>> GetVertices<TType>() where TType : class;
    }
}