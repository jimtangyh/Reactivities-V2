using System;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Commands;

public class EditActivity
{
    public class Commands : IRequest
    {
        public required Activity Activity { get; set; } 
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Commands>
    {
        public async Task Handle(Commands request, CancellationToken cancellationToken)
        {
            var activity = await context.Activities.FindAsync([request.Activity.Id], cancellationToken) 
                            ?? throw new Exception("Activity not found");
            mapper.Map(request.Activity,  activity);            
            context.Activities.Update(activity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
