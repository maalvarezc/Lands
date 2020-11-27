using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace Lands.Models
{   //Revisar las respuestas de los servicios
    class Responses
    {
        public bool IsSucess { get; set; }
        public string message { get; set; }
        public object Result { get; set; }
    }
}
