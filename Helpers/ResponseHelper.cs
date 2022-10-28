using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSWGAF.Helpers
{
    public class ResponseHelper
    {
 
        public static ResponseObject<T> Response<T> (bool res, int StatusCode, string Message, T data)
        {
            return new ResponseObject<T>()
            {
                Res = res, //Custom
                StatusCode = StatusCode,
                Message = Message,
                Data = data
            };
        }

        public static List<ModelErrors> GetModelStateErrors(ModelStateDictionary Model)
        {
            return Model.Select(x => new ModelErrors() { Res = false, Key = x.Key, Messages = x.Value.Errors.Select(y => y.ErrorMessage).ToList() }).ToList();
        }
    }
    public class ResponseObject<T>
    {
        public bool Res { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

    }

    public class ModelErrors
    {
        public bool Res { get; set; }
        public string Key { get; set; }
        public List<string> Messages { get; set; }
    }
}
