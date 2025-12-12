using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace App.Services
{
    public class ServiceResult<T>
    {//başarılı olduğunda ve başarısız olduğunda herhangi birini doldurcam
        public T? data { get; set; } //başarılı olduğunda data dolcak 
        public List<string>? ErrorMessage { get; set; }
        //public bool IsSuccess
        //{
        //    get
        //    {
        //        return ErrorMessage == null || ErrorMessage.Count == 0;
        //    }
        //}
        [JsonIgnore]
        public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
        [JsonIgnore]
        public bool IsFail => !IsSuccess;
        [JsonIgnore]
        public HttpStatusCode Status { get; set; }

        [JsonIgnore] public string? UrlAsCreated { get; set; }

        //static factory method
        public static ServiceResult<T> Success (T Data,HttpStatusCode status = HttpStatusCode.OK)
        {
            return new ServiceResult<T>()
            {
                data = Data,
                Status = status,
            };
        }

        public static ServiceResult<T> SuccessAsCreated(T Data, string urlAsCreated)
        {
            return new ServiceResult<T>()
            {
                data = Data,
                Status = HttpStatusCode.Created,
                UrlAsCreated = urlAsCreated,
            };
        }

        public static ServiceResult<T> Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>()
            {
                ErrorMessage = errorMessage,
                Status = status,
            };
        }

        public static ServiceResult<T> Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>()
            {
                //ErrorMessage = new List<string>() { errorMessage }
                 ErrorMessage = [errorMessage], //.net 8 ile gelmiş
                 Status = status,
            };
        }
    }

    public class ServiceResult
    {//update ve delete işlemlerinde geriye bişey döndürmek zorunda değilim 
        //public T? data { get; set; } //başarılı olduğunda data dolcak 
        public List<string>? ErrorMessage { get; set; }
        //public bool IsSuccess
        //{
        //    get
        //    {
        //        return ErrorMessage == null || ErrorMessage.Count == 0;
        //    }
        //}
        [JsonIgnore]
        public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
        [JsonIgnore]
        public bool IsFail => !IsSuccess;
        [JsonIgnore]
        public HttpStatusCode Status { get; set; }

        //static factory method
        public static ServiceResult Success(HttpStatusCode status = HttpStatusCode.OK)
        {
            return new ServiceResult()
            {
                Status = status,
            };
        }

        public static ServiceResult Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult()
            {
                ErrorMessage = errorMessage,
                Status = status,
            };
        }

        public static ServiceResult Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult()
            {
                //ErrorMessage = new List<string>() { errorMessage }
                ErrorMessage = [errorMessage], //.net 8 ile gelmiş
                Status = status,
            };
        }
    }
}
