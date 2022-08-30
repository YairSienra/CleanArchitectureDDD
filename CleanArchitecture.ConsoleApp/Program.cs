using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

StreamerDbContext dbContext = new();
await QueryFilter();

Console.WriteLine("Presione cualquer tecla para finalizar su programa");
Console.ReadKey();

async Task QueryFilter ()
{
    Console.WriteLine("ingrese un streamer de twitch");

    var request = Console.ReadLine();
    var streamer = await dbContext!.Streamers!.Where(x => x.Name == request).ToListAsync();

    foreach (var st in streamer)
    {
        Console.WriteLine($"{ st.Name} - {st.StreamerId}");
    }
}



  /*  Streamer streamer = new Streamer()
    {
        Name = "NicoFavro",
        Url = "https://www.twitch.tv/favroTv"
    };

    dbContext!.Streamers!.Add(streamer);

    await dbContext.SaveChangesAsync();

    var listaVideos = new List<Video>()
    {
        new Video
        {
            Name = "R6",
            StreamerId = streamer.StreamerId,
        },
           new Video
        {
            Name = "R6 Yair full crack",
            StreamerId = streamer.StreamerId,
        },
           new Video
        {
            Name = "Algo distinto",
            StreamerId = streamer.StreamerId,
        },
           new Video
        {
            Name = "R6 con buck Camino a diamante",
            StreamerId = streamer.StreamerId,
        }
    };

    await dbContext.AddRangeAsync(listaVideos);

    await dbContext.SaveChangesAsync(); */