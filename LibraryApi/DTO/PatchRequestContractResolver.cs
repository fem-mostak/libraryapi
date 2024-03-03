using LibraryApi.TDO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace LibraryApi.DTO
{
    public class PatchRequestContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);

            prop.SetIsSpecified += (o, o1) =>
            {
                if (o is PatchDtoBase patchDtoBase)
                {
                    patchDtoBase.SetHasProperty(prop.PropertyName);
                }
            };

            return prop;
        }
    }
}
