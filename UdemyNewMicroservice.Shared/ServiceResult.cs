using Refit;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace UdemyNewMicroservice.Shared
{
    public class ServiceResult
    {
        [JsonIgnore] //zaten status kodu var
        public HttpStatusCode Status { get; set; }
        public ProblemDetails? Fail { get; set; } //başarısızsa dönecek problem detayları,basarılıysa null olacak

        [JsonIgnore]
        public bool IsSucces => Fail == null;

        [JsonIgnore]
        public bool IsFail => !IsSucces;

        //static factory method
        public static ServiceResult SuccessAsNoContent()
        {
            return new ServiceResult
            {
                Status = HttpStatusCode.NoContent
            };
        }
        public static ServiceResult ErrorAsNotFound()
        {
            return new ServiceResult
            {
                Status = HttpStatusCode.NotFound,
                Fail = new Microsoft.AspNetCore.Mvc.ProblemDetails
                {
                    Title = "Not Found",
                    Detail = "The requested resource was not found."
                }
            };
        }
        public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode status)
        {
            return new ServiceResult
            {
                Status = status,
                Fail = problemDetails
            };
        }
        //sadece title ve desc vermek istersek
        public static ServiceResult Error(string title, string description, HttpStatusCode status)
        {
            return new ServiceResult
            {
                Status = status,
                Fail = new  ProblemDetails
                {
                    Title = title,
                    Detail = description,
                    Status = (int)status

                }
            };
        }
        //sadece title istersek : ör : VeriTabanına bağlanamadı
        public static ServiceResult Error(string title, HttpStatusCode status)
        {
            return new ServiceResult
            {
                Status = status,
                Fail = new  ProblemDetails
                {
                    Title = title,
                    Status = (int)status

                }
            };
        }
        public static ServiceResult ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult
                {
                    Status = exception.StatusCode,
                    Fail = new  ProblemDetails()
                    {
                        Title = "Error",
                        Detail = "An error occurred but no details were provided."
                    }
                };
            }

            var problemDetails = JsonSerializer.Deserialize< ProblemDetails>(
                    exception.Content,
                    new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            return new ServiceResult
            {
                Status = exception.StatusCode,
                Fail = problemDetails
            };
        }
        public static ServiceResult ErrorFromValidation(IDictionary<string, object?> errors)
        {
            return new ServiceResult
            {
                Status = HttpStatusCode.BadRequest,
                Fail = new  ProblemDetails
                {
                    Title = "Validation errors occured",
                    Detail = "Please check the errors property for more details",
                    Extensions = errors,
                    Status = HttpStatusCode.BadRequest.GetHashCode()
                }
            };
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }
        public string? UrlAsCreated { get; set; }

        //200
        public static ServiceResult<T> SuccessAsOk(T data)
        {
            return new ServiceResult<T>
            {
                Status = HttpStatusCode.OK,
                Data = data
            };
        }
        //Created => response headerda location bilgisi => /api/products/3
        public static ServiceResult<T> SuccessAsCreated(T data,string url)
        {
            return new ServiceResult<T>
            {
                Status = HttpStatusCode.Created,
                Data = data,
                UrlAsCreated = url
            };
        }
        public new static ServiceResult<T> Error( ProblemDetails problemDetails,HttpStatusCode status)
        {
            return new ServiceResult<T>
            {
                Status = status,
                Fail = problemDetails
            };
        }
        //sadece title ve desc vermek istersek
        public new static ServiceResult<T> Error(string title,string description, HttpStatusCode status)
        {
            return new ServiceResult<T>
            {
                Status = status,
                Fail = new  ProblemDetails
                {
                    Title = title,
                    Detail = description,
                    Status= (int)status

                }
            };
        }
        //sadece title istersek : ör : VeriTabanına bağlanamadı
        public new static ServiceResult<T> Error(string title, HttpStatusCode status)
        {
            return new ServiceResult<T>
            {
                Status = status,
                Fail = new  ProblemDetails
                {
                    Title = title,
                    Status = (int)status

                }
            };
        }
        public new static ServiceResult<T> ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult<T>
                {
                    Status = exception.StatusCode,
                    Fail = new  ProblemDetails()
                    {
                        Title = "Error",
                        Detail = "An error occurred but no details were provided."
                    }
                };
            }

            var problemDetails = JsonSerializer.Deserialize< ProblemDetails>(
                    exception.Content,
                    new System.Text.Json.JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            return new ServiceResult<T>
            {
                Status = exception.StatusCode,
                Fail = problemDetails
            };
        }
        public new static ServiceResult<T> ErrorFromValidation(IDictionary<string,object?> errors )
        {
            return new ServiceResult<T>
            {
                Status = HttpStatusCode.BadRequest,
                Fail = new  ProblemDetails
                {
                    Title = "Validation errors occured",
                    Detail = "Please check the errors property for more details",
                    Extensions  =errors,
                    Status = HttpStatusCode.BadRequest.GetHashCode()
                }
            };
        }


    }
}
