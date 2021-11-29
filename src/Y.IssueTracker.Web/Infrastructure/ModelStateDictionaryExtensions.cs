namespace Y.IssueTracker.Web.Infrastructure;

using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

internal static class ModelStateDictionaryExtensions
{
    public static void AddModelErrors(this ModelStateDictionary modelState, KeyValuePair<string, string>[] errors)
    {
        foreach (var (key, value) in errors)
        {
            modelState.AddModelError(key, value);
        }
    }
}
