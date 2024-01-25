using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QdaoCaseManager.Domain.Exceptions;

public class InvalidDataException : Exception
{
    public List<string> InvalidFields { get; private set; }

    public InvalidDataException(List<string> invalidFields, string message)
        : base(message)
    {
        InvalidFields = invalidFields;
    }

    public InvalidDataException(string message)
        : base(message)
    {
        InvalidFields = new List<string>();
    }

    public override string ToString()
    {
        string message = base.ToString();
        if (InvalidFields.Any())
        {
            message += Environment.NewLine + "Invalid fields:";
            foreach (string field in InvalidFields)
            {
                message += Environment.NewLine + "- " + field;
            }
        }
        return message;
    }
}
