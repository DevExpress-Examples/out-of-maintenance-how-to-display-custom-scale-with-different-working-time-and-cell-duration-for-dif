using System;
using System.Data;
using System.Configuration;
using DevExpress.XtraScheduler;
using System.ComponentModel;
using DevExpress.Utils.Serializing;
using DevExpress.XtraScheduler.Native;

namespace WindowsFormsApplication1 {


    #region #mytimescalefixedinterval
    public class MyTimeScaleDaysInterval : MyTimeScaleMinutesInterval {
        public MyTimeScaleDaysInterval() : base(TimeSpan.FromDays(1)) { }

        public override string FormatCaption(DateTime start, DateTime end) {
            return start.ToString("dd - MMMM");
        }
    }


    public class MyTimeScaleHoursInterval : MyTimeScaleMinutesInterval {
        public MyTimeScaleHoursInterval() : base(TimeSpan.FromHours(1)) { }

        public override string FormatCaption(DateTime start, DateTime end) {
            return start.ToString("HH");
        }
    }

     public class MyTimeScaleMinutesInterval : TimeScaleFixedInterval {


        TimeSpan GetStartDate(DateTime someDate) {
            if(someDate.DayOfWeek == DayOfWeek.Saturday)
                return TimeSpan.FromHours(9);
            else if(someDate.DayOfWeek == DayOfWeek.Sunday)
                return TimeSpan.FromHours(12);
            else
                return TimeSpan.FromHours(7);
        }

        TimeSpan GetEndDate(DateTime someDate) {
            if(someDate.DayOfWeek == DayOfWeek.Saturday)
                return TimeSpan.FromHours(18);
            else if(someDate.DayOfWeek == DayOfWeek.Sunday)
                return TimeSpan.FromHours(15);
            else
                return TimeSpan.FromHours(21);
        }

        TimeSpan GetCorrectedValue(DateTime someDate) {
            if(Value >= TimeSpan.FromHours(1)) {
                return Value;
            }
            if(someDate.DayOfWeek == DayOfWeek.Saturday)
                return TimeSpan.FromMinutes(20);
            else if(someDate.DayOfWeek == DayOfWeek.Sunday)
                return TimeSpan.FromMinutes(30);
            else
                return Value;        
        }

        public MyTimeScaleMinutesInterval() { }

        public MyTimeScaleMinutesInterval(TimeSpan duration)
            : base(duration) {
        }


        public override DateTime Floor(DateTime date) {
            if(date == DateTime.MinValue)
                return DateTime.MinValue;
            if(date.TimeOfDay < GetStartDate(date))
                return date.Date.AddDays(-1) + GetEndDate(date.Date.AddDays(-1)) - Value;

            // base method calling
            //DateTime result = base.Floor(date);
            DateTime result = DateTimeHelper.Floor(date, GetCorrectedValue(date));

            if(result.TimeOfDay == GetEndDate(result))
                return result - Value;
            if(result.TimeOfDay > GetEndDate(result))
                return date.Date + GetEndDate(result);
            if(result.TimeOfDay < GetStartDate(result))
                return result.Date + GetStartDate(result);
            return result;
        }

        public override DateTime GetNextDate(DateTime date) {
            //if(date.TimeOfDay == GetStartDate(date))
            //    date = base.Floor(date);
            if(date.TimeOfDay == GetStartDate(date))
                date = DateTimeHelper.Floor(date, GetCorrectedValue(date));
            
            //DateTime result = base.GetNextDate(date);
            DateTime result = HasNextDate(date) ? date + GetCorrectedValue(date) : date;
            if(result.TimeOfDay >= GetEndDate(result))
                return result.Date.AddDays(1) + GetStartDate(result.Date.AddDays(1));
            if(result.TimeOfDay <= GetStartDate(result))
                return result + GetStartDate(result);
            return result;
        }

        protected override bool HasNextDate(DateTime date) {
            return date <= DateTime.MaxValue - GetCorrectedValue(date);
        }

        public override string FormatCaption(DateTime start, DateTime end) {
            return start.ToString("HH:mm");
        }

        protected override TimeSpan SortingWeight {
            get {
                return Value;
            }
        }
    }
    #endregion #mytimescalefixedinterval
}