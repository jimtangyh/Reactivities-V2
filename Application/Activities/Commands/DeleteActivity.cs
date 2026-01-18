using System;
using MediatR;
using Persistence;

namespace Application.Activities.Commands;

public class DeleteActivity
{
    public class Commands : IRequest
    {
        public required string Id { get; set; }
    }

      public class Handler(AppDbContext context) : IRequestHandler<Commands>
    {
        public async Task Handle(Commands request, CancellationToken cancellationToken)
        {
            var activity = await context.Activities.FindAsync([request.Id], cancellationToken) 
                            ?? throw new Exception("Activity not found");
          
            context.Activities.Remove(activity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

}
