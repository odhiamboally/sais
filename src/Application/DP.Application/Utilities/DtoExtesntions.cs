using DP.Application.Dtos.Auth;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Utilities;
public static class DtoExtesntions
{
    public static T UpdateUserId<T>(this T model, string userId) where T : class
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        // Get the UserId property using reflection
        var property = typeof(T).GetProperty("UserId");
        if (property == null || !property.CanWrite)
            throw new InvalidOperationException($"The type {typeof(T).Name} does not have a writable 'UserId' property.");

        // Set the UserId property value
        property.SetValue(model, userId);

        return model;
    }

    public static VerifyCodeRequest UpdateUserId(this VerifyCodeRequest model, string userId)
    {
        return model with { UserId = userId };
    }

}
