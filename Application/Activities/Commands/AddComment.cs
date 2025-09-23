using System;
using System.Diagnostics;
using Application.Activities.DTOs;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Commands;

public class AddComment
{
  public class Command : IRequest<Result<CommentsDto>>
  {
    public required string Body { get; set; }
    public required string ActivityId { get; set; }
  }

  public class Handler(AppDbContext context, IMapper mapper, IUserAccessor userAccessor) : IRequestHandler<Command, Result<CommentsDto>>
  {
    public async Task<Result<CommentsDto>> Handle(Command request, CancellationToken cancellationToken)
    {
      var activity = await context.Activities
     .Include(x => x.Comments)
     .ThenInclude(x => x.User)
     .FirstOrDefaultAsync(x => x.Id == request.ActivityId, cancellationToken);

      if (activity == null) return Result<CommentsDto>.Failure("Could not find activity", 404);

      var user = await userAccessor.GetUserAsync();

      var comment = new Comment
      {
        UserId = user.Id,
        ActivityId = activity.Id,
        Body = request.Body
      };

      activity.Comments.Add(comment);
      var result = await context.SaveChangesAsync(cancellationToken) > 0;
      
      return result ?
      Result<CommentsDto>.Success(mapper.Map<CommentsDto>(comment))
      : Result<CommentsDto>.Failure("Failed to add comment", 400);
    }
  }
}
