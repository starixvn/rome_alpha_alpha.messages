// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1118:Utility classes should not have public constructors", Justification = "<Pending>", Scope = "type", Target = "~T:Alpha.Messages.Api.App_Start.DependencyConfig")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S2325:Methods and properties that don't access instance data should be static", Justification = "<Pending>", Scope = "member", Target = "~M:Alpha.Messages.Api.WebApiApplication.Application_Start")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S103:Lines should not be too long", Justification = "<Pending>")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1449:Culture should be specified for \"string\" operations", Justification = "<Pending>", Scope = "member", Target = "~M:Alpha.Messages.Api.Filter.HmacAuthorizationFilterAttribute.IsValidRequest(System.Net.Http.HttpRequestMessage,System.String,System.String,System.String,System.String)~System.Boolean")]