using Rocky_Models;

namespace Rocky_DataAccess.Repository.IRepository
{
    public interface IInquiryDetailRepository : IRepository<InquiryDetails>
    {
        void Update(InquiryDetails obj);
    }
}
