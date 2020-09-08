using Boors.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace InstaMarket.Web.Core.Business

{
    public class Process
    {
        public IQueryable<TransactionHourlyChangeStage> ParameterProcess(IQueryable<TransactionHourlyChangeStage> TransactionHourlyChangeStages,
            string propName, int operationId, int operatorId, string variable1, string variable2)
        {
            try
            {
                return GetOperationResult(TransactionHourlyChangeStages, operationId, propName, operatorId, variable1, variable2);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private Expression<Func<TransactionHourlyChangeStage, bool>> WhereCluz(string propName, int operatorId, string variable1, string variable2)
        {
            ParameterExpression argParam = Expression.Parameter(typeof(TransactionHourlyChangeStage), "s");
            Expression nameProperty = Expression.Property(argParam, propName);

            var propertyType = nameProperty.Type; //get Type Of Field that we wanna use : like datetime

            var converter = TypeDescriptor.GetConverter(nameProperty.Type); //set it to converter

            var result1 = converter.ConvertFrom(variable1); //convert variable to that type
            object result2 = null;
            if (variable2 != null)
            {
                result2 = converter.ConvertFrom(variable2);

            }


            ConstantExpression val1;
            ConstantExpression val2;

            Expression e1 = null;

            switch (operatorId)
            {
                case 1:// "More Than":
                    val1 = Expression.Constant(result1);
                    e1 = Expression.GreaterThan(nameProperty, val1);
                    break;
                case 2:// "Less Than":
                    val1 = Expression.Constant(result1);
                    e1 = Expression.LessThan(nameProperty, val1);
                    break;
                case 3:// "Equal":
                    val1 = Expression.Constant(result1);

                    e1 = Expression.Equal(nameProperty, val1);
                    break;
                case 1002:// "NotEqual":

                    val1 = Expression.Constant(result1);
                    e1 = Expression.NotEqual(nameProperty, val1);
                    break;
                case 4:// "Between":

                    val1 = Expression.Constant(result1);
                    val2 = Expression.Constant(result2);
                    e1 = Expression.GreaterThan(nameProperty, val1);
                    Expression e2 = Expression.LessThan(nameProperty, val2);
                    e1 = Expression.AndAlso(e1, e2);
                    break;
            }

            var lambda = Expression.Lambda<Func<TransactionHourlyChangeStage, bool>>(e1, argParam);
            return lambda;
        }


        private Expression<Func<TransactionHourlyChangeStage, bool>> MAX_MIN_SUM_Cluz(string propName)
        {
            ParameterExpression argParam = Expression.Parameter(typeof(TransactionHourlyChangeStage), "s");
            Expression nameProperty = Expression.Property(argParam, propName);
            var lambda = Expression.Lambda<Func<TransactionHourlyChangeStage, bool>>(nameProperty, argParam);
            return lambda;
        }

        public bool GetOperatorResult(dynamic tempOperationResult, int operatorId, string variable1, string variable2)
        {
            var converter = TypeDescriptor.GetConverter(tempOperationResult);
            var result1 = converter.ConvertFrom(variable1);
            var result2 = converter.ConvertFrom(variable2);
            switch (operatorId)
            {
                case 1:// "More Than":
                    return tempOperationResult > result1;
                case 2:// "Less Than":
                    return tempOperationResult < variable1;
                case 3:// "Equal":
                    return tempOperationResult == variable1;
                case 1002:// "NotEqual":
                    return tempOperationResult != variable1;
                case 4:// "Between":
                    return tempOperationResult <= variable1 & tempOperationResult >= result2;
            }
            return false;
        }

        public IQueryable<TransactionHourlyChangeStage> GetOperationResult(IQueryable<TransactionHourlyChangeStage> TransactionHourlyChangeStages, int operationId, string propName, int operatorId, string variable1, string variable2)
        {
            switch (operationId)
            {
                case 1:// "Count":
                    if (GetOperatorResult(TransactionHourlyChangeStages.Count(), operatorId, variable1, variable2))
                    {
                        return TransactionHourlyChangeStages; // return all of it 
                    }
                    else
                    {
                        return TransactionHourlyChangeStages.Where(x => x.TransactionHourlyChangeStageId == -1); //return null    
                    }
                case 2: // "Average":
                    if (GetOperatorResult(TransactionHourlyChangeStages.Average(AVR_Cluz(propName)), operatorId, variable1, variable2))
                    {
                        return TransactionHourlyChangeStages; // return all of it 
                    }
                    else
                    {
                        return TransactionHourlyChangeStages.Where(x => x.TransactionHourlyChangeStageId == -1); //return null    
                    }
                case 3: // "Minimum":
                    if (GetOperatorResult(TransactionHourlyChangeStages.Max(MAX_MIN_SUM_Cluz(propName)), operatorId, variable1, variable2))
                    {
                        return TransactionHourlyChangeStages; // return all of it 
                    }
                    else
                    {
                        return TransactionHourlyChangeStages.Where(x => x.TransactionHourlyChangeStageId == -1); //return null    
                    }
                case 4: //"Maximum":
                    if (GetOperatorResult(TransactionHourlyChangeStages.Max(MAX_MIN_SUM_Cluz(propName)), operatorId, variable1, variable2))
                    {
                        return TransactionHourlyChangeStages; // return all of it 
                    }
                    else
                    {
                        return TransactionHourlyChangeStages.Where(x => x.TransactionHourlyChangeStageId == -1); //return null    
                    }
                case 5:// "Self":
                    return TransactionHourlyChangeStages.Where(WhereCluz(propName, operatorId, variable1, variable2));
                default:
                    return null;
            }
        }

        private Expression<Func<TransactionHourlyChangeStage, decimal>> AVR_Cluz(string propName)
        {
            ParameterExpression argParam = Expression.Parameter(typeof(TransactionHourlyChangeStage), "s");
            Expression nameProperty = Expression.Property(argParam, propName);
            var lambda = Expression.Lambda<Func<TransactionHourlyChangeStage, decimal>>(nameProperty, argParam);
            return lambda;
        }

        //private object GetProp(TransactionHourlyChangeStage x, string propName)
        //{
        //    return x.GetType().GetProperty(propName).GetValue(x, null);
        //}
    }
}