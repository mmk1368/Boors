using Boors.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace InstaMarket.Web.Core.Business

{
    public class Operator
    {
        public bool OperatorProcess(Int64 data, string operatorName, string variable1, string variable2)
        {
            int counter1 = int.Parse(variable1);
            int counter2 = 0;
            if (!string.IsNullOrWhiteSpace(variable2))
            {
                counter2 = int.Parse(variable2);
            }
            try
            {
                switch (operatorName)
                {
                    case "More Than":
                        if (data > counter1)
                        {
                            return true;
                        }
                        break;
                    case "Less Than":
                        if (data < counter1)
                        {
                            return true;
                        }
                        break;
                    case "Equal":
                        if (data == counter1)
                        {
                            return true;
                        }
                        break;
                    case "Between":
                        if (data > counter1 && data <= counter2)
                        {
                            return true;
                        }
                        break;
                }
                return true ;
            }
            catch (Exception)
            {
                throw;
            }
        } //process based on operator

    }
}