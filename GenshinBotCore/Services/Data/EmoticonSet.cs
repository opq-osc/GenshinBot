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
            this.pictureStorage = scope.ServiceProvider.GetRequiredService<IPictureStorage>();
        }

        private readonly IPictureStorage pictureStorage;

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

            return await pictureStorage.GetPicture(Emoticons.Skip(rand).First().Url);
        }

        public async Task<string> GetRandomEmoticonAsync(string keyword)
        {
            var fillterd = GetEmoticons(keyword);
            var success = fillterd.TryGetNonEnumeratedCount(out var count);
            if (!success) count = fillterd.Count();
            var rand = Random.Shared.Next(count);

            return await pictureStorage.GetPicture(Emoticons.Skip(rand).First().Url);
        }
    }
}
