using AutoMapper;

namespace Givt.OnlineCheckout.API.Utils;

public static class AutoMapperExtensionMerge
{
    public static TResult MergeInto<TResult>(this IMapper mapper, object item1, object item2)
    {
        return mapper.Map(item2, mapper.Map<TResult>(item1));
    }
}