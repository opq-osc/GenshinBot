using GenshinBotCore.Models.MihoyoBBS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCore.Services.Data
{
    public class EmoticonSet
    {
        public EmoticonSet(IServiceProvider serviceProvider)
        {
            Emoticons = new List<Emoticon>(512);
            using var scope = serviceProvider.CreateScope();
            this.serviceProvider = serviceProvider;
        }

        private readonly IServiceProvider serviceProvider;

        public IEnumerable<Emoticon> Emoticons { get; set; }

        public IEnumerable<Emoticon> GetEmoticons(string keyword)
        {
            return Emoticons.Where(e => e.Name.Contains(keyword));
        }

        public async Task<string> GetRandomEmoticonAsync()
        {
            var success = Emoticons.TryGetNonEnumeratedCount(out var count);
            if (!success) count = Emoticons.Count();
            var rand = Random.Shared.Next(count);

            var url = Emoticons.Skip(rand).First().Url;

            using var scope = serviceProvider.CreateScope();
            var pictureStorage = scope.ServiceProvider.GetRequiredService<IPictureStorage>();

            return await pictureStorage.GetPicture(url);
        }

        public async Task<string> GetRandomEmoticonAsync(string keyword)
        {
            var fillterd = GetEmoticons(keyword);
            var success = fillterd.TryGetNonEnumeratedCount(out var count);
            if (!success) count = fillterd.Count();
            var rand = Random.Shared.Next(count);

            using var scope = serviceProvider.CreateScope();
            var pictureStorage = scope.ServiceProvider.GetRequiredService<IPictureStorage>();

            return await pictureStorage.GetPicture(fillterd.Skip(rand).First().Url);
        }
    }
}
