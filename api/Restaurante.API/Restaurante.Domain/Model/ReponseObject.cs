using Restaurante.Domain.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurante.Domain.Model
{
    public class ReponseObject
    {
        public ReponseObject()
        {
            Message = string.Empty;
            Status = ResponseObjectStatus.NotDefined;
        }

        public ReponseObject(string message, ResponseObjectStatus status)
        {
            Message = message;
            Status = status;
        }

        public Guid TransactionId { get; set; }
        public string Message { get; set; }
        public ResponseObjectStatus Status { get; set; }
    }

    public class ReponseObject<T> : ReponseObject where T : IEquatable<T>
    {
        public ReponseObject(T data) {
            Data = data;
        }

        public T Data { get; set; }
    }
}
