using System;

namespace AivenEcommerce.V1.Modules.ImgBB.Dto
{
    public partial class ImgBbResponse
    {
        public Data Data { get; set; }

        public bool Success { get; set; }

        public long Status { get; set; }
    }

    public partial class Data
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public Uri UrlViewer { get; set; }

        public Uri Url { get; set; }

        public Uri DisplayUrl { get; set; }

        public long Size { get; set; }

        public long Time { get; set; }

        public long Expiration { get; set; }

        public Image Image { get; set; }

        public Image Thumb { get; set; }

        public Image Medium { get; set; }

        public Uri DeleteUrl { get; set; }
    }

    public partial class Image
    {
        public string Filename { get; set; }

        public string Name { get; set; }

        public string Mime { get; set; }

        public string Extension { get; set; }

        public Uri Url { get; set; }
    }
}
