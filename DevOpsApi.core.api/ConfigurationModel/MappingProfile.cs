using AutoMapper;
using DevOpsApi.core.api.Data.Entities;
using DevOpsApi.core.api.Models;
using DevOpsApi.core.api.Models.POSTempus;

namespace DevOpsApi.core.api.ConfigurationModel
{
    public class MappingProfile : Profile
    {
        //this.CreateMap<Comment, CommentModel>()
        //        .ForMember(dest => dest.PostedBy, o => o.MapFrom(source => $"{source.CreatedBy.FirstName} {source.CreatedBy.LastName}"))
        //        .ForMember(dest => dest.PostedOn, o => o.MapFrom(source => source.CreatedOn))
        //        .ForMember(dest => dest.Comment, o => o.MapFrom(source => source.Text))
        //        ;

        public MappingProfile()
        {
            this.CreateMap<Location, LocationModel>()
                .ReverseMap();

            this.CreateMap<PosInvoice, PosInvoiceModel>()
                ;

            this.CreateMap<SISPosInvoice, SISPosInvoiceModel>()
                ;

            this.CreateMap<SISPosInvoice, PosInvoiceModel>()
                ;
            this.CreateMap<POSDeviceConfigurationHostName, POSDeviceConfigurationHostNameModel>()
                ;

            this.CreateMap<POSConfiguration, POSConfigurationModel>()
                ;

            this.CreateMap<POSDeviceConfiguration, POSDeviceConfigurationModel>()
                ;

            this.CreateMap<POSLoginDetail, POSLoginDetailsModel>()
                ;
        }
    }
}
