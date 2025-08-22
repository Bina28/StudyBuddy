using System;
using Application.Activities.Commands;
using Application.Activities.DTOs;
using FluentValidation;

namespace Application.Activities.Validators;

public class CreateActivityValidator : BaseActivtyValidator<CreateActivity.Command, CreateActivityDto>
{
  public CreateActivityValidator(): base(x=>x.ActivityDto)
  {
  }
}
