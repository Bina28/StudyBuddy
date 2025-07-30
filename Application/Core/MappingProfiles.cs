using System;
using System.Runtime.InteropServices;
using AutoMapper;
using Domain;

namespace Application.Core;

public class MappingProfiles : Profile
{
  public MappingProfiles()
  {
    CreateMap<Activity, Activity>();
  }
}
