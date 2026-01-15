# MediatR 请求（Request）实例（基于本项目）

本项目在 API 层通过 `IMediator.Send(request)` 发送请求，在 Application 层用 `IRequestHandler<TRequest, TResponse>` / `IRequestHandler<TRequest>` 处理请求（CQRS 风格）。

## 1) 注册位置（MediatR + AutoMapper）

`API/Program.cs`：

```csharp
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<GetActivityList.Handler>());

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfiles>());
```

## 2) Query 示例：获取列表（无参数）

文件：`Application/Activities/Queries/GetActivityList.cs`

请求定义 + Handler：

```csharp
public class GetActivityList
{
    public class Query : IRequest<List<Activity>> {}

    public class Handler(AppDbContext context) : IRequestHandler<Query, List<Activity>>
    {
        public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            => await context.Activities.ToListAsync(cancellationToken);
    }
}
```

Controller 里请求实例：

```csharp
return await Mediator.Send(new GetActivityList.Query());
```

## 3) Query 示例：获取详情（带参数）

文件：`Application/Activities/Queries/GetActivityDetails.cs`

```csharp
return await Mediator.Send(new GetActivityDetails.Query { Id = id });
```

## 4) Command 示例：创建（写数据，返回 id）

文件：`Application/Activities/Commands/CreateActivity.cs`

```csharp
var id = await Mediator.Send(new CreateActivity.Commands
{
    Activity = activity
});
```

## 5) Command 示例：编辑（写数据，无返回值）

文件：`Application/Activities/Commands/EditActivity.cs`

请求定义是 `IRequest`（无返回值），Controller 里请求实例：

```csharp
await Mediator.Send(new EditActivity.Commands { Activity = activity });
return NoContent();
```

编辑逻辑里用 AutoMapper 把传入的 `request.Activity` 映射到数据库查出来的 `activity`：

```csharp
mapper.Map(request.Activity, activity);
await context.SaveChangesAsync(cancellationToken);
```

映射配置在：`Application/Core/MappingProfiles.cs`

```csharp
CreateMap<Domain.Activity, Domain.Activity>();
```

