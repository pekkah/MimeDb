using Xunit;

namespace MimeDb.Facts
{
    public class MimeTypeFacts
    {
        [Theory]
        [InlineData("json", "application/json")]
        [InlineData("md", "text/markdown")]
        public void TryGet(string extension, string mimeType)
        {
            /* Given */
            /* When */
            var found = MimeType.TryGet(extension, out var actual);

            /* Then */
            Assert.True(found);
            Assert.Equal(mimeType, actual.Type);
        }
    }
}