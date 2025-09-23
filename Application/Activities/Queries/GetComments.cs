using System;
using Application.Activities.DTOs;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries;

public class GetComments
{
  public class Query : IRequest<Result<List<CommentsDto>>>
  {
    public required string ActivityId { get; set; }
  }

  public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, Result<List<CommentsDto>>>
  {
    public async Task<Result<List<CommentsDto>>> Handle(Query request, CancellationToken cancellationToken)
    {
      var comments = await context.Comments
      .Where(x => x.ActivityId == request.ActivityId)
      .OrderByDescending(x => x.CreatedAt)
      .ProjectTo<CommentsDto>(mapper.ConfigurationProvider)
      .ToListAsync(cancellationToken);

      return Result<List<CommentsDto>>.Success(comments);
    }
  }
}
