using System.Collections.Generic;

namespace Rocky_Models.ViewModels
{
    public class InquiryVM
    {
        public InquiryHeader InquiryHeader { get; set; }
        public IEnumerable<InquiryDetails> InquiryDetail { get; set;}
    }
}
