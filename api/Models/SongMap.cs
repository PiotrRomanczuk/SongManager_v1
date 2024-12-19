using CsvHelper.Configuration;
using SongsAPI.Models;

namespace SongsAPI.Models
{
    public sealed class SongMap : ClassMap<Song>
    {
        public SongMap()
        {
            Map(m => m.Id).Name("id").Convert(args =>
            {
                if (Guid.TryParse(args.Row.GetField("id"), out Guid id))
                    return id;
                return Guid.NewGuid();
            });
            Map(m => m.Title).Name("title");
            Map(m => m.Level).Name("level");
            Map(m => m.SongKey).Name("key");
            Map(m => m.Author).Name("author");
            Map(m => m.UltimateGuitarLink).Name("Ultimate-Guitar Link");
            Map(m => m.CreatedAt).Name("created_at").Convert(args =>
            {
                if (DateTime.TryParse(args.Row.GetField("created_at"), out DateTime date))
                    return date;
                return DateTime.UtcNow;
            });
        }
    }
}
