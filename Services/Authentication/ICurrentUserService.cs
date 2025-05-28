using System;

namespace TodoApi.Services.Authentication;

public interface ICurrentUserService
{
    public int? GetUserId();
}
