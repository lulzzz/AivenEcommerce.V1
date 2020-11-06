using System;

namespace AivenEcommerce.V1.Domain.Entities
{
    public class Modal
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BotonText { get; set; }
        public Uri BotonUri { get; set; }
        public Uri Image { get; set; }
        public string ColorBackgroundHex { get; set; }
        public string ColorFontHex { get; set; }
        public short Order { get; set; }
        public bool IsActive { get; set; }
    }
}
