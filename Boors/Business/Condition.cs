using Boors.Entities;
using Boors.Services;
using InstaMarket.Web.Core.Data;
using InstaMarket.Web.Core.DTOModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaMarket.Web.Core.Business
{
    public class ConditionClass
    {
        DataBase _dataBase;
        public ConditionClass()
        {
            _dataBase = new DataBase();
        }

        public IQueryable<TransactionHourlyChangeStage> GetConditionResult(Condition condition)
        {
            return SolveCondition(condition);
        }
        private IQueryable<TransactionHourlyChangeStage> SolveCondition(Condition methodCondition)
        {
            var condition = _dataBase.Condition.GetAll(x => x.Id == methodCondition.Id).SingleOrDefault();
            if (condition.FirstChild != null && condition.SecondChild != null && condition.ConditionOperatorId != null) // if childs is not Root
            {
                return ConditionOperator(condition);
            }
            else
            {
                IQueryable<TransactionHourlyChangeStage> TransactionHourlyChangeStages = GetTransactionsResults(condition);
                return new Process().ParameterProcess(TransactionHourlyChangeStages, condition.PropName, condition.OperationId.Value
                    , condition.OperatorId.Value, condition.Variable1, condition.Variable2);
            }
        }

        private IQueryable<TransactionHourlyChangeStage> GetTransactionsResults(Condition condition)
        {
            DateTime StartTime = (TimeRound.TimeRoundDown(DateTime.Now, TimeSpan.FromMinutes(30))).AddSeconds(-condition.Period.Value);
            DateTime EndTime = TimeRound.TimeRoundDown(DateTime.Now, TimeSpan.FromMinutes(30));
            return _dataBase.TransactionHourlyChangeStage.GetAll(x => x.CreationTime >= StartTime &&
            x.CreationTime < EndTime);
        }

        private IQueryable<TransactionHourlyChangeStage> ConditionOperator(Condition condition)
        {
            switch (condition.ConditionOperatorId)
            {
                case 1:

                    return from u in SolveCondition(condition.FirstChild)
                             join x in SolveCondition(condition.SecondChild) on u.TransactionHourlyChangeStageId equals x.TransactionHourlyChangeStageId
                             select u; //its And
                case 2:
                    return SolveCondition(condition.FirstChild).Union(SolveCondition(condition.SecondChild)); // Its OR
                default:
                    throw new Exception("Unhandled ConditionOperatorId");
            }
        }
    }
}
