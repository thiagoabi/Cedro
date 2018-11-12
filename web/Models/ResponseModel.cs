using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web
{
    public class ResponseModel<TModel> 
        where TModel : class
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public TModel Data { get; set; }

        public ResponseModel(bool ok, string message, TModel data)
        {
            Ok = ok;
            Message = message;
            Data = data ?? default(TModel);
        }
    }
}
