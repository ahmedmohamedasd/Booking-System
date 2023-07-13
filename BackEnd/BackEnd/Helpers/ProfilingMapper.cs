using AutoMapper;
using BackEnd.Dtos;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Helpers
{
    public class ProfilingMapper:Profile
    {
        public ProfilingMapper()
        {
            CreateMap<BlockArea, BlockAreaDto>().ReverseMap(); // revers Map that data can map data from source and distination 
            CreateMap<Guest, GuestInfo>().ReverseMap();
            CreateMap<Guest, GuestDtos>().ReverseMap();
            CreateMap<Activity, ActivityDtos>().ReverseMap();
            CreateMap<Ticket, TicketDtos>().ReverseMap();
            CreateMap<GuestEvent, GuestEventDtos>().ReverseMap();
        }
    }
}
