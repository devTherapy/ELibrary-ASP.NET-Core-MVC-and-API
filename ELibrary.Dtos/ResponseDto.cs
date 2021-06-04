using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Dtos
{
    public class ResponseDto<T>
    {
        public int StatusCode {get; set;}
        public bool Success { get; set; } = false;
        public T Data { get; set; }
        public string Message { get; set; }

        public bool Prev { get; set; } = false;
        public bool Next { get; set; } = false;
        public int PageIndex { get; set; }
    }
}
