using System;
using ExRam.Gremlinq.Core;
using ExRam.Gremlinq.Core.Models;
using GraphDemo.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static ExRam.Gremlinq.Core.GremlinQuerySource;

namespace GraphDemo.Commands
{
	public class TestCommand : ICommand
	{
        private readonly IGremlinQuerySource _gremlinQuerySource;
        
        public TestCommand(IConfiguration configuration)
		{
            _gremlinQuerySource = g
                    .ConfigureEnvironment(env => env
                        .UseModel(GraphModel
                            .FromBaseTypes<Person, Edge>(lookup => lookup
                                .IncludeAssembliesOfBaseTypes())
                            .ConfigureProperties(model => model
                                .ConfigureElement<Person>(conf => conf
                                    .IgnoreOnUpdate(x => x.FamilyId)))))
                    .UseCosmosDb(config => config
                        .At(new Uri(configuration["CosmosDb:ServerUri"]))
                        .OnDatabase(configuration["CosmosDb:DatabaseName"])
                        .OnGraph("people")
                        .AuthenticateBy(configuration["CosmosDb:AccessKey"]));
        }

        public async Task ExecuteAsync()
        {
            var result = await _gremlinQuerySource.AddV(new Person { Id = Guid.NewGuid(), FamilyId = "Farrell" });
            var personResult = result.FirstOrDefault();
            Console.WriteLine($"{personResult?.FamilyId ?? "No Family"}");
        }
    }

    public class Person
    {
        public Guid Id { get; init; }

        [JsonProperty("familyId")]
        public string FamilyId { get; set; }
    }
}

