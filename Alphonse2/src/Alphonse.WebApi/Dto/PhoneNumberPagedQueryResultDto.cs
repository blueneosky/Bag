using Alphonse.WebApi.Dbo;

namespace Alphonse.WebApi.Dto;

public class PhoneNumberPagedQueryResultDto : PagedQueryResultDtoBase<PhoneNumberDbo>
{
    public PhoneNumberPagedQueryResultDto() { }

    public PhoneNumberPagedQueryResultDto(IPagedQueryResultContext<PhoneNumberDbo> context)
        : base(context) { }
}
