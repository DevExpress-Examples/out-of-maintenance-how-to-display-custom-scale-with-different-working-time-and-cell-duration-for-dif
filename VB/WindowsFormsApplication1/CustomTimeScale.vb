Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports DevExpress.XtraScheduler
Imports System.ComponentModel
Imports DevExpress.Utils.Serializing
Imports DevExpress.XtraScheduler.Native

Namespace WindowsFormsApplication1


	#Region "#mytimescalefixedinterval"
	Public Class MyTimeScaleDaysInterval
		Inherits MyTimeScaleMinutesInterval
		Public Sub New()
			MyBase.New(TimeSpan.FromDays(1))
		End Sub

		Public Overrides Function FormatCaption(ByVal start As DateTime, ByVal [end] As DateTime) As String
			Return start.ToString("dd - MMMM")
		End Function
	End Class


	Public Class MyTimeScaleHoursInterval
		Inherits MyTimeScaleMinutesInterval
		Public Sub New()
			MyBase.New(TimeSpan.FromHours(1))
		End Sub

		Public Overrides Function FormatCaption(ByVal start As DateTime, ByVal [end] As DateTime) As String
			Return start.ToString("HH")
		End Function
	End Class

	 Public Class MyTimeScaleMinutesInterval
		 Inherits TimeScaleFixedInterval


		Private Function GetStartDate(ByVal someDate As DateTime) As TimeSpan
			If someDate.DayOfWeek = DayOfWeek.Saturday Then
				Return TimeSpan.FromHours(9)
			ElseIf someDate.DayOfWeek = DayOfWeek.Sunday Then
				Return TimeSpan.FromHours(12)
			Else
				Return TimeSpan.FromHours(7)
			End If
		End Function

		Private Function GetEndDate(ByVal someDate As DateTime) As TimeSpan
			If someDate.DayOfWeek = DayOfWeek.Saturday Then
				Return TimeSpan.FromHours(18)
			ElseIf someDate.DayOfWeek = DayOfWeek.Sunday Then
				Return TimeSpan.FromHours(15)
			Else
				Return TimeSpan.FromHours(21)
			End If
		End Function

		Private Function GetCorrectedValue(ByVal someDate As DateTime) As TimeSpan
			If Value >= TimeSpan.FromHours(1) Then
				Return Value
			End If
			If someDate.DayOfWeek = DayOfWeek.Saturday Then
				Return TimeSpan.FromMinutes(20)
			ElseIf someDate.DayOfWeek = DayOfWeek.Sunday Then
				Return TimeSpan.FromMinutes(30)
			Else
				Return Value
			End If
		End Function

		Public Sub New()
		End Sub

		Public Sub New(ByVal duration As TimeSpan)
			MyBase.New(duration)
		End Sub


		Public Overrides Function Floor(ByVal [date] As DateTime) As DateTime
			If [date] = DateTime.MinValue Then
				Return DateTime.MinValue
			End If
			If [date].TimeOfDay < GetStartDate([date]) Then
				Return [date].Date.AddDays(-1) + GetEndDate([date].Date.AddDays(-1)) - Value
			End If

			' base method calling
			'DateTime result = base.Floor(date);
			Dim result As DateTime = DateTimeHelper.Floor([date], GetCorrectedValue([date]))

			If result.TimeOfDay = GetEndDate(result) Then
				Return result - Value
			End If
			If result.TimeOfDay > GetEndDate(result) Then
				Return [date].Date + GetEndDate(result)
			End If
			If result.TimeOfDay < GetStartDate(result) Then
				Return result.Date + GetStartDate(result)
			End If
			Return result
		End Function

		Public Overrides Function GetNextDate(ByVal [date] As DateTime) As DateTime
			'if(date.TimeOfDay == GetStartDate(date))
			'    date = base.Floor(date);
			If [date].TimeOfDay = GetStartDate([date]) Then
				[date] = DateTimeHelper.Floor([date], GetCorrectedValue([date]))
			End If

			'DateTime result = base.GetNextDate(date);
			Dim result As DateTime = If(HasNextDate([date]), [date] + GetCorrectedValue([date]), [date])
			If result.TimeOfDay >= GetEndDate(result) Then
				Return result.Date.AddDays(1) + GetStartDate(result.Date.AddDays(1))
			End If
			If result.TimeOfDay <= GetStartDate(result) Then
				Return result + GetStartDate(result)
			End If
			Return result
		End Function

		Protected Overrides Function HasNextDate(ByVal [date] As DateTime) As Boolean
			Return [date] <= DateTime.MaxValue - GetCorrectedValue([date])
		End Function

		Public Overrides Function FormatCaption(ByVal start As DateTime, ByVal [end] As DateTime) As String
			Return start.ToString("HH:mm")
		End Function

		Protected Overrides ReadOnly Property SortingWeight() As TimeSpan
			Get
				Return Value
			End Get
		End Property
	 End Class
	#End Region ' #mytimescalefixedinterval
End Namespace