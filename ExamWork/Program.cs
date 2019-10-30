using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace ExamWork.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var examWorkUI = new ExamWorkUI();
            examWorkUI.Action();
        }
    }
}
