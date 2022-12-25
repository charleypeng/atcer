// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using ATCer.HRCenter.Domains;
using ATCer.HRCenter.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.HRCenter.Utils
{
    public class WorkTimeUtil
    {
        //public static double GetUserMultiplier(ControllerInfo ctlInfo, Models.TimeItem timeItem)
        //{
        //    if (ctlInfo == null)
        //    {
        //        Logging.LogEx("没有该用户的数据");
        //        throw new Exception("数据库中没有该用户的数据");
        //    }
        //    if (ctlInfo.Department == Department.Area)
        //    {
        //        if (ctlInfo.UserLevel == Levels.WuJi)
        //        {
        //            if (timeItem.SectorName.Contains("通报") || timeItem.SectorName.Contains("计划席"))
        //            {
        //                return Config.conf.PlanAndReportMultiplier;
        //            }
        //            else if (timeItem.CurrentControllerRole == ControllerRole.Student)
        //            {
        //                return 1;
        //            }
        //            else
        //            {
        //                return Config.conf.WujiMultiplier;
        //            }
        //        }
        //        else
        //        {
        //            return 1;
        //        }
        //    }
        //    else
        //    {
        //        if (ctlInfo.UserLevel == Levels.WuJi && timeItem.CurrentControllerRole != ControllerRole.Student)
        //        {
        //            return Config.conf.WujiMultiplier;
        //        }
        //        else
        //        {
        //            return 1;
        //        }
        //    }
        //}
        public interface IRange<T>
        {
            T Start { get; }
            T End { get; }
            bool Includes(T value);
            bool Includes(IRange<T> range);
        }

        public class DateRange : IRange<DateTimeOffset>
        {
            public DateRange(DateTimeOffset start, DateTimeOffset end)
            {
                Start = start;
                End = end;
            }
            public DateTimeOffset Start { get; private set; }
            public DateTimeOffset End { get; private set; }

            public bool Includes(DateTimeOffset value)
            {
                return (Start <= value) && (value <= End);
            }

            public bool Includes(IRange<DateTimeOffset> range)
            {
                return (Start <= range.Start) && (range.End <= End);
            }
        }

        /// <summary>
        /// By giving the begin and end work time it will calculate the day and night work time as T1 and T2
        /// </summary>
        /// <param name="timeA"></param>
        /// <param name="timeB"></param>
        /// <param name="conf"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static Tuple<TimeSpan, TimeSpan> WorkTimeCalculate(DateTimeOffset timeA, DateTimeOffset timeB, WorkTimeConfDto conf)
        {
            if (conf == null)
                throw new ArgumentNullException("work time conf can not be null");

            TimeSpan dayWorkHour = new TimeSpan();
            TimeSpan nightWorkHour = new TimeSpan();
            var daySpan = (timeB.Date - timeA.Date).TotalDays;

            if (daySpan < 0 || daySpan > 1)
            {
                throw new Exception("值班时间过长，请检查值班时间：  " + timeA.ToString());
            }
            
            DateTimeOffset dayA = new DateTime(timeA.Year, timeA.Month, timeA.Day, conf.DaySpan.Hours, conf.DaySpan.Minutes, conf.DaySpan.Seconds);
            DateTimeOffset nightA = new DateTime(timeA.Year, timeA.Month, timeA.Day, conf.NightSpan.Hours, conf.NightSpan.Minutes, conf.NightSpan.Seconds);
            DateTimeOffset midNightA = nightA.AddHours(2);
            DateTimeOffset lastMidNightA = midNightA.AddDays(-1);
            //DateTimeOffset nextDayA = dayA.AddDays(1);

            DateTimeOffset dayB = new DateTime(timeB.Year, timeB.Month, timeB.Day, conf.DaySpan.Hours, conf.DaySpan.Minutes, conf.DaySpan.Seconds);
            DateTimeOffset nightB = new DateTime(timeB.Year, timeB.Month, timeB.Day, conf.NightSpan.Hours, conf.NightSpan.Minutes, conf.NightSpan.Seconds);
            DateTimeOffset midNightB = nightB.AddHours(2);
            DateTimeOffset lastMidNightB = midNightB.AddDays(-1);
            //DateTimeOffset nextDayB = dayB.AddDays(1);

            var incA1 = new DateRange(lastMidNightA, dayA);
            var incA2 = new DateRange(dayA, nightA);
            var incA3 = new DateRange(nightA, midNightA);

            var incB1 = new DateRange(lastMidNightB, dayB);
            var incB2 = new DateRange(dayB, nightB);
            //var incB3 = new DateRange(nightB, midNightB);

            if (daySpan == 0)
            {
                if (incA1.Includes(timeA) && incA1.Includes(timeB))
                {
                    nightWorkHour = timeB - timeA;
                }
                else if (incA1.Includes(timeA) && incA2.Includes(timeB))
                {
                    nightWorkHour = dayA - timeA;
                    dayWorkHour = timeB - dayA;
                }
                else if (incA3.Includes(timeA) && incA3.Includes(timeB))
                {
                    nightWorkHour = timeB - timeA;
                }
                else if (incA1.Includes(timeA) && incA3.Includes(timeB))
                {
                    var span1 = dayA - timeA;
                    var span2 = timeB - nightA;
                    nightWorkHour = span1 + span2;
                    dayWorkHour = nightA - dayA;
                }
                else if (incA2.Includes(timeA) && incA2.Includes(timeB))
                {
                    dayWorkHour = timeB - timeA;
                }
                else if (incA2.Includes(timeA) && incA3.Includes(timeB))
                {
                    dayWorkHour = nightA - timeA;
                    nightWorkHour = timeB - nightA;
                }
                else
                {
                    throw new Exception("未知的时间计算");
                }
            }
            else if (daySpan == 1)
            {
                //not going to happen
                if (incA1.Includes(timeA) && incB1.Includes(timeB))
                {

                    //var span1 = dayA - timeA;
                    //var span2 = span1.Add()
                    dayWorkHour = timeB - dayB;

                }
                else if (incA2.Includes(timeA) && incB1.Includes(timeB))
                {
                    dayWorkHour = nightA - timeA;
                    nightWorkHour = timeB - nightA;

                }
                else if (incA2.Includes(timeA) && incB2.Includes(timeB))
                {
                    var day1 = nightA - timeA;
                    nightWorkHour = dayB - nightA;
                    dayWorkHour = day1.Add(timeB - dayB);
                }
                else if (incA3.Includes(timeA) && incB1.Includes(timeB))
                {
                    nightWorkHour = timeB - timeA;
                }
                else
                {
                    throw new Exception("值班时间过长");
                }
            }
            return new Tuple<TimeSpan, TimeSpan>(dayWorkHour, nightWorkHour);
        }
    }
}
