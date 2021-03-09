namespace MusicHub
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            MusicHubDbContext context =
                new MusicHubDbContext();



            DbInitializer.ResetDatabase(context);

            var result = ExportSongsAboveDuration(context, 4);

            Console.WriteLine(result);
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albums = context.Producers
                .FirstOrDefault(x => x.Id == producerId)
                .Albums
                .Select(album => new
                {
                    AlbumName = album.Name,
                    ReleaseDate = album.ReleaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                    ProducerName = album.Producer.Name,
                    Songs = album.Songs.Select(song => new
                    {
                        SongName = song.Name,
                        Price = song.Price,
                        WriterName = song.Writer.Name
                    })
                    .OrderByDescending(x => x.SongName)
                    .ThenBy(x => x.WriterName)
                    .ToList(),
                    AlbumPrice = album.Price
                })
                .OrderByDescending(x => x.AlbumPrice)
                .ToList();

            var sb = new StringBuilder();

            foreach (var album in albums)
            {
                sb.AppendLine($"-AlbumName: {album.AlbumName}");
                sb.AppendLine($"-ReleaseDate: {album.ReleaseDate}");
                sb.AppendLine($"-ProducerName: {album.ProducerName}");
                sb.AppendLine("-Songs:");

                int counter = 1;

                foreach (var song in album.Songs)
                {
                    sb.AppendLine($"---#{counter++}");
                    sb.AppendLine($"---SongName: {song.SongName}");
                    sb.AppendLine($"---Price: {song.Price:F2}");
                    sb.AppendLine($"---Writer: {song.WriterName}");
                }

                sb.AppendLine($"-AlbumPrice: {album.AlbumPrice:F2}");
            }

            return sb.ToString().TrimEnd();


        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var allSongs = context.Songs
                .ToList()
                .Where(x => x.Duration.TotalSeconds > duration)
                .Select(x => new
                {
                    SongName = x.Name,
                    PerformerFullName = x.SongPerformers
                        .Select(p => p.Performer.FirstName + " " + p.Performer.LastName)
                        .FirstOrDefault(),
                    WriterName = x.Writer.Name,
                    AlbumProducer = x.Album.Producer.Name,
                    Duration = x.Duration

                })
                .OrderBy(x => x.SongName)
                .ThenBy(x => x.WriterName)
                .ThenBy(x => x.PerformerFullName)
                .ToList();

            var sb = new StringBuilder();

            int counter = 1;

            foreach (var song in allSongs)
            {
                sb.AppendLine($"-Song #{counter++}");
                sb.AppendLine($"---SongName: {song.SongName}");
                sb.AppendLine($"---Writer: {song.WriterName}");
                sb.AppendLine($"---Performer: {song.PerformerFullName}");
                sb.AppendLine($"---AlbumProducer: {song.AlbumProducer}");
                sb.AppendLine($"---Duration: {song.Duration:c}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
