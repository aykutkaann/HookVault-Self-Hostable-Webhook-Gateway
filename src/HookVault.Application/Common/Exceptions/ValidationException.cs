using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;



namespace HookVault.Application.Common.Exceptions
{
    [SuppressMessage("Design", "CA1032:Implement standard exception constructors",
    Justification = "These exceptions are only constructed with required context — parameterless/message-only constructors would allow invalid states.")]
    public sealed class ValidationException :Exception
    {
        public ValidationException(IReadOnlyDictionary<string, string[]> errors): base("One or more validation failures occured.")
        {
            Errors = errors;
        }

        public IReadOnlyDictionary<string,string[]> Errors { get;  }
    }
}
