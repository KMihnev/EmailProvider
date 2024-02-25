//Includes
using AutoMapper;

namespace EmailServiceIntermediate.Mapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration);
    }
}