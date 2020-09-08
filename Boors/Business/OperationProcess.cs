using Boors.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace InstaMarket.Web.Core.Business

{
    public class Operation
    {
        public bool OperationProcess(List<Int64> data, string operationName, string operatorName, string counter1, string counter2, int fullDataCount = 0)
        {
            try
            {
                switch (operationName)
                {
                    case "Count":
                        return new Operator().OperatorProcess(data[0], operatorName, counter1, counter2);
                    case "Average":
                        int i = 0;
                        Int64 temp = 0;
                        foreach (var item in data)
                        {
                            temp += item;
                            i++;
                        }
                        temp = temp / i;
                        return new Operator().OperatorProcess(temp, operatorName, counter1, counter2);
                    case "Minimum":
                        Int64 Min = data[0];

                        foreach (var item in data)
                        {
                            if (Min > item)
                            {
                                Min = item;
                            }
                        }
                        return new Operator().OperatorProcess(Min, operatorName, counter1, counter2);
                    case "Maximum":
                        Int64 Max = data[0];

                        foreach (var item in data)
                        {
                            if (Max < item)
                            {
                                Max = item;
                            }
                        }
                        return new Operator().OperatorProcess(Max, operatorName, counter1, counter2);

                    case "Percent":
                        int Percent = (data.Count * 100) / fullDataCount;
                        return new Operator().OperatorProcess(Percent, operatorName, counter1, counter2);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        } //process based on operation
    }
}