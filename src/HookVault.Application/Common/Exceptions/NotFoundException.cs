using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;



namespace HookVault.Application.Common.Exceptions
{
    [SuppressMessage("Design", "CA1032:Implement standard exception constructors",
    Justification = "These exceptions are only constructed with required context — parameterless/message-only constructors would allow invalid states.")]
    public sealed class NotFoundException : Exception
    {
        public NotFoundException(string entityName, object key)
            : base($"{entityName} with key '{key}' was not found.")
        {
            EntityName = entityName;
            Key = key;
        }

        public string EntityName { get; }
        public object Key { get; }
    }
}
