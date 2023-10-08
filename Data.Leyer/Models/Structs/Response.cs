﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Leyer.Models.Structs;

public struct Response<T>
{
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    public string Message { get; set; }
    public string DataJson { get; set; }
    public IEnumerable<T> Data { get; set; }

}