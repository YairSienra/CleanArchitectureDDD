using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastucture.Persistence
{
    public class StreamerDbContextSeed
    {
        public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContextSeed> logger)
        {
           if(!context.Streamer!.Any())
            {
                context.Streamer!.AddRange(GetPreConfigureStreamer());
                await context.SaveChangesAsync();
                logger.LogInformation("Estamos Agregando nuevos datos a la database {context}", typeof(StreamerDbContext).Name);
            }
        }

        private static IEnumerable<Streamer> GetPreConfigureStreamer()
        {
            return new List<Streamer>
            {
                new Streamer
                {
                    Name = "Yair Sienra", Url = "yairsienra.com.ar"

                }
            };
        }
    }
}
