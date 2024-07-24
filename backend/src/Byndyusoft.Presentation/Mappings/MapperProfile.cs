using AutoMapper;
using Byndyusoft.Presentation.Contracts.Requests;
using Byndyusoft.Presentation.Contracts.Responses;
using Byndyusoft.Core.Models;

namespace byndyusoft_test_app.Mappings;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
		CreateMap<CalculationRequest, Expression>()
			.ForMember(dest => dest.Input, opt => opt.MapFrom(src => src.Expression));

		CreateMap<ExpressionResult, CalculationResponse>()
			.ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.Result));

	}
}
