// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

[assembly: SuppressMessage("Design", "CA1054:URI-like parameters should not be strings", Justification = "System.Uri doesn't work well with ModelBinding")]
[assembly: SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "System.Uri doesn't work well with ModelBinding")]
