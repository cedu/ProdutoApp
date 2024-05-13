using AutoMapper;
using ProdutoApp.API.Models;
using ProdutoApp.Domain.Entities;

namespace ProdutoApp.API.Mappings
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ProdutoPostRequestModel, Produto>();
            CreateMap<ProdutoPutRequestModel, Produto>();
            CreateMap<Produto, ProdutoGetResponseModel>();
        }
    }
}
